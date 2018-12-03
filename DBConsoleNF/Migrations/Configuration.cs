using DBConsoleNF.DataAccess.Models;

namespace DBConsoleNF.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DBConsoleNF.DataAccess.MyContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        

        protected override void Seed(DBConsoleNF.DataAccess.MyContext context)
        {

            var userAndrian = new User
            {
                Id = 1,
                Name = "Andrian"
            };
            var userMarian = new User
            {
                Id = 2,
                Name = "Marian"
            };
            var userVasi = new User
            {
                Id = 3,
                Name = "Vasi"
            };

            context.Users.AddOrUpdate(x => x.Id, userAndrian);
            context.Users.AddOrUpdate(x => x.Id, userMarian);
            context.Users.AddOrUpdate(x => x.Id, userVasi);

            context.DocumentInfos.AddOrUpdate(x => x.Id, new DocumentInfo
            {
                Id = 1,
                Type = "Buletin",
                Owner = userAndrian,
                CreateDate = DateTime.Now.AddMonths(-5),
                Status = DocumentStatus.Open.ToString(),
                Price = 10m,
                Pages = 2,
            });
            context.DocumentInfos.AddOrUpdate(x => x.Id, new DocumentInfo
            {
                Id = 2,
                Type = "Hospital Document",
                Owner = userMarian,
                CreateDate = DateTime.Now.AddMonths(-3),
                Status = DocumentStatus.Closed.ToString(),
                IsDeleted = true,
                Price = 5m,
                Pages = 3,
            });
            context.DocumentInfos.AddOrUpdate(x => x.Id, new DocumentInfo
            {
                Id = 3,
                Type = "Studen Document",
                Owner = userVasi,
                CreateDate = DateTime.Now.AddMonths(-9),
                Status = DocumentStatus.Open.ToString(),
                IsDeleted = true,
                Price = 1m,
                Pages = 1,
            });
            context.DocumentInfos.AddOrUpdate(x => x.Id, new DocumentInfo
            {
                Id = 4,
                Type = "Driver License",
                Owner = userVasi,
                CreateDate = DateTime.Now.AddMonths(-6),
                Status =  DocumentStatus.Unknown.ToString(),
                Price = 7m,
                Pages = 2,
            });

            context.SaveChanges();
        }
    }
}
