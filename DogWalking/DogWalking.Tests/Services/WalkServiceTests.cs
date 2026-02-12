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
    /// Unit tests for <see cref="WalkService"/>.
    /// </summary>
    [TestClass]
    public class WalkServiceTests
    {
        private static (WalkService Service, Mock<IWalkRepository> Repository) CreateService(
            Action<Mock<IWalkRepository>> setup = null)
        {
            var repository = new Mock<IWalkRepository>(MockBehavior.Strict);
            setup?.Invoke(repository);
            return (new WalkService(repository.Object), repository);
        }

        [TestMethod]
        public void Constructor_ShouldThrow_WhenRepositoryIsNull()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new WalkService(null));
        }

        [TestMethod]
        public void Add_ShouldThrow_WhenDtoIsNull()
        {
            var (service, repository) = CreateService();

            Assert.ThrowsException<ArgumentNullException>(() => service.Add(null));

            repository.Verify(r => r.Add(It.IsAny<Walk>()), Times.Never);
        }

        [TestMethod]
        public void Add_ShouldThrow_WhenDtoIsInvalid()
        {
            var (service, repository) = CreateService();
            var dto = new WalkDto { DogId = 0, WalkDate = DateTime.Today, DurationMinutes = 30 };

            Assert.ThrowsException<ArgumentException>(() => service.Add(dto));

            repository.Verify(r => r.Add(It.IsAny<Walk>()), Times.Never);
        }

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

        [TestMethod]
        public void Update_ShouldThrow_WhenWalkNotFound()
        {
            var dto = new WalkDto { DogId = 1, WalkDate = DateTime.Today, DurationMinutes = 20 };
            var (service, repository) = CreateService(r =>
                r.Setup(x => x.GetById(7)).Returns((Walk)null));

            Assert.ThrowsException<InvalidOperationException>(() => service.Update(7, dto));

            repository.Verify(r => r.GetById(7), Times.Once);
            repository.Verify(r => r.Update(It.IsAny<Walk>()), Times.Never);
        }

        [TestMethod]
        public void Update_ShouldMapAndCallRepository_WhenValid()
        {
            var existing = new Walk
            {
                Id = 8,
                DogId = 1,
                WalkDate = DateTime.Today.AddDays(-1),
                DurationMinutes = 15,
                Dog = new Dog { Id = 1, Name = "Rex" }
            };
            var dto = new WalkDto { DogId = 2, WalkDate = DateTime.Today, DurationMinutes = 60 };

            var (service, repository) = CreateService(r =>
            {
                r.Setup(x => x.GetById(8)).Returns(existing);
                r.Setup(x => x.Update(It.Is<Walk>(w =>
                    w.DogId == dto.DogId &&
                    w.WalkDate == dto.WalkDate &&
                    w.DurationMinutes == dto.DurationMinutes)));
            });

            service.Update(8, dto);

            repository.Verify(r => r.GetById(8), Times.Once);
            repository.Verify(r => r.Update(It.Is<Walk>(w =>
                w.DogId == dto.DogId &&
                w.WalkDate == dto.WalkDate &&
                w.DurationMinutes == dto.DurationMinutes)), Times.Once);
        }

        [TestMethod]
        public void GetAll_ShouldReturnMappedListIncludingDogName()
        {
            var walks = new List<Walk>
            {
                new Walk
                {
                    Id = 1,
                    DogId = 3,
                    WalkDate = DateTime.Today,
                    DurationMinutes = 30,
                    Dog = new Dog { Id = 3, Name = "Luna" }
                }
            };

            var (service, repository) = CreateService(r =>
                r.Setup(x => x.GetAll()).Returns(walks));

            var result = service.GetAll();

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(1, result[0].Id);
            Assert.AreEqual(3, result[0].DogId);
            Assert.AreEqual("Luna", result[0].DogName);
            repository.Verify(r => r.GetAll(), Times.Once);
        }

        [TestMethod]
        public void Search_ShouldReturnGetAll_WhenSearchTermIsNullOrWhitespace()
        {
            var walks = new List<Walk>();
            var (service, repository) = CreateService(r =>
                r.Setup(x => x.GetAll()).Returns(walks));

            var resultNull = service.Search(null);
            var resultWhitespace = service.Search("   ");

            Assert.AreEqual(0, resultNull.Count);
            Assert.AreEqual(0, resultWhitespace.Count);
            repository.Verify(r => r.GetAll(), Times.Exactly(2));
            repository.Verify(r => r.Search(It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        public void Search_ShouldTrimAndCallSearch_WhenSearchTermHasValue()
        {
            var walks = new List<Walk>
            {
                new Walk
                {
                    Id = 10,
                    DogId = 2,
                    WalkDate = DateTime.Today,
                    DurationMinutes = 25,
                    Dog = new Dog { Id = 2, Name = "Bolt" }
                }
            };

            var (service, repository) = CreateService(r =>
                r.Setup(x => x.Search("bolt")).Returns(walks));

            var result = service.Search("  bolt  ");

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("Bolt", result[0].DogName);
            repository.Verify(r => r.Search("bolt"), Times.Once);
            repository.Verify(r => r.GetAll(), Times.Never);
        }

        [TestMethod]
        public void Delete_ShouldCallRepositoryDelete()
        {
            var (service, repository) = CreateService(r =>
            {
                r.Setup(x => x.GetById(5)).Returns(new Walk { Id = 5 });
                r.Setup(x => x.Delete(5));
            });

            service.Delete(5);

            repository.Verify(r => r.GetById(5), Times.Once);
            repository.Verify(r => r.Delete(5), Times.Once);
        }
    }
}
