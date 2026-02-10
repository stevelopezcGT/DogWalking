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
    /// Unit tests for <see cref="WalkService"/>.
    /// </summary>
    [TestClass]
    public class WalkServiceTests
    {
        /// <summary>
        /// Creates a service instance with a strict repository mock.
        /// </summary>
        private static (WalkService Service, Mock<IWalkRepository> Repository) CreateService(
            Action<Mock<IWalkRepository>> setup = null)
        {
            var repository = new Mock<IWalkRepository>(MockBehavior.Strict);
            setup?.Invoke(repository);
            return (new WalkService(repository.Object), repository);
        }

        /// <summary>
        /// Verifies constructor throws when repository is null.
        /// </summary>
        [TestMethod]
        public void Constructor_ShouldThrow_WhenRepositoryIsNull()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new WalkService(null));
        }

        /// <summary>
        /// Verifies add throws when DTO is null.
        /// </summary>
        [TestMethod]
        public void Add_ShouldThrow_WhenDtoIsNull()
        {
            var (service, repository) = CreateService();

            Assert.ThrowsException<ArgumentNullException>(() => service.Add(null));

            repository.Verify(r => r.Add(It.IsAny<Walk>()), Times.Never);
        }

        /// <summary>
        /// Verifies add throws when walk date is default.
        /// </summary>
        [TestMethod]
        public void Add_ShouldThrow_WhenWalkDateIsDefault()
        {
            var (service, repository) = CreateService();
            var dto = new WalkDto { DogId = 1, WalkDate = default(DateTime), DurationMinutes = 30 };

            Assert.ThrowsException<ArgumentException>(() => service.Add(dto));

            repository.Verify(r => r.Add(It.IsAny<Walk>()), Times.Never);
        }

        /// <summary>
        /// Verifies add throws when duration is invalid.
        /// </summary>
        [TestMethod]
        public void Add_ShouldThrow_WhenDurationIsInvalid()
        {
            var (service, repository) = CreateService();
            var dto = new WalkDto { DogId = 1, WalkDate = DateTime.Today, DurationMinutes = 0 };

            Assert.ThrowsException<ArgumentException>(() => service.Add(dto));

            repository.Verify(r => r.Add(It.IsAny<Walk>()), Times.Never);
        }

        /// <summary>
        /// Verifies add maps DTO values and calls repository.
        /// </summary>
        [TestMethod]
        public void Add_ShouldMapAndCallRepository_WhenDtoIsValid()
        {
            var dto = new WalkDto { DogId = 2, WalkDate = DateTime.Today, DurationMinutes = 45 };
            var (service, repository) = CreateService(r =>
                r.Setup(x => x.Add(It.Is<Walk>(w =>
                    w.DogId == dto.DogId &&
                    w.WalkDate == dto.WalkDate &&
                    w.DurationMinutes == dto.DurationMinutes))));

            service.Add(dto);

            repository.Verify(r => r.Add(It.Is<Walk>(w =>
                w.DogId == dto.DogId &&
                w.WalkDate == dto.WalkDate &&
                w.DurationMinutes == dto.DurationMinutes)), Times.Once);
        }
    }
}
