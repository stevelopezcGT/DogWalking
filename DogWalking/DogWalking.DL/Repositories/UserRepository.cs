using DogWalking.DL.Context;
using DogWalking.DL.Entities;
using DogWalking.DL.Repositories.Base;
using System.Linq;

namespace DogWalking.DL.Repositories
{
    /// <summary>
    /// Repository implementation for users.
    /// </summary>
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        /// <summary>
        /// Initializes a new instance of <see cref="UserRepository"/>.
        /// </summary>
        public UserRepository() : base(new DogWalkingContext())
        {
        }

        /// <summary>
        /// Gets a user by credentials.
        /// </summary>
        /// <param name="username">Username.</param>
        /// <param name="password">Password.</param>
        /// <returns>Matching user or <c>null</c>.</returns>
        public User GetByCredentials(string username, string password)
        {
            return _context
                .Users
                .FirstOrDefault(u =>
                    u.Username == username &&
                    u.Password == password);
        }
    }
}
