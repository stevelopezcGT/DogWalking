using DogWalking.DL.Context;
using DogWalking.DL.Entities;
using DogWalking.DL.Repositories.Base;
using System.Collections.Generic;
using System.Linq;

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
        public DogRepository() : base(new DogWalkingContext())
        {
        }

        /// <summary>
        /// Gets all dogs.
        /// </summary>
        /// <returns>List of dogs.</returns>
        public List<Dog> GetAll()
        {
            return _context.Dogs
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
            return _context.Dogs
                .AsNoTracking()
                .Where(c =>
                    c.Name.Contains(searchTerm) ||
                    c.Client.Name.Contains(searchTerm)
                )
                .ToList();
        }

        /// <summary>
        /// Deletes a dog by id.
        /// </summary>
        /// <param name="dogId">Dog id.</param>
        public void Delete(int dogId)
        {
            var entity = _context.Dogs.Find(dogId);
            if (entity != null)
            {
                Remove(entity);
            }
        }
    }
}
