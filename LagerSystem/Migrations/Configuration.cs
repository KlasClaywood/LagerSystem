namespace LagerSystem.Migrations
{
    using LagerSystem.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<LagerSystem.DataAccessLayer.StoreContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(LagerSystem.DataAccessLayer.StoreContext context)
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
            context.Items.AddOrUpdate(
                new StockItem { Name = "Adder", Price = new Nullable<decimal>(87.8M), Shelf = "Shipyard", Description = "The Adder is another classic design, this ship was tailored as a general utility vessel. The original model was first built in 2914 by Outworld Workshops and the type is now manufactured by Zorgon Peterson. Although lightly armed it has more cargo capacity than a Sidewinder and this ship can often be seen doing shuttle runs as well as light trading." }
                );
        }
    }
}
