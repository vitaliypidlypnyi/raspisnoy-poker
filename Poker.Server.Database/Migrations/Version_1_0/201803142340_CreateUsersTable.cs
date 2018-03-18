using System;
using FluentMigrator;

namespace Poker.Server.Database.Migrations.Version_1_0
{
    [Migration(201803142340, author: "vpidlypnii")]
    public class CreateUsersTable : Migration
    {
        private const string UserTable = "User";

        public override void Up()
        {
            Create.Table(UserTable)
                .WithColumn("Id").AsInt32().Identity().NotNullable().PrimaryKey("PK_UserId")
                .WithColumn("UserName").AsString().Unique().NotNullable()
                .WithColumn("Password").AsString().NotNullable()
                .WithColumn("CreateDate").AsDateTime().NotNullable()
                .WithColumn("BirthDate").AsDateTime()
                .WithColumn("IsAdmin").AsBoolean().NotNullable().WithDefaultValue("0");

            Insert.IntoTable(UserTable).Row(new { UserName = "Buddy", Password = "7308e975a547f6aa56b8d754fbc8a33f19e209ae8c320df586d71fc2d1252776", CreateDate = DateTime.Now, BirthDate = new DateTime(1990, 4, 5) });
        }

        public override void Down()
        {
            Delete.Table(UserTable);
        }
    }
}
