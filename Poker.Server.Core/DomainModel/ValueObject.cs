using System.Collections.Generic;
using System.Linq;

namespace Poker.Core.DomainModel
{
    public abstract class ValueObject
    {
        protected abstract IEnumerable<object> GetEqualityComponents();

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            if (ReferenceEquals(null, obj)) return false;
            if (GetType() != obj.GetType()) return false;

            var valueObject = obj as ValueObject;
            return valueObject != null && GetEqualityComponents().SequenceEqual(valueObject.GetEqualityComponents());
        }

        public override int GetHashCode()
        {
            return string.Join("", GetEqualityComponents().Select(c => c.GetHashCode())).GetHashCode();
        }
    }
}
