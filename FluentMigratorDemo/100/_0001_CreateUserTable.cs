using FluentMigrator;

namespace FluentMigratorDemo
{
    #region FluentMigrator
    [Migration(0001)] // You have to decorate the class with the number of migration and also it should be unique among other migration classes and make it public
    public class _0001_CreateUserTable : Migration
    {
        public override void Up()
        {
            Create.Table(tableName: "User")
                .WithColumn(name: "Id").AsInt32().NotNullable().Identity().PrimaryKey()
                .WithColumn(name: "Name").AsString(size: 250).NotNullable()
                .WithColumn(name: "Email").AsString(size: 250).Nullable()
                .WithColumn(name: "IsActive").AsBoolean().WithDefaultValue(value: true).NotNullable()
                .WithColumn("DateCreated").AsDateTime().NotNullable().WithDefaultValue(new SQLServerFunction("getutcdate()"))
                .WithColumn("CreatedByUserId").AsInt32().Nullable().ForeignKey("User", "Id")
                .WithColumn("LastModified").AsDateTime().NotNullable().WithDefaultValue(new SQLServerFunction("getutcdate()"))
                .WithColumn("LastModifiedByUserId").AsInt32().Nullable().ForeignKey(Tables.User, "Id");
        }

        public override void Down()
        {
            Delete.Table(tableName: "User");
        }
    }
    #endregion

    #region FluentMigrator with constants and extension methods
    //[Migration(0001)]
    //public class _0001_CreateUserTable : Migration
    //{
    //    public override void Up()
    //    {
    //        Create.Table(Tables.User)
    //            .AutoId()
    //            .WithColumn(name: "Name").AsString(StringLength.Medium).NotNullable()
    //            .WithColumn(name: "Email").AsString(StringLength.Medium).Nullable()
    //            .WithColumn(name: "NationalId").AsString(StringLength.Medium).Nullable()
    //            .Bool(name: "IsActive", defaultValue: true)
    //            .ChangeInfo();
    //    }

    //    public override void Down()
    //    {
    //        Delete.Table(Tables.User);
    //    }

    //}
    #endregion
}
