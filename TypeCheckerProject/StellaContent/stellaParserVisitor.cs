//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.9.2
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from D:\RiderProjects\TypeCheckerProject\TypeCheckerProject\StellaContent\stellaParser.g4 by ANTLR 4.9.2

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using IToken = Antlr4.Runtime.IToken;

/// <summary>
/// This interface defines a complete generic visitor for a parse tree produced
/// by <see cref="stellaParser"/>.
/// </summary>
/// <typeparam name="Result">The return type of the visit operation.</typeparam>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.9.2")]
[System.CLSCompliant(false)]
public interface IstellaParserVisitor<Result> : IParseTreeVisitor<Result> {
	/// <summary>
	/// Visit a parse tree produced by <see cref="stellaParser.start_Program"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStart_Program([NotNull] stellaParser.Start_ProgramContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="stellaParser.start_Expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStart_Expr([NotNull] stellaParser.Start_ExprContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="stellaParser.start_Type"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStart_Type([NotNull] stellaParser.Start_TypeContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="stellaParser.program"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitProgram([NotNull] stellaParser.ProgramContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>LanguageCore</c>
	/// labeled alternative in <see cref="stellaParser.languageDecl"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLanguageCore([NotNull] stellaParser.LanguageCoreContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>AnExtension</c>
	/// labeled alternative in <see cref="stellaParser.extension"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAnExtension([NotNull] stellaParser.AnExtensionContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>DeclFun</c>
	/// labeled alternative in <see cref="stellaParser.decl"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDeclFun([NotNull] stellaParser.DeclFunContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>DeclFunGeneric</c>
	/// labeled alternative in <see cref="stellaParser.decl"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDeclFunGeneric([NotNull] stellaParser.DeclFunGenericContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>DeclTypeAlias</c>
	/// labeled alternative in <see cref="stellaParser.decl"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDeclTypeAlias([NotNull] stellaParser.DeclTypeAliasContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>DeclExceptionType</c>
	/// labeled alternative in <see cref="stellaParser.decl"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDeclExceptionType([NotNull] stellaParser.DeclExceptionTypeContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>DeclExceptionVariant</c>
	/// labeled alternative in <see cref="stellaParser.decl"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDeclExceptionVariant([NotNull] stellaParser.DeclExceptionVariantContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>InlineAnnotation</c>
	/// labeled alternative in <see cref="stellaParser.annotation"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitInlineAnnotation([NotNull] stellaParser.InlineAnnotationContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="stellaParser.paramDecl"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitParamDecl([NotNull] stellaParser.ParamDeclContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>Fold</c>
	/// labeled alternative in <see cref="stellaParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFold([NotNull] stellaParser.FoldContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>Add</c>
	/// labeled alternative in <see cref="stellaParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAdd([NotNull] stellaParser.AddContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>IsZero</c>
	/// labeled alternative in <see cref="stellaParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitIsZero([NotNull] stellaParser.IsZeroContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>Var</c>
	/// labeled alternative in <see cref="stellaParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitVar([NotNull] stellaParser.VarContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>TypeAbstraction</c>
	/// labeled alternative in <see cref="stellaParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitTypeAbstraction([NotNull] stellaParser.TypeAbstractionContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>Divide</c>
	/// labeled alternative in <see cref="stellaParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDivide([NotNull] stellaParser.DivideContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>LessThan</c>
	/// labeled alternative in <see cref="stellaParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLessThan([NotNull] stellaParser.LessThanContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>DotRecord</c>
	/// labeled alternative in <see cref="stellaParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDotRecord([NotNull] stellaParser.DotRecordContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>GreaterThan</c>
	/// labeled alternative in <see cref="stellaParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitGreaterThan([NotNull] stellaParser.GreaterThanContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>Equal</c>
	/// labeled alternative in <see cref="stellaParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitEqual([NotNull] stellaParser.EqualContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>Throw</c>
	/// labeled alternative in <see cref="stellaParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitThrow([NotNull] stellaParser.ThrowContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>Multiply</c>
	/// labeled alternative in <see cref="stellaParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitMultiply([NotNull] stellaParser.MultiplyContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>ConstMemory</c>
	/// labeled alternative in <see cref="stellaParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitConstMemory([NotNull] stellaParser.ConstMemoryContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>List</c>
	/// labeled alternative in <see cref="stellaParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitList([NotNull] stellaParser.ListContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>TryCatch</c>
	/// labeled alternative in <see cref="stellaParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitTryCatch([NotNull] stellaParser.TryCatchContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>Head</c>
	/// labeled alternative in <see cref="stellaParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitHead([NotNull] stellaParser.HeadContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>TerminatingSemicolon</c>
	/// labeled alternative in <see cref="stellaParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitTerminatingSemicolon([NotNull] stellaParser.TerminatingSemicolonContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>NotEqual</c>
	/// labeled alternative in <see cref="stellaParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitNotEqual([NotNull] stellaParser.NotEqualContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>ConstUnit</c>
	/// labeled alternative in <see cref="stellaParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitConstUnit([NotNull] stellaParser.ConstUnitContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>Sequence</c>
	/// labeled alternative in <see cref="stellaParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitSequence([NotNull] stellaParser.SequenceContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>ConstFalse</c>
	/// labeled alternative in <see cref="stellaParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitConstFalse([NotNull] stellaParser.ConstFalseContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>Abstraction</c>
	/// labeled alternative in <see cref="stellaParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAbstraction([NotNull] stellaParser.AbstractionContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>ConstInt</c>
	/// labeled alternative in <see cref="stellaParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitConstInt([NotNull] stellaParser.ConstIntContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>Variant</c>
	/// labeled alternative in <see cref="stellaParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitVariant([NotNull] stellaParser.VariantContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>ConstTrue</c>
	/// labeled alternative in <see cref="stellaParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitConstTrue([NotNull] stellaParser.ConstTrueContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>Subtract</c>
	/// labeled alternative in <see cref="stellaParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitSubtract([NotNull] stellaParser.SubtractContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>TypeCast</c>
	/// labeled alternative in <see cref="stellaParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitTypeCast([NotNull] stellaParser.TypeCastContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>If</c>
	/// labeled alternative in <see cref="stellaParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitIf([NotNull] stellaParser.IfContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>Application</c>
	/// labeled alternative in <see cref="stellaParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitApplication([NotNull] stellaParser.ApplicationContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>Deref</c>
	/// labeled alternative in <see cref="stellaParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDeref([NotNull] stellaParser.DerefContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>IsEmpty</c>
	/// labeled alternative in <see cref="stellaParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitIsEmpty([NotNull] stellaParser.IsEmptyContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>Panic</c>
	/// labeled alternative in <see cref="stellaParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPanic([NotNull] stellaParser.PanicContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>LessThanOrEqual</c>
	/// labeled alternative in <see cref="stellaParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLessThanOrEqual([NotNull] stellaParser.LessThanOrEqualContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>Succ</c>
	/// labeled alternative in <see cref="stellaParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitSucc([NotNull] stellaParser.SuccContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>Inl</c>
	/// labeled alternative in <see cref="stellaParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitInl([NotNull] stellaParser.InlContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>GreaterThanOrEqual</c>
	/// labeled alternative in <see cref="stellaParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitGreaterThanOrEqual([NotNull] stellaParser.GreaterThanOrEqualContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>Inr</c>
	/// labeled alternative in <see cref="stellaParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitInr([NotNull] stellaParser.InrContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>Match</c>
	/// labeled alternative in <see cref="stellaParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitMatch([NotNull] stellaParser.MatchContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>LogicNot</c>
	/// labeled alternative in <see cref="stellaParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLogicNot([NotNull] stellaParser.LogicNotContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>ParenthesisedExpr</c>
	/// labeled alternative in <see cref="stellaParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitParenthesisedExpr([NotNull] stellaParser.ParenthesisedExprContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>Tail</c>
	/// labeled alternative in <see cref="stellaParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitTail([NotNull] stellaParser.TailContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>Record</c>
	/// labeled alternative in <see cref="stellaParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitRecord([NotNull] stellaParser.RecordContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>LogicAnd</c>
	/// labeled alternative in <see cref="stellaParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLogicAnd([NotNull] stellaParser.LogicAndContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>TypeApplication</c>
	/// labeled alternative in <see cref="stellaParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitTypeApplication([NotNull] stellaParser.TypeApplicationContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>LetRec</c>
	/// labeled alternative in <see cref="stellaParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLetRec([NotNull] stellaParser.LetRecContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>LogicOr</c>
	/// labeled alternative in <see cref="stellaParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLogicOr([NotNull] stellaParser.LogicOrContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>TryWith</c>
	/// labeled alternative in <see cref="stellaParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitTryWith([NotNull] stellaParser.TryWithContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>Pred</c>
	/// labeled alternative in <see cref="stellaParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPred([NotNull] stellaParser.PredContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>TypeAsc</c>
	/// labeled alternative in <see cref="stellaParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitTypeAsc([NotNull] stellaParser.TypeAscContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>NatRec</c>
	/// labeled alternative in <see cref="stellaParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitNatRec([NotNull] stellaParser.NatRecContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>Unfold</c>
	/// labeled alternative in <see cref="stellaParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitUnfold([NotNull] stellaParser.UnfoldContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>Ref</c>
	/// labeled alternative in <see cref="stellaParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitRef([NotNull] stellaParser.RefContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>DotTuple</c>
	/// labeled alternative in <see cref="stellaParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitDotTuple([NotNull] stellaParser.DotTupleContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>Fix</c>
	/// labeled alternative in <see cref="stellaParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitFix([NotNull] stellaParser.FixContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>Let</c>
	/// labeled alternative in <see cref="stellaParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLet([NotNull] stellaParser.LetContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>Assign</c>
	/// labeled alternative in <see cref="stellaParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAssign([NotNull] stellaParser.AssignContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>Tuple</c>
	/// labeled alternative in <see cref="stellaParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitTuple([NotNull] stellaParser.TupleContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>ConsList</c>
	/// labeled alternative in <see cref="stellaParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitConsList([NotNull] stellaParser.ConsListContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="stellaParser.patternBinding"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPatternBinding([NotNull] stellaParser.PatternBindingContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="stellaParser.binding"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitBinding([NotNull] stellaParser.BindingContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="stellaParser.matchCase"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitMatchCase([NotNull] stellaParser.MatchCaseContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>PatternCons</c>
	/// labeled alternative in <see cref="stellaParser.pattern"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPatternCons([NotNull] stellaParser.PatternConsContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>PatternTuple</c>
	/// labeled alternative in <see cref="stellaParser.pattern"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPatternTuple([NotNull] stellaParser.PatternTupleContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>PatternList</c>
	/// labeled alternative in <see cref="stellaParser.pattern"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPatternList([NotNull] stellaParser.PatternListContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>PatternRecord</c>
	/// labeled alternative in <see cref="stellaParser.pattern"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPatternRecord([NotNull] stellaParser.PatternRecordContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>PatternVariant</c>
	/// labeled alternative in <see cref="stellaParser.pattern"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPatternVariant([NotNull] stellaParser.PatternVariantContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>PatternAsc</c>
	/// labeled alternative in <see cref="stellaParser.pattern"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPatternAsc([NotNull] stellaParser.PatternAscContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>PatternInt</c>
	/// labeled alternative in <see cref="stellaParser.pattern"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPatternInt([NotNull] stellaParser.PatternIntContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>PatternInr</c>
	/// labeled alternative in <see cref="stellaParser.pattern"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPatternInr([NotNull] stellaParser.PatternInrContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>PatternTrue</c>
	/// labeled alternative in <see cref="stellaParser.pattern"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPatternTrue([NotNull] stellaParser.PatternTrueContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>PatternInl</c>
	/// labeled alternative in <see cref="stellaParser.pattern"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPatternInl([NotNull] stellaParser.PatternInlContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>PatternVar</c>
	/// labeled alternative in <see cref="stellaParser.pattern"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPatternVar([NotNull] stellaParser.PatternVarContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>ParenthesisedPattern</c>
	/// labeled alternative in <see cref="stellaParser.pattern"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitParenthesisedPattern([NotNull] stellaParser.ParenthesisedPatternContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>PatternSucc</c>
	/// labeled alternative in <see cref="stellaParser.pattern"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPatternSucc([NotNull] stellaParser.PatternSuccContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>PatternFalse</c>
	/// labeled alternative in <see cref="stellaParser.pattern"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPatternFalse([NotNull] stellaParser.PatternFalseContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>PatternUnit</c>
	/// labeled alternative in <see cref="stellaParser.pattern"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitPatternUnit([NotNull] stellaParser.PatternUnitContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="stellaParser.labelledPattern"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitLabelledPattern([NotNull] stellaParser.LabelledPatternContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>TypeTuple</c>
	/// labeled alternative in <see cref="stellaParser.stellatype"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitTypeTuple([NotNull] stellaParser.TypeTupleContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>TypeTop</c>
	/// labeled alternative in <see cref="stellaParser.stellatype"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitTypeTop([NotNull] stellaParser.TypeTopContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>TypeBool</c>
	/// labeled alternative in <see cref="stellaParser.stellatype"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitTypeBool([NotNull] stellaParser.TypeBoolContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>TypeRef</c>
	/// labeled alternative in <see cref="stellaParser.stellatype"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitTypeRef([NotNull] stellaParser.TypeRefContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>TypeRec</c>
	/// labeled alternative in <see cref="stellaParser.stellatype"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitTypeRec([NotNull] stellaParser.TypeRecContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>TypeSum</c>
	/// labeled alternative in <see cref="stellaParser.stellatype"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitTypeSum([NotNull] stellaParser.TypeSumContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>TypeVar</c>
	/// labeled alternative in <see cref="stellaParser.stellatype"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitTypeVar([NotNull] stellaParser.TypeVarContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>TypeVariant</c>
	/// labeled alternative in <see cref="stellaParser.stellatype"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitTypeVariant([NotNull] stellaParser.TypeVariantContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>TypeUnit</c>
	/// labeled alternative in <see cref="stellaParser.stellatype"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitTypeUnit([NotNull] stellaParser.TypeUnitContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>TypeNat</c>
	/// labeled alternative in <see cref="stellaParser.stellatype"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitTypeNat([NotNull] stellaParser.TypeNatContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>TypeBottom</c>
	/// labeled alternative in <see cref="stellaParser.stellatype"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitTypeBottom([NotNull] stellaParser.TypeBottomContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>TypeParens</c>
	/// labeled alternative in <see cref="stellaParser.stellatype"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitTypeParens([NotNull] stellaParser.TypeParensContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>TypeFun</c>
	/// labeled alternative in <see cref="stellaParser.stellatype"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitTypeFun([NotNull] stellaParser.TypeFunContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>TypeForAll</c>
	/// labeled alternative in <see cref="stellaParser.stellatype"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitTypeForAll([NotNull] stellaParser.TypeForAllContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>TypeRecord</c>
	/// labeled alternative in <see cref="stellaParser.stellatype"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitTypeRecord([NotNull] stellaParser.TypeRecordContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>TypeList</c>
	/// labeled alternative in <see cref="stellaParser.stellatype"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitTypeList([NotNull] stellaParser.TypeListContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="stellaParser.recordFieldType"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitRecordFieldType([NotNull] stellaParser.RecordFieldTypeContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="stellaParser.variantFieldType"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitVariantFieldType([NotNull] stellaParser.VariantFieldTypeContext context);
}
