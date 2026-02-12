using DogWalking.DL.Entities;
using System.Collections.Generic;

namespace DogWalking.DL.Repositories
{
    /// <summary>
    /// Repository contract for walks.
    /// </summary>
    public interface IWalkRepository
    {
        /// <summary>
        /// Adds a walk.
        /// </summary>
        /// <param name="walk">Walk to add.</param>
        void Add(Walk walk);

        /// <summary>
        /// Gets all walks.
        /// </summary>
        /// <returns>List of walks.</returns>
        List<Walk> GetAll();

        /// <summary>
        /// Searches walks by term.
        /// </summary>
        /// <param name="term">Search term by dog name.</param>
        /// <returns>Matching walks.</returns>
        List<Walk> Search(string term);

        /// <summary>
        /// Deletes a walk by id.
        /// </summary>
        /// <param name="walkId">Walk id.</param>
        void Delete(int walkId);

        /// <summary>
        /// Gets walks by dog id.
        /// </summary>
        /// <param name="dogId">Dog id.</param>
        /// <returns>List of walks for the dog.</returns>
        List<Walk> GetByDog(int dogId);

        /// <summary>
        /// Gets a walk by id.
        /// </summary>
        /// <param name="walkId">Walk id.</param>
        /// <returns>Matching walk or <c>null</c>.</returns>
        Walk GetById(int walkId);

        /// <summary>
        /// Updates a walk.
        /// </summary>
        /// <param name="walk">Walk to update.</param>
        void Update(Walk walk);
    }
}
