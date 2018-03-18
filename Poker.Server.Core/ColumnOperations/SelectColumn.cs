namespace Poker.Core.ColumnOperations
{
    public class SelectColumn : OperationColumn
    {
        protected SelectColumn()
        {
        }

        private SelectColumn(string[] columns)
            : base(columns)
        {
        }

        public static SelectColumn All()
        {
            return new SelectColumn();
        }

        public static SelectColumn Custom(params string[] columns)
        {
            return new SelectColumn(columns);
        }
    }
}
