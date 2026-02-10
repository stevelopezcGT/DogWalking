using DogWalking.DL.Entities;
using System.Collections.Generic;

namespace DogWalking.DL.Repositories
{
    /// <summary>
    /// Defines data access operations for <see cref="Client"/> entities.
    /// </summary>
    /// <remarks>
    /// Implementations are responsible for persisting, retrieving and removing
    /// client records from the underlying data store.
    /// </remarks>
    public interface IClientRepository
    {
        /// <summary>
        /// Adds a new <see cref="Client"/> to the data store.
        /// </summary>
        /// <param name="client">The client to add. The implementation may validate this argument and throw an exception if it is null or invalid.</param>
        void Add(Client client);

        /// <summary>
        /// Retrieves all clients from the data store.
        /// </summary>
        /// <returns>
        /// A <see cref="List{Client}"/> containing all clients. Implementations should return an empty list if no clients exist; this method will not return <c>null</c>.
        /// </returns>
        List<Client> GetAll();

        /// <summary>
        /// Searches for clients that match the provided search term.
        /// </summary>
        /// <param name="searchTerm">
        /// The term to search for. Interpretation (e.g. name, email, address) is implementation-specific.
        /// If <c>null</c> or empty, implementations may return all clients or an empty list depending on repository semantics.
        /// </param>
        /// <returns>
        /// A <see cref="List{Client}"/> of clients that match the search criteria. Implementations should return an empty list if there are no matches; this method will not return <c>null</c>.
        /// </returns>
        List<Client> Search(string searchTerm);

        /// <summary>
        /// Deletes the client with the specified identifier from the data store.
        /// </summary>
        /// <param name="clientId">The identifier of the client to delete.</param>
        /// <remarks>
        /// Implementations should handle the case where the client does not exist (silent no-op or throw, depending on implementation choice).
        /// </remarks>
        void Delete(int clientId);
    }
}