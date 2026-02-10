using DogWalking.DL.Context;

namespace DogWalking.DL.Repositories.Base
{
    /// <summary>
    /// Provides a base repository implementation for entity types.
    /// </summary>
    /// <typeparam name="T">The type of the entity handled by the repository. Must be a reference type.</typeparam>
    /// <remarks>
    /// Derived repository classes can extend this base to expose additional query and persistence operations.
    /// This base implementation uses the provided <see cref="DogWalkingContext"/> to perform data access.
    /// </remarks>
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        /// <summary>
        /// The Entity Framework database context used by the repository.
        /// </summary>
        protected readonly DogWalkingContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryBase{T}"/> class.
        /// </summary>
        /// <param name="context">The <see cref="DogWalkingContext"/> instance used for data access. Must not be <c>null</c>.</param>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="context"/> is <c>null</c>.</exception>
        protected RepositoryBase(DogWalkingContext context)
        {
            _context = context ?? throw new System.ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Adds the specified entity to the context and persists changes to the database.
        /// </summary>
        /// <param name="entity">The entity to add. Must not be <c>null</c>.</param>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="entity"/> is <c>null</c>.</exception>
        /// <exception cref="System.InvalidOperationException">May be thrown by the underlying context when the operation fails.</exception>
        /// <remarks>
        /// This method attaches the entity to the context in the Added state and then calls <see cref="DogWalkingContext.SaveChanges"/>.
        /// Consumers should consider transaction scope if multiple operations must be persisted atomically.
        /// </remarks>
        public virtual void Add(T entity)
        {
            if (entity == null)
            {
                throw new System.ArgumentNullException(nameof(entity));
            }

            _context.Set<T>().Add(entity);
            _context.SaveChanges();
        }

        /// <summary>
        /// Removes the specified entity from the context and persists changes to the database.
        /// </summary>
        /// <param name="entity">The entity to remove. Must not be <c>null</c>.</param>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="entity"/> is <c>null</c>.</exception>
        /// <exception cref="System.InvalidOperationException">May be thrown by the underlying context when the operation fails.</exception>
        /// <remarks>
        /// This method marks the entity for deletion and then calls <see cref="DogWalkingContext.SaveChanges"/>.
        /// Ensure the entity is attached to the context or obtainable by key before calling this method.
        /// </remarks>
        public virtual void Remove(T entity)
        {
            if (entity == null)
            {
                throw new System.ArgumentNullException(nameof(entity));
            }

            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }
    }
}