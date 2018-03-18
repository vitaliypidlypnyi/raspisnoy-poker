namespace Poker.Core.ColumnOperations
{
    public class SaveColumn : OperationColumn
    {
        private SaveColumn(string[] columns)
            : base(columns)
        {
        }

        public static SaveColumn Custom(params string[] columns)
        {
            return new SaveColumn(columns);
        }
    }
}
