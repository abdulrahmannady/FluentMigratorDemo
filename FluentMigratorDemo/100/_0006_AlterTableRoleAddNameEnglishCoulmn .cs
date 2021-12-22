using FluentMigrator;

namespace FluentMigratorDemo
{
    [Migration(0006)]
    public class _0006_AlterTableRoleAddNameEnglishCoulmn : Migration
    {
        public override void Up()
        {
            Alter.Table(Tables.Role)
                .AddColumn("NameEnglish").AsString(StringLength.Medium).Nullable();
        }

        public override void Down()
        {
            Delete.Column("NameEnglish").FromTable(Tables.Role);
        }
    }
}
