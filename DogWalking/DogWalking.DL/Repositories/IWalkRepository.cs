using DogWalking.DL.Entities;
using System.Collections.Generic;

namespace DogWalking.DL.Repositories
{
    /// <summary>
    /// Defines operations for storing and retrieving <see cref="Walk"/> entities.
    /// </summary>
    public interface IWalkRepository
    {
        /// <summary>
        /// Adds a new <see cref="Walk"/> to the repository.
        /// </summary>
        /// <param name="walk">The <see cref="Walk"/> instance to add. Must not be null.</param>
        void Add(Walk walk);

        /// <summary>
        /// Retrieves all <see cref="Walk"/> entities from the repository.
        /// </summary>
        /// <returns>A <see cref="List{Walk}"/> containing all stored walks. The list may be empty if no walks exist.</returns>
        List<Walk> GetAll();

        /// <summary>
        /// Searches for <see cref="Walk"/> entities that match the specified search term.
        /// </summary>
        /// <param name="searchTerm">The text used to filter walks. Interpretation (e.g., by name, description, or other fields) is implementation-defined; a null or empty value may return no results or all items depending on implementation.</param>
        /// <returns>A <see cref="List{Walk}"/> of walks that match the search criteria. The list may be empty if no matches are found.</returns>
        List<Walk> Search(string searchTerm);

        /// <summary>
        /// Deletes the <see cref="Walk"/> with the specified identifier from the repository.
        /// </summary>
        /// <param name="walkId">The identifier of the walk to delete.</param>
        void Delete(int walkId);
    }
}