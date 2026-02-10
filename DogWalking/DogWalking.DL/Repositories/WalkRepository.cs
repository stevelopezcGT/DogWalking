using DogWalking.DL.Context;
using DogWalking.DL.Entities;
using DogWalking.DL.Repositories.Base;
using System.Collections.Generic;
using System.Linq;

namespace DogWalking.DL.Repositories
{
    /// <summary>
    /// Repository implementation for walks.
    /// </summary>
    public class WalkRepository : RepositoryBase<Walk>, IWalkRepository
    {
        /// <summary>
        /// Initializes a new instance of <see cref="WalkRepository"/>.
        /// </summary>
        public WalkRepository() : base(new DogWalkingContext())
        {
        }

        /// <summary>
        /// Gets all walks.
        /// </summary>
        /// <returns>List of walks.</returns>
        public List<Walk> GetAll()
        {
            return _context.Walks
                .AsNoTracking()
                .ToList();
        }

        /// <summary>
        /// Searches walks by dog name.
        /// </summary>
        /// <param name="searchTerm">Search term.</param>
        /// <returns>Matching walks.</returns>
        public List<Walk> Search(string searchTerm)
        {
            return _context.Walks
                .AsNoTracking()
                .Where(c =>
                    c.Dog.Name.Contains(searchTerm)
                )
                .ToList();
        }

        /// <summary>
        /// Deletes a walk by id.
        /// </summary>
        /// <param name="walkId">Walk id.</param>
        public void Delete(int walkId)
        {
            var entity = _context.Walks.Find(walkId);
            if (entity != null)
            {
                Remove(entity);
            }
        }
    }
}
