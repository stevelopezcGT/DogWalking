namespace DogWalking.DL.Migrations
{
    using DogWalking.DL.Context;
    using DogWalking.DL.Entities;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<DogWalkingContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DogWalking.DL.Context.DogWalkingContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
            if (!context.Users.Any(u => u.Username == "admin"))
            {
                context.Users.Add(new User { Username = "admin", Password = "admin" });
            }
        }
    }
}