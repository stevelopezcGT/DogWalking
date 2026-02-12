using DogWalking.DL.Entities;
using System.Collections.Generic;

namespace DogWalking.DL.Repositories
{
    /// <summary>
    /// Repository contract for clients.
    /// </summary>
    public interface IClientRepository
    {
        /// <summary>
        /// Adds a client.
        /// </summary>
        /// <param name="client">Client to add.</param>
        void Add(Client client);

        /// <summary>
        /// Gets all clients.
        /// </summary>
        /// <returns>List of clients.</returns>
        List<Client> GetAll();

        /// <summary>
        /// Searches clients by term.
        /// </summary>
        /// <param name="searchTerm">Search term.</param>
        /// <returns>Matching clients.</returns>
        List<Client> Search(string searchTerm);

        /// <summary>
        /// Deletes a client by id.
        /// </summary>
        /// <param name="clientId">Client id.</param>
        void Delete(int clientId);

        /// <summary>
        /// Gets a client by id.
        /// </summary>
        /// <param name="clientId">Client id.</param>
        /// <returns>Matching client or <c>null</c>.</returns>
        Client GetById(int clientId);

        /// <summary>
        /// Updates a client.
        /// </summary>
        /// <param name="client">Client to update.</param>
        void Update(Client client);
    }
}
