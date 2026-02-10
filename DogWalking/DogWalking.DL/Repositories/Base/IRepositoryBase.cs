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
        /// Removes an entity.
        /// </summary>
        /// <param name="entity">Entity to remove.</param>
        void Remove(T entity);
    }
}
