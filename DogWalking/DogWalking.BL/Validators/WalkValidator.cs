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
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            if (dto.DogId <= 0)
                throw new ArgumentException("Dog is required.");

            if (dto.WalkDate == default)
                throw new ArgumentException("Walk date is required.");

            if (dto.DurationMinutes <= 0 || dto.DurationMinutes > 480)
                throw new ArgumentException("Walk duration must be between 1 and 480 minutes.");
        }
    }
}
