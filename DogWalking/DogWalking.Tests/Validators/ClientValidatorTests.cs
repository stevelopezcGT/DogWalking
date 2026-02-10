using DogWalking.BL.DTOs;
using DogWalking.BL.Validators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DogWalking.Tests.Validators
{
    /// <summary>
    /// Unit tests for <see cref="ClientValidator"/>.
    /// </summary>
    [TestClass]
    public class ClientValidatorTests
    {
        /// <summary>
        /// Creates a DTO with valid values.
        /// </summary>
        private static ClientDto CreateValidDto()
        {
            return new ClientDto
            {
                Name = "John Doe",
                Phone = "123456"
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

            Assert.ThrowsException<ArgumentException>(() => ClientValidator.Validate(dto));
        }

        /// <summary>
        /// Verifies validate throws when phone is empty.
        /// </summary>
        [TestMethod]
        public void Validate_ShouldThrow_WhenPhoneIsEmpty()
        {
            var dto = CreateValidDto();
            dto.Phone = string.Empty;

            Assert.ThrowsException<ArgumentException>(() => ClientValidator.Validate(dto));
        }

        /// <summary>
        /// Verifies validate passes for valid data.
        /// </summary>
        [TestMethod]
        public void Validate_ShouldNotThrow_WhenDataIsValid()
        {
            var dto = CreateValidDto();

            ClientValidator.Validate(dto);
        }
    }
}
