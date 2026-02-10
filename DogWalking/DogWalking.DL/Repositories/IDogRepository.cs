using DogWalking.DL.Entities;
using System.Collections.Generic;

namespace DogWalking.DL.Repositories
{
    /// <summary>
    /// Defines repository operations for <see cref="Dog"/> entities.
    /// Implementations are responsible for persistence and retrieval of dogs.
    /// </summary>
    public interface IDogRepository
    {
        /// <summary>
        /// Adds the provided <see cref="Dog"/> to the repository.
        /// </summary>
        /// <param name="dog">The <see cref="Dog"/> instance to add. Implementations may throw <see cref="System.ArgumentNullException"/> if <paramref name="dog"/> is null.</param>
        void Add(Dog dog);

        /// <summary>
        /// Retrieves all <see cref="Dog"/> entities from the repository.
        /// </summary>
        /// <returns>
        /// A <see cref="List{Dog}"/> containing all dogs. Implementations should return an empty list if no dogs exist rather than null.
        /// </returns>
        List<Dog> GetAll();

        /// <summary>
        /// Searches the repository for <see cref="Dog"/> entities that match the specified search term.
        /// </summary>
        /// <param name="searchTerm">
        /// The term to search for. Interpretation is implementation-specific (for example: name, breed, or other fields).
        /// If <c>null</c> or empty, implementations may return all dogs.
        /// </param>
        /// <returns>
        /// A <see cref="List{Dog}"/> containing matching dogs. Implementations should return an empty list if no matches are found rather than null.
        /// </returns>
        List<Dog> Search(string searchTerm);

        /// <summary>
        /// Deletes the dog with the specified identifier from the repository.
        /// </summary>
        /// <param name="dogId">The identifier of the dog to delete.</param>
        /// <remarks>
        /// Implementations may choose to do nothing if the <paramref name="dogId"/> does not exist.
        /// </remarks>
        void Delete(int dogId);
    }
}