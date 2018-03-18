namespace Poker.Core.SqlOperations
{
    public interface IOperationVisitor
    {
        string Visit(PropertyOperation operation);

        string Visit(EqualsOperation operation);

        string Visit(TrueOperation operation);

        string Visit(NotOperation operation);

        string Visit(AndOperation operation);

        string Visit(OrOperation operation);

        string Visit(ConstantOperation operation);

        string Visit(InOperation operation);

        string Visit(NullOperation operation);

        string Visit(ArrayOperation operation);

        string Visit(CustomOperation operation);
    }
}
