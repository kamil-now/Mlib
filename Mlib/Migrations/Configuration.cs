namespace Mlib.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Mlib.Data.MlibData>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Mlib.Data.MlibData";
        }

        protected override void Seed(Mlib.Data.MlibData context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
