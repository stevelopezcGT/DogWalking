using DogWalking.BL.DTOs;
using DogWalking.BL.Validators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DogWalking.Tests.Validators
{
    /// <summary>
    /// Unit tests for <see cref="LoginValidator"/>.
    /// </summary>
    [TestClass]
    public class LoginValidatorTests
    {
        /// <summary>
        /// Verifies validate throws when DTO is null.
        /// </summary>
        [TestMethod]
        public void Validate_ShouldThrow_WhenDtoIsNull()
        {
            Assert.ThrowsException<ArgumentNullException>(() => LoginValidator.Validate(null));
        }

        /// <summary>
        /// Verifies validate throws when username is empty.
        /// </summary>
        [TestMethod]
        public void Validate_ShouldThrow_WhenUsernameIsEmpty()
        {
            var dto = new LoginDto { Username = "", Password = "admin" };

            Assert.ThrowsException<ArgumentException>(() => LoginValidator.Validate(dto));
        }

        /// <summary>
        /// Verifies validate throws when password is empty.
        /// </summary>
        [TestMethod]
        public void Validate_ShouldThrow_WhenPasswordIsEmpty()
        {
            var dto = new LoginDto { Username = "admin", Password = "" };

            Assert.ThrowsException<ArgumentException>(() => LoginValidator.Validate(dto));
        }

        /// <summary>
        /// Verifies validate passes for valid data.
        /// </summary>
        [TestMethod]
        public void Validate_ShouldNotThrow_WhenDataIsValid()
        {
            var dto = new LoginDto { Username = "admin", Password = "admin" };

            LoginValidator.Validate(dto);
        }
    }
}
