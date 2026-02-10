using DogWalking.DL.Context;

namespace DogWalking.DL.Repositories.Base
{
    /// <summary>
    /// Base repository implementation.
    /// </summary>
    /// <typeparam name="T">Entity type.</typeparam>
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected readonly DogWalkingContext _context;

        /// <summary>
        /// Initializes a new instance of <see cref="RepositoryBase{T}"/>.
        /// </summary>
        /// <param name="context">Database context.</param>
        protected RepositoryBase(DogWalkingContext context)
        {
            _context = context ?? throw new System.ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Adds an entity and saves changes.
        /// </summary>
        /// <param name="entity">Entity to add.</param>
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
        /// Removes an entity and saves changes.
        /// </summary>
        /// <param name="entity">Entity to remove.</param>
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
