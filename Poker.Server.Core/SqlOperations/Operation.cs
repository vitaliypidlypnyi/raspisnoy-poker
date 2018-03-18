using System.Collections.Generic;

namespace Poker.Core.SqlOperations
{
    public abstract class Operation
    {
        public abstract string Accept(IOperationVisitor visitor);

        public abstract object SatisfiedBy(Dictionary<string, object> dictionary);

        public AndOperation And(Operation operation)
        {
            return And(this, operation);
        }

        public OrOperation Or(Operation operation)
        {
            return Or(this, operation);
        }

        public static AndOperation And(Operation left, Operation right)
        {
            return new AndOperation(left, right);
        }

        public static OrOperation Or(Operation left, Operation right)
        {
            return new OrOperation(left, right);
        }

        public static EqualsOperation Equals(Operation left, Operation right)
        {
            return new EqualsOperation(left, right);
        }

        public static InOperation In(PropertyOperation left, ArrayOperation right)
        {
            return new InOperation(left, right);
        }

        public static ArrayOperation Array(string name, params object[] items)
        {
            return new ArrayOperation(name, items);
        }

        public static PropertyOperation Column(string name)
        {
            return new PropertyOperation(name);
        }

        public static ConstantOperation Constant(string name, object value)
        {
            return new ConstantOperation(name, value);
        }

        public static NotOperation Not(Operation operation)
        {
            return new NotOperation(operation);
        }

        public static TrueOperation IsTrue(Operation operation)
        {
            return new TrueOperation(operation);
        }

        public static NotOperation IsFalse(Operation operation)
        {
            return new NotOperation(operation);
        }

        public static NullOperation IsNull(PropertyOperation operation)
        {
            return new NullOperation(operation);
        }

        public static CustomOperation Custom(string operation, ConstantOperation constant)
        {
            return new CustomOperation(operation, constant);
        }

        #region extended

        public static Operation In(string name, params object[] items)
        {
            return In(Column(name), Array(name, items));
        }

        public static Operation NotIn(string name, params object[] items)
        {
            return Not(In(Column(name), Array(name, items)));
        }

        public static Operation IsNull(string name)
        {
            return IsNull(Column(name));
        }

        public static Operation IsNotNull(string name)
        {
            return Not(IsNull(Column(name)));
        }

        public static Operation IsTrue(string name)
        {
            return IsTrue(Column(name));
        }

        public static Operation IsFalse(string name)
        {
            return IsFalse(Column(name));
        }

        public static Operation Is(string name, bool value)
        {
            return Equals(Column(name), Constant(name, value));
        }

        public static Operation Equals(string name, object value)
        {
            return Equals(Column(name), Constant(name, value));
        }

        public static Operation IdEquals(int id)
        {
            return Equals("Id", id);
        }

        #endregion
    }
}
