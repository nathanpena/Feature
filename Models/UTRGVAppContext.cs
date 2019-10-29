using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
namespace Features.Models
{
    public class UTRGVAppContext : DbContext
    {
        public UTRGVAppContext() : base("DefaultConnection")
        {

        }

        static UTRGVAppContext()
        {
            Database.SetInitializer<UTRGVAppContext>(
            new MigrateDatabaseToLatestVersion<UTRGVAppContext, Migrations.Configuration>());
            //Database.Initialize(false);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            modelBuilder.Types().Configure(entity => entity.ToTable("Feature" + "_" + entity.ClrType.Name));
        }

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<File> Files { get; set; }
        /*-- AddModel --*/
        
    }
}