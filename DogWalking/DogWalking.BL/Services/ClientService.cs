using DogWalking.BL.DTOs;
using DogWalking.BL.Validators;
using DogWalking.DL.Entities;
using DogWalking.DL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DogWalking.BL.Services
{
    /// <summary>
    /// Service for client operations.
    /// </summary>
    public class ClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IDogRepository _dogRepository;

        /// <summary>
        /// Initializes a new instance of <see cref="ClientService"/>.
        /// </summary>
        /// <param name="clientRepository">Client repository instance.</param>
        /// <param name="dogRepository">Dog repository instance.</param>
        public ClientService(IClientRepository clientRepository, IDogRepository dogRepository)
        {
            _clientRepository = clientRepository ?? throw new ArgumentNullException(nameof(clientRepository));
            _dogRepository = dogRepository ?? throw new ArgumentNullException(nameof(dogRepository));
        }

        /// <summary>
        /// Validates and adds a client.
        /// </summary>
        /// <param name="dto">Client data.</param>
        public void Add(ClientDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            ClientValidator.Validate(dto);

            _clientRepository.Add(new Client
            {
                Name = dto.Name,
                Phone = dto.Phone
            });
        }

        /// <summary>
        /// Gets all clients.
        /// </summary>
        /// <returns>List of clients.</returns>
        public List<ClientDto> GetAll()
        {

            var clients = _clientRepository.GetAll();

            return clients
                .Select(c => MapToDto(c)).ToList();
        }

        /// <summary>
        /// Searches clients by term.
        /// </summary>
        /// <param name="searchTerm">Search term.</param>
        /// <returns>Matching clients or all clients when the term is blank.</returns>
        public List<ClientDto> Search(string searchTerm)
        {
            searchTerm = searchTerm?.Trim();
            var clients = string.IsNullOrWhiteSpace(searchTerm) ? _clientRepository.GetAll() : _clientRepository.Search(searchTerm.Trim());

            return clients
               .Select(c => MapToDto(c)).ToList();
        }

        /// <summary>
        /// Deletes a client by id.
        /// </summary>
        /// <param name="clientId">Client id.</param>
        public void Delete(int clientId)
        {
            _ = _clientRepository.GetById(clientId) ?? throw new InvalidOperationException("Client not found.");

            var dogs = _dogRepository
                .GetByClient(clientId);

            if (dogs.Any())
                throw new InvalidOperationException("Cannot delete a client that has active dogs.");

            _clientRepository.Delete(clientId);
        }

        /// <summary>
        /// Returns a client by id when active, or <c>null</c> when not found.
        /// </summary>
        /// <param name="id">Client identifier.</param>
        /// <returns>Client data for the requested id, or <c>null</c>.</returns>
        public ClientDto GetById(int id)
        {
            var client = _clientRepository.GetById(id);

            if (client == null)
                return null;

            return MapToDto(client) ;
        }

        /// <summary>
        /// Validates and updates an existing client using the provided DTO.
        /// </summary>
        /// <param name="clientId">Client identifier.</param>
        /// <param name="clientDto">Updated client data.</param>
        public void Update(int clientId, ClientDto clientDto)
        {
            if (clientDto == null)
                throw new ArgumentNullException(nameof(clientDto));

            ClientValidator.Validate(clientDto);

            var client = _clientRepository.GetById(clientId) ?? throw new InvalidOperationException("Client not found.");

            client.Name = clientDto.Name;
            client.Phone = clientDto.Phone;

            _clientRepository.Update(client);
        }

        private ClientDto MapToDto(Client c)
        {
            return new ClientDto
            {
                Name = c.Name,
                Phone = c.Phone,
                Id = c.Id
            };
        }
    }
}
