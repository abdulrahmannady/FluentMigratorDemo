using FluentMigrator;

namespace FluentMigratorDemo
{
    [Migration(0002)]
    public class _0002_CreateRoleTable : Migration
    {
        public override void Up()
        {
            Create.Table(Tables.Role)
                    .AutoId()
                    .WithColumn("Name").AsString(StringLength.Medium).Unique().Nullable()
                    .Bool("IsActive", true).NotNullable();
        }

        public override void Down()
        {
            Delete.Table(Tables.Role);
        }
    }
}
