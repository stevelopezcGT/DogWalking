using DogWalking.DL.Context;
using DogWalking.DL.Entities;
using DogWalking.DL.Repositories.Base;
using System.Collections.Generic;
using System.Linq;

namespace DogWalking.DL.Repositories
{
    /// <summary>
    /// Repository providing data access operations for <see cref="Client"/> entities.
    /// </summary>
    /// <remarks>
    /// Inherits common CRUD behavior from <see cref="RepositoryBase{Client}"/>.
    /// This repository constructs its own <see cref="DogWalkingContext"/> instance.
    /// Consumers should be aware that the context's lifetime is managed by this repository
    /// and that changes are not persisted until <c>SaveChanges</c> is invoked on the context
    /// (typically through repository base methods or an external unit of work).
    /// </remarks>
    public class ClientRepository : RepositoryBase<Client>, IClientRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ClientRepository"/> class
        /// using a new instance of <see cref="DogWalkingContext"/>.
        /// </summary>
        public ClientRepository() : base(new DogWalkingContext())
        {
        }

        /// <summary>
        /// Retrieves all clients from the data store.
        /// </summary>
        /// <returns>
        /// A <see cref="List{T}"/> of <see cref="Client"/> containing all clients.
        /// Entities are returned with no change tracking applied.
        /// </returns>
        public List<Client> GetAll()
        {
            return _context.Clients
                .AsNoTracking()
                .ToList();
        }

        /// <summary>
        /// Searches for clients whose name contains the specified search term
        /// or who have associated dogs whose names contain the specified term.
        /// </summary>
        /// <param name="searchTerm">The substring to search for in client names and dog names.</param>
        /// <returns>
        /// A <see cref="List{T}"/> of <see cref="Client"/> that match the search criteria.
        /// Matching is performed using <c>Contains</c> and therefore follows the database collation rules
        /// (case-sensitivity and culture-specific behavior depend on the database configuration).
        /// </returns>
        public List<Client> Search(string searchTerm)
        {
            return _context.Clients
                .AsNoTracking()
                .Where(c =>
                    c.Name.Contains(searchTerm) ||
                    c.Dogs.Any(d => d.Name.Contains(searchTerm))
                )
                .ToList();
        }

        /// <summary>
        /// Deletes the client with the specified identifier from the context if it exists.
        /// </summary>
        /// <param name="clientId">The identifier of the client to delete.</param>
        /// <remarks>
        /// This method removes the entity from the context but does not persist changes to the database.
        /// Callers must ensure that <c>SaveChanges</c> is invoked (directly or via a unit of work) to commit the deletion.
        /// </remarks>
        public void Delete(int clientId)
        {
            var entity = _context.Clients.Find(clientId);
            if (entity != null)
            {
                Remove(entity);
            }
        }
    }
}