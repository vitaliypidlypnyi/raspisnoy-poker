using System;
using System.Reflection;

namespace Poker.Core.Validation
{
    public class Contract
    {
        private static readonly ExceptionResolver ExceptionResolver = new ExceptionResolver();

        protected Contract()
        {
        }

        public static void Requires<TException>(bool value, string message)
            where TException : Exception, new()
        {
            if (value) return;

            var exception = ExceptionResolver.Create<TException>(message);
            throw exception;
        }

        public static void Assume<TException>(bool value, string message)
           where TException : Exception, new()
        {
            if (value) return;

            var exception = ExceptionResolver.Create<TException>(message);
            throw exception;
        }

        public static void RequiresNotNull(object value, string message)
        {
            Requires<ArgumentNullException>(!ReferenceEquals(null, value), message);
        }

        public static void RequiresEquals(object value1, object value2, string message)
        {
            Requires<ArgumentException>(Equals(value1, value2), message);
        }

        public static void RequiresNotEquals(object value1, object value2, string message)
        {
            Requires<ArgumentException>(!Equals(value1, value2), message);
        }

        public static void RequiresNotEmpty(string value, string message)
        {
            Requires<ArgumentException>(value != null && value.Trim().Length != 0, message);
        }

        public static void RequiresLength(string value, int lenght, string message)
        {
            Requires<ArgumentException>(value != null && value.Trim().Length == lenght, message);
        }

        public static void RequiresLength(string value, int minimum, int maximum, string message)
        {
            Func<string, bool> predicate = obj =>
            {
                var lenght = obj.Trim().Length;
                return lenght >= minimum && lenght <= maximum;
            };

            Requires<ArgumentException>(value != null && predicate(value), message);
        }

        public static void RequiresEnum<TType>(Enum @enum, string message)
            where TType : struct
        {
            var type = typeof(TType);
            Contract.RequiresEquals(true, type.GetTypeInfo().IsEnum, $"'{type.FullName}' is not enum.");
            if (ReferenceEquals(@enum, null)) return;

            Requires<ArgumentException>(Enum.IsDefined(type, Convert.ToInt32(@enum)), message);
        }

        public static void AssumeNotNull(object value, string message)
        {
            Assume<InvalidOperationException>(!ReferenceEquals(null, value), message);
        }

        public static void AssumeEquals(object value1, object value2, string message)
        {
            Assume<InvalidOperationException>(Equals(value1, value2), message);
        }

        public static void AssumeNotEquals(object value1, object value2, string message)
        {
            Assume<InvalidOperationException>(!Equals(value1, value2), message);
        }

        public static void AssumeNotEmpty(string value, string message)
        {
            Assume<InvalidOperationException>(value != null && value.Trim().Length != 0, message);
        }

        public static void AssumeLength(string value, int maximum, string message)
        {
            Assume<InvalidOperationException>(value != null && value.Trim().Length <= maximum, message);
        }

        public static void AssumeLength(string value, int minimum, int maximum, string message)
        {
            Func<string, bool> predicate = obj =>
            {
                var lenght = obj.Trim().Length;
                return lenght >= minimum && lenght <= maximum;
            };

            Assume<InvalidOperationException>(value != null && predicate(value), message);
        }

        public static void RequiresFunc(Func<object, bool> predicate, object obj, string message)
        {
            Assume<InvalidOperationException>(predicate(obj), message);
        }
    }
}
