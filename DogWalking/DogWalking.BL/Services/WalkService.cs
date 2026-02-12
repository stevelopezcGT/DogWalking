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
    /// Service for walk operations.
    /// </summary>
    public class WalkService
    {
        private readonly IWalkRepository _walkRepository;

        /// <summary>
        /// Initializes a new instance of <see cref="WalkService"/>.
        /// </summary>
        /// <param name="walkRepository">Walk repository instance.</param>
        public WalkService(IWalkRepository walkRepository)
        {
            _walkRepository = walkRepository ?? throw new ArgumentNullException(nameof(walkRepository));
        }

        /// <summary>
        /// Gets all walks.
        /// </summary>
        /// <returns>List of walks.</returns>
        public List<WalkDto> GetAll()
        {
            var walks = _walkRepository.GetAll();

            return walks
                .Select(MapToDto)
                .ToList();
        }

        /// <summary>
        /// Gets a walk by id.
        /// </summary>
        /// <param name="id">Walk id.</param>
        /// <returns>Matching walk or <c>null</c>.</returns>
        public WalkDto GetById(int id)
        {
            var walk = _walkRepository.GetById(id);

            if (walk == null)
                return null;

            return MapToDto(walk);
        }

        /// <summary>
        /// Searches walks by term.
        /// </summary>
        /// <param name="searchTerm">Search term.</param>
        /// <returns>Matching walks or all walks when the term is blank.</returns>
        public List<WalkDto> Search(string searchTerm)
        {
            searchTerm = searchTerm?.Trim();
            var walks = string.IsNullOrWhiteSpace(searchTerm)
                ? _walkRepository.GetAll()
                : _walkRepository.Search(searchTerm);

            return walks
                .Select(MapToDto)
                .ToList();
        }

        /// <summary>
        /// Validates and adds a walk.
        /// </summary>
        /// <param name="dto">Walk data.</param>
        /// <returns>Created walk data.</returns>
        public WalkDto Add(WalkDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            WalkValidator.Validate(dto);

            var walk = new Walk
            {
                DogId = dto.DogId,
                WalkDate = dto.WalkDate,
                DurationMinutes = dto.DurationMinutes
            };

            _walkRepository.Add(walk);

            return MapToDto(walk);
        }

        /// <summary>
        /// Validates and updates a walk.
        /// </summary>
        /// <param name="id">Walk id.</param>
        /// <param name="dto">Walk data.</param>
        /// <returns>Updated walk data.</returns>
        public WalkDto Update(int id, WalkDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            WalkValidator.Validate(dto);

            var walk = _walkRepository.GetById(id) ?? throw new InvalidOperationException("Walk not found.");

            walk.DogId = dto.DogId;
            walk.WalkDate = dto.WalkDate;
            walk.DurationMinutes = dto.DurationMinutes;

            _walkRepository.Update(walk);

            return MapToDto(walk);
        }

        /// <summary>
        /// Deletes a walk by id.
        /// </summary>
        /// <param name="id">Walk id.</param>
        public void Delete(int id)
        {
            _ = _walkRepository.GetById(id) ?? throw new InvalidOperationException("Walk not found.");
            _walkRepository.Delete(id);
        }

        private static WalkDto MapToDto(Walk walk)
        {
            return new WalkDto
            {
                Id = walk.Id,
                DogId = walk.DogId,
                ClientId = walk.Dog != null ? walk.Dog.ClientId : 0,
                ClientName = walk.Dog?.Client?.Name,
                DogName = walk.Dog?.Name,
                WalkDate = walk.WalkDate,
                DurationMinutes = walk.DurationMinutes
            };
        }
    }
}
