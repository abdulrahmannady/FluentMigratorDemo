using FluentMigrator;

namespace FluentMigratorDemo.Seed
{
    [Migration(1001)]
    public class _1001_SeedRoles : Migration
    {
        public override void Up()
        {
            Execute.Sql(@"SET IDENTITY_INSERT dbo.Role ON;");

            Insert.IntoTable(Tables.Role).Row(
                    new
                    {
                        Id = (short)Role.Admin,
                        NameEnglish = "Administrator",
                        NameArabic = "مدير النظام",
                        IsActive = true
                    });

            Insert.IntoTable(Tables.Role).Row(
                    new
                    {
                        Id = (short)Role.User,
                        NameEnglish = "User",
                        NameArabic = "مستخدم",
                        IsActive = true
                    });

            Execute.Sql(@"SET IDENTITY_INSERT dbo.Role OFF;");
        }
        public override void Down()
        {
            //
        }
    }
}
