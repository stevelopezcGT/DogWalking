using DogWalking.BL.DTOs;
using System;

namespace DogWalking.BL.Validators
{
    /// <summary>
    /// Validates dog data.
    /// </summary>
    public static class DogValidator
    {
        /// <summary>
        /// Validates a dog DTO.
        /// </summary>
        /// <param name="dto">Dog data.</param>
        public static void Validate(DogDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new ArgumentException("Dog name is required.");

            if (dto.Age <= 0)
                throw new ArgumentException("Dog age must be greater than zero.");
        }
    }
}
