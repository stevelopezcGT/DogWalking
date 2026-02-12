using DogWalking.BL.DTOs;
using DogWalking.BL.Services;
using DogWalking.DL.Entities;
using DogWalking.DL.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

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
        private static (
            DogService Service,
            Mock<IDogRepository> DogRepository,
            Mock<IWalkRepository> WalkRepository) CreateService(
            Action<Mock<IDogRepository>> dogSetup = null,
            Action<Mock<IWalkRepository>> walkSetup = null)
        {
            var dogRepository = new Mock<IDogRepository>(MockBehavior.Strict);
            var walkRepository = new Mock<IWalkRepository>(MockBehavior.Strict);
            dogSetup?.Invoke(dogRepository);
            walkSetup?.Invoke(walkRepository);
            return (new DogService(dogRepository.Object, walkRepository.Object), dogRepository, walkRepository);
        }

        /// <summary>
        /// Verifies constructor throws when repository is null.
        /// </summary>
        [TestMethod]
        public void Constructor_ShouldThrow_WhenRepositoryIsNull()
        {
            var walkRepository = new Mock<IWalkRepository>(MockBehavior.Strict);

            Assert.ThrowsException<ArgumentNullException>(() => new DogService(null, walkRepository.Object));
        }

        /// <summary>
        /// Verifies add throws when DTO is null.
        /// </summary>
        [TestMethod]
        public void Add_ShouldThrow_WhenDtoIsNull()
        {
            var (service, repository, walkRepository) = CreateService();

            Assert.ThrowsException<ArgumentNullException>(() => service.Add(null));

            repository.Verify(r => r.Add(It.IsAny<Dog>()), Times.Never);
            walkRepository.VerifyNoOtherCalls();
        }

        /// <summary>
        /// Verifies add throws when name is invalid.
        /// </summary>
        [TestMethod]
        public void Add_ShouldThrow_WhenNameIsInvalid()
        {
            var (service, repository, walkRepository) = CreateService();
            var dto = new DogDto { ClientId = 1, Name = "", Breed = "Lab", Age = 3 };

            Assert.ThrowsException<ArgumentException>(() => service.Add(dto));

            repository.Verify(r => r.Add(It.IsAny<Dog>()), Times.Never);
            walkRepository.VerifyNoOtherCalls();
        }

        /// <summary>
        /// Verifies add throws when age is invalid.
        /// </summary>
        [TestMethod]
        public void Add_ShouldThrow_WhenAgeIsInvalid()
        {
            var (service, repository, walkRepository) = CreateService();
            var dto = new DogDto { ClientId = 1, Name = "Rex", Breed = "Lab", Age = 0 };

            Assert.ThrowsException<ArgumentException>(() => service.Add(dto));

            repository.Verify(r => r.Add(It.IsAny<Dog>()), Times.Never);
            walkRepository.VerifyNoOtherCalls();
        }

        /// <summary>
        /// Verifies add maps DTO values and calls repository.
        /// </summary>
        [TestMethod]
        public void Add_ShouldMapAndCallRepository_WhenDtoIsValid()
        {
            var dto = new DogDto { ClientId = 3, Name = "Rex", Breed = "Lab", Age = 4 };
            var (service, repository, walkRepository) = CreateService(dogSetup: r =>
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
            walkRepository.VerifyNoOtherCalls();
        }

        [TestMethod]
        public void Delete_ShouldThrow_WhenActiveWalksExist()
        {
            var (service, dogRepository, walkRepository) = CreateService(
                dogSetup: r => r.Setup(x => x.GetById(5)).Returns(new Dog { Id = 5, ClientId = 1, Name = "Rex", Breed = "Lab", Age = 3 }),
                walkSetup: r => r.Setup(x => x.GetByDog(5)).Returns(new List<Walk> { new Walk { Id = 2, DogId = 5, DurationMinutes = 30, WalkDate = DateTime.Today } }));

            Assert.ThrowsException<InvalidOperationException>(() => service.Delete(5));

            dogRepository.Verify(r => r.GetById(5), Times.Once);
            walkRepository.Verify(r => r.GetByDog(5), Times.Once);
            dogRepository.Verify(r => r.Delete(It.IsAny<int>()), Times.Never);
        }

        [TestMethod]
        public void Delete_ShouldCallRepository_WhenNoActiveWalksExist()
        {
            var (service, dogRepository, walkRepository) = CreateService(
                dogSetup: r =>
                {
                    r.Setup(x => x.GetById(5)).Returns(new Dog { Id = 5, ClientId = 1, Name = "Rex", Breed = "Lab", Age = 3 });
                    r.Setup(x => x.Delete(5));
                },
                walkSetup: r => r.Setup(x => x.GetByDog(5)).Returns(new List<Walk>()));

            service.Delete(5);

            dogRepository.Verify(r => r.GetById(5), Times.Once);
            walkRepository.Verify(r => r.GetByDog(5), Times.Once);
            dogRepository.Verify(r => r.Delete(5), Times.Once);
        }
    }
}
