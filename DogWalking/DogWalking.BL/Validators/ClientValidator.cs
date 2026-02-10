using DogWalking.BL.DTOs;
using System;

namespace DogWalking.BL.Validators
{
    /// <summary>
    /// Provides validation logic for <see cref="ClientDto"/> instances.
    /// </summary>
    /// <remarks>
    /// This static validator enforces that required client properties are present
    /// before a client is processed or persisted.
    /// </remarks>
    public static class ClientValidator
    {
        /// <summary>
        /// Validates the specified <paramref name="dto"/> to ensure required client
        /// properties are populated.
        /// </summary>
        /// <param name="dto">The <see cref="ClientDto"/> to validate. The instance is expected to be non-null.</param>
        /// <exception cref="ArgumentException">
        /// Thrown when the client's <see cref="ClientDto.Name"/> or <see cref="ClientDto.Phone"/>
        /// is null, empty, or consists only of whitespace.
        /// </exception>
        /// <exception cref="NullReferenceException">
        /// May be thrown if <paramref name="dto"/> is <c>null</c> because the current
        /// implementation accesses its properties without a null check.
        /// </exception>
        public static void Validate(ClientDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new ArgumentException("Client name is required.");

            if (string.IsNullOrWhiteSpace(dto.Phone))
                throw new ArgumentException("Phone number is required.");
        }
    }
}