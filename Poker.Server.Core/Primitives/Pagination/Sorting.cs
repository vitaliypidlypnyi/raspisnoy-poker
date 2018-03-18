using System.Collections.Generic;
using Poker.Core.Validation;

namespace Poker.Core.Primitives.Pagination
{
    public class Sorting
    {
        private readonly SortItem[] _sortItems;

        public Sorting(string property, SortDirection direction) : this(new SortItem(property, direction))
        {
        }

        public Sorting(params SortItem[] sortItems)
        {
            Contract.RequiresNotNull(sortItems, "sortItems != null");

            _sortItems = sortItems;
        }

        public IReadOnlyCollection<SortItem> SortItems => _sortItems;
    }
}
