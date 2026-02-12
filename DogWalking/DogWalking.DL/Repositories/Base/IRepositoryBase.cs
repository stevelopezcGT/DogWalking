using System.Linq;

namespace DogWalking.DL.Repositories.Base
{
    /// <summary>
    /// Base repository contract.
    /// </summary>
    /// <typeparam name="T">Entity type.</typeparam>
    internal interface IRepositoryBase<T> where T : class
    {

        /// <summary>
        /// Adds an entity.
        /// </summary>
        /// <param name="entity">Entity to add.</param>
        void Add(T entity);

        /// <summary>
        /// Updates an entity.
        /// </summary>
        /// <param name="entity">Entity to update.</param>
        void Update(T entity);

        /// <summary>
        /// Removes an entity.
        /// </summary>
        /// <param name="entity">Entity to remove.</param>
        void SoftDelete(T entity);

        /// <summary>
        /// Gets an entity by id.
        /// </summary>
        /// <param name="id">Entity id.</param>
        /// <returns>Matching entity or <c>null</c>.</returns>
        T GetById(int id);


    }
}
