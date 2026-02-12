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

        /// <summary>
        /// Initializes a new instance of <see cref="ClientService"/>.
        /// </summary>
        /// <param name="clientRepository">Client repository instance.</param>
        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository ?? throw new ArgumentNullException(nameof(clientRepository));
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
            _clientRepository.Delete(clientId);
        }

        public ClientDto GetById(int id)
        {
            var client = _clientRepository.GetById(id);

            if (client == null)
                return null;

            return MapToDto(client) ;
        }

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
