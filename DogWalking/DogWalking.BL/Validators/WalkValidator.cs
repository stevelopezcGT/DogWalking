using DogWalking.BL.DTOs;
using System;

namespace DogWalking.BL.Validators
{
    /// <summary>
    /// Provides validation helpers for <see cref="WalkDto"/>.
    /// </summary>
    /// <remarks>
    /// This static validator checks that required properties on a <see cref="WalkDto"/> instance
    /// meet basic business rules. It throws <see cref="ArgumentException"/> when validation fails.
    /// </remarks>
    public static class WalkValidator
    {
        /// <summary>
        /// Validates the specified <see cref="WalkDto"/>.
        /// </summary>
        /// <param name="dto">
        /// The <see cref="WalkDto"/> to validate. The caller is expected to pass a non-null instance.
        /// The validator checks the <see cref="DogWalking.BL.DTOs.WalkDto.WalkDate"/> and
        /// <see cref="DogWalking.BL.DTOs.WalkDto.DurationMinutes"/> properties.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Thrown when the walk date is not provided (<c>dto.WalkDate == default</c>) or when the
        /// duration is not greater than zero (<c>dto.DurationMinutes &lt;= 0</c>).
        /// </exception>
        public static void Validate(WalkDto dto)
        {
            if (dto.WalkDate == default)
                throw new ArgumentException("Walk date is required.");

            if (dto.DurationMinutes <= 0)
                throw new ArgumentException("Walk duration must be greater than zero.");
        }
    }
}