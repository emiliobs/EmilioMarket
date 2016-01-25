namespace EmilioMarket.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<EmilioMarket.Models.EmilioMarketContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            //permite que se pierda datos en las migraciones y no me sque error:
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "EmilioMarket.Models.EmilioMarketContext";
        }

        protected override void Seed(EmilioMarket.Models.EmilioMarketContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
