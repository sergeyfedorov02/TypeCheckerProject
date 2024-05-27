namespace TypeCheckerProject;

using static stellaParser;
using static StandardTypes;
using static CheckTypes;

public class UnexpectedTypeException : Exception
{
    public IType Expected { get; }
    public IType Actual { get; }
    public ExprContext Expr { get; }

    public UnexpectedTypeException(IType expected, IType actual, ExprContext expr)
    {
        Expected = expected;
        Actual = actual;
        Expr = expr;
    }
}

public class OccursInfiniteTypeException : Exception
{
    public IType Expected { get; }
    public IType Actual { get; }
    public ExprContext Expr { get; }

    public OccursInfiniteTypeException(IType expected, IType actual, ExprContext expr)
    {
        Expected = expected;
        Actual = actual;
        Expr = expr;
    }
}

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

    public static UniversalTypeVar? TryGetGeneric(string genericName, Stack<List<UniversalTypeVar>> generics)
    {
        return generics.AsEnumerable().Reverse().FirstOrDefault(generic => generic.Any(v => v.Name == genericName))
            ?.First(v => v.Name == genericName);
    }

    public static IType VisitContextWithGenerics(Func<IType> function, List<UniversalTypeVar> curGenerics,
        Stack<List<UniversalTypeVar>> generics)
    {
        generics.Push(curGenerics);
        try
        {
            return function();
        }
        finally
        {
            generics.Pop();
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

    public static bool CheckExhaustiveMatchPatterns(IType expectedType, List<PatternContext> patterns,
        List<Constraint> constraintsList, HashSet<string> extensions)
    {
        var curPatterns = patterns.Select(GetPattern).ToList();

        if (curPatterns.Any(p => p is PatternVarContext))
        {
            return true;
        }

        var curExpectedType = expectedType;
        if (curExpectedType is TypeVariable curExpectedTypeVariable)
        {
            try
            {
                var subst = VisitConstraint(constraintsList, extensions);
                foreach (var curSubst in subst.Where(curSubst => curSubst.Item1 == curExpectedTypeVariable))
                {
                    curExpectedType = curSubst.Item2;
                    break;
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }

        switch (curExpectedType)
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
                    return CheckExhaustiveMatchPatterns(type, tupleList.Select(p => p._patterns[index]).ToList(),
                        constraintsList, extensions);
                }).All(result => result);

            case TypeRecord typeRecord:
                return typeRecord.Fields.All(field =>
                {
                    var recordList = curPatterns.OfType<PatternRecordContext>()
                        .Where(p => p._patterns.Count == typeRecord.Fields.Count() &&
                                    p._patterns.Any(pp => pp.label.Text.Equals(field.Item1))).SelectMany(p =>
                            p._patterns.Where(pp => pp.label.Text.Equals(field.Item1)).Select(pp => pp.pattern()))
                        .ToList();
                    return CheckExhaustiveMatchPatterns(field.Item2, recordList, constraintsList, extensions);
                });

            case TypeSum typeSum:
                var inlList = curPatterns.OfType<PatternInlContext>().Select(p => p.pattern()).ToList();
                var inrList = curPatterns.OfType<PatternInrContext>().Select(p => p.pattern()).ToList();
                return inlList.Count != 0 && inrList.Count != 0 &&
                       CheckExhaustiveMatchPatterns(typeSum.Inl, inlList, constraintsList, extensions) &&
                       CheckExhaustiveMatchPatterns(typeSum.Inr, inrList, constraintsList, extensions);

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
                                    patternsWithSize.Select(it => it[j]).ToList(), constraintsList, extensions))
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
                                                                                       .ToList(), constraintsList,
                                                                                   extensions));
                });

            case TypeVariable:
            case UniversalTypeVar:
            case UniversalType:
                return true;

            default:
                return false;
        }
    }

    public static List<(TypeVariable, IType)> VisitConstraint(List<Constraint> constraintsList,
        HashSet<string> extensions)
    {
        if (constraintsList.Count == 0)
        {
            return new List<(TypeVariable, IType)>();
        }

        var constraint = constraintsList.First();
        var tail = constraintsList.Skip(1).ToList();

        if (constraint.Lhv is TypeVariable && constraint.Rhv is TypeVariable && constraint.Lhv == constraint.Rhv)
        {
            return VisitConstraint(tail, extensions);
        }

        if (constraint.Lhv is TypeVariable lhvVariable &&
            !ContainsType(lhvVariable, constraint.Rhv, constraint.ExprContext))
        {
            return VisitConstraint(tail.Select(c => ReplaceConstraintType(c, lhvVariable, constraint.Rhv)).ToList(),
                    extensions)
                .Concat(new List<(TypeVariable, IType)> { (lhvVariable, constraint.Rhv) }).ToList();
        }

        if (constraint.Rhv is TypeVariable rhvVariable &&
            !ContainsType(rhvVariable, constraint.Lhv, constraint.ExprContext))
        {
            return VisitConstraint(tail.Select(c => ReplaceConstraintType(c, rhvVariable, constraint.Lhv)).ToList(),
                    extensions)
                .Concat(new List<(TypeVariable, IType)> { (rhvVariable, constraint.Lhv) }).ToList();
        }

        switch (constraint.Lhv)
        {
            case TypeFunction lhvFunction when constraint.Rhv is TypeFunction rhvFunction:
                return VisitConstraint(tail.Concat(new List<Constraint>
                {
                    new(lhvFunction.ArgumentTypes.First(), rhvFunction.ArgumentTypes.First(),
                        constraint.ExprContext),
                    new(lhvFunction.ReturnType, rhvFunction.ReturnType, constraint.ExprContext)
                }).ToList(), extensions);

            case TypeList lhvList when constraint.Rhv is TypeList rhvList:
                return VisitConstraint(tail.Concat(new List<Constraint>
                {
                    new(lhvList.ListType, rhvList.ListType, constraint.ExprContext)
                }).ToList(), extensions);

            case TypeSum lhvSum when constraint.Rhv is TypeSum rhvSum:
                return VisitConstraint(tail.Concat(new List<Constraint>
                {
                    new(lhvSum.Inl, rhvSum.Inl, constraint.ExprContext),
                    new(lhvSum.Inr, rhvSum.Inr, constraint.ExprContext)
                }).ToList(), extensions);

            case TypeTuple lhvTuple when constraint.Rhv is TypeTuple rhvTuple:
                if (lhvTuple.TupleTypes.Count == 2 && rhvTuple.TupleTypes.Count == 2)
                {
                    return VisitConstraint(tail.Concat(new List<Constraint>
                    {
                        new(lhvTuple.TupleTypes[0], rhvTuple.TupleTypes[0], constraint.ExprContext),
                        new(lhvTuple.TupleTypes[1], rhvTuple.TupleTypes[1], constraint.ExprContext)
                    }).ToList(), extensions);
                }

                break;

            default:
            {
                if (EqualsIType(constraint.Lhv, constraint.Rhv, extensions))
                {
                    return VisitConstraint(tail, extensions);
                }

                break;
            }
        }

        throw new UnexpectedTypeException(constraint.Lhv, constraint.Rhv, constraint.ExprContext);
    }

    private static bool ContainsType(TypeVariable first, IType second, ExprContext exprContext)
    {
        var contains = second switch
        {
            TypeFunction typeFunction => ContainsType(first, typeFunction.ArgumentTypes.First(), exprContext) ||
                                         ContainsType(first, typeFunction.ReturnType, exprContext),
            TypeTuple tupleType => ContainsType(first, tupleType.TupleTypes[0], exprContext) ||
                                   ContainsType(first, tupleType.TupleTypes[1], exprContext),
            TypeSum typeSum => ContainsType(first, typeSum.Inl, exprContext) ||
                               ContainsType(first, typeSum.Inr, exprContext),
            TypeList typeList => ContainsType(first, typeList.ListType, exprContext),
            TypeVariable typeVariable => first == typeVariable,
            _ => false
        };

        if (contains)
        {
            throw new OccursInfiniteTypeException(first, second, exprContext);
        }

        return contains;
    }

    private static Constraint ReplaceConstraintType(Constraint constraint, TypeVariable constraintType, IType type)
    {
        IType ReplaceType(IType curType)
        {
            if (curType is TypeVariable typeVariable && typeVariable.ToString().Equals(constraintType.ToString()))
            {
                return type;
            }

            return curType switch
            {
                TypeFunction typeFunction => new TypeFunction(typeFunction.ArgumentTypes.Select(ReplaceType).ToList(),
                    ReplaceType(typeFunction.ReturnType)),
                TypeTuple typeTuple => new TypeTuple(typeTuple.TupleTypes.Select(ReplaceType).ToList()),
                TypeSum typeSum => new TypeSum(ReplaceType(typeSum.Inl), ReplaceType(typeSum.Inr)),
                TypeList typeList => new TypeList(ReplaceType(typeList.ListType)),
                _ => curType
            };
        }

        return new Constraint(ReplaceType(constraint.Lhv), ReplaceType(constraint.Rhv), constraint.ExprContext);
    }
}