namespace Features.Migrations
{
    using Models;
    using Providers;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Threading.Tasks;

    internal sealed class Configuration : DbMigrationsConfiguration<UTRGVAppContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "Features";

        }


        protected override void Seed(UTRGVAppContext context)
        { 

            var roles = new Role[2];
	 	 	 roles[0] = new Role { Name = "Faculty" }; 
	 	 	 roles[1] = new Role { Name = "Admin" }; 
            context.Roles.AddOrUpdate(r => r.Name, roles);
            context.SaveChanges();

        }
    }
}
