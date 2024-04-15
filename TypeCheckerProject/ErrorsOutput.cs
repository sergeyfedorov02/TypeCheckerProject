using System.Text;
using Antlr4.Runtime;
using static stellaParser;
using static TypeCheckerProject.StandardTypes;

namespace TypeCheckerProject;

public static class ErrorsOutput
{
    public static string ErrorMissingMain()
    {
        var sb = new StringBuilder();
        sb.AppendLine("ERROR_MISSING_MAIN:");
        sb.AppendLine("в программе отсутствует функция main");

        return sb.ToString();
    }

    public static string ErrorUndefinedVariable(string variable, RuleContext expression, stellaParser parser)
    {
        var sb = new StringBuilder();
        sb.AppendLine("ERROR_UNDEFINED_VARIABLE:");
        sb.AppendLine("присутствует необъявленная переменная");
        sb.AppendLine($"{variable}");
        sb.AppendLine("в выражении");
        sb.AppendLine($"{expression.ToStringTree(parser)}");

        return sb.ToString();
    }

    public static string ErrorUnexpectedTypeForExpression(IType? expectedType, IType actualType, ExprContext expression,
        stellaParser parser)
    {
        var sb = new StringBuilder();
        sb.AppendLine("ERROR_UNEXPECTED_TYPE_FOR_EXPRESSION:");
        sb.AppendLine("ожидается тип");
        sb.AppendLine($"{expectedType}");
        sb.AppendLine("но получен тип");
        sb.AppendLine($"{actualType}");
        sb.AppendLine("для выражения");
        sb.AppendLine($"{expression.ToStringTree(parser)}");

        return sb.ToString();
    }

    public static string ErrorNotAFunction(IType actualType, ExprContext expression, stellaParser parser)
    {
        var sb = new StringBuilder();
        sb.AppendLine("ERROR_NOT_A_FUNCTION:");
        sb.AppendLine("ожидается тип Function");
        sb.AppendLine("но получен тип");
        sb.AppendLine($"{actualType}");
        sb.AppendLine("для выражения");
        sb.AppendLine($"{expression.ToStringTree(parser)}");

        return sb.ToString();
    }

    public static string ErrorNotATuple(IType actualType, ExprContext expression, stellaParser parser)
    {
        var sb = new StringBuilder();
        sb.AppendLine("ERROR_NOT_A_TUPLE:");
        sb.AppendLine("ожидается тип Tuple");
        sb.AppendLine("но получен тип");
        sb.AppendLine($"{actualType}");
        sb.AppendLine("для выражения");
        sb.AppendLine($"{expression.ToStringTree(parser)}");

        return sb.ToString();
    }

    public static string ErrorNotARecord(IType actualType, ExprContext expression, stellaParser parser)
    {
        var sb = new StringBuilder();
        sb.AppendLine("ERROR_NOT_A_RECORD:");
        sb.AppendLine("ожидается тип Record");
        sb.AppendLine("но получен тип");
        sb.AppendLine($"{actualType}");
        sb.AppendLine("для выражения");
        sb.AppendLine($"{expression.ToStringTree(parser)}");

        return sb.ToString();
    }

    public static string ErrorNotAList(IType actualType, ExprContext expression, stellaParser parser)
    {
        var sb = new StringBuilder();
        sb.AppendLine("ERROR_NOT_A_LIST:");
        sb.AppendLine("ожидается тип List");
        sb.AppendLine("но получен тип");
        sb.AppendLine($"{actualType}");
        sb.AppendLine("для выражения");
        sb.AppendLine($"{expression.ToStringTree(parser)}");

        return sb.ToString();
    }

    public static string ErrorUnexpectedLambda(IType expectedType, ExprContext expression, stellaParser parser)
    {
        var sb = new StringBuilder();
        sb.AppendLine("ERROR_UNEXPECTED_LAMBDA:");
        sb.AppendLine("ожидается не функциональный тип");
        sb.AppendLine($"{expectedType}");
        sb.AppendLine("но получен функциональный тип");
        sb.AppendLine("для выражения");
        sb.AppendLine($"{expression.ToStringTree(parser)}");

        return sb.ToString();
    }

    public static string ErrorUnexpectedTypeForParameter(IType expectedType, IType actualType, ExprContext expression,
        stellaParser parser)
    {
        var sb = new StringBuilder();
        sb.AppendLine("ERROR_UNEXPECTED_TYPE_FOR_PARAMETER:");
        sb.AppendLine("ожидается параметр типа");
        sb.AppendLine($"{expectedType}");
        sb.AppendLine("но получен тип");
        sb.AppendLine($"{actualType}");
        sb.AppendLine("для выражения");
        sb.AppendLine($"{expression.ToStringTree(parser)}");

        return sb.ToString();
    }

    public static string ErrorUnexpectedTuple(IType expectedType, ExprContext expression, stellaParser parser)
    {
        var sb = new StringBuilder();
        sb.AppendLine("ERROR_UNEXPECTED_TUPLE:");
        sb.AppendLine("ожидается не тип Tuple");
        sb.AppendLine($"{expectedType}");
        sb.AppendLine("но получен тип Tuple");
        sb.AppendLine("для выражения");
        sb.AppendLine($"{expression.ToStringTree(parser)}");

        return sb.ToString();
    }

    public static string ErrorUnexpectedRecord(IType expectedType, ExprContext expression, stellaParser parser)
    {
        var sb = new StringBuilder();
        sb.AppendLine("ERROR_UNEXPECTED_RECORD:");
        sb.AppendLine("ожидается не тип Record");
        sb.AppendLine($"{expectedType}");
        sb.AppendLine("но получен тип Record");
        sb.AppendLine("для выражения");
        sb.AppendLine($"{expression.ToStringTree(parser)}");

        return sb.ToString();
    }

    public static string ErrorUnexpectedList(IType expectedType, ExprContext expression, stellaParser parser)
    {
        var sb = new StringBuilder();
        sb.AppendLine("ERROR_UNEXPECTED_LIST:");
        sb.AppendLine("ожидается не тип List");
        sb.AppendLine($"{expectedType}");
        sb.AppendLine("но получен тип List");
        sb.AppendLine("для выражения");
        sb.AppendLine($"{expression.ToStringTree(parser)}");

        return sb.ToString();
    }

    public static string ErrorUnexpectedInjection(IType expectedType, ExprContext expression, stellaParser parser)
    {
        var sb = new StringBuilder();
        sb.AppendLine("ERROR_UNEXPECTED_INJECTION:");
        sb.AppendLine("ожидается не тип-сумма");
        sb.AppendLine($"{expectedType}");
        sb.AppendLine("но получен Injection");
        sb.AppendLine("для выражения");
        sb.AppendLine($"{expression.ToStringTree(parser)}");

        return sb.ToString();
    }

    public static string ErrorUnexpectedVariant(IType expectedType, ExprContext expression, stellaParser parser)
    {
        var sb = new StringBuilder();
        sb.AppendLine("ERROR_UNEXPECTED_VARIANT:");
        sb.AppendLine("ожидается не тип Variant");
        sb.AppendLine($"{expectedType}");
        sb.AppendLine("но получен тип Variant");
        sb.AppendLine("для выражения");
        sb.AppendLine($"{expression.ToStringTree(parser)}");

        return sb.ToString();
    }

    public static string ErrorMissingRecordFields(IType expectedType, IType actualType, ExprContext expression,
        IEnumerable<(string, IType)> fields, stellaParser parser)
    {
        var fieldsString = string.Join(", ", fields.Select(field => $"{field.Item1} : {field.Item2}"));

        var sb = new StringBuilder();
        sb.AppendLine("ERROR_MISSING_RECORD_FIELDS:");
        sb.AppendLine("ожидается тип");
        sb.AppendLine($"{expectedType}");
        sb.AppendLine("но получен тип Record");
        sb.AppendLine($"{actualType}");
        sb.AppendLine("в котором отсутствуют ожидаемые поля");
        sb.AppendLine($"{fieldsString}");
        sb.AppendLine("для выражения");
        sb.AppendLine($"{expression.ToStringTree(parser)}");

        return sb.ToString();
    }

    public static string ErrorUnexpectedRecordFields(IType expectedType, IType actualType, ExprContext expression,
        IEnumerable<(string, IType)> fields, stellaParser parser)
    {
        var fieldsString = string.Join(", ", fields.Select(field => $"{field.Item1} : {field.Item2}"));

        var sb = new StringBuilder();
        sb.AppendLine("ERROR_UNEXPECTED_RECORD_FIELDS:");
        sb.AppendLine("ожидается тип");
        sb.AppendLine($"{expectedType}");
        sb.AppendLine("но получен тип Record");
        sb.AppendLine($"{actualType}");
        sb.AppendLine("в котором присутствуют поля, которых нет в ожидаемом типе Record");
        sb.AppendLine($"{fieldsString}");
        sb.AppendLine("для выражения");
        sb.AppendLine($"{expression.ToStringTree(parser)}");

        return sb.ToString();
    }

    public static string ErrorUnexpectedFieldAccess(string label, IType currentRecord, ExprContext expression,
        stellaParser parser)
    {
        var sb = new StringBuilder();
        sb.AppendLine("ERROR_UNEXPECTED_FIELD_ACCESS:");
        sb.AppendLine("попытка извлечь отсутствующее поле записи");
        sb.AppendLine($"{label}");
        sb.AppendLine("для типа Record");
        sb.AppendLine($"{currentRecord}");
        sb.AppendLine("для выражения");
        sb.AppendLine($"{expression.ToStringTree(parser)}");

        return sb.ToString();
    }

    public static string ErrorUnexpectedVariantLabel(string label, IType currentVariant,
        ExprContext expression, stellaParser parser)
    {
        var sb = new StringBuilder();
        sb.AppendLine("ERROR_UNEXPECTED_VARIANT_LABEL:");
        sb.AppendLine("получена неизвестная метка");
        sb.AppendLine($"{label}");
        sb.AppendLine("для типа Variant");
        sb.AppendLine($"{currentVariant}");
        sb.AppendLine("для выражения");
        sb.AppendLine($"{expression.ToStringTree(parser)}");

        return sb.ToString();
    }

    public static string ErrorTupleIndexOfBounds(int indexOfBounds, ExprContext expression, stellaParser parser)
    {
        var sb = new StringBuilder();
        sb.AppendLine("ERROR_TUPLE_INDEX_OUT_OF_BOUNDS:");
        sb.AppendLine("попытка извлечь отсутствующий компонент кортежа");
        sb.AppendLine($"{indexOfBounds + 1}");
        sb.AppendLine("для выражения");
        sb.AppendLine($"{expression.ToStringTree(parser)}");

        return sb.ToString();
    }

    public static string ErrorUnexpectedTupleLength(IType expectedTuple, ExprContext expression, stellaParser parser)
    {
        var sb = new StringBuilder();
        sb.AppendLine("ERROR_UNEXPECTED_TUPLE_LENGTH:");
        sb.AppendLine("ожидается Tuple");
        sb.AppendLine($"{expectedTuple}");
        sb.AppendLine("длина которого");
        sb.AppendLine($"{(expectedTuple as TypeTuple)!.TupleTypes.Count}");
        sb.AppendLine("для выражения");
        sb.AppendLine($"{expression.ToStringTree(parser)}");

        return sb.ToString();
    }

    public static string ErrorAmbiguousSumType(ExprContext expression, stellaParser parser)
    {
        var sb = new StringBuilder();
        sb.AppendLine("ERROR_AMBIGUOUS_SUM_TYPE:");
        sb.AppendLine("тип инъекции");
        sb.AppendLine($"{expression.ToStringTree(parser)}");
        sb.AppendLine("невозможно определить");
        sb.AppendLine("(в данном контексте отсутствует ожидаемый тип-сумма)");

        return sb.ToString();
    }

    public static string ErrorAmbiguousVariantType(ExprContext expression, stellaParser parser)
    {
        var sb = new StringBuilder();
        sb.AppendLine("ERROR_AMBIGUOUS_VARIANT_TYPE:");
        sb.AppendLine("тип варианта");
        sb.AppendLine($"{expression.ToStringTree(parser)}");
        sb.AppendLine("невозможно определить");
        sb.AppendLine("(в данном контексте отсутствует ожидаемый тип варианта)");

        return sb.ToString();
    }

    public static string ErrorAmbiguousListType(ExprContext expression, stellaParser parser)
    {
        var sb = new StringBuilder();
        sb.AppendLine("ERROR_AMBIGUOUS_LIST_TYPE:");
        sb.AppendLine("тип списка");
        sb.AppendLine($"{expression.ToStringTree(parser)}");
        sb.AppendLine("невозможно определить");
        sb.AppendLine("(в данном контексте отсутствует ожидаемый тип списка)");

        return sb.ToString();
    }

    public static string ErrorAmbiguousPatternType(PatternContext pattern, stellaParser parser)
    {
        var sb = new StringBuilder();
        sb.AppendLine("ERROR_AMBIGUOUS_PATTERN_TYPE:");
        sb.AppendLine("тип паттерна");
        sb.AppendLine($"{pattern.ToStringTree(parser)}");
        sb.AppendLine("невозможно определить");
        sb.AppendLine("(в данном контексте отсутсвует ожидаемый тип паттерна)");

        return sb.ToString();
    }

    public static string ErrorIllegalEmptyMatching(ExprContext expression, stellaParser parser)
    {
        var sb = new StringBuilder();
        sb.AppendLine("ERROR_ILLEGAL_EMPTY_MATCHING:");
        sb.AppendLine("match выражение");
        sb.AppendLine($"{expression.ToStringTree(parser)}");
        sb.AppendLine("с пустым списком альтернатив");

        return sb.ToString();
    }

    public static string ErrorNonexhaustiveMatchPatterns(IType expectedType, ExprContext expression,
        stellaParser parser)
    {
        var sb = new StringBuilder();
        sb.AppendLine("ERROR_NONEXHAUSTIVE_MATCH_PATTERNS:");
        sb.AppendLine("не все образцы для типа");
        sb.AppendLine($"{expectedType}");
        sb.AppendLine("перечислены в match-выражении");
        sb.AppendLine($"{expression.ToStringTree(parser)}");

        return sb.ToString();
    }

    public static string ErrorUnexpectedPatternForType(IType? currentType, PatternContext pattern, stellaParser parser)
    {
        var sb = new StringBuilder();
        sb.AppendLine("ERROR_UNEXPECTED_PATTERN_FOR_TYPE:");
        sb.AppendLine("образец в match-выражении");
        sb.AppendLine($"{pattern.ToStringTree(parser)}");
        sb.AppendLine("не соответствует типу разбираемого выражения");
        sb.AppendLine($"{currentType}");

        return sb.ToString();
    }

    public static string ErrorIncorrectArityOfMain(int numberArg)
    {
        var sb = new StringBuilder();
        sb.AppendLine("ERROR_INCORRECT_ARITY_OF_MAIN:");
        sb.AppendLine("функция main объявлена с N парамтерами");
        sb.AppendLine($"где N = {numberArg} != 1");

        return sb.ToString();
    }

    public static string ErrorIncorrectNumberOfArguments(int expectedNumberArg, int actualNumberArg,
        ExprContext expression, stellaParser parser)
    {
        var sb = new StringBuilder();
        sb.AppendLine("ERROR_INCORRECT_NUMBER_OF_ARGUMENTS:");
        sb.AppendLine("вызов функции");
        sb.AppendLine($"{expression.ToStringTree(parser)}");
        sb.AppendLine("происходит с некорректным количеством аргументов");
        sb.AppendLine($"Ожидается {expectedNumberArg} аргументов, а было получено {actualNumberArg}");

        return sb.ToString();
    }

    public static string ErrorUnexpectedNumberOfParametersInLambda(int expectedNumberArg, int actualNumberArg,
        ExprContext expression, stellaParser parser)
    {
        var sb = new StringBuilder();
        sb.AppendLine("ERROR_UNEXPECTED_NUMBER_OF_PARAMETERS_IN_LAMBDA:");
        sb.AppendLine("количество параметров анонимной функции");
        sb.AppendLine($"{expression.ToStringTree(parser)}");
        sb.AppendLine("не совпадает с оижадемым количеством параметров");
        sb.AppendLine($"Ожидается {expectedNumberArg} параметров, а было получено {actualNumberArg}");

        return sb.ToString();
    }

    public static string ErrorUnexpectedDataForNullaryLabel(IType someExprData, ExprContext expression,
        stellaParser parser)
    {
        var sb = new StringBuilder();
        sb.AppendLine("ERROR_UNEXPECTED_DATA_FOR_NULLARY_LABEL:");
        sb.AppendLine("вариант");
        sb.AppendLine($"{expression.ToStringTree(parser)}");
        sb.AppendLine("содержит данные");
        sb.AppendLine($"{someExprData}");
        sb.AppendLine("хотя ожидается тег без данных");

        return sb.ToString();
    }

    public static string ErrorMissingDataForLabel(IType someTyping, ExprContext expression, stellaParser parser)
    {
        var sb = new StringBuilder();
        sb.AppendLine("ERROR_MISSING_DATA_FOR_LABEL:");
        sb.AppendLine("вариант");
        sb.AppendLine($"{expression.ToStringTree(parser)}");
        sb.AppendLine("не содержит данные");
        sb.AppendLine("хотя ожидается тег с данными");
        sb.AppendLine($"{someTyping}");

        return sb.ToString();
    }

    public static string ErrorUnexpectedNonNullaryVariantPattern(IType somePatternData, PatternContext pattern,
        stellaParser parser)
    {
        var sb = new StringBuilder();
        sb.AppendLine("ERROR_UNEXPECTED_NON_NULLARY_VARIANT_PATTERN:");
        sb.AppendLine("образец варианта");
        sb.AppendLine($"{pattern.ToStringTree(parser)}");
        sb.AppendLine("содержит тег с данными");
        sb.AppendLine($"{somePatternData}");
        sb.AppendLine("Хотя в типе разбираемого выражения этого тег указан без данных");

        return sb.ToString();
    }

    public static string ErrorUnexpectedNullaryVariantPattern(IType someTyping, PatternContext pattern,
        stellaParser parser)
    {
        var sb = new StringBuilder();
        sb.AppendLine("ERROR_UNEXPECTED_NULLARY_VARIANT_PATTERN:");
        sb.AppendLine("образец варианта");
        sb.AppendLine($"{pattern.ToStringTree(parser)}");
        sb.AppendLine("содержит тег без данных");
        sb.AppendLine("Хотя в типе разбираемого выражения этого тег указан с данными");
        sb.AppendLine($"{someTyping}");

        return sb.ToString();
    }

    public static string ErrorDuplicateVariantTypeFields(IEnumerable<string> labelNamesCollection,
        IType variant, stellaParser parser)
    {
        var labelNames = string.Join(", ", labelNamesCollection);

        var sb = new StringBuilder();
        sb.AppendLine("ERROR_DUPLICATE_VARIANT_TYPE_FIELDS:");
        sb.AppendLine("повторяющиеся метки варианта");
        sb.AppendLine($"{labelNames}");
        sb.AppendLine("в варианте типа");
        sb.AppendLine($"{variant}");

        return sb.ToString();
    }

    public static string ErrorAmbiguousReferenceType(ConstMemoryContext constMemoryContext, stellaParser parser)
    {
        var sb = new StringBuilder();
        sb.AppendLine("ERROR_AMBIGUOUS_REFERENCE_TYPE:");
        sb.AppendLine("неоднозначный тип адреса памяти");
        sb.AppendLine($"{constMemoryContext.ToStringTree(parser)}");

        return sb.ToString();
    }

    public static string ErrorNotAReference(ExprContext exprContext, stellaParser parser)
    {
        var sb = new StringBuilder();
        sb.AppendLine("ERROR_NOT_A_REFERENCE:");
        sb.AppendLine("попытка разыменовать(Deref)");
        sb.AppendLine("или присвоить значение(Assign)");
        sb.AppendLine("выражению не ссылочного типа");
        sb.AppendLine($"{exprContext.ToStringTree(parser)}");

        return sb.ToString();
    }

    public static string ErrorUnexpectedMemoryAddress(IType curType, ExprContext exprContext, stellaParser parser)
    {
        var sb = new StringBuilder();
        sb.AppendLine("ERROR_UNEXPECTED_MEMORY_ADDRESS:");
        sb.AppendLine("адрес памяти");
        sb.AppendLine($"{exprContext.ToStringTree(parser)}");
        sb.AppendLine("используется там, где ожидается тип, отличный от типа-ссылки");
        sb.AppendLine($"{curType}");

        return sb.ToString();
    }
    
    public static string ErrorAmbiguousPanicType(ExprContext exprContext, stellaParser parser)
    {
        var sb = new StringBuilder();
        sb.AppendLine("ERROR_AMBIGUOUS_PANIC_TYPE:");
        sb.AppendLine("неоднозначный тип ошибки");
        sb.AppendLine("для выражения");
        sb.AppendLine($"{exprContext.ToStringTree(parser)}");

        return sb.ToString();
    }
    
    public static string ErrorExceptionTypeNotDeclared(ExprContext exprContext, stellaParser parser)
    {
        var sb = new StringBuilder();
        sb.AppendLine("ERROR_EXCEPTION_TYPE_NOT_DECLARED:");
        sb.AppendLine("в программе используются исключения,");
        sb.AppendLine("но не объявлен их тип");
        sb.AppendLine($"{exprContext.ToStringTree(parser)}");

        return sb.ToString();
    }
    
    public static string ErrorAmbiguousThrowType(ExprContext exprContext, stellaParser parser)
    {
        var sb = new StringBuilder();
        sb.AppendLine("ERROR_AMBIGUOUS_THROW_TYPE:");
        sb.AppendLine("неоднозначный тип ошибки");
        sb.AppendLine("для выражения");
        sb.AppendLine($"{exprContext.ToStringTree(parser)}");

        return sb.ToString();
    }
}