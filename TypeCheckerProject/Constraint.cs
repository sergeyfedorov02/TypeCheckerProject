namespace TypeCheckerProject;

using static stellaParser;
using static StandardTypes;

public class Constraint
{
    public IType Lhv { get; }
    public IType Rhv { get; }
    public ExprContext ExprContext { get; }

    public Constraint(IType lhv, IType rhv, ExprContext exprContext)
    {
        Lhv = lhv;
        Rhv = rhv;
        ExprContext = exprContext;
    }
}