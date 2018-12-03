using System.Data.Entity;
using DBConsoleNF.DataAccess.Models;

namespace DBConsoleNF.DataAccess
{
    public class MyContext : DbContext
    {
        public MyContext() : base("name=MyContext")
        {
        }

        public DbSet<DocumentInfo> DocumentInfos { get; set; }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}