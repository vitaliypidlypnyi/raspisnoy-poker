namespace Poker.Core.Primitives.Pagination
{
    public struct Pagination
    {
        public Pagination(uint pageIndex, uint pageSize)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
        }

        public bool HasValue => PageIndex != default(uint) || PageSize != default(uint);

        public uint PageIndex { get; }

        public uint PageSize { get; }
    }
}
