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

        public List<DogDto> GetAll()
        {
            var dogs = _dogRepository.GetAll();

            return dogs
                .Select(d => MapToDto(d))
                .ToList();
        }

        public List<DogDto> Search(string searchTerm)
        {
            searchTerm = searchTerm?.Trim();
            var dogs = string.IsNullOrWhiteSpace(searchTerm)
                ? _dogRepository.GetAll()
                : _dogRepository.Search(searchTerm);

            return dogs
                .Select(d => MapToDto(d))
                .ToList();
        }

        public void Delete(int dogId)
        {
            _ = _dogRepository.GetById(dogId) ?? throw new InvalidOperationException("Dog not found.");
            _dogRepository.Delete(dogId);
        }

        public DogDto GetById(int dogId)
        {
            var dog = _dogRepository.GetById(dogId);

            if (dog == null)
                return null;

            return MapToDto(dog);
        }

        public void Update(int dogId, DogDto dogDto)
        {
            if (dogDto == null)
                throw new ArgumentNullException(nameof(dogDto));

            DogValidator.Validate(dogDto);

            var dog = _dogRepository.GetById(dogId) ?? throw new InvalidOperationException("Dog not found.");

            dog.ClientId = dogDto.ClientId;
            dog.Name = dogDto.Name;
            dog.Breed = dogDto.Breed;
            dog.Age = dogDto.Age;

            _dogRepository.Update(dog);
        }

        private DogDto MapToDto(Dog dog)
        {
            return new DogDto
            {
                Id = dog.Id,
                ClientId = dog.ClientId,
                ClientName = dog.Client?.Name,
                Name = dog.Name,
                Breed = dog.Breed,
                Age = dog.Age
            };
        }
    }
}
