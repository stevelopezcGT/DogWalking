using DogWalking.DL.Context;
using DogWalking.DL.Entities;
using DogWalking.DL.Repositories.Base;
using System.Linq;

namespace DogWalking.DL.Repositories
{
    /// <summary>
    /// Repository class for performing data access operations for <see cref="User"/> entities.
    /// Inherits the generic repository functionality from <see cref="RepositoryBase{T}"/>.
    /// </summary>
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class.
        /// </summary>
        /// <remarks>
        /// This constructor creates a new <see cref="DogWalkingContext"/> instance and passes it
        /// to the base <see cref="RepositoryBase{T}"/> constructor. Consumers should prefer
        /// dependency injection in higher-level components when possible to manage context lifetime.
        /// </remarks>
        public UserRepository() : base(new DogWalkingContext())
        {
        }

        /// <summary>
        /// Retrieves a <see cref="User"/> by the supplied credentials.
        /// </summary>
        /// <param name="username">The username to match.</param>
        /// <param name="password">The password to match.</param>
        /// <returns>
        /// The matching <see cref="User"/> if a user with the specified <paramref name="username"/>
        /// and <paramref name="password"/> exists; otherwise, <c>null</c>.
        /// </returns>
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