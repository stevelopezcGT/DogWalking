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
        private readonly IWalkRepository _walkRepository;

        /// <summary>
        /// Initializes a new instance of <see cref="DogService"/>.
        /// </summary>
        /// <param name="dogRepository">Dog repository instance.</param>
        /// <param name="walkRepository">Walk repository instance.</param>
        public DogService(IDogRepository dogRepository, IWalkRepository walkRepository)
        {
            _dogRepository = dogRepository ?? throw new ArgumentNullException(nameof(dogRepository));
            _walkRepository = walkRepository ?? throw new ArgumentNullException(nameof(walkRepository));
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

        /// <summary>
        /// Returns all active dogs mapped to DTOs.
        /// </summary>
        /// <returns>List of active dogs.</returns>
        public List<DogDto> GetAll()
        {
            var dogs = _dogRepository.GetAll();

            return dogs
                .Select(d => MapToDto(d))
                .ToList();
        }

        /// <summary>
        /// Searches dogs by name-related fields, or returns all when the term is blank.
        /// </summary>
        /// <param name="searchTerm">Free-text search term.</param>
        /// <returns>Matching dogs or all active dogs.</returns>
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

        /// <summary>
        /// Deletes a dog by id after verifying it exists.
        /// </summary>
        /// <param name="dogId">Dog identifier.</param>
        public void Delete(int dogId)
        {
            _ = _dogRepository.GetById(dogId) ?? throw new InvalidOperationException("Dog not found.");

            var walks = _walkRepository
                .GetByDog(dogId)
                .ToList();

            if (walks.Any())
                throw new InvalidOperationException("Cannot delete a dog that has active walks.");

            _dogRepository.Delete(dogId);
        }

        /// <summary>
        /// Returns a dog by id when active, or <c>null</c> when not found.
        /// </summary>
        /// <param name="dogId">Dog identifier.</param>
        /// <returns>Dog data for the requested id, or <c>null</c>.</returns>
        public DogDto GetById(int dogId)
        {
            var dog = _dogRepository.GetById(dogId);

            if (dog == null)
                return null;

            return MapToDto(dog);
        }

        /// <summary>
        /// Validates and updates an existing dog using the provided DTO.
        /// </summary>
        /// <param name="dogId">Dog identifier.</param>
        /// <param name="dogDto">Updated dog data.</param>
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
