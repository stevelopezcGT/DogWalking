using DogWalking.BL.DTOs;
using System;

namespace DogWalking.BL.Validators
{
    /// <summary>
    /// Validates login data.
    /// </summary>
    public static class LoginValidator
    {
        /// <summary>
        /// Validates a login DTO.
        /// </summary>
        /// <param name="dto">Login data.</param>
        public static void Validate(LoginDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            if (string.IsNullOrWhiteSpace(dto.Username))
                throw new ArgumentException("Username is required.");

            if (string.IsNullOrWhiteSpace(dto.Password))
                throw new ArgumentException("Password is required.");
        }
    }
}
