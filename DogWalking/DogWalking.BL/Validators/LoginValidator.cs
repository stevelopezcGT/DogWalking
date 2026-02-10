using DogWalking.BL.DTOs;
using System;

namespace DogWalking.BL.Validators
{
    /// <summary>
    /// Provides validation logic for login requests.
    /// </summary>
    /// <remarks>
    /// This static validator performs basic non-empty checks on the provided
    /// <see cref="LoginDto"/>. It throws <see cref="ArgumentException"/> when
    /// required fields are missing or consist only of whitespace.
    /// </remarks>
    public static class LoginValidator
    {
        /// <summary>
        /// Validates the specified <see cref="LoginDto"/>.
        /// </summary>
        /// <param name="dto">The login data transfer object to validate. Must not be <c>null</c>.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="dto"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException">
        /// Thrown when required properties on the <paramref name="dto"/> are missing or contain only whitespace:
        /// - "Username is required." if <see cref="LoginDto.Username"/> is null, empty, or whitespace.
        /// - "Password is required." if <see cref="LoginDto.Password"/> is null, empty, or whitespace.
        /// </exception>
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