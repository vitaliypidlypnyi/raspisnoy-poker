using System.Collections.Generic;
using Poker.Core.DomainModel;
using Poker.Core.Validation;

namespace Poker.Core.Maps
{
    public class ColumnType : ValueObject
    {
        public ColumnType(string name, bool isKey = false, string dbType = null, bool onlyInsert = false)
        {
            Contract.RequiresNotEmpty(name, $"Argument '{nameof(name)}' is invalid.");

            Name = name;
            IsKey = isKey;
            DbType = dbType;
            OnlyInsert = onlyInsert;
        }

        public string Name { get; }

        public string DbType { get; }

        public bool IsKey { get; }

        public bool OnlyInsert { get; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
        }
    }
}
