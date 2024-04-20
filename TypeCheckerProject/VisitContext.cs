﻿using static stellaParser;
using static TypeCheckerProject.StandardTypes;

namespace TypeCheckerProject;

public static class VisitContext
{
    public static void AddExtension(IEnumerable<string> newExtensions, HashSet<string> extensions)
    {
        extensions.UnionWith(newExtensions);
    }
    
    public static void AddVariableTypeInfo(string name, IType type, Dictionary<string, Stack<IType>> variableTypeInfo)
    {
        if (!variableTypeInfo.ContainsKey(name))
        {
            variableTypeInfo[name] = new Stack<IType>();
        }

        variableTypeInfo[name].Push(type);
    }

    public static void AddVariableTypeInfo(Dictionary<string, IType> addVariableTypeInfo,
        Dictionary<string, Stack<IType>> variableTypeInfo)
    {
        foreach (var (variable, type) in addVariableTypeInfo)
        {
            AddVariableTypeInfo(variable, type, variableTypeInfo);
        }
    }

    public static void DeleteVariableTypeInfo(string variableName, Dictionary<string, Stack<IType>> variableTypeInfo)
    {
        variableTypeInfo[variableName].Pop();
        if (variableTypeInfo[variableName].Count == 0)
        {
            variableTypeInfo.Remove(variableName);
        }
    }

    public static void DeleteVariableTypeInfo(Dictionary<string, IType> deleteVariableTypeInfo,
        Dictionary<string, Stack<IType>> variableTypeInfo)
    {
        foreach (var (variable, _) in deleteVariableTypeInfo)
        {
            DeleteVariableTypeInfo(variable, variableTypeInfo);
        }
    }

    public static IType? TryGetVariableType(string variableName, Dictionary<string, Stack<IType>> variableTypeInfo)
    {
        return variableTypeInfo.ContainsKey(variableName) ? variableTypeInfo[variableName].Peek() : null;
    }

    public static IType VisitContextWithExpectedType(Func<IType> function, IType? expectedType,
        Stack<IType?> expectedTypes)
    {
        expectedTypes.Push(expectedType);
        try
        {
            return function();
        }
        finally
        {
            expectedTypes.Pop();
        }
    }

    public static void CheckNotAExpectedType(IType? currentType, Func<string> error)
    {
        if (currentType is null)
        {
            throw new Exception(error());
        }
    }

    private static PatternContext GetPattern(PatternContext pattern)
    {
        return pattern switch
        {
            PatternAscContext context => GetPattern(context.pattern_),
            ParenthesisedPatternContext context => GetPattern(context.pattern_),
            _ => pattern
        };
    }

    public static bool CheckExhaustiveMatchPatterns(IType expectedType, List<PatternContext> patterns)
    {
        var curPatterns = patterns.Select(GetPattern).ToList();
        
        if (curPatterns.Any(p => p is PatternVarContext))
        {
            return true;
        }

        switch (expectedType)
        {
            case TypeNat:
                var numbers = curPatterns.OfType<PatternIntContext>().Select(n => int.Parse(n.n.Text))
                    .ToHashSet();
                int? length = null;
                curPatterns
                    .OfType<PatternSuccContext>()
                    .Select(it =>
                    {
                        var count = 0;
                        PatternContext pattern = it;
                        while (pattern is PatternSuccContext context)
                        {
                            count++;
                            pattern = context.pattern();
                        }

                        return new { Result = count, Pattern = pattern };
                    })
                    .OrderBy(it => it.Result)
                    .ToList()
                    .ForEach(it =>
                    {
                        switch (it.Pattern)
                        {
                            case PatternVarContext when length == null:
                                length = it.Result;
                                break;
                            case PatternIntContext context:
                                numbers.Add(it.Result + int.Parse(context.n.Text));
                                break;
                        }
                    });
                return length != null && Enumerable.Range(0, length.Value).All(i => numbers.Contains(i));

            case TypeBool:
                return curPatterns.Any(p => p is PatternTrueContext) &&
                       curPatterns.Any(p => p is PatternFalseContext);

            case TypeUnit:
                return curPatterns.Any(p => p is PatternUnitContext);

            case TypeTuple typeTuple:
                var tupleList = curPatterns.OfType<PatternTupleContext>()
                    .Where(p => p._patterns.Count == typeTuple.TupleTypes.Count).ToList();

                return typeTuple.TupleTypes.Select((type, index) =>
                {
                    return CheckExhaustiveMatchPatterns(type, tupleList.Select(p => p._patterns[index]).ToList());
                }).All(result => result);

            case TypeRecord typeRecord:
                return typeRecord.Fields.All(field =>
                {
                    var recordList = curPatterns.OfType<PatternRecordContext>()
                        .Where(p => p._patterns.Count == typeRecord.Fields.Count() &&
                                    p._patterns.Any(pp => pp.label.Text.Equals(field.Item1))).SelectMany(p =>
                            p._patterns.Where(pp => pp.label.Text.Equals(field.Item1)).Select(pp => pp.pattern()))
                        .ToList();
                    return CheckExhaustiveMatchPatterns(field.Item2, recordList);
                });

            case TypeSum typeSum:
                var inlList = curPatterns.OfType<PatternInlContext>().Select(p => p.pattern()).ToList();
                var inrList = curPatterns.OfType<PatternInrContext>().Select(p => p.pattern()).ToList();
                return inlList.Count != 0 && inrList.Count != 0 &&
                       CheckExhaustiveMatchPatterns(typeSum.Inl, inlList) &&
                       CheckExhaustiveMatchPatterns(typeSum.Inr, inrList);

            case TypeList typeList:
                var patternsList = curPatterns
                    .OfType<PatternListContext>()
                    .Select(p => p._patterns.ToList())
                    .ToList();
                var consVariableList = new List<List<PatternContext>>();
                var patternConsContexts = curPatterns.OfType<PatternConsContext>().ToList();

                foreach (var patternConsContext in patternConsContexts)
                {
                    var nestedPatterns = new List<PatternContext>();
                    PatternContext currentPattern = patternConsContext;

                    while (currentPattern is PatternConsContext context)
                    {
                        nestedPatterns.Add(context.head);
                        currentPattern = context.tail;
                    }

                    switch (currentPattern)
                    {
                        case PatternVarContext:
                            consVariableList.Add(nestedPatterns);
                            patternsList.Add(nestedPatterns);
                            break;
                        case PatternListContext context:
                            nestedPatterns.AddRange(context._patterns);
                            patternsList.Add(nestedPatterns);
                            break;
                    }
                }

                if (consVariableList.Count == 0)
                {
                    return false;
                }

                return consVariableList.Any(consVariable =>
                {
                    for (var i = 0; i <= consVariable.Count; i++)
                    {
                        var patternsWithSize = patternsList.Where(it => it.Count == i).ToList();
                        for (var j = 0; j < i; j++)
                        {
                            if (!CheckExhaustiveMatchPatterns(typeList.ListType,
                                    patternsWithSize.Select(it => it[j]).ToList()))
                            {
                                return false;
                            }
                        }
                    }

                    return true;
                });

            case TypeVariant typeVariant:
                var variantList = curPatterns.OfType<PatternVariantContext>().ToList();
                return typeVariant.Variants.All(v =>
                {
                    return variantList.Any(vp => vp.label.Text == v.Item1) && (v.Item2 is null ||
                                                                               CheckExhaustiveMatchPatterns(v.Item2,
                                                                                   variantList.Where(vp =>
                                                                                           vp.label.Text == v.Item1)
                                                                                       .Select(vp => vp.pattern())
                                                                                       .ToList()));
                });

            default:
                return false;
        }
    }
}