using FluentMigrator;
using System.Collections.Generic;

namespace FluentMigratorDemo.Seed
{
    public class User
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public int RoleId { get; set; }
    }

    [Migration(1002)]
    public class _1002_SeedUsers : Migration
    {
        public static List<User> Users = new List<User>()
        {
            new User { Name = "Amr", Email = "Amr@silverkey.com", RoleId = (int)Role.Admin },
            new User { Name = "Nady", Email = "Nady@silverkey.com", RoleId = (int)Role.User },
        };

        public override void Up()
        {
            var id = 1;
            foreach (var u in Users)
            {
                Insert.IntoTable(Tables.User).Row(
                    new
                    {
                        Name = u.Name,
                        Email = u.Email,
                        IsActive = true
                    }
                );

                Insert.IntoTable(Tables.UserRole).Row(
                        new
                        {
                            UserId = id,
                            RoleId = u.RoleId
                        }
                    );

                id++;
            }
        }

        public override void Down()
        {
            //
        }
    }
}

