using Antlr4.Runtime.Tree;
using static stellaParser;
using static TypeCheckerProject.ErrorsOutput;
using static TypeCheckerProject.StandardTypes;
using static TypeCheckerProject.VisitContext;
using static TypeCheckerProject.CheckTypes;

namespace TypeCheckerProject;

public record TypeChecker(stellaParser Parser) : IstellaParserVisitor<IType>
{
    private Dictionary<string, Stack<IType>> _variableTypeInfo = new();
    private readonly Stack<IType?> _expectedTypes = new();
    private IType? _exceptionType;
    private readonly HashSet<string> _extensions = new();

    public IType Visit(IParseTree tree)
    {
        throw new NotImplementedException();
    }

    public IType VisitChildren(IRuleNode node)
    {
        throw new NotImplementedException();
    }

    public IType VisitTerminal(ITerminalNode node)
    {
        throw new NotImplementedException();
    }

    public IType VisitErrorNode(IErrorNode node)
    {
        throw new NotImplementedException();
    }

    public IType VisitStart_Program(Start_ProgramContext context)
    {
        throw new NotImplementedException();
    }

    public IType VisitStart_Expr(Start_ExprContext context)
    {
        throw new NotImplementedException();
    }

    public IType VisitStart_Type(Start_TypeContext context)
    {
        throw new NotImplementedException();
    }

    public IType VisitProgram(ProgramContext context)
    {
        foreach (var extension in context._extensions)
        {
            extension.Accept(this);
        }

        foreach (var decl in context._decls)
        {
            var type = decl.Accept(this);

            if (decl is not DeclFunContext declFunContext) continue;

            AddVariableTypeInfo(declFunContext.name.Text, type, _variableTypeInfo);

            if (declFunContext.name.Text != "main") continue;

            var numberArg = (type as TypeFunction)!.ArgumentTypes.Count;
            if (numberArg != 1)
            {
                throw new Exception(ErrorIncorrectArityOfMain(numberArg));
            }

            return type;
        }

        throw new Exception(ErrorMissingMain());
    }

    public IType VisitLanguageCore(LanguageCoreContext context)
    {
        throw new NotImplementedException();
    }

    public IType VisitAnExtension(AnExtensionContext context)
    {
        var newExtensions = context._extensionNames.Select(ext => ext.Text);
        AddExtension(newExtensions, _extensions);

        return new TypeUnit();
    }

    public IType VisitDeclFun(DeclFunContext context)
    {
        var paramDecls = context._paramDecls;
        var paramNameTypeDict = paramDecls.ToDictionary(p => p.name.Text, p => p.paramType.Accept(this));
        var returnType = context.returnType.Accept(this);
        var functionType = new TypeFunction(paramNameTypeDict.Values.ToList(), returnType);

        AddVariableTypeInfo(context.name.Text, functionType, _variableTypeInfo);

        AddVariableTypeInfo(paramNameTypeDict, _variableTypeInfo);

        var nestedFunctionsDict = context._localDecls.OfType<DeclFunContext>()
            .ToDictionary(f => f.name.Text, f => f.Accept(this));

        AddVariableTypeInfo(nestedFunctionsDict, _variableTypeInfo);

        var returnExprType = VisitContextWithExpectedType(() => context.returnExpr.Accept(this),
            returnType, _expectedTypes);

        DeleteVariableTypeInfo(nestedFunctionsDict.Union(paramNameTypeDict).ToDictionary(),
            _variableTypeInfo);

        DeleteVariableTypeInfo(context.name.Text, _variableTypeInfo);

        if (!EqualsIType(returnExprType, returnType, _extensions, context.returnExpr, Parser))
        {
            ChooseUnexpectedTypeSubtype(_extensions, returnType, returnExprType, context.returnExpr, Parser);
        }

        return functionType;
    }

    public IType VisitDeclFunGeneric(DeclFunGenericContext context)
    {
        throw new NotImplementedException();
    }

    public IType VisitDeclTypeAlias(DeclTypeAliasContext context)
    {
        throw new NotImplementedException();
    }

    public IType VisitDeclExceptionType(DeclExceptionTypeContext context)
    {
        var exceptionType = context.exceptionType.Accept(this);
        _exceptionType = exceptionType;

        return exceptionType;
    }

    public IType VisitDeclExceptionVariant(DeclExceptionVariantContext context)
    {
        var name = context.name.Text;
        var variantType = context.variantType.Accept(this);

        var curVariant = (_exceptionType as TypeVariant)?.Variants;
        var newValuesVariant = new List<(string, IType?)> { (name, variantType) };
        _exceptionType = curVariant is null
            ? new TypeVariant(newValuesVariant)
            : new TypeVariant(curVariant.Concat(newValuesVariant));

        return variantType;
    }

    public IType VisitInlineAnnotation(InlineAnnotationContext context)
    {
        throw new NotImplementedException();
    }

    public IType VisitParamDecl(ParamDeclContext context)
    {
        return context.paramType.Accept(this);
    }

    public IType VisitFold(FoldContext context)
    {
        throw new NotImplementedException();
    }

    public IType VisitAdd(AddContext context)
    {
        throw new NotImplementedException();
    }

    public IType VisitIsZero(IsZeroContext context)
    {
        var typeNat = new TypeNat();
        var argumentType = VisitContextWithExpectedType(() => context.n.Accept(this), typeNat, _expectedTypes);
        if (!EqualsIType(argumentType, typeNat, _extensions))
        {
            ChooseUnexpectedTypeSubtype(_extensions, typeNat, argumentType, context, Parser);
        }

        return new TypeBool();
    }

    public IType VisitVar(VarContext context)
    {
        var variableName = context.name.Text;
        var variableType = TryGetVariableType(variableName, _variableTypeInfo);

        if (variableType is null) throw new Exception(ErrorUndefinedVariable(variableName, context.Parent, Parser));
        return variableType;
    }

    public IType VisitTypeAbstraction(TypeAbstractionContext context)
    {
        throw new NotImplementedException();
    }

    public IType VisitDivide(DivideContext context)
    {
        throw new NotImplementedException();
    }

    public IType VisitLessThan(LessThanContext context)
    {
        throw new NotImplementedException();
    }

    public IType VisitDotRecord(DotRecordContext context)
    {
        var exprType = VisitContextWithExpectedType(() => context.expr_.Accept(this), null, _expectedTypes);

        CheckNotAExpectedType(exprType as TypeRecord, () => ErrorNotARecord(exprType, context.expr_, Parser));

        var record = exprType as TypeRecord;
        var label = context.label.Text;

        foreach (var field in record!.Fields)
        {
            if (field.Item1.Equals(label))
            {
                return field.Item2;
            }
        }

        throw new Exception(ErrorUnexpectedFieldAccess(label, record, context, Parser));
    }

    public IType VisitGreaterThan(GreaterThanContext context)
    {
        throw new NotImplementedException();
    }

    public IType VisitEqual(EqualContext context)
    {
        throw new NotImplementedException();
    }

    public IType VisitThrow(ThrowContext context)
    {
        CheckNotAExpectedType(_exceptionType, () => ErrorExceptionTypeNotDeclared(context, Parser));

        var exprType = VisitContextWithExpectedType(() => context.expr_.Accept(this), _exceptionType, _expectedTypes);

        if (!EqualsIType(exprType, _exceptionType!, _extensions))
        {
            ChooseUnexpectedTypeSubtype(_extensions, _exceptionType!, exprType, context, Parser);
        }

        var expectedType = _expectedTypes.Peek();
        if (expectedType is not null) return expectedType;

        if (_extensions.Contains("#ambiguous-type-as-bottom"))
        {
            return new TypeBottom();
        }

        throw new Exception(ErrorAmbiguousThrowType(context, Parser));
    }

    public IType VisitMultiply(MultiplyContext context)
    {
        throw new NotImplementedException();
    }

    public IType VisitConstMemory(ConstMemoryContext context)
    {
        var expectedType = _expectedTypes.Peek();
        CheckNotAExpectedType(expectedType, () => ErrorAmbiguousReferenceType(context, Parser));

        var typeRef = expectedType as TypeRef;
        CheckNotAExpectedType(typeRef, () => ErrorUnexpectedMemoryAddress(expectedType!, context, Parser));

        return expectedType!;
    }

    public IType VisitList(ListContext context)
    {
        var expectedType = _expectedTypes.Peek();
        if (expectedType is not null)
        {
            if (!_extensions.Contains("#structural-subtyping") && expectedType is not TypeList)
            {
                throw new Exception(ErrorUnexpectedList(expectedType, context, Parser));
            }
        }

        var contextExpr = context._exprs;

        if (contextExpr.Count == 0)
        {
            return expectedType switch
            {
                null => _extensions.Contains("#ambiguous-type-as-bottom")
                    ? new TypeList(new TypeBottom())
                    : throw new Exception(ErrorAmbiguousListType(context, Parser)),
                _ => expectedType
            };
        }

        var firstElementInListType = VisitContextWithExpectedType(() => contextExpr.First().Accept(this),
            (expectedType as TypeList)?.ListType, _expectedTypes);

        var exprContexts = contextExpr.Skip(1);
        _expectedTypes.Push(firstElementInListType);

        foreach (var expr in exprContexts)
        {
            var exprType = expr.Accept(this);
            if (!EqualsIType(exprType, firstElementInListType, _extensions))
            {
                ChooseUnexpectedTypeSubtype(_extensions, firstElementInListType, exprType, context, Parser);
            }
        }

        _expectedTypes.Pop();

        return new TypeList(firstElementInListType);
    }

    public IType VisitTryCatch(TryCatchContext context)
    {
        var tryExprType = context.tryExpr.Accept(this);

        var curExceptionType = _exceptionType;
        CheckNotAExpectedType(curExceptionType, () => ErrorExceptionTypeNotDeclared(context, Parser));

        var patternType =
            VisitContextWithExpectedType(() => context.pat.Accept(this), curExceptionType, _expectedTypes);
        if (!EqualsIType(curExceptionType!, patternType, _extensions))
        {
            throw new Exception(ErrorUnexpectedPatternForType(curExceptionType, context.pat, Parser));
        }

        var fallbackExprType =
            VisitContextWithExpectedType(() => context.fallbackExpr.Accept(this), tryExprType, _expectedTypes);

        if (!EqualsIType(fallbackExprType, tryExprType, _extensions))
        {
            ChooseUnexpectedTypeSubtype(_extensions, tryExprType, fallbackExprType, context, Parser);
        }

        return tryExprType;
    }

    public IType VisitTryCastAs(TryCastAsContext context)
    {
        VisitContextWithExpectedType(() => context.tryExpr.Accept(this), null, _expectedTypes);

        var type = context.type_.Accept(this);
        var patternType =
            VisitContextWithExpectedType(() => context.pattern_.Accept(this), type, _expectedTypes);
        if (!EqualsIType(type, patternType, _extensions))
        {
            throw new Exception(ErrorUnexpectedPatternForType(type, context.pattern_, Parser));
        }

        var exprType = context.expr_.Accept(this);
        var fallbackExprType =
            VisitContextWithExpectedType(() => context.fallbackExpr.Accept(this), exprType, _expectedTypes);

        if (!EqualsIType(fallbackExprType, exprType, _extensions))
        {
            ChooseUnexpectedTypeSubtype(_extensions, fallbackExprType, exprType, context, Parser);
        }

        return fallbackExprType;
    }

    public IType VisitHead(HeadContext context)
    {
        var listType = VisitContextWithExpectedType(() => context.list.Accept(this), null, _expectedTypes);

        CheckNotAExpectedType(listType as TypeList, () => ErrorNotAList(listType, context, Parser));

        return (listType as TypeList)!.ListType;
    }

    public IType VisitTerminatingSemicolon(TerminatingSemicolonContext context)
    {
        return context.expr_.Accept(this);
    }

    public IType VisitNotEqual(NotEqualContext context)
    {
        throw new NotImplementedException();
    }

    public IType VisitConstUnit(ConstUnitContext context)
    {
        return new TypeUnit();
    }

    public IType VisitSequence(SequenceContext context)
    {
        var expectedType = new TypeUnit();
        var leftType = VisitContextWithExpectedType(() => context.expr1.Accept(this), expectedType, _expectedTypes);
        if (!EqualsIType(leftType, expectedType, _extensions))
        {
            ChooseUnexpectedTypeSubtype(_extensions, expectedType, leftType, context, Parser);
        }

        return context.expr2.Accept(this);
    }

    public IType VisitConstFalse(ConstFalseContext context)
    {
        return new TypeBool();
    }

    public IType VisitAbstraction(AbstractionContext context)
    {
        var expectedType = _expectedTypes.Peek();
        if (expectedType is not null && expectedType is not TypeFunction)
        {
            throw new Exception(ErrorUnexpectedLambda(expectedType, context, Parser));
        }

        var argumentTypes = (expectedType as TypeFunction)?.ArgumentTypes;

        var paramDecls = context._paramDecls;
        if (argumentTypes is not null && argumentTypes.Count != paramDecls.Count)
        {
            throw new Exception(
                ErrorUnexpectedNumberOfParametersInLambda(argumentTypes.Count, paramDecls.Count, context, Parser));
        }

        var paramNameTypeDict = new Dictionary<string, IType>();

        for (var i = 0; i < paramDecls?.Count; i++)
        {
            var paramDeclName = paramDecls[i].name.Text;
            var expectedParamType = argumentTypes?[i];
            var index = i;
            var paramType = VisitContextWithExpectedType(() => paramDecls[index].Accept(this), expectedParamType,
                _expectedTypes);

            if (expectedParamType is not null && !_extensions.Contains("#structural-subtyping"))
            {
                if (!EqualsIType(expectedParamType, paramType, _extensions))
                {
                    throw new Exception(ErrorUnexpectedTypeForParameter(paramType, expectedParamType,
                        context.returnExpr,
                        Parser));
                }
            }

            paramNameTypeDict.Add(paramDeclName, paramType);
        }

        foreach (var (paramName, paramIType) in paramNameTypeDict)
        {
            AddVariableTypeInfo(paramName, paramIType, _variableTypeInfo);
        }

        var returnType = VisitContextWithExpectedType(() => context.returnExpr.Accept(this),
            (expectedType as TypeFunction)?.ReturnType, _expectedTypes);
        var functionType = new TypeFunction(paramNameTypeDict.Values.ToList(), returnType);

        foreach (var (paramName, _) in paramNameTypeDict)
        {
            DeleteVariableTypeInfo(paramName, _variableTypeInfo);
        }

        return functionType;
    }

    public IType VisitConstInt(ConstIntContext context)
    {
        return new TypeNat();
    }

    public IType VisitVariant(VariantContext context)
    {
        var expectedType = _expectedTypes.Peek();

        if (expectedType is null)
        {
            if (!_extensions.Contains("#structural-subtyping"))
                throw new Exception(ErrorAmbiguousVariantType(context, Parser));

            var variants = new List<(string, IType?)>
            {
                (
                    context.label.Text,
                    context.rhs is not null
                        ? VisitContextWithExpectedType(() => context.rhs.Accept(this), null, _expectedTypes)
                        : null
                )
            };

            return new TypeVariant(variants);
        }

        CheckNotAExpectedType(expectedType as TypeVariant,
            () => ErrorUnexpectedVariant(expectedType, context, Parser));


        var variant = expectedType as TypeVariant;

        var label = context.label.Text;
        var (expectedVariantLabel, expectedVariantType) = variant!.Variants.FirstOrDefault(v => v.Item1.Equals(label));

        if (expectedVariantLabel is null)
        {
            throw new Exception(ErrorUnexpectedVariantLabel(label, variant, context, Parser));
        }

        if (expectedVariantType is null && context.rhs is not null)
        {
            throw new Exception(ErrorUnexpectedDataForNullaryLabel(variant, context, Parser));
        }

        if (expectedVariantType is not null && context.rhs is null)
        {
            throw new Exception(ErrorMissingDataForLabel(variant, context, Parser));
        }

        if (context.rhs is null) return variant;
        var variantType =
            VisitContextWithExpectedType(() => context.rhs.Accept(this), expectedVariantType, _expectedTypes);

        if (!EqualsIType(variantType, expectedVariantType!, _extensions))
        {
            ChooseUnexpectedTypeSubtype(_extensions, expectedVariantType!, variantType, context, Parser);
        }

        return variant;
    }

    public IType VisitConstTrue(ConstTrueContext context)
    {
        return new TypeBool();
    }

    public IType VisitSubtract(SubtractContext context)
    {
        throw new NotImplementedException();
    }

    public IType VisitTypeCast(TypeCastContext context)
    {
        VisitContextWithExpectedType(() => context.expr_.Accept(this), null, _expectedTypes);
        return context.stellatype().Accept(this);
    }

    public IType VisitIf(IfContext context)
    {
        var typeBool = new TypeBool();
        var conditionType =
            VisitContextWithExpectedType(() => context.condition.Accept(this), typeBool, _expectedTypes);

        if (!EqualsIType(conditionType, typeBool, _extensions))
        {
            ChooseUnexpectedTypeSubtype(_extensions, typeBool, conditionType, context.condition, Parser);
        }

        var thenExprType = context.thenExpr.Accept(this);
        var elseExprType = context.elseExpr.Accept(this);
        if (!EqualsIType(elseExprType, thenExprType, _extensions))
        {
            ChooseUnexpectedTypeSubtype(_extensions, thenExprType, elseExprType, context, Parser);
        }

        return thenExprType;
    }

    public IType VisitApplication(ApplicationContext context)
    {
        var functionType = VisitContextWithExpectedType(() => context.fun.Accept(this), null, _expectedTypes);

        CheckNotAExpectedType(functionType as TypeFunction, () => ErrorNotAFunction(functionType, context.fun, Parser));

        var function = functionType as TypeFunction;
        var argumentTypes = function!.ArgumentTypes;

        if (argumentTypes.Count != context._args.Count)
        {
            throw new Exception(ErrorIncorrectNumberOfArguments(argumentTypes.Count, context._args.Count, context,
                Parser));
        }

        for (var index = 0; index < argumentTypes.Count; index++)
        {
            var currentArgType = argumentTypes[index];
            var contextArgsIndex = index;
            var actualType = VisitContextWithExpectedType(() => context._args[contextArgsIndex].Accept(this),
                currentArgType, _expectedTypes);
            if (!EqualsIType(actualType, currentArgType, _extensions))
            {
                ChooseUnexpectedTypeSubtype(_extensions, currentArgType, actualType, context, Parser);
            }
        }

        return function.ReturnType;
    }

    public IType VisitDeref(DerefContext context)
    {
        var curExpectedType = _expectedTypes.Peek();
        var exprExpectedType = curExpectedType is not null
            ? new TypeRef(curExpectedType)
            : null;

        var expressionType =
            VisitContextWithExpectedType(() => context.expr_.Accept(this), exprExpectedType, _expectedTypes);

        var typeRef = expressionType as TypeRef;
        CheckNotAExpectedType(typeRef, () => ErrorNotAReference(context, Parser));

        return typeRef!.InternalType;
    }

    public IType VisitIsEmpty(IsEmptyContext context)
    {
        var argumentType = VisitContextWithExpectedType(() => context.list.Accept(this), null, _expectedTypes);

        CheckNotAExpectedType(argumentType as TypeList, () => ErrorNotAList(argumentType, context, Parser));

        return new TypeBool();
    }

    public IType VisitPanic(PanicContext context)
    {
        var expectedType = _expectedTypes.Peek();

        if (expectedType is not null) return expectedType;

        if (_extensions.Contains("#ambiguous-type-as-bottom"))
        {
            return new TypeBottom();
        }

        throw new Exception(ErrorAmbiguousPanicType(context, Parser));
    }

    public IType VisitLessThanOrEqual(LessThanOrEqualContext context)
    {
        throw new NotImplementedException();
    }

    public IType VisitSucc(SuccContext context)
    {
        var typeNat = new TypeNat();
        var argumentType = VisitContextWithExpectedType(() => context.n.Accept(this), typeNat, _expectedTypes);

        if (!EqualsIType(argumentType, typeNat, _extensions))
        {
            ChooseUnexpectedTypeSubtype(_extensions, typeNat, argumentType, context, Parser);
        }

        return typeNat;
    }

    public IType VisitInl(InlContext context)
    {
        var expectedType = _expectedTypes.Peek();
        if (expectedType is null && !_extensions.Contains("#ambiguous-type-as-bottom"))
        {
            throw new Exception(ErrorAmbiguousSumType(context, Parser));
        }

        if (expectedType is not null)
        {
            CheckNotAExpectedType(expectedType as TypeSum,
                () => ErrorUnexpectedInjection(expectedType, context, Parser));
        }

        var inlType = VisitContextWithExpectedType(() => context.expr_.Accept(this), (expectedType as TypeSum)?.Inl,
            _expectedTypes);
        return new TypeSum(inlType, (expectedType as TypeSum)?.Inr ?? new TypeBottom());
    }

    public IType VisitGreaterThanOrEqual(GreaterThanOrEqualContext context)
    {
        throw new NotImplementedException();
    }

    public IType VisitInr(InrContext context)
    {
        var expectedType = _expectedTypes.Peek();

        if (expectedType is null && !_extensions.Contains("#ambiguous-type-as-bottom"))
        {
            throw new Exception(ErrorAmbiguousSumType(context, Parser));
        }

        if (expectedType is not null)
        {
            CheckNotAExpectedType(expectedType as TypeSum,
                () => ErrorUnexpectedInjection(expectedType, context, Parser));
        }

        var inrType = VisitContextWithExpectedType(() => context.expr_.Accept(this), (expectedType as TypeSum)?.Inr,
            _expectedTypes);
        return new TypeSum((expectedType as TypeSum)?.Inl ?? new TypeBottom(), inrType);
    }

    public IType VisitMatch(MatchContext context)
    {
        var expectedType = _expectedTypes.Peek();
        var exprType = VisitContextWithExpectedType(() => context.expr_.Accept(this), null, _expectedTypes);
        var cases = context._cases;

        if (cases.Count == 0)
        {
            throw new Exception(ErrorIllegalEmptyMatching(context, Parser));
        }

        var resultType = expectedType;

        foreach (var curCase in cases)
        {
            var patternType =
                VisitContextWithExpectedType(() => curCase.pattern_.Accept(this), exprType, _expectedTypes);
            if (!EqualsIType(exprType, patternType, _extensions))
            {
                ChooseUnexpectedTypeSubtype(_extensions, patternType, exprType, context, Parser);
            }

            resultType =
                VisitContextWithExpectedType(() => curCase.expr_.Accept(this), expectedType ?? null, _expectedTypes);
        }

        var patterns = cases.Select(it => it.pattern_).ToList();
        if (!CheckExhaustiveMatchPatterns(exprType, patterns))
        {
            throw new Exception(ErrorNonexhaustiveMatchPatterns(exprType, context, Parser));
        }

        return resultType!;
    }

    public IType VisitLogicNot(LogicNotContext context)
    {
        throw new NotImplementedException();
    }

    public IType VisitParenthesisedExpr(ParenthesisedExprContext context)
    {
        return context.expr_.Accept(this);
    }

    public IType VisitTail(TailContext context)
    {
        var listType = VisitContextWithExpectedType(() => context.list.Accept(this), null, _expectedTypes);

        CheckNotAExpectedType(listType as TypeList, () => ErrorNotAList(listType, context, Parser));

        return listType;
    }

    public IType VisitRecord(RecordContext context)
    {
        var expectedType = _expectedTypes.Peek();

        if (expectedType is not null && expectedType is not TypeRecord)
        {
            throw new Exception(ErrorUnexpectedRecord(expectedType, context, Parser));
        }

        var fields = context._bindings.Select(field =>
        {
            var fieldName = field.name.Text;
            var expectedFieldType = (expectedType as TypeRecord)?.Fields.FirstOrDefault(f => f.Item1.Equals(fieldName))
                .Item2;
            var currentFieldType =
                VisitContextWithExpectedType(() => field.rhs.Accept(this), expectedFieldType, _expectedTypes);
            return (fieldName, currentFieldType);
        });

        var valueTuples = fields as (string fieldName, IType currentFieldType)[] ?? fields.ToArray();
        var currentField = new TypeRecord(valueTuples);

        if (expectedType is not null && _extensions.Contains("#structural-subtyping"))
        {
            if (EqualsIType(currentField, expectedType, _extensions, context, Parser))
            {
                return currentField;
            }

            if (expectedType is TypeRecord expectedRecord)
            {
                var enumerableUnexpectedFields = valueTuples.Select(f => f.fieldName)
                    .Except(expectedRecord.Fields.Select(f => f.Item1));
                var unexpectedFields = enumerableUnexpectedFields as string[] ?? enumerableUnexpectedFields.ToArray();
                if (unexpectedFields.Length != 0)
                {
                    throw new Exception(ErrorUnexpectedRecordFields(expectedRecord, currentField, context,
                        unexpectedFields, Parser));
                }

                var enumerableMissingFields = expectedRecord.Fields.Select(f => f.Item1)
                    .Except(valueTuples.Select(f => f.fieldName));
                var missingFields = enumerableMissingFields as string[] ?? enumerableMissingFields.ToArray();
                if (missingFields.Length != 0)
                {
                    throw new Exception(ErrorMissingRecordFields(expectedRecord, currentField, context,
                        missingFields, Parser));
                }
            }

            ChooseUnexpectedTypeSubtype(_extensions, expectedType, currentField, context, Parser);
        }

        var expectedAsRecord = expectedType as TypeRecord;

        if (expectedAsRecord == null || !expectedAsRecord.Fields.Any()) return expectedAsRecord ?? currentField;


        var enumerable = ExceptRecords(valueTuples, expectedAsRecord.Fields, _extensions);
        var tuples = enumerable as (string, IType)[] ?? enumerable.ToArray();
        if (tuples.Length != 0)
        {
            throw new Exception(
                ErrorUnexpectedRecordFields(expectedAsRecord, currentField, context, tuples, Parser));
        }

        enumerable = ExceptRecords(expectedAsRecord.Fields, valueTuples, _extensions);
        tuples = enumerable as (string, IType)[] ?? enumerable.ToArray();
        if (tuples.Length != 0)
        {
            throw new Exception(
                ErrorMissingRecordFields(expectedAsRecord, currentField, context, tuples, Parser));
        }

        return expectedAsRecord;
    }

    public IType VisitLogicAnd(LogicAndContext context)
    {
        throw new NotImplementedException();
    }

    public IType VisitTypeApplication(TypeApplicationContext context)
    {
        throw new NotImplementedException();
    }

    public IType VisitLetRec(LetRecContext context)
    {
        var expectedType = _expectedTypes.Peek();

        foreach (var patternBinding in context._patternBindings)
        {
            var patternType = VisitContextWithExpectedType(() => patternBinding.pat.Accept(this), null, _expectedTypes);
            VisitContextWithExpectedType(() => patternBinding.expr().Accept(this), patternType, _expectedTypes);
        }

        var exprType = VisitContextWithExpectedType(() => context.expr().Accept(this), expectedType, _expectedTypes);
        return exprType;
    }

    public IType VisitLogicOr(LogicOrContext context)
    {
        throw new NotImplementedException();
    }

    public IType VisitTryWith(TryWithContext context)
    {
        var tryExprType = context.tryExpr.Accept(this);
        var fallbackExprType =
            VisitContextWithExpectedType(() => context.fallbackExpr.Accept(this), tryExprType, _expectedTypes);

        if (!EqualsIType(fallbackExprType, tryExprType, _extensions))
        {
            ChooseUnexpectedTypeSubtype(_extensions, tryExprType, fallbackExprType, context, Parser);
        }

        return tryExprType;
    }

    public IType VisitPred(PredContext context)
    {
        var typeNat = new TypeNat();
        var nType = VisitContextWithExpectedType(() => context.n.Accept(this), typeNat, _expectedTypes);

        if (!EqualsIType(nType, typeNat, _extensions))
        {
            ChooseUnexpectedTypeSubtype(_extensions, typeNat, nType, context, Parser);
        }

        return typeNat;
    }

    public IType VisitTypeAsc(TypeAscContext context)
    {
        var expectedType = context.type_.Accept(this);
        var exprType = VisitContextWithExpectedType(() => context.expr_.Accept(this), expectedType, _expectedTypes);

        if (!EqualsIType(exprType, expectedType, _extensions))
        {
            ChooseUnexpectedTypeSubtype(_extensions, expectedType, exprType, context, Parser);
        }

        return expectedType;
    }

    public IType VisitNatRec(NatRecContext context)
    {
        var typeNat = new TypeNat();
        var nType = VisitContextWithExpectedType(() => context.n.Accept(this), typeNat, _expectedTypes);

        if (!EqualsIType(nType, typeNat, _extensions))
        {
            ChooseUnexpectedTypeSubtype(_extensions, typeNat, nType, context, Parser);
        }

        var initialType = context.initial.Accept(this);
        var typeFunction = new TypeFunction(new List<IType> { typeNat },
            new TypeFunction(new List<IType> { initialType }, initialType));
        var stepType = VisitContextWithExpectedType(() => context.step.Accept(this), typeFunction, _expectedTypes);

        if (!EqualsIType(stepType, typeFunction, _extensions))
        {
            ChooseUnexpectedTypeSubtype(_extensions, typeFunction, stepType, context, Parser);
        }

        return initialType;
    }

    public IType VisitUnfold(UnfoldContext context)
    {
        throw new NotImplementedException();
    }

    public IType VisitRef(RefContext context)
    {
        var expectedType = _expectedTypes.Peek();

        if (expectedType is not null && !_extensions.Contains("#structural-subtyping"))
        {
            if (expectedType is not TypeRef)
            {
                throw new Exception(ErrorUnexpectedReference(context, Parser));
            }
        }

        var internalType = VisitContextWithExpectedType(() => context.expr_.Accept(this),
            (expectedType as TypeRef)?.InternalType, _expectedTypes);

        return new TypeRef(internalType);
    }

    public IType VisitDotTuple(DotTupleContext context)
    {
        var tupleType = VisitContextWithExpectedType(() => context.expr_.Accept(this), null, _expectedTypes);

        CheckNotAExpectedType(tupleType as TypeTuple, () => ErrorNotATuple(tupleType, context, Parser));

        var tuple = tupleType as TypeTuple;
        var index = int.Parse(context.index.Text) - 1;

        if (tuple!.TupleTypes.Count <= index)
        {
            throw new Exception(ErrorTupleIndexOfBounds(index, context, Parser));
        }

        return tuple.TupleTypes[index];
    }

    public IType VisitFix(FixContext context)
    {
        var curExpectedType = _expectedTypes.Peek();
        var exprExpectedType = curExpectedType is not null
            ? new TypeFunction(new List<IType> { curExpectedType }, curExpectedType)
            : null;

        var exprType = VisitContextWithExpectedType(() => context.expr_.Accept(this), exprExpectedType, _expectedTypes);

        CheckNotAExpectedType(exprType as TypeFunction, () => ErrorNotAFunction(exprType, context.expr_, Parser));

        if ((exprType as TypeFunction)!.ArgumentTypes.Count != 1)
        {
            throw new Exception(ErrorNotAFunction(exprType, context.expr_, Parser));
        }

        var exprTypeFunction = exprType as TypeFunction;
        var expectedType = new TypeFunction(exprTypeFunction!.ArgumentTypes, exprTypeFunction.ArgumentTypes.First());
        if (!EqualsIType(exprType, expectedType, _extensions))
        {
            ChooseUnexpectedTypeSubtype(_extensions, expectedType, exprType, context, Parser);
        }

        return expectedType.ArgumentTypes.First();
    }

    public IType VisitLet(LetContext context)
    {
        var expectedType = _expectedTypes.Peek();
        var savedVariableTypeInfo = new Dictionary<string, Stack<IType>>();

        foreach (var (variable, types) in _variableTypeInfo)
        {
            foreach (var type in types)
            {
                if (!savedVariableTypeInfo.ContainsKey(variable))
                {
                    savedVariableTypeInfo[variable] = new Stack<IType>();
                }

                savedVariableTypeInfo[variable].Push(type);
            }
        }

        foreach (var patternBinding in context._patternBindings)
        {
            var exprType = VisitContextWithExpectedType(() => patternBinding.expr().Accept(this), null, _expectedTypes);
            VisitContextWithExpectedType(() => patternBinding.pat.Accept(this), exprType, _expectedTypes);
        }

        var contextExprType =
            VisitContextWithExpectedType(() => context.expr().Accept(this), expectedType ?? null, _expectedTypes);

        _variableTypeInfo = savedVariableTypeInfo;

        return contextExprType;
    }

    public IType VisitAssign(AssignContext context)
    {
        var lhsType = VisitContextWithExpectedType(() => context.lhs.Accept(this), null, _expectedTypes);

        var lhsAsRef = lhsType as TypeRef;
        CheckNotAExpectedType(lhsAsRef, () => ErrorNotAReference(context, Parser));

        var rhsType =
            VisitContextWithExpectedType(() => context.rhs.Accept(this), lhsAsRef!.InternalType, _expectedTypes);
        if (!EqualsIType(rhsType, lhsAsRef.InternalType, _extensions))
        {
            ChooseUnexpectedTypeSubtype(_extensions, lhsAsRef.InternalType, rhsType, context, Parser);
        }

        return new TypeUnit();
    }

    public IType VisitTuple(TupleContext context)
    {
        var expectedType = _expectedTypes.Peek();
        if (expectedType is not null && expectedType is not TypeTuple)
        {
            throw new Exception(ErrorUnexpectedTuple(expectedType, context, Parser));
        }

        var tuple = expectedType as TypeTuple;

        if (tuple is not null && tuple.TupleTypes.Count != context._exprs.Count)
        {
            throw new Exception(ErrorUnexpectedTupleLength(tuple, context, Parser));
        }

        var tupleTypes = context._exprs.Select((expr, i) =>
        {
            var expectedTupleType = tuple?.TupleTypes[i];
            var currentTupleType =
                VisitContextWithExpectedType(() => expr.Accept(this), expectedTupleType, _expectedTypes);
            return currentTupleType;
        }).ToList();

        return new TypeTuple(tupleTypes);
    }

    public IType VisitConsList(ConsListContext context)
    {
        var expectedType = _expectedTypes.Peek();

        if (expectedType is not null)
        {
            if (!_extensions.Contains("#structural-subtyping") && expectedType is not TypeList)
            {
                throw new Exception(ErrorUnexpectedList(expectedType, context, Parser));
            }
        }

        var list = expectedType as TypeList;

        var headType = VisitContextWithExpectedType(() => context.head.Accept(this), list?.ListType, _expectedTypes);
        var expectedListType = new TypeList(headType);
        var tailType = VisitContextWithExpectedType(() => context.tail.Accept(this), expectedListType, _expectedTypes);

        if (!EqualsIType(tailType, expectedListType, _extensions))
        {
            ChooseUnexpectedTypeSubtype(_extensions, expectedListType, tailType, context, Parser);
        }

        return expectedListType;
    }

    public IType VisitPatternBinding(PatternBindingContext context)
    {
        throw new NotImplementedException();
    }

    public IType VisitBinding(BindingContext context)
    {
        throw new NotImplementedException();
    }

    public IType VisitMatchCase(MatchCaseContext context)
    {
        throw new NotImplementedException();
    }

    public IType VisitPatternVariant(PatternVariantContext context)
    {
        var expectedType = _expectedTypes.Peek();

        CheckNotAExpectedType(expectedType as TypeVariant,
            () => ErrorUnexpectedPatternForType(expectedType, context, Parser));

        var variant = expectedType as TypeVariant;
        var label = context.label.Text;

        var duplicateKeys = variant!.Variants.GroupBy(item => item.Item1)
            .Where(group => group.Count() > 1)
            .Select(group => group.Key);
        var labelNamesCollection = duplicateKeys as string[] ?? duplicateKeys.ToArray();
        if (labelNamesCollection.Length != 0)
        {
            throw new Exception(ErrorDuplicateVariantTypeFields(labelNamesCollection, variant, Parser));
        }

        var variantsDict = variant.Variants.ToDictionary(item => item.Item1, item => item.Item2);

        if (!variantsDict.ContainsKey(label))
        {
            throw new Exception(ErrorUnexpectedPatternForType(variant, context, Parser));
        }

        var variantLabelType = variantsDict[label];
        if (variantLabelType is not null && context.pattern_ is null)
        {
            throw new Exception(ErrorUnexpectedNullaryVariantPattern(variant, context, Parser));
        }

        if (variantLabelType is null && context.pattern_ is not null)
        {
            throw new Exception(ErrorUnexpectedNonNullaryVariantPattern(variant, context, Parser));
        }

        if (variantLabelType is not null)
        {
            VisitContextWithExpectedType(() => context.pattern_!.Accept(this), variantLabelType, _expectedTypes);
        }

        return variant;
    }

    public IType VisitPatternAsc(PatternAscContext context)
    {
        var expectedType = context.stellatype().Accept(this);
        var patternType =
            VisitContextWithExpectedType(() => context.pattern_.Accept(this), expectedType, _expectedTypes);

        if (!EqualsIType(expectedType, patternType, _extensions))
        {
            throw new Exception(ErrorUnexpectedPatternForType(patternType, context, Parser));
        }

        return patternType;
    }

    public IType VisitPatternInl(PatternInlContext context)
    {
        var expectedType = _expectedTypes.Peek();

        CheckNotAExpectedType(expectedType as TypeSum,
            () => ErrorUnexpectedPatternForType(expectedType!, context, Parser));

        var typeSum = expectedType as TypeSum;
        VisitContextWithExpectedType(() => context.pattern_.Accept(this), typeSum!.Inl, _expectedTypes);

        return typeSum;
    }

    public IType VisitPatternInr(PatternInrContext context)
    {
        var expectedType = _expectedTypes.Peek();

        CheckNotAExpectedType(expectedType as TypeSum,
            () => ErrorUnexpectedPatternForType(expectedType!, context, Parser));

        var typeSum = expectedType as TypeSum;
        VisitContextWithExpectedType(() => context.pattern_.Accept(this), typeSum!.Inr, _expectedTypes);

        return typeSum;
    }

    public IType VisitPatternTuple(PatternTupleContext context)
    {
        var expectedType = _expectedTypes.Peek();
        if (expectedType is not null && expectedType is not TypeTuple)
        {
            throw new Exception(ErrorUnexpectedPatternForType(expectedType, context, Parser));
        }

        var tuple = expectedType as TypeTuple;

        if (tuple is not null && tuple.TupleTypes.Count != context._patterns.Count)
        {
            throw new Exception(ErrorUnexpectedPatternForType(tuple, context, Parser));
        }

        var tupleTypes = context._patterns.Select((expr, i) =>
        {
            var expectedTupleType = tuple?.TupleTypes[i];
            var currentTupleType =
                VisitContextWithExpectedType(() => expr.Accept(this), expectedTupleType, _expectedTypes);
            return currentTupleType;
        }).ToList();

        return new TypeTuple(tupleTypes);
    }

    public IType VisitPatternRecord(PatternRecordContext context)
    {
        var expectedType = _expectedTypes.Peek();
        if (expectedType is not null && expectedType is not TypeRecord)
        {
            throw new Exception(ErrorUnexpectedPatternForType(expectedType, context, Parser));
        }

        var record = expectedType as TypeRecord;

        if (context._patterns.Count != record?.Fields.Count())
        {
            throw new Exception(ErrorUnexpectedPatternForType(expectedType, context, Parser));
        }

        foreach (var pattern in context._patterns)
        {
            var labelName = pattern.label.Text;

            if (!record.Fields.ToDictionary().ContainsKey(labelName))
            {
                throw new Exception(ErrorUnexpectedPatternForType(expectedType, context, Parser));
            }

            var expectedFieldType = record.Fields.ToDictionary()[labelName];

            var labelType =
                VisitContextWithExpectedType(() => pattern.Accept(this), expectedFieldType, _expectedTypes);

            if (!EqualsIType(labelType, expectedFieldType, _extensions))
            {
                throw new Exception(ErrorUnexpectedPatternForType(expectedType, context, Parser));
            }
        }

        return record;
    }

    public IType VisitPatternList(PatternListContext context)
    {
        var expectedType = _expectedTypes.Peek();
        if (expectedType is not null && expectedType is not TypeList)
        {
            throw new Exception(ErrorUnexpectedPatternForType(expectedType, context, Parser));
        }

        var expectedListType = (expectedType as TypeList)!.ListType;

        foreach (var pattern in context._patterns)
        {
            var curListType =
                VisitContextWithExpectedType(() => pattern.Accept(this), expectedListType, _expectedTypes);
            if (!EqualsIType(expectedListType, curListType, _extensions))
            {
                throw new Exception(ErrorUnexpectedPatternForType(curListType, context, Parser));
            }
        }

        return new TypeList(expectedListType);
    }

    public IType VisitPatternCons(PatternConsContext context)
    {
        var expectedType = _expectedTypes.Peek();
        if (expectedType is not null && expectedType is not TypeList)
        {
            throw new Exception(ErrorUnexpectedPatternForType(expectedType, context, Parser));
        }

        var list = expectedType as TypeList;

        var headType = VisitContextWithExpectedType(() => context.head.Accept(this), list?.ListType, _expectedTypes);
        var expectedListType = new TypeList(headType);
        var tailType = VisitContextWithExpectedType(() => context.tail.Accept(this), expectedListType, _expectedTypes);

        if (!EqualsIType(expectedListType, tailType, _extensions))
        {
            throw new Exception(ErrorUnexpectedPatternForType(tailType, context, Parser));
        }

        return expectedListType;
    }

    public IType VisitPatternFalse(PatternFalseContext context)
    {
        return new TypeBool();
    }

    public IType VisitPatternTrue(PatternTrueContext context)
    {
        return new TypeBool();
    }

    public IType VisitPatternUnit(PatternUnitContext context)
    {
        return new TypeUnit();
    }

    public IType VisitPatternCastAs(PatternCastAsContext context)
    {
        var type = context.type_.Accept(this);
        var patternType =
            VisitContextWithExpectedType(() => context.pattern_.Accept(this), type, _expectedTypes);
        if (!EqualsIType(type, patternType, _extensions))
        {
            throw new Exception(ErrorUnexpectedPatternForType(type, context.pattern_, Parser));
        }

        return patternType;
    }

    public IType VisitPatternInt(PatternIntContext context)
    {
        return new TypeNat();
    }

    public IType VisitPatternSucc(PatternSuccContext context)
    {
        var expectedType = _expectedTypes.Peek()!;

        CheckNotAExpectedType(expectedType as TypeNat,
            () => ErrorUnexpectedPatternForType(expectedType, context, Parser));

        var argumentType =
            VisitContextWithExpectedType(() => context.pattern_.Accept(this), expectedType, _expectedTypes);

        return argumentType;
    }

    public IType VisitPatternVar(PatternVarContext context)
    {
        var variableName = context.name.Text;
        var variableType = _expectedTypes.Peek();

        CheckNotAExpectedType(variableType, () => ErrorAmbiguousPatternType(context, Parser));

        AddVariableTypeInfo(variableName, variableType!, _variableTypeInfo);
        return variableType!;
    }

    public IType VisitParenthesisedPattern(ParenthesisedPatternContext context)
    {
        return context.pattern_.Accept(this);
    }

    public IType VisitLabelledPattern(LabelledPatternContext context)
    {
        var expectedType = _expectedTypes.Peek()!;
        var typeRecord =
            VisitContextWithExpectedType(() => context.pattern_.Accept(this), expectedType, _expectedTypes);

        return typeRecord;
    }

    public IType VisitTypeTuple(TypeTupleContext context)
    {
        var typesList = context._types.Select(typeContext => typeContext.Accept(this)).ToList();
        return new TypeTuple(typesList);
    }

    public IType VisitTypeTop(TypeTopContext context)
    {
        return new TypeTop();
    }

    public IType VisitTypeBool(TypeBoolContext context)
    {
        return new TypeBool();
    }

    public IType VisitTypeRef(TypeRefContext context)
    {
        return new TypeRef(context.type_.Accept(this));
    }

    public IType VisitTypeRec(TypeRecContext context)
    {
        throw new NotImplementedException();
    }

    public IType VisitTypeAuto(TypeAutoContext context)
    {
        throw new NotImplementedException();
    }

    public IType VisitTypeSum(TypeSumContext context)
    {
        return new TypeSum(context.left.Accept(this), context.right.Accept(this));
    }

    public IType VisitTypeVar(TypeVarContext context)
    {
        throw new NotImplementedException();
    }

    public IType VisitTypeVariant(TypeVariantContext context)
    {
        var variants = context._fieldTypes
            .Select(field =>
            {
                var variantLabel = field.label.Text;
                var variantType = field.stellatype()?.Accept(this);
                return (variantLabel, variantType);
            });

        return new TypeVariant(variants);
    }

    public IType VisitTypeUnit(TypeUnitContext context)
    {
        return new TypeUnit();
    }

    public IType VisitTypeNat(TypeNatContext context)
    {
        return new TypeNat();
    }

    public IType VisitTypeBottom(TypeBottomContext context)
    {
        return new TypeBottom();
    }

    public IType VisitTypeParens(TypeParensContext context)
    {
        return context.stellatype().Accept(this);
    }

    public IType VisitTypeFun(TypeFunContext context)
    {
        return new TypeFunction(context._paramTypes.Select(p => p.Accept(this)).ToList(),
            context.returnType.Accept(this));
    }

    public IType VisitTypeForAll(TypeForAllContext context)
    {
        throw new NotImplementedException();
    }

    public IType VisitTypeRecord(TypeRecordContext context)
    {
        var fields = context._fieldTypes
            .Select(field =>
            {
                var fieldLabel = field.label.Text;
                var fieldType = field.stellatype().Accept(this);
                return (fieldLabel, fieldType);
            });

        return new TypeRecord(fields);
    }

    public IType VisitTypeList(TypeListContext context)
    {
        return new TypeList(context.stellatype().Accept(this));
    }

    public IType VisitRecordFieldType(RecordFieldTypeContext context)
    {
        throw new NotImplementedException();
    }

    public IType VisitVariantFieldType(VariantFieldTypeContext context)
    {
        throw new NotImplementedException();
    }
}