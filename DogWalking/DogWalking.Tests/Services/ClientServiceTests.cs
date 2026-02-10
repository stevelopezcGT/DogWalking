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
    /// Unit tests for <see cref="ClientService"/>.
    /// </summary>
    [TestClass]
    public class ClientServiceTests
    {
        /// <summary>
        /// Creates a service instance with a strict repository mock.
        /// </summary>
        private static (ClientService Service, Mock<IClientRepository> Repository) CreateService(
            Action<Mock<IClientRepository>> setup = null)
        {
            var repository = new Mock<IClientRepository>(MockBehavior.Strict);
            setup?.Invoke(repository);
            return (new ClientService(repository.Object), repository);
        }

        /// <summary>
        /// Verifies constructor throws when repository is null.
        /// </summary>
        [TestMethod]
        public void Constructor_ShouldThrow_WhenRepositoryIsNull()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new ClientService(null));
        }

        /// <summary>
        /// Verifies add throws when DTO is null.
        /// </summary>
        [TestMethod]
        public void Add_ShouldThrow_WhenDtoIsNull()
        {
            var (service, repository) = CreateService();

            Assert.ThrowsException<ArgumentNullException>(() => service.Add(null));

            repository.Verify(r => r.Add(It.IsAny<Client>()), Times.Never);
        }

        /// <summary>
        /// Verifies add throws when name is invalid.
        /// </summary>
        [TestMethod]
        public void Add_ShouldThrow_WhenNameIsInvalid()
        {
            var (service, repository) = CreateService();
            var dto = new ClientDto { Name = "", Phone = "123" };

            Assert.ThrowsException<ArgumentException>(() => service.Add(dto));

            repository.Verify(r => r.Add(It.IsAny<Client>()), Times.Never);
        }

        /// <summary>
        /// Verifies add throws when phone is invalid.
        /// </summary>
        [TestMethod]
        public void Add_ShouldThrow_WhenPhoneIsInvalid()
        {
            var (service, repository) = CreateService();
            var dto = new ClientDto { Name = "John", Phone = "   " };

            Assert.ThrowsException<ArgumentException>(() => service.Add(dto));

            repository.Verify(r => r.Add(It.IsAny<Client>()), Times.Never);
        }

        /// <summary>
        /// Verifies add maps DTO values and calls repository.
        /// </summary>
        [TestMethod]
        public void Add_ShouldMapAndCallRepository_WhenDtoIsValid()
        {
            var dto = new ClientDto { Name = "John", Phone = "555" };
            var (service, repository) = CreateService(r =>
                r.Setup(x => x.Add(It.Is<Client>(c => c.Name == dto.Name && c.Phone == dto.Phone))));

            service.Add(dto);

            repository.Verify(r => r.Add(It.Is<Client>(c => c.Name == dto.Name && c.Phone == dto.Phone)), Times.Once);
        }

        /// <summary>
        /// Verifies get-all returns repository output.
        /// </summary>
        [TestMethod]
        public void GetAll_ShouldReturnRepositoryResult()
        {
            var expected = new List<Client> { new Client { Id = 1, Name = "Jane" } };
            var (service, repository) = CreateService(r => r.Setup(x => x.GetAll()).Returns(expected));

            var result = service.GetAll();

            Assert.AreSame(expected, result);
            repository.Verify(r => r.GetAll(), Times.Once);
        }

        /// <summary>
        /// Verifies search falls back to get-all for null search term.
        /// </summary>
        [TestMethod]
        public void Search_ShouldReturnGetAll_WhenSearchTermIsNull()
        {
            var expected = new List<Client>();
            var (service, repository) = CreateService(r => r.Setup(x => x.GetAll()).Returns(expected));

            var result = service.Search(null);

            Assert.AreSame(expected, result);
            repository.Verify(r => r.GetAll(), Times.Once);
            repository.Verify(r => r.Search(It.IsAny<string>()), Times.Never);
        }

        /// <summary>
        /// Verifies search falls back to get-all for whitespace search term.
        /// </summary>
        [TestMethod]
        public void Search_ShouldReturnGetAll_WhenSearchTermIsWhitespace()
        {
            var expected = new List<Client>();
            var (service, repository) = CreateService(r => r.Setup(x => x.GetAll()).Returns(expected));

            var result = service.Search("   ");

            Assert.AreSame(expected, result);
            repository.Verify(r => r.GetAll(), Times.Once);
            repository.Verify(r => r.Search(It.IsAny<string>()), Times.Never);
        }

        /// <summary>
        /// Verifies search trims input and uses repository search.
        /// </summary>
        [TestMethod]
        public void Search_ShouldTrimAndCallSearch_WhenSearchTermHasValue()
        {
            var expected = new List<Client> { new Client { Id = 2, Name = "John" } };
            var (service, repository) = CreateService(r => r.Setup(x => x.Search("john")).Returns(expected));

            var result = service.Search("  john  ");

            Assert.AreSame(expected, result);
            repository.Verify(r => r.Search("john"), Times.Once);
            repository.Verify(r => r.GetAll(), Times.Never);
        }

        /// <summary>
        /// Verifies delete calls repository with the requested id.
        /// </summary>
        [TestMethod]
        public void Delete_ShouldCallRepository_WithClientId()
        {
            var (service, repository) = CreateService(r => r.Setup(x => x.Delete(7)));

            service.Delete(7);

            repository.Verify(r => r.Delete(7), Times.Once);
        }
    }
}
