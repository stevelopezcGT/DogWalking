using DogWalking.BL.DTOs;
using DogWalking.BL.Validators;
using DogWalking.DL.Repositories;

namespace DogWalking.BL.Services
{
    /// <summary>
    /// Provides authentication related operations.
    /// </summary>
    /// <remarks>
    /// This service is responsible for validating credentials and delegating
    /// the user lookup to the injected <see cref="IUserRepository"/>.
    /// </remarks>
    public class AuthService
    {
        /// <summary>
        /// Repository used to query users and their credentials.
        /// </summary>
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthService"/> class.
        /// </summary>
        /// <param name="userRepository">The user repository used by this service. Must not be <c>null</c>.</param>
        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// Attempts to authenticate a user by validating the supplied <paramref name="dto"/>
        /// and checking the repository for a matching user record.
        /// </summary>
        /// <param name="dto">The login data transfer object containing username and password.</param>
        /// <returns><c>true</c> if a user with the provided credentials exists; otherwise, <c>false</c>.</returns>
        /// <exception cref="System.ArgumentNullException">
        /// Thrown when <paramref name="dto"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="System.Exception">
        /// Propagates any exception thrown by <see cref="LoginValidator.Validate(LoginDto)"/> or the repository.
        /// </exception>
        public bool Login(LoginDto dto)
        {
            LoginValidator.Validate(dto);

            return _userRepository
                .GetByCredentials(dto.Username, dto.Password) != null;
        }
    }
}