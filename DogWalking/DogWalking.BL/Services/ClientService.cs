using DogWalking.BL.DTOs;
using DogWalking.BL.Validators;
using DogWalking.DL.Entities;
using DogWalking.DL.Repositories;
using System;
using System.Collections.Generic;

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
        public List<Client> GetAll()
        {
            return _clientRepository.GetAll();
        }

        /// <summary>
        /// Searches clients by term.
        /// </summary>
        /// <param name="searchTerm">Search term.</param>
        /// <returns>Matching clients or all clients when the term is blank.</returns>
        public List<Client> Search(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return _clientRepository.GetAll();

            return _clientRepository.Search(searchTerm.Trim());
        }

        /// <summary>
        /// Deletes a client by id.
        /// </summary>
        /// <param name="clientId">Client id.</param>
        public void Delete(int clientId)
        {
            _clientRepository.Delete(clientId);
        }
    }
}
