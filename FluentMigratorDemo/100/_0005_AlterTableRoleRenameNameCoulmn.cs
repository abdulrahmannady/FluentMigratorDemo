using FluentMigrator;

namespace FluentMigratorDemo
{
    [Migration(0005)]
    public class _0005_AlterTableRoleRenameNameCoulmn : Migration
    {
        public override void Up()
        {
            Rename.Column(oldName: "Name").OnTable(Tables.Role).To(name: "NameArabic");
        }

        public override void Down()
        {
            Rename.Column(oldName: "NameArabic").OnTable(Tables.Role).To(name: "Name");
        }

    }
}
