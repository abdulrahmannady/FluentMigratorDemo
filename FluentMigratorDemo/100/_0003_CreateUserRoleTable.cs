using FluentMigrator;

namespace FluentMigratorDemo
{
    #region Migration
    [Migration(0003)]
    public class _0003_CreateUserRoleTable : Migration
    {
        public override void Up()
        {
            Create.Table(Tables.UserRole)
                .AutoId()
                .WithColumn("UserId").AsInt32().ForeignKey("FK_UserRole_User", Tables.User, "Id").NotNullable()
                .WithColumn("RoleId").AsInt32().ForeignKey("FK_UserRole_Role", Tables.Role, "Id").NotNullable()
                .Bool("IsActive", true).NotNullable();
        }

        public override void Down()
        {
            Delete.ForeignKey(new ForginKeyName(Tables.UserRole, Tables.User).ToString()).OnTable(Tables.UserRole);

            Delete.ForeignKey(new ForginKeyName(Tables.UserRole, Tables.Role).ToString()).OnTable(Tables.UserRole);

            Delete.Table(Tables.UserRole);
        }
    }
    #endregion

    #region  AutoReversingMigration
    //[Migration(0003)]
    //public class _0003_CreateUserRoleTable : AutoReversingMigration
    //{
    //    public override void Up()
    //    {
    //        Create.Table(Tables.UserRole)
    //            .AutoId()
    //            .WithColumn("UserId").AsInt32().ForeignKey("FK_UserRole_User", Tables.User, "Id").NotNullable()
    //            .WithColumn("RoleId").AsInt32().ForeignKey("FK_UserRole_Role", Tables.Role, "Id").NotNullable()
    //            .Bool("IsActive", true).NotNullable();
    //    }
    //}
    #endregion
}
