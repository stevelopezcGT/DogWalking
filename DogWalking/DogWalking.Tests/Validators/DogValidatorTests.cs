using DogWalking.BL.DTOs;
using DogWalking.BL.Validators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DogWalking.Tests.Validators
{
    /// <summary>
    /// Unit tests for <see cref="DogValidator"/>.
    /// </summary>
    [TestClass]
    public class DogValidatorTests
    {
        /// <summary>
        /// Creates a DTO with valid values.
        /// </summary>
        private static DogDto CreateValidDto()
        {
            return new DogDto
            {
                ClientId = 1,
                Name = "Rex",
                Breed = "Labrador",
                Age = 3
            };
        }

        /// <summary>
        /// Verifies validate throws when name is empty.
        /// </summary>
        [TestMethod]
        public void Validate_ShouldThrow_WhenNameIsEmpty()
        {
            var dto = CreateValidDto();
            dto.Name = string.Empty;

            Assert.ThrowsException<ArgumentException>(() => DogValidator.Validate(dto));
        }

        /// <summary>
        /// Verifies validate throws when age is invalid.
        /// </summary>
        [TestMethod]
        public void Validate_ShouldThrow_WhenAgeIsInvalid()
        {
            var dto = CreateValidDto();
            dto.Age = 0;

            Assert.ThrowsException<ArgumentException>(() => DogValidator.Validate(dto));
        }

        /// <summary>
        /// Verifies validate passes for valid data.
        /// </summary>
        [TestMethod]
        public void Validate_ShouldNotThrow_WhenDataIsValid()
        {
            var dto = CreateValidDto();

            DogValidator.Validate(dto);
        }
    }
}
