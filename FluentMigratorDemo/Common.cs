using FluentMigrator;
using FluentMigrator.Builders.Create.Table;

namespace FluentMigratorDemo
{
    public static class FluentMigratorExtensions
    {
        public static ICreateTableWithColumnOrSchemaSyntax AutoId(this ICreateTableWithColumnOrSchemaSyntax self)
        {
            self.WithColumn("Id").AsInt32().NotNullable().Identity().PrimaryKey();
            return self;
        }

        public static ICreateTableColumnOptionOrWithColumnSyntax Bool(this ICreateTableColumnOptionOrWithColumnSyntax self,
            string name, bool defaultValue)
        {
            self.WithColumn(name).AsBoolean().WithDefaultValue(defaultValue).NotNullable();

            return self;
        }

        public static ICreateTableColumnOptionOrWithColumnSyntax Identifier(this ICreateTableColumnOptionOrWithColumnSyntax self)
        {
            return self.WithColumn("Identifier").AsGuid().NotNullable().WithDefaultValue("newid()");
        }

        public static ICreateTableColumnOptionOrWithColumnSyntax ChangeInfo(this ICreateTableColumnOptionOrWithColumnSyntax self)
        {
            self.WithColumn("DateCreated").AsDateTime().NotNullable().WithDefaultValue(new SQLServerFunction("getutcdate()"))
                 .WithColumn("CreatedByUserId").AsInt32().Nullable().ForeignKey(Tables.User, "Id")
                 .WithColumn("LastModified").AsDateTime().NotNullable().WithDefaultValue(new SQLServerFunction("getutcdate()"))
                 .WithColumn("LastModifiedByUserId").AsInt32().Nullable().ForeignKey(Tables.User, "Id");

            return self;
        }
    }

    internal static class StringLength
    {
        public const int Long = 550;
        public const int Medium = 250;
        public const int Short = 100;
        public const int SuperExtraLong = 30000;
        public const int SuperLong = 10000;
        public const int VeryShort = 50;
        public const int Ten = 10; // for Gender
        public const int Fourteen = 14; // for NationalId
    }

    internal class SQLServerFunction
    {
        private string _functionName;

        public SQLServerFunction(string functionName)
        {
            _functionName = functionName;
        }

        public override string ToString()
        {
            return _functionName;
        }
    }


    // Enforce foreign key convention naming
    public class ForginKeyName
    {
        private string _value;

        public ForginKeyName(string table, string targetTable)
        {
            _value = "FK_" + table + "_" + targetTable;
        }

        public override string ToString()
        {
            return _value;
        }
    }
}




