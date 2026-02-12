using DogWalking.DL.Context;
using DogWalking.DL.Entities;
using DogWalking.DL.Repositories.Base;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DogWalking.DL.Repositories
{
    /// <summary>
    /// Repository implementation for walks.
    /// </summary>
    public class WalkRepository : RepositoryBase<Walk>, IWalkRepository
    {
        /// <summary>
        /// Initializes a new instance of <see cref="WalkRepository"/>.
        /// </summary>
        /// <param name="ctx">Database context.</param>
        public WalkRepository(DogWalkingContext ctx) : base(ctx)
        {
        }

        /// <summary>
        /// Gets all walks.
        /// </summary>
        /// <returns>List of walks.</returns>
        public List<Walk> GetAll()
        {
            return Query()
                .Include(w => w.Dog)
                .Include(w => w.Dog.Client)
                .AsNoTracking()
                .ToList();
        }

        /// <summary>
        /// Searches walks by dog name.
        /// </summary>
        /// <param name="term">Search term.</param>
        /// <returns>Matching walks.</returns>
        public List<Walk> Search(string term)
        {
            return Query()
                .Include(w => w.Dog)
                .Include(w => w.Dog.Client)
                .AsNoTracking()
                .Where(w => w.Dog.Name.Contains(term) && w.Dog.IsActive)
                .ToList();
        }

        /// <summary>
        /// Gets a walk by id.
        /// </summary>
        /// <param name="walkId">Walk id.</param>
        /// <returns>Matching walk or <c>null</c>.</returns>
        public override Walk GetById(int walkId)
        {
            return Query()
                .Include(w => w.Dog)
                .Include(w => w.Dog.Client)
                .SingleOrDefault(w => w.Id == walkId);
        }

        /// <summary>
        /// Gets walks by dog id.
        /// </summary>
        /// <param name="dogId">Dog id.</param>
        /// <returns>List of walks for the dog.</returns>
        public List<Walk> GetByDog(int dogId)
        {
            return Query()
                .AsNoTracking()
                .Where(d => d.DogId == dogId)
                .ToList();
        }

        /// <summary>
        /// Deletes a walk by id.
        /// </summary>
        /// <param name="walkId">Walk id.</param>
        public void Delete(int walkId)
        {
            var entity = GetById(walkId);
            if (entity != null)
            {
                SoftDelete(entity);
            }
        }
    }
}
