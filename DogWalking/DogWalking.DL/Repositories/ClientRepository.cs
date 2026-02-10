using DogWalking.DL.Context;
using DogWalking.DL.Entities;
using DogWalking.DL.Repositories.Base;
using System.Collections.Generic;
using System.Linq;

namespace DogWalking.DL.Repositories
{
    /// <summary>
    /// Repository implementation for clients.
    /// </summary>
    public class ClientRepository : RepositoryBase<Client>, IClientRepository
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ClientRepository"/>.
        /// </summary>
        public ClientRepository() : base(new DogWalkingContext())
        {
        }

        /// <summary>
        /// Gets all clients.
        /// </summary>
        /// <returns>List of clients.</returns>
        public List<Client> GetAll()
        {
            return _context.Clients
                .AsNoTracking()
                .ToList();
        }

        /// <summary>
        /// Searches clients by term.
        /// </summary>
        /// <param name="searchTerm">Search term.</param>
        /// <returns>Matching clients.</returns>
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
        /// Deletes a client by id.
        /// </summary>
        /// <param name="clientId">Client id.</param>
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
