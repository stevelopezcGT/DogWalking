namespace DogWalking.DL.Repositories.Base
{
    /// <summary>
    /// Defines a minimal base repository contract for managing entities of type <typeparamref name="T"/>.
    /// Implementations are expected to provide the underlying persistence behavior for adding and
    /// removing entities. This interface is intentionally minimal to serve as a common base for
    /// repository types within the data layer.
    /// </summary>
    /// <typeparam name="T">The entity type the repository manages. Must be a reference type.</typeparam>
    internal interface IRepositoryBase<T> where T : class
    {
        /// <summary>
        /// Adds the specified <paramref name="entity"/> to the underlying data store or change tracker.
        /// Implementations should ensure the entity is prepared for persistence (for example, attached
        /// to the context or marked for insertion).
        /// </summary>
        /// <param name="entity">The entity to add. Must not be <c>null</c>.</param>
        void Add(T entity);

        /// <summary>
        /// Removes the specified <paramref name="entity"/> from the underlying data store or change tracker.
        /// Implementations should ensure the entity is correctly removed or marked for deletion.
        /// </summary>
        /// <param name="entity">The entity to remove. Must not be <c>null</c>.</param>
        void Remove(T entity);
    }
}