using DogWalking.BL.DTOs;
using DogWalking.BL.Validators;
using DogWalking.DL.Entities;
using DogWalking.DL.Repositories;
using System;

namespace DogWalking.BL.Services
{
    /// <summary>
    /// Service for dog operations.
    /// </summary>
    public class DogService
    {
        private readonly IDogRepository _dogRepository;

        /// <summary>
        /// Initializes a new instance of <see cref="DogService"/>.
        /// </summary>
        /// <param name="dogRepository">Dog repository instance.</param>
        public DogService(IDogRepository dogRepository)
        {
            _dogRepository = dogRepository ?? throw new ArgumentNullException(nameof(dogRepository));
        }

        /// <summary>
        /// Validates and adds a dog.
        /// </summary>
        /// <param name="dto">Dog data.</param>
        public void Add(DogDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

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
