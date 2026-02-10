using DogWalking.DL.Entities;

namespace DogWalking.DL.Repositories
{
    /// <summary>
    /// Repository contract for users.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Gets a user by credentials.
        /// </summary>
        /// <param name="username">Username.</param>
        /// <param name="password">Password.</param>
        /// <returns>Matching user or <c>null</c>.</returns>
        User GetByCredentials(string username, string password);
    }
}
