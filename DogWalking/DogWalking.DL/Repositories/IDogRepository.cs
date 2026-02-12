using DogWalking.DL.Entities;
using System.Collections.Generic;

namespace DogWalking.DL.Repositories
{
    /// <summary>
    /// Repository contract for dogs.
    /// </summary>
    public interface IDogRepository
    {
        /// <summary>
        /// Adds a dog.
        /// </summary>
        /// <param name="dog">Dog to add.</param>
        void Add(Dog dog);

        /// <summary>
        /// Gets all dogs.
        /// </summary>
        /// <returns>List of dogs.</returns>
        List<Dog> GetAll();

        /// <summary>
        /// Gets dogs by client id.
        /// </summary>
        /// <param name="clientId">Client id.</param>
        /// <returns>List of dogs for the client.</returns>
        List<Dog> GetByClient(int clientId);

        /// <summary>
        /// Searches dogs by term.
        /// </summary>
        /// <param name="searchTerm">Search term.</param>
        /// <returns>Matching dogs.</returns>
        List<Dog> Search(string searchTerm);

        /// <summary>
        /// Deletes a dog by id.
        /// </summary>
        /// <param name="dogId">Dog id.</param>
        void Delete(int dogId);

        /// <summary>
        /// Gets a dog by id.
        /// </summary>
        /// <param name="dogId">Dog id.</param>
        /// <returns>Matching dog or <c>null</c>.</returns>
        Dog GetById(int dogId);
    }
}
