using DogWalking.BL.DTOs;
using DogWalking.BL.Validators;
using DogWalking.DL.Entities;
using DogWalking.DL.Repositories;

namespace DogWalking.BL.Services
{
    /// <summary>
    /// Provides operations for managing dog-related business logic.
    /// </summary>
    /// <remarks>
    /// This service validates incoming data transfer objects (DTOs) and delegates
    /// persistence to an <see cref="IDogRepository"/>.
    /// </remarks>
    public class DogService
    {
        /// <summary>
        /// Repository used to persist and retrieve <see cref="Dog"/> entities.
        /// </summary>
        private readonly IDogRepository _dogRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="DogService"/> class.
        /// </summary>
        /// <param name="dogRepository">The repository used for dog persistence. Must not be null.</param>
        public DogService(IDogRepository dogRepository)
        {
            _dogRepository = dogRepository;
        }

        /// <summary>
        /// Validates the provided <see cref="DogDto"/> and adds a new <see cref="Dog"/> entity
        /// to the underlying repository.
        /// </summary>
        /// <param name="dto">The data transfer object containing dog information to add. Must not be null.</param>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="dto"/> is null.</exception>
        /// <exception cref="System.Exception">May be thrown when validation fails or persistence fails. Exceptions from <see cref="DogValidator"/> and <see cref="IDogRepository"/> are propagated.</exception>
        public void Add(DogDto dto)
        {
            DogValidator.Validate(dto);

            _dogRepository.Add(new Dog
            {
                ClientId = dto.ClientId,
                Name = dto.Name,
                Breed = dto.Breed,
                Age = dto.Age
            });
        }
    }
}