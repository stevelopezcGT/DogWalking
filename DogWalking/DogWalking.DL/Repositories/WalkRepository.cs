using DogWalking.DL.Context;
using DogWalking.DL.Entities;
using DogWalking.DL.Repositories.Base;
using System.Collections.Generic;
using System.Linq;

namespace DogWalking.DL.Repositories
{
    /// <summary>
    /// Repository for <see cref="Walk"/> entities that provides common data access operations.
    /// Inherits behavior from <see cref="RepositoryBase{Walk}"/> and implements <see cref="IWalkRepository"/>.
    /// </summary>
    public class WalkRepository : RepositoryBase<Walk>, IWalkRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WalkRepository"/> class.
        /// </summary>
        /// <remarks>
        /// A new <see cref="DogWalkingContext"/> is created and passed to the base repository.
        /// </remarks>
        public WalkRepository() : base(new DogWalkingContext())
        {
        }

        /// <summary>
        /// Retrieves all <see cref="Walk"/> entities from the data store.
        /// </summary>
        /// <returns>
        /// A <see cref="List{Walk}"/> containing all walks. Entities are loaded using no-tracking for read-only scenarios.
        /// </returns>
        public List<Walk> GetAll()
        {
            return _context.Walks
                .AsNoTracking()
                .ToList();
        }

        /// <summary>
        /// Searches for walks where the associated dog's name contains the specified search term.
        /// </summary>
        /// <param name="searchTerm">The substring to search for in the <see cref="Dog.Name"/> property.</param>
        /// <returns>
        /// A <see cref="List{Walk}"/> of walks whose associated dog's name contains <paramref name="searchTerm"/>.
        /// The query is executed with no-tracking.
        /// </returns>
        /// <remarks>
        /// The search uses <see cref="string.Contains(string)"/> on <see cref="Dog.Name"/>; passing a null <paramref name="searchTerm"/> will result in a <see cref="System.ArgumentNullException"/> or a runtime error depending on the provider.
        /// </remarks>
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
        /// Deletes the <see cref="Walk"/> entity with the specified identifier.
        /// </summary>
        /// <param name="walkId">The identifier of the walk to delete.</param>
        /// <remarks>
        /// If no entity with the given <paramref name="walkId"/> exists, no action is taken.
        /// </remarks>
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