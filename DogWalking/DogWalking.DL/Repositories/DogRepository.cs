using DogWalking.DL.Context;
using DogWalking.DL.Entities;
using DogWalking.DL.Repositories.Base;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;


namespace DogWalking.DL.Repositories
{
    /// <summary>
    /// Repository implementation for dogs.
    /// </summary>
    public class DogRepository : RepositoryBase<Dog>, IDogRepository
    {
        /// <summary>
        /// Initializes a new instance of <see cref="DogRepository"/>.
        /// </summary>
        public DogRepository(DogWalkingContext ctx) : base(ctx)
        {
        }

        /// <summary>
        /// Gets all dogs.
        /// </summary>
        /// <returns>List of dogs.</returns>
        public List<Dog> GetAll()
        {
            return Query()
                .Include(d => d.Client)
                .AsNoTracking()
                .ToList();
        }

        /// <summary>
        /// Searches dogs by term.
        /// </summary>
        /// <param name="searchTerm">Search term.</param>
        /// <returns>Matching dogs.</returns>
        public List<Dog> Search(string searchTerm)
        {
            return Query()
                .Include(d => d.Client)
                .AsNoTracking()
                .Where(c =>
                    c.Name.Contains(searchTerm) ||
                    (c.Client.Name.Contains(searchTerm) && c.Client.IsActive)
                )
                .ToList();
        }

        /// <summary>
        /// Gets a dog by id.
        /// </summary>
        /// <param name="dogId">Dog id.</param>
        /// <returns>Matching dog or <c>null</c>.</returns>
        public override Dog GetById(int dogId)
        {
            return Query()
                .Include(d => d.Client)
                .SingleOrDefault(d => d.Id == dogId);
        }

        /// <summary>
        /// Gets dogs by client id.
        /// </summary>
        /// <param name="clientId">Client id.</param>
        /// <returns>List of dogs for the client.</returns>
        public List<Dog> GetByClient(int clientId)
        {
            return Query()
                .AsNoTracking()
                .Where(d => d.ClientId == clientId)
                .ToList();
        }

        /// <summary>
        /// Deletes a dog by id.
        /// </summary>
        /// <param name="dogId">Dog id.</param>
        public void Delete(int dogId)
        {
            var entity = GetById(dogId);
            if (entity != null)
            {
                SoftDelete(entity);
            }
        }
    }
}
