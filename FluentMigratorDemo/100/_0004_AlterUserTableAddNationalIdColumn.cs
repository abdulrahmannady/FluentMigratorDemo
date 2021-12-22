using FluentMigrator;

namespace FluentMigratorDemo._100
{
    [Migration(0004)]
    public class _0004_AlterUserTableAddNationalIdColumn : Migration
    {
        public override void Up()
        {
            Alter.Table(Tables.User)
                .AddColumn("NationalId").AsString(StringLength.Fourteen).Nullable();
        }

        public override void Down()
        {
            Delete.Column("NationalId").FromTable(Tables.User);
        }
    }
}
