using DogWalking.DL.Entities;

namespace DogWalking.DL.Repositories
{
    /// <summary>
    /// Defines repository operations for user entities.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Retrieves a <see cref="User"/> that matches the specified credentials.
        /// </summary>
        /// <param name="username">The username to locate. This is typically the user's login identifier.</param>
        /// <param name="password">The password to validate for the specified <paramref name="username"/>.</param>
        /// <returns>
        /// The <see cref="User"/> that matches the supplied credentials, or <c>null</c> if no matching user is found
        /// or the credentials are invalid.
        /// </returns>
        User GetByCredentials(string username, string password);
    }
}