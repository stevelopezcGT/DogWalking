using DogWalking.BL.DTOs;
using DogWalking.BL.Validators;
using DogWalking.DL.Entities;
using DogWalking.DL.Repositories;
using System.Collections.Generic;

namespace DogWalking.BL.Services
{
    /// <summary>
    /// Provides business logic for managing clients.
    /// </summary>
    /// <remarks>
    /// This service acts as the business layer façade for client operations.
    /// It validates incoming DTOs and delegates persistence concerns to an
    /// injected <see cref="IClientRepository"/>.
    /// </remarks>
    public class ClientService
    {
        private readonly IClientRepository _clientRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientService"/> class.
        /// </summary>
        /// <param name="clientRepository">
        /// The repository used to persist and retrieve <see cref="Client"/> entities.
        /// This dependency is expected to be provided by the caller (typically via DI).
        /// </param>
        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        /// <summary>
        /// Validates the provided client DTO and adds a new client to the repository.
        /// </summary>
        /// <param name="dto">The client data transfer object containing the information to persist.</param>
        /// <remarks>
        /// This method calls <see cref="ClientValidator.Validate"/> to perform validation.
        /// Validation may throw an exception if the <paramref name="dto"/> is invalid.
        /// After successful validation a new <see cref="Client"/> entity is created and
        /// passed to the configured repository via <see cref="IClientRepository.Add"/>.
        /// </remarks>
        public void Add(ClientDto dto)
        {
            ClientValidator.Validate(dto);

            _clientRepository.Add(new Client
            {
                Name = dto.Name,
                Phone = dto.Phone
            });
        }

        /// <summary>
        /// Retrieves all clients from the repository.
        /// </summary>
        /// <returns>A list containing all <see cref="Client"/> entities.</returns>
        public List<Client> GetAll()
        {
            return _clientRepository.GetAll();
        }

        /// <summary>
        /// Searches for clients that match the provided search term.
        /// </summary>
        /// <param name="searchTerm">
        /// The search phrase to match against client data. If <c>null</c>, empty, or whitespace,
        /// the method returns all clients.
        /// </param>
        /// <returns>
        /// A list of <see cref="Client"/> entities that match the search criteria,
        /// or all clients if the search term is blank.
        /// </returns>
        /// <remarks>
        /// The provided <paramref name="searchTerm"/> is trimmed before being passed to the repository.
        /// The actual search logic is delegated to <see cref="IClientRepository.Search(string)"/>.
        /// </remarks>
        public List<Client> Search(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return _clientRepository.GetAll();

            return _clientRepository.Search(searchTerm.Trim());
        }

        /// <summary>
        /// Deletes the client with the specified identifier.
        /// </summary>
        /// <param name="clientId">The identifier of the client to delete.</param>
        /// <remarks>
        /// Deletion is delegated to <see cref="IClientRepository.Delete(int)"/>.
        /// Repository behavior for non-existent identifiers (e.g. no-op or exception) depends on the repository implementation.
        /// </remarks>
        public void Delete(int clientId)
        {
            _clientRepository.Delete(clientId);
        }
    }
}