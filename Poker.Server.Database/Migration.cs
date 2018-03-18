using System;
using FluentMigrator;

namespace Poker.Server.Database
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class MigrationAttribute : FluentMigrator.MigrationAttribute
    {
        public string Author { get; private set; }

        public string Feature { get; private set; }

        public MigrationAttribute(long version, string description = null, string author = null, string feature = null) 
            : this(version, TransactionBehavior.Default, description, author, feature)
        {

        }

        public MigrationAttribute(long version, TransactionBehavior behavior, string description = null, string author = null, string feature = null) 
            : base(version, behavior, description)
        {
            Author = author;
            Feature = feature;
        }
    }
}
