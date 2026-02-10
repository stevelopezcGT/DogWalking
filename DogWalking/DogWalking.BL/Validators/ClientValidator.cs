using DogWalking.BL.DTOs;
using System;

namespace DogWalking.BL.Validators
{
    /// <summary>
    /// Validates client data.
    /// </summary>
    public static class ClientValidator
    {
        /// <summary>
        /// Validates a client DTO.
        /// </summary>
        /// <param name="dto">Client data.</param>
        public static void Validate(ClientDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new ArgumentException("Client name is required.");

            if (string.IsNullOrWhiteSpace(dto.Phone))
                throw new ArgumentException("Phone number is required.");
        }
    }
}
