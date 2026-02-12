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
        private static (
            ClientService Service,
            Mock<IClientRepository> ClientRepository,
            Mock<IDogRepository> DogRepository) CreateService(
            Action<Mock<IClientRepository>> clientSetup = null,
            Action<Mock<IDogRepository>> dogSetup = null)
        {
            var clientRepository = new Mock<IClientRepository>(MockBehavior.Strict);
            var dogRepository = new Mock<IDogRepository>(MockBehavior.Strict);
            clientSetup?.Invoke(clientRepository);
            dogSetup?.Invoke(dogRepository);
            return (new ClientService(clientRepository.Object, dogRepository.Object), clientRepository, dogRepository);
        }

        /// <summary>
        /// Verifies constructor throws when repository is null.
        /// </summary>
        [TestMethod]
        public void Constructor_ShouldThrow_WhenRepositoryIsNull()
        {
            var dogRepository = new Mock<IDogRepository>(MockBehavior.Strict);

            Assert.ThrowsException<ArgumentNullException>(() => new ClientService(null, dogRepository.Object));
        }

        /// <summary>
        /// Verifies add throws when DTO is null.
        /// </summary>
        [TestMethod]
        public void Add_ShouldThrow_WhenDtoIsNull()
        {
            var (service, repository, dogRepository) = CreateService();

            Assert.ThrowsException<ArgumentNullException>(() => service.Add(null));

            repository.Verify(r => r.Add(It.IsAny<Client>()), Times.Never);
            dogRepository.VerifyNoOtherCalls();
        }

        /// <summary>
        /// Verifies add throws when name is invalid.
        /// </summary>
        [TestMethod]
        public void Add_ShouldThrow_WhenNameIsInvalid()
        {
            var (service, repository, dogRepository) = CreateService();
            var dto = new ClientDto { Name = "", Phone = "123" };

            Assert.ThrowsException<ArgumentException>(() => service.Add(dto));

            repository.Verify(r => r.Add(It.IsAny<Client>()), Times.Never);
            dogRepository.VerifyNoOtherCalls();
        }

        /// <summary>
        /// Verifies add throws when phone is invalid.
        /// </summary>
        [TestMethod]
        public void Add_ShouldThrow_WhenPhoneIsInvalid()
        {
            var (service, repository, dogRepository) = CreateService();
            var dto = new ClientDto { Name = "John", Phone = "   " };

            Assert.ThrowsException<ArgumentException>(() => service.Add(dto));

            repository.Verify(r => r.Add(It.IsAny<Client>()), Times.Never);
            dogRepository.VerifyNoOtherCalls();
        }

        /// <summary>
        /// Verifies add maps DTO values and calls repository.
        /// </summary>
        [TestMethod]
        public void Add_ShouldMapAndCallRepository_WhenDtoIsValid()
        {
            var dto = new ClientDto { Name = "John", Phone = "555" };
            var (service, repository, dogRepository) = CreateService(clientSetup: r =>
                r.Setup(x => x.Add(It.Is<Client>(c => c.Name == dto.Name && c.Phone == dto.Phone))));

            service.Add(dto);

            repository.Verify(r => r.Add(It.Is<Client>(c => c.Name == dto.Name && c.Phone == dto.Phone)), Times.Once);
            dogRepository.VerifyNoOtherCalls();
        }

        /// <summary>
        /// Verifies get-all returns repository output.
        /// </summary>
        [TestMethod]
        public void GetAll_ShouldReturnRepositoryResult()
        {
            var expected = new List<Client> { new Client { Id = 1, Name = "Jane" } };
            var (service, repository, dogRepository) = CreateService(clientSetup: r => r.Setup(x => x.GetAll()).Returns(expected));

            var result = service.GetAll();

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("Jane", result[0].Name);
            repository.Verify(r => r.GetAll(), Times.Once);
            dogRepository.VerifyNoOtherCalls();
        }

        /// <summary>
        /// Verifies search falls back to get-all for null search term.
        /// </summary>
        [TestMethod]
        public void Search_ShouldReturnGetAll_WhenSearchTermIsNull()
        {
            var expected = new List<Client>();
            var (service, repository, dogRepository) = CreateService(clientSetup: r => r.Setup(x => x.GetAll()).Returns(expected));

            var result = service.Search(null);

            Assert.AreEqual(0, result.Count);
            repository.Verify(r => r.GetAll(), Times.Once);
            repository.Verify(r => r.Search(It.IsAny<string>()), Times.Never);
            dogRepository.VerifyNoOtherCalls();
        }

        /// <summary>
        /// Verifies search falls back to get-all for whitespace search term.
        /// </summary>
        [TestMethod]
        public void Search_ShouldReturnGetAll_WhenSearchTermIsWhitespace()
        {
            var expected = new List<Client>();
            var (service, repository, dogRepository) = CreateService(clientSetup: r => r.Setup(x => x.GetAll()).Returns(expected));

            var result = service.Search("   ");

            Assert.AreEqual(0, result.Count);
            repository.Verify(r => r.GetAll(), Times.Once);
            repository.Verify(r => r.Search(It.IsAny<string>()), Times.Never);
            dogRepository.VerifyNoOtherCalls();
        }

        /// <summary>
        /// Verifies search trims input and uses repository search.
        /// </summary>
        [TestMethod]
        public void Search_ShouldTrimAndCallSearch_WhenSearchTermHasValue()
        {
            var expected = new List<Client> { new Client { Id = 2, Name = "John" } };
            var (service, repository, dogRepository) = CreateService(clientSetup: r => r.Setup(x => x.Search("john")).Returns(expected));

            var result = service.Search("  john  ");

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("John", result[0].Name);
            repository.Verify(r => r.Search("john"), Times.Once);
            repository.Verify(r => r.GetAll(), Times.Never);
            dogRepository.VerifyNoOtherCalls();
        }

        /// <summary>
        /// Verifies delete throws when client has active dogs.
        /// </summary>
        [TestMethod]
        public void Delete_ShouldThrow_WhenActiveDogsExist()
        {
            var (service, repository, dogRepository) = CreateService(
                clientSetup: r => r.Setup(x => x.GetById(7)).Returns(new Client { Id = 7, Name = "John", Phone = "555" }),
                dogSetup: r => r.Setup(x => x.GetByClient(7)).Returns(new System.Collections.Generic.List<Dog> { new Dog { Id = 9, ClientId = 7, Name = "Rex" } }));

            Assert.ThrowsException<InvalidOperationException>(() => service.Delete(7));

            repository.Verify(r => r.GetById(7), Times.Once);
            dogRepository.Verify(r => r.GetByClient(7), Times.Once);
            repository.Verify(r => r.Delete(It.IsAny<int>()), Times.Never);
        }

        /// <summary>
        /// Verifies delete calls repository when client has no active dogs.
        /// </summary>
        [TestMethod]
        public void Delete_ShouldCallRepository_WhenNoActiveDogsExist()
        {
            var (service, repository, dogRepository) = CreateService(
                clientSetup: r =>
                {
                    r.Setup(x => x.GetById(7)).Returns(new Client { Id = 7, Name = "John", Phone = "555" });
                    r.Setup(x => x.Delete(7));
                },
                dogSetup: r => r.Setup(x => x.GetByClient(7)).Returns(new System.Collections.Generic.List<Dog>()));

            service.Delete(7);

            repository.Verify(r => r.GetById(7), Times.Once);
            dogRepository.Verify(r => r.GetByClient(7), Times.Once);
            repository.Verify(r => r.Delete(7), Times.Once);
        }

        [TestMethod]
        public void Update_ShouldThrow_WhenDtoIsNull()
        {
            var (service, repository, dogRepository) = CreateService();

            Assert.ThrowsException<ArgumentNullException>(() =>
                service.Update(1, null));

            repository.Verify(r => r.Update(It.IsAny<Client>()), Times.Never);
            dogRepository.VerifyNoOtherCalls();
        }

        [TestMethod]
        public void Update_ShouldThrow_WhenDtoIsInvalid()
        {
            var (service, repository, dogRepository) = CreateService();
            var dto = new ClientDto { Name = "", Phone = "123" };

            Assert.ThrowsException<ArgumentException>(() =>
                service.Update(1, dto));

            repository.Verify(r => r.Update(It.IsAny<Client>()), Times.Never);
            dogRepository.VerifyNoOtherCalls();
        }

        [TestMethod]
        public void Update_ShouldThrow_WhenClientNotFound()
        {
            var dto = new ClientDto { Name = "John", Phone = "123" };

            var (service, repository, dogRepository) = CreateService(clientSetup: r =>
                r.Setup(x => x.GetById(1)).Returns((Client)null));

            Assert.ThrowsException<InvalidOperationException>(() =>
                service.Update(1, dto));

            repository.Verify(r => r.Update(It.IsAny<Client>()), Times.Never);
            dogRepository.VerifyNoOtherCalls();
        }

        [TestMethod]
        public void Update_ShouldMapAndCallRepository_WhenValid()
        {
            var existing = new Client { Id = 1, Name = "Old", Phone = "000" };
            var dto = new ClientDto { Name = "New", Phone = "999" };

            var (service, repository, dogRepository) = CreateService(clientSetup: r =>
            {
                r.Setup(x => x.GetById(1)).Returns(existing);
                r.Setup(x => x.Update(It.Is<Client>(c =>
                    c.Name == dto.Name &&
                    c.Phone == dto.Phone)));
            });

            service.Update(1, dto);

            repository.Verify(r => r.Update(It.Is<Client>(c =>
                c.Name == dto.Name &&
                c.Phone == dto.Phone)), Times.Once);
            repository.Verify(r => r.GetById(1), Times.Once);
            dogRepository.VerifyNoOtherCalls();

        }

        [TestMethod]
        public void GetById_ShouldReturnMappedDto_WhenClientExists()
        {
            var client = new Client { Id = 1, Name = "John", Phone = "123" };

            var (service, _, dogRepository) = CreateService(clientSetup: r =>
                r.Setup(x => x.GetById(1)).Returns(client));

            var result = service.GetById(1);

            Assert.IsNotNull(result);
            Assert.AreEqual("John", result.Name);
            Assert.AreEqual("123", result.Phone);
            dogRepository.VerifyNoOtherCalls();
        }

        [TestMethod]
        public void GetById_ShouldReturnNull_WhenClientDoesNotExist()
        {
            var (service, _, dogRepository) = CreateService(clientSetup: r =>
                r.Setup(x => x.GetById(1)).Returns((Client)null));

            var result = service.GetById(1);

            Assert.IsNull(result);
            dogRepository.VerifyNoOtherCalls();
        }

    }
}
