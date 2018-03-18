using System;
using System.Collections.Generic;

namespace Poker.Core.Validation
{
    internal class ExceptionResolver
    {
        private readonly Dictionary<Type, Func<string, Exception>> _resolvers = new Dictionary<Type, Func<string, Exception>>
        {
            {typeof(ArgumentNullException), message => new ArgumentNullException(message, (Exception)null) },
            {typeof(ArgumentException), message => new ArgumentException(message) },
            {typeof(InvalidOperationException), message => new InvalidOperationException(message) },
            {typeof(ArgumentOutOfRangeException), message => new ArgumentOutOfRangeException(message) },
            {typeof(IndexOutOfRangeException), message => new IndexOutOfRangeException(message) }
        };

        public Exception Create<TException>(string message)
            where TException : Exception, new()
        {
            var exceptionType = typeof(TException);
            if (!_resolvers.ContainsKey(exceptionType))
                throw new InvalidOperationException($"Cannot found exception resolver for {exceptionType.Name}");

            return _resolvers[exceptionType](message);
        }
    }
}
