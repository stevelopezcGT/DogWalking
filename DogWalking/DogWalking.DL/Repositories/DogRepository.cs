using DogWalking.DL.Context;
using DogWalking.DL.Entities;
using DogWalking.DL.Repositories.Base;
using System.Collections.Generic;
using System.Linq;

namespace DogWalking.DL.Repositories
{
    /// <summary>
    /// Repository for performing data operations against <see cref="Dog"/> entities.
    /// Encapsulates access to the <see cref="DogWalkingContext"/> and exposes methods
    /// for retrieving, searching and deleting dog records.
    /// </summary>
    public class DogRepository : RepositoryBase<Dog>, IDogRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DogRepository"/> class.
        /// A new instance of <see cref="DogWalkingContext"/> is created and passed to the base repository.
        /// </summary>
        public DogRepository() : base(new DogWalkingContext())
        {
        }

        /// <summary>
        /// Retrieves all dogs from the data store.
        /// </summary>
        /// <returns>
        /// A list of <see cref="Dog"/> entities representing all dogs in the data store.
        /// Returned entities are loaded with no tracking (detached from the context).
        /// </returns>
        public List<Dog> GetAll()
        {
            return _context.Dogs
                .AsNoTracking()
                .ToList();
        }

        /// <summary>
        /// Searches for dogs whose name or their client's name contains the provided search term.
        /// </summary>
        /// <param name="searchTerm">The term to search for within dog names and client names.</param>
        /// <returns>
        /// A list of <see cref="Dog"/> entities that match the search criteria.
        /// Returned entities are loaded with no tracking (detached from the context).
        /// </returns>
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
        /// Deletes the dog with the specified identifier from the data store.
        /// </summary>
        /// <param name="dogId">The identifier of the dog to delete.</param>
        /// <remarks>
        /// If no dog with the specified identifier exists, the method performs no action.
        /// The actual deletion is delegated to the base repository <see cref="RepositoryBase{T}.Remove"/>.
        /// </remarks>
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