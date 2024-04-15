namespace TypeCheckerProject;

public static class StandardTypes
{
    public interface IType
    {
    }

    public record TypeNat : IType
    {
        public override string ToString()
        {
            return "Nat";
        }
    }

    public record TypeBool : IType
    {
        public override string ToString()
        {
            return "Bool";
        }
    }

    public record TypeUnit : IType
    {
        public override string ToString()
        {
            return "Unit";
        }
    }

    public record TypeFunction(List<IType> ArgumentTypes, IType ReturnType) : IType
    {
        public override string ToString()
        {
            return $"fn({string.Join(", ", ArgumentTypes)}) -> {ReturnType}";
        }
    }


    public record TypeTuple(List<IType> TupleTypes) : IType
    {
        public override string ToString()
        {
            return "{" + string.Join(", ", TupleTypes) + "}";
        }
    }


    public record TypeRecord(IEnumerable<(string, IType)> Fields) : IType
    {
        public override string ToString()
        {
            return "{" + string.Join(", ", Fields.Select(field => $"{field.Item1} : {field.Item2}")) + "}";
        }
    }

    public record TypeSum(IType Inl, IType Inr) : IType
    {
        public override string ToString()
        {
            return $"{Inl} + {Inr}";
        }
    }

    public record TypeList(IType ListType) : IType
    {
        public override string ToString()
        {
            return $"[{ListType}]";
        }
    }

    public record TypeVariant(IEnumerable<(string, IType?)> Variants) : IType
    {
        public override string ToString()
        {
            return
                $"<| {string.Join(", ", Variants.Select(it => $"{it.Item1}{(it.Item2 != null ? $" : {it.Item2}" : "")}"))} |>";
        }
    }

    public record TypeRef(IType InternalType) : IType
    {
        public override string ToString()
        {
            return
                $"&{InternalType}";
        }
    }


    public static bool EqualsIType(IType first, IType second)
    {
        return first.ToString() is not null && first.ToString()!.Equals(second.ToString());
    }

    public static IEnumerable<(string, IType)> ExceptRecords(IEnumerable<(string, IType)> values,
        IEnumerable<(string, IType)> expectedValues)
    {
        var exceptResult = new Dictionary<string, IType>();
        foreach (var value in values)
        {
            var containsFlag = false;
            foreach (var expectedValue in expectedValues)
            {
                if (value.Item1.Equals(expectedValue.Item1))
                {
                    if (EqualsIType(value.Item2, expectedValue.Item2))
                    {
                        containsFlag = true;
                        break;
                    }
                }
            }

            if (!containsFlag)
            {
                exceptResult[value.Item1] = value.Item2;
            }
        }

        return exceptResult.Select(kv => (kv.Key, kv.Value));
    }
}