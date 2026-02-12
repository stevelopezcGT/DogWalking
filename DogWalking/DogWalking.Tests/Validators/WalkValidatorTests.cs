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
        private static WalkDto CreateValidDto()
        {
            return new WalkDto
            {
                DogId = 1,
                WalkDate = DateTime.Today,
                DurationMinutes = 30
            };
        }

        [TestMethod]
        public void Validate_ShouldThrow_WhenDtoIsNull()
        {
            Assert.ThrowsException<ArgumentNullException>(() => WalkValidator.Validate(null));
        }

        [TestMethod]
        public void Validate_ShouldThrow_WhenDogIdIsInvalid()
        {
            var dto = CreateValidDto();
            dto.DogId = 0;

            Assert.ThrowsException<ArgumentException>(() => WalkValidator.Validate(dto));
        }

        [TestMethod]
        public void Validate_ShouldThrow_WhenWalkDateIsDefault()
        {
            var dto = CreateValidDto();
            dto.WalkDate = default(DateTime);

            Assert.ThrowsException<ArgumentException>(() => WalkValidator.Validate(dto));
        }

        [TestMethod]
        public void Validate_ShouldThrow_WhenDurationIsInvalid()
        {
            var dto = CreateValidDto();
            dto.DurationMinutes = 0;

            Assert.ThrowsException<ArgumentException>(() => WalkValidator.Validate(dto));
        }

        [TestMethod]
        public void Validate_ShouldNotThrow_WhenDataIsValid()
        {
            var dto = CreateValidDto();

            WalkValidator.Validate(dto);
        }
    }
}
