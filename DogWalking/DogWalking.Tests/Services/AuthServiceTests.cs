using DogWalking.BL.DTOs;
using DogWalking.BL.Services;
using DogWalking.DL.Entities;
using DogWalking.DL.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace DogWalking.Tests.Services
{
    /// <summary>
    /// Unit tests for <see cref="AuthService"/>.
    /// </summary>
    [TestClass]
    public class AuthServiceTests
    {
        /// <summary>
        /// Creates a service instance with a strict repository mock.
        /// </summary>
        private static (AuthService Service, Mock<IUserRepository> Repository) CreateService(
            Action<Mock<IUserRepository>> setup = null)
        {
            var repository = new Mock<IUserRepository>(MockBehavior.Strict);
            setup?.Invoke(repository);
            return (new AuthService(repository.Object), repository);
        }

        /// <summary>
        /// Verifies constructor throws when repository is null.
        /// </summary>
        [TestMethod]
        public void Constructor_ShouldThrow_WhenRepositoryIsNull()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new AuthService(null));
        }

        /// <summary>
        /// Verifies login throws when DTO is null.
        /// </summary>
        [TestMethod]
        public void Login_ShouldThrow_WhenDtoIsNull()
        {
            var (service, repository) = CreateService();

            Assert.ThrowsException<ArgumentNullException>(() => service.Login(null));

            repository.Verify(r => r.GetByCredentials(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        /// <summary>
        /// Verifies login throws when username is invalid.
        /// </summary>
        [TestMethod]
        public void Login_ShouldThrow_WhenUsernameIsInvalid()
        {
            var (service, repository) = CreateService();
            var dto = new LoginDto { Username = "   ", Password = "admin" };

            Assert.ThrowsException<ArgumentException>(() => service.Login(dto));

            repository.Verify(r => r.GetByCredentials(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        /// <summary>
        /// Verifies login throws when password is invalid.
        /// </summary>
        [TestMethod]
        public void Login_ShouldThrow_WhenPasswordIsInvalid()
        {
            var (service, repository) = CreateService();
            var dto = new LoginDto { Username = "admin", Password = "" };

            Assert.ThrowsException<ArgumentException>(() => service.Login(dto));

            repository.Verify(r => r.GetByCredentials(It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        /// <summary>
        /// Verifies login returns true for valid credentials.
        /// </summary>
        [TestMethod]
        public void Login_ShouldReturnTrue_WhenUserExists()
        {
            var (service, repository) = CreateService(r =>
                r.Setup(x => x.GetByCredentials("admin", "admin")).Returns(new User { Id = 1 }));

            var result = service.Login(new LoginDto { Username = "admin", Password = "admin" });

            Assert.IsTrue(result);
            repository.Verify(r => r.GetByCredentials("admin", "admin"), Times.Once);
        }

        /// <summary>
        /// Verifies login returns false for invalid credentials.
        /// </summary>
        [TestMethod]
        public void Login_ShouldReturnFalse_WhenUserDoesNotExist()
        {
            var (service, repository) = CreateService(r =>
                r.Setup(x => x.GetByCredentials("admin", "wrong")).Returns((User)null));

            var result = service.Login(new LoginDto { Username = "admin", Password = "wrong" });

            Assert.IsFalse(result);
            repository.Verify(r => r.GetByCredentials("admin", "wrong"), Times.Once);
        }
    }
}
