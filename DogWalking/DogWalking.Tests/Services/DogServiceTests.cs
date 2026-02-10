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
    /// Unit tests for <see cref="DogService"/>.
    /// </summary>
    [TestClass]
    public class DogServiceTests
    {
        /// <summary>
        /// Creates a service instance with a strict repository mock.
        /// </summary>
        private static (DogService Service, Mock<IDogRepository> Repository) CreateService(
            Action<Mock<IDogRepository>> setup = null)
        {
            var repository = new Mock<IDogRepository>(MockBehavior.Strict);
            setup?.Invoke(repository);
            return (new DogService(repository.Object), repository);
        }

        /// <summary>
        /// Verifies constructor throws when repository is null.
        /// </summary>
        [TestMethod]
        public void Constructor_ShouldThrow_WhenRepositoryIsNull()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new DogService(null));
        }

        /// <summary>
        /// Verifies add throws when DTO is null.
        /// </summary>
        [TestMethod]
        public void Add_ShouldThrow_WhenDtoIsNull()
        {
            var (service, repository) = CreateService();

            Assert.ThrowsException<ArgumentNullException>(() => service.Add(null));

            repository.Verify(r => r.Add(It.IsAny<Dog>()), Times.Never);
        }

        /// <summary>
        /// Verifies add throws when name is invalid.
        /// </summary>
        [TestMethod]
        public void Add_ShouldThrow_WhenNameIsInvalid()
        {
            var (service, repository) = CreateService();
            var dto = new DogDto { ClientId = 1, Name = "", Breed = "Lab", Age = 3 };

            Assert.ThrowsException<ArgumentException>(() => service.Add(dto));

            repository.Verify(r => r.Add(It.IsAny<Dog>()), Times.Never);
        }

        /// <summary>
        /// Verifies add throws when age is invalid.
        /// </summary>
        [TestMethod]
        public void Add_ShouldThrow_WhenAgeIsInvalid()
        {
            var (service, repository) = CreateService();
            var dto = new DogDto { ClientId = 1, Name = "Rex", Breed = "Lab", Age = 0 };

            Assert.ThrowsException<ArgumentException>(() => service.Add(dto));

            repository.Verify(r => r.Add(It.IsAny<Dog>()), Times.Never);
        }

        /// <summary>
        /// Verifies add maps DTO values and calls repository.
        /// </summary>
        [TestMethod]
        public void Add_ShouldMapAndCallRepository_WhenDtoIsValid()
        {
            var dto = new DogDto { ClientId = 3, Name = "Rex", Breed = "Lab", Age = 4 };
            var (service, repository) = CreateService(r =>
                r.Setup(x => x.Add(It.Is<Dog>(d =>
                    d.ClientId == dto.ClientId &&
                    d.Name == dto.Name &&
                    d.Breed == dto.Breed &&
                    d.Age == dto.Age))));

            service.Add(dto);

            repository.Verify(r => r.Add(It.Is<Dog>(d =>
                d.ClientId == dto.ClientId &&
                d.Name == dto.Name &&
                d.Breed == dto.Breed &&
                d.Age == dto.Age)), Times.Once);
        }
    }
}
