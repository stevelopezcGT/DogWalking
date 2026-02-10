using DogWalking.BL.DTOs;
using DogWalking.BL.Validators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DogWalking.Tests.Validators
{
    /// <summary>
    /// Unit tests for <see cref="WalkValidator"/>.
    /// </summary>
    [TestClass]
    public class WalkValidatorTests
    {
        /// <summary>
        /// Creates a DTO with valid values.
        /// </summary>
        private static WalkDto CreateValidDto()
        {
            return new WalkDto
            {
                DogId = 1,
                WalkDate = DateTime.Today,
                DurationMinutes = 30
            };
        }

        /// <summary>
        /// Verifies validate throws when walk date is default.
        /// </summary>
        [TestMethod]
        public void Validate_ShouldThrow_WhenWalkDateIsDefault()
        {
            var dto = CreateValidDto();
            dto.WalkDate = default(DateTime);

            Assert.ThrowsException<ArgumentException>(() => WalkValidator.Validate(dto));
        }

        /// <summary>
        /// Verifies validate throws when duration is invalid.
        /// </summary>
        [TestMethod]
        public void Validate_ShouldThrow_WhenDurationIsInvalid()
        {
            var dto = CreateValidDto();
            dto.DurationMinutes = 0;

            Assert.ThrowsException<ArgumentException>(() => WalkValidator.Validate(dto));
        }

        /// <summary>
        /// Verifies validate passes for valid data.
        /// </summary>
        [TestMethod]
        public void Validate_ShouldNotThrow_WhenDataIsValid()
        {
            var dto = CreateValidDto();

            WalkValidator.Validate(dto);
        }
    }
}
