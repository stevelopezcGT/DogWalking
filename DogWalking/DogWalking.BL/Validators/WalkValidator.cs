using DogWalking.BL.DTOs;
using System;

namespace DogWalking.BL.Validators
{
    /// <summary>
    /// Validates walk data.
    /// </summary>
    public static class WalkValidator
    {
        /// <summary>
        /// Validates a walk DTO.
        /// </summary>
        /// <param name="dto">Walk data.</param>
        public static void Validate(WalkDto dto)
        {
            if (dto.WalkDate == default)
                throw new ArgumentException("Walk date is required.");

            if (dto.DurationMinutes <= 0)
                throw new ArgumentException("Walk duration must be greater than zero.");
        }
    }
}
