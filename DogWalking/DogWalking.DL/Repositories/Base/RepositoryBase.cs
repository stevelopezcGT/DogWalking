using DogWalking.Common;
using DogWalking.DL.Context;
using DogWalking.DL.Entities;
using System;
using System.Data.Entity;
using System.Linq;

namespace DogWalking.DL.Repositories.Base
{
    /// <summary>
    /// Base repository implementation.
    /// </summary>
    /// <typeparam name="T">Entity type.</typeparam>
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : BaseEntity
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
        /// Gets the current user for audit fields.
        /// </summary>
        /// <returns>Current user identifier.</returns>
        protected virtual string GetCurrentUser()
        {
            return AppSession.CurrentUsername ?? "system";
        }

        /// <summary>
        /// Returns a base query applying soft-delete filtering (IsActive = true).
        /// </summary>
        /// <returns>Queryable active entities.</returns>
        protected IQueryable<T> Query()
        {
            return _context.Set<T>().Where(e => e.IsActive);
        }

        /// <summary>
        /// Gets an entity by id.
        /// </summary>
        /// <param name="id">Entity id.</param>
        /// <returns>Matching entity or <c>null</c>.</returns>
        public virtual T GetById(int id)
        {
            return Query().SingleOrDefault(e => e.Id == id);
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

            // Automatically sets creation audit fields before persisting.
            entity.CreatedAt = DateTime.UtcNow;
            entity.CreatedBy = GetCurrentUser();
            entity.IsActive = true;

            _context.Set<T>().Add(entity);
            _context.SaveChanges();
        }

        /// <summary>
        /// Updates an entity and saves changes.
        /// </summary>
        /// <param name="entity">Entity to update.</param>
        public virtual void Update(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            // Automatically updates modification audit fields before persisting.
            entity.UpdatedAt = DateTime.UtcNow;
            entity.UpdatedBy = GetCurrentUser();

            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        /// <summary>
        /// Soft deletes an entity by marking it inactive and updating audit fields.
        /// </summary>
        /// <param name="entity">Entity to soft delete.</param>
        public virtual void SoftDelete(T entity)
        {
            if (entity == null)
            {
                throw new System.ArgumentNullException(nameof(entity));
            }

            // Performs soft delete by deactivating the row and stamping update audit fields.
            entity.IsActive = false;
            entity.UpdatedAt = DateTime.UtcNow;
            entity.UpdatedBy = GetCurrentUser();

            _context.SaveChanges();
        }
    }
}
