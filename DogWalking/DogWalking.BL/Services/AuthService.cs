using DogWalking.BL.DTOs;
using DogWalking.BL.Validators;
using DogWalking.DL.Repositories;
using System;

namespace DogWalking.BL.Services
{
    /// <summary>
    /// Service for authentication operations.
    /// </summary>
    public class AuthService
    {
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// Initializes a new instance of <see cref="AuthService"/>.
        /// </summary>
        /// <param name="userRepository">User repository instance.</param>
        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        /// <summary>
        /// Validates credentials and checks whether a user exists.
        /// </summary>
        /// <param name="dto">Login data.</param>
        /// <returns><c>true</c> when credentials match an existing user; otherwise <c>false</c>.</returns>
        public bool Login(LoginDto dto)
        {
            LoginValidator.Validate(dto);

            var user = _userRepository.GetByCredentials(dto.Username, dto.Password);
            return user != null;
        }
    }
}
