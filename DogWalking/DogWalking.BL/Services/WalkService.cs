using DogWalking.BL.DTOs;
using DogWalking.BL.Validators;
using DogWalking.DL.Entities;
using DogWalking.DL.Repositories;
using System;

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
        /// Validates and adds a walk.
        /// </summary>
        /// <param name="dto">Walk data.</param>
        public void Add(WalkDto dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            WalkValidator.Validate(dto);

            _walkRepository.Add(new Walk
            {
                DogId = dto.DogId,
                WalkDate = dto.WalkDate,
                DurationMinutes = dto.DurationMinutes
            });
        }
    }
}
