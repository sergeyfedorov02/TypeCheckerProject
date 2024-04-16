using static stellaParser;
using static TypeCheckerProject.StandardTypes;
using static TypeCheckerProject.ErrorsOutput;

namespace TypeCheckerProject;

public static class CheckTypes
{
    public static bool EqualsIType(IType first, IType second, HashSet<string> extensions, ExprContext? context = null,
        stellaParser? parser = null)
    {
        if (first.ToString() is not null && first.ToString()!.Equals(second.ToString()))
        {
            return true;
        }

        if (second is TypeTop || first is TypeBottom)
        {
            return true;
        }

        if (!extensions.Contains("#structural-subtyping"))
        {
            return false;
        }

        return first switch
        {
            TypeFunction firstTypeFunc when second is TypeFunction secondTypeFunc => CheckTypeFunction(firstTypeFunc,
                secondTypeFunc, extensions),

            TypeTuple firstTypeTuple when second is TypeTuple secondTypeTuple => CheckTypeTuple(firstTypeTuple,
                secondTypeTuple, extensions, context, parser),

            TypeRecord firstTypeRecord when second is TypeRecord secondTypeRecord => CheckTypeRecord(firstTypeRecord,
                secondTypeRecord, extensions, context, parser),

            TypeSum firstTypeSum when second is TypeSum secondTypeSum => CheckTypeSum(firstTypeSum, secondTypeSum,
                extensions),

            TypeList firstTypeList when second is TypeList secondTypeList => CheckTypeList(firstTypeList,
                secondTypeList, extensions),

            TypeVariant firstTypeVariant when second is TypeVariant secondTypeVariant => CheckTypeVariant(
                firstTypeVariant, secondTypeVariant, extensions, context, parser),

            TypeRef firstTypeRef when second is TypeRef secondTypeRef => CheckTypeRef(firstTypeRef, secondTypeRef,
                extensions),

            _ => false
        };
    }

    private static bool CheckTypeFunction(TypeFunction firstTypeFunc, TypeFunction secondTypeFunc,
        HashSet<string> extensions)
    {
        if (!EqualsIType(firstTypeFunc.ReturnType, secondTypeFunc.ReturnType, extensions))
        {
            return false;
        }

        if (firstTypeFunc.ArgumentTypes.Count != secondTypeFunc.ArgumentTypes.Count)
        {
            return false;
        }

        return firstTypeFunc.ArgumentTypes
            .Select((value, index) => EqualsIType(secondTypeFunc.ArgumentTypes[index], value, extensions))
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
            .All(pair => EqualsIType(pair.Value, secondFields[pair.Key], extensions));
    }

    private static bool CheckTypeTuple(TypeTuple firstTypeTuple, TypeTuple secondTypeTuple,
        HashSet<string> extensions, ExprContext? context, stellaParser? parser)
    {
        if (firstTypeTuple.TupleTypes.Count != secondTypeTuple.TupleTypes.Count)
        {
            throw new Exception(ErrorUnexpectedTupleLength(firstTypeTuple, context!, parser!));
        }

        return firstTypeTuple.TupleTypes
            .Select((type, index) => EqualsIType(type, secondTypeTuple.TupleTypes[index], extensions))
            .All(result => result);
    }

    private static bool CheckTypeSum(TypeSum firstTypeSum, TypeSum secondTypeSum, HashSet<string> extensions)
    {
        return EqualsIType(firstTypeSum.Inl, secondTypeSum.Inl, extensions) &&
               EqualsIType(firstTypeSum.Inr, secondTypeSum.Inr, extensions);
    }

    private static bool CheckTypeList(TypeList firstTypeList, TypeList secondTypeList, HashSet<string> extensions)
    {
        return EqualsIType(firstTypeList.ListType, secondTypeList.ListType, extensions);
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

        return firstVariants.All(pair =>
        {
            var result = pair.Value is null || EqualsIType(pair.Value, secondVariants[pair.Key]!, extensions);
            return result;
        });
    }

    private static bool CheckTypeRef(TypeRef firstTypeRef, TypeRef secondTypeRef, HashSet<string> extensions)
    {
        return EqualsIType(firstTypeRef.InternalType, secondTypeRef.InternalType, extensions);
    }
}