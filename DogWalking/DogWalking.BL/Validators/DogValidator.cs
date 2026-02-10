using DogWalking.BL.DTOs;
using System;

namespace DogWalking.BL.Validators
{
    /// <summary>
    /// Provides validation logic for <see cref="DogDto"/> instances.
    /// </summary>
    /// <remarks>
    /// This static validator throws <see cref="ArgumentException"/> when validation rules fail.
    /// The method preserves existing behavior and does not perform a null check on <paramref name="dto"/>.
    /// Callers should ensure the <paramref name="dto"/> instance is not null before calling <see cref="Validate"/>.
    /// </remarks>
    public static class DogValidator
    {
        /// <summary>
        /// Validates the specified <see cref="DogDto"/> instance.
        /// </summary>
        /// <param name="dto">The dog data transfer object to validate.</param>
        /// <exception cref="ArgumentException">
        /// Thrown when <see cref="DogDto.Name"/> is null, empty, or whitespace,
        /// or when <see cref="DogDto.Age"/> is less than or equal to zero.
        /// </exception>
        /// <exception cref="NullReferenceException">
        /// May be thrown if <paramref name="dto"/> is <c>null</c> because this method does not guard against null inputs.
        /// </exception>
        public static void Validate(DogDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new ArgumentException("Dog name is required.");

            if (dto.Age <= 0)
                throw new ArgumentException("Dog age must be greater than zero.");
        }
    }
}