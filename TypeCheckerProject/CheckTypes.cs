using static stellaParser;
using static TypeCheckerProject.StandardTypes;
using static TypeCheckerProject.ErrorsOutput;

namespace TypeCheckerProject;

public static class CheckTypes
{
    public static bool EqualsIType(IType first, IType second, HashSet<string> extensions, ExprContext? context = null,
        stellaParser? parser = null)
    {
        if (first is UniversalTypeVar firstUniversalVar && second is UniversalTypeVar secondUniversalVar)
        {
            return firstUniversalVar.Name == secondUniversalVar.Name;
        }

        if (first is UniversalType firstUniversal && second is UniversalType secondUniversal)
        {
            if (firstUniversal.Variables.Count != secondUniversal.Variables.Count)
            {
                return false;
            }

            var typesDictionary = firstUniversal.Variables.Select((typeVar, index) =>
            {
                var value = secondUniversal.Variables[index];
                return (typeVar, value);
            }).ToDictionary(pair => pair.typeVar, pair => pair.value as IType);
            var replaceType = ReplaceType(firstUniversal, typesDictionary);

            return EqualsIType(replaceType, secondUniversal.NestedType, extensions, context, parser);
        }

        if (first.ToString() is not null && first.ToString()!.Equals(second.ToString()))
        {
            return true;
        }

        if (!extensions.Contains("#structural-subtyping"))
        {
            return false;
        }

        if (second is TypeTop || first is TypeBottom)
        {
            return true;
        }

        return first switch
        {
            TypeFunction firstTypeFunc when second is TypeFunction secondTypeFunc => CheckTypeFunction(firstTypeFunc,
                secondTypeFunc, extensions, context, parser),

            TypeTuple firstTypeTuple when second is TypeTuple secondTypeTuple => CheckTypeTuple(firstTypeTuple,
                secondTypeTuple, extensions, context, parser),

            TypeRecord firstTypeRecord when second is TypeRecord secondTypeRecord => CheckTypeRecord(firstTypeRecord,
                secondTypeRecord, extensions, context, parser),

            TypeSum firstTypeSum when second is TypeSum secondTypeSum => CheckTypeSum(firstTypeSum, secondTypeSum,
                extensions, context, parser),

            TypeList firstTypeList when second is TypeList secondTypeList => CheckTypeList(firstTypeList,
                secondTypeList, extensions, context, parser),

            TypeVariant firstTypeVariant when second is TypeVariant secondTypeVariant => CheckTypeVariant(
                firstTypeVariant, secondTypeVariant, extensions, context, parser),

            TypeRef firstTypeRef when second is TypeRef secondTypeRef => CheckTypeRef(firstTypeRef, secondTypeRef,
                extensions, context, parser),

            _ => false
        };
    }

    private static bool CheckTypeFunction(TypeFunction firstTypeFunc, TypeFunction secondTypeFunc,
        HashSet<string> extensions, ExprContext? context, stellaParser? parser)
    {
        if (!EqualsIType(firstTypeFunc.ReturnType, secondTypeFunc.ReturnType, extensions, context, parser))
        {
            return false;
        }

        if (firstTypeFunc.ArgumentTypes.Count != secondTypeFunc.ArgumentTypes.Count)
        {
            throw new Exception(ErrorIncorrectNumberOfArguments(firstTypeFunc.ArgumentTypes.Count,
                secondTypeFunc.ArgumentTypes.Count, context!, parser!));
        }

        return firstTypeFunc.ArgumentTypes
            .Select((value, index) =>
                EqualsIType(secondTypeFunc.ArgumentTypes[index], value, extensions, context, parser))
            .All(result => result);
    }

    private static bool CheckTypeRecord(TypeRecord firstTypeRecord, TypeRecord secondTypeRecord,
        HashSet<string> extensions, ExprContext? context, stellaParser? parser)
    {
        var firstFields = firstTypeRecord.Fields.ToDictionary();
        var secondFields = secondTypeRecord.Fields.ToDictionary();

        var secondExceptFirst = secondFields.Keys.Except(firstFields.Keys).ToList();

        if (secondExceptFirst.Count != 0)
        {
            throw new Exception(
                ErrorMissingRecordFields(firstTypeRecord, secondTypeRecord, context!, secondExceptFirst, parser!));
        }

        return firstFields.Where(pair => secondFields.ContainsKey(pair.Key))
            .All(pair => EqualsIType(pair.Value, secondFields[pair.Key], extensions, context, parser));
    }

    private static bool CheckTypeTuple(TypeTuple firstTypeTuple, TypeTuple secondTypeTuple,
        HashSet<string> extensions, ExprContext? context, stellaParser? parser)
    {
        if (firstTypeTuple.TupleTypes.Count != secondTypeTuple.TupleTypes.Count)
        {
            throw new Exception(ErrorUnexpectedTupleLength(firstTypeTuple.TupleTypes.Count, context!, parser!));
        }

        return firstTypeTuple.TupleTypes
            .Select((type, index) => EqualsIType(type, secondTypeTuple.TupleTypes[index], extensions, context, parser))
            .All(result => result);
    }

    private static bool CheckTypeSum(TypeSum firstTypeSum, TypeSum secondTypeSum, HashSet<string> extensions,
        ExprContext? context, stellaParser? parser)
    {
        return EqualsIType(firstTypeSum.Inl, secondTypeSum.Inl, extensions, context, parser) &&
               EqualsIType(firstTypeSum.Inr, secondTypeSum.Inr, extensions, context, parser);
    }

    private static bool CheckTypeList(TypeList firstTypeList, TypeList secondTypeList, HashSet<string> extensions,
        ExprContext? context, stellaParser? parser)
    {
        return EqualsIType(firstTypeList.ListType, secondTypeList.ListType, extensions, context, parser);
    }

    private static bool CheckTypeVariant(TypeVariant firstTypeVariant, TypeVariant secondTypeVariant,
        HashSet<string> extensions, ExprContext? context, stellaParser? parser)
    {
        var firstVariants = firstTypeVariant.Variants.ToDictionary();
        var secondVariants = secondTypeVariant.Variants.ToDictionary();

        var firstExceptSecond = firstVariants.Keys.Except(secondVariants.Keys).ToList();

        if (firstExceptSecond.Count != 0)
        {
            var label = firstExceptSecond.First();
            throw new Exception(ErrorUnexpectedVariantLabel(label, secondTypeVariant, context!,
                parser!));
        }

        foreach (var key in secondVariants.Keys.Intersect(firstVariants.Keys))
        {
            if (secondVariants[key] is not null && firstVariants[key] is null)
            {
                throw new Exception(ErrorMissingTypeForLabel(secondVariants[key]!, context!, parser!));
            }
        }

        foreach (var key in secondVariants.Keys.Intersect(firstVariants.Keys))
        {
            if (secondVariants[key] is null && firstVariants[key] is not null)
            {
                throw new Exception(ErrorUnexpectedTypeForNullaryLabel(firstVariants[key]!, context!, parser!));
            }
        }

        return firstVariants.All(pair =>
        {
            var result = pair.Value is null ||
                         EqualsIType(pair.Value, secondVariants[pair.Key]!, extensions, context, parser);
            return result;
        });
    }

    private static bool CheckTypeRef(TypeRef firstTypeRef, TypeRef secondTypeRef, HashSet<string> extensions,
        ExprContext? context, stellaParser? parser)
    {
        return EqualsIType(firstTypeRef.InternalType, secondTypeRef.InternalType, extensions, context, parser) &&
               EqualsIType(secondTypeRef.InternalType, firstTypeRef.InternalType, extensions, context, parser);
    }

    public static IType ReplaceType(IType curType, Dictionary<UniversalTypeVar, IType> typesDictionary)
    {
        if (curType is UniversalType universalType)
        {
            var replacedType = ReplaceType(universalType.NestedType, typesDictionary);
            return !typesDictionary.Keys.All(it => universalType.Variables.Contains(it))
                ? universalType with { NestedType = replacedType }
                : replacedType;
        }

        return curType switch
        {
            TypeFunction typeFunction => new TypeFunction(
                typeFunction.ArgumentTypes.Select(argType => ReplaceType(argType, typesDictionary)).ToList(),
                ReplaceType(typeFunction.ReturnType, typesDictionary)),

            TypeTuple typeTuple => new TypeTuple(typeTuple.TupleTypes.Select(type => ReplaceType(type, typesDictionary))
                .ToList()),

            TypeRecord typeRecord => new TypeRecord(typeRecord.Fields
                .Select(field =>
                {
                    var fieldLabel = field.Item1;
                    var fieldType = ReplaceType(field.Item2, typesDictionary);
                    return (fieldLabel, fieldType);
                }).ToList()),

            TypeSum typeSum => new TypeSum(ReplaceType(typeSum.Inl, typesDictionary),
                ReplaceType(typeSum.Inr, typesDictionary)),

            TypeList typeList => new TypeList(ReplaceType(typeList.ListType, typesDictionary)),

            TypeVariant typeVariant => new TypeVariant(typeVariant.Variants.Select(variant =>
            {
                var variantLabel = variant.Item1;
                var variantType = ReplaceType(variant.Item2, typesDictionary);
                return (variantLabel, variantType);
            }).ToList()),

            UniversalTypeVar universalTypeVar =>
                typesDictionary.TryGetValue(universalTypeVar, out var substitutedType)
                    ? substitutedType
                    : universalTypeVar,

            _ => curType
        };
    }
}