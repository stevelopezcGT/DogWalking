using DogWalking.DL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DogWalking.DL.Context
{
    /// <summary>
    /// Entity Framework database context for the DogWalking application.
    /// </summary>
    public class DogWalkingContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of <see cref="DogWalkingContext"/>.
        /// </summary>
        public DogWalkingContext() : base("DogWalkingDb")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        /// <summary>
        /// Gets or sets clients.
        /// </summary>
        public DbSet<Client> Clients { get; set; }
        /// <summary>
        /// Gets or sets dogs.
        /// </summary>
        public DbSet<Dog> Dogs { get; set; }
        /// <summary>
        /// Gets or sets walks.
        /// </summary>
        public DbSet<Walk> Walks { get; set; }
        /// <summary>
        /// Gets or sets users.
        /// </summary>
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            #region Client
            modelBuilder.Entity<Client>()
                    .Property(x => x.Name)
                    .IsRequired()
                    .HasMaxLength(100);

            modelBuilder.Entity<Client>()
                .Property(x => x.Phone)
                .IsRequired()
                .HasMaxLength(20);
            #endregion

            #region Dog
            modelBuilder.Entity<Dog>()
                    .Property(x => x.Name)
                    .IsRequired();

            modelBuilder.Entity<Dog>()
                .Property(x => x.Age)
                .IsRequired();
            #endregion

            #region Walk
            modelBuilder.Entity<Walk>()
                    .Property(x => x.DurationMinutes)
                    .IsRequired(); 
            #endregion

            base.OnModelCreating(modelBuilder);
        }
    }
}
