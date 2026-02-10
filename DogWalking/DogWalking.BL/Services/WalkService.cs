using DogWalking.BL.DTOs;
using DogWalking.BL.Validators;
using DogWalking.DL.Entities;
using DogWalking.DL.Repositories;

namespace DogWalking.BL.Services
{
    /// <summary>
    /// Provides business logic for creating and managing walk records.
    /// Orchestrates validation of incoming data and delegates persistence to an <see cref="IWalkRepository"/>.
    /// </summary>
    public class WalkService
    {
        private readonly IWalkRepository _walkRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="WalkService"/> class.
        /// </summary>
        /// <param name="walkRepository">The repository used to persist <see cref="Walk"/> entities. Must not be <c>null</c>.</param>
        public WalkService(IWalkRepository walkRepository)
        {
            _walkRepository = walkRepository;
        }

        /// <summary>
        /// Validates the specified <see cref="WalkDto"/> and adds a corresponding <see cref="Walk"/> entity to the repository.
        /// </summary>
        /// <param name="dto">The data transfer object describing the walk to add. Must not be <c>null</c>.</param>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="dto"/> is <c>null</c>.</exception>
        /// <exception cref="System.Exception">May be thrown if validation fails or if the repository operation fails.</exception>
        public void Add(WalkDto dto)
        {
            // Guard clause: make null expectation explicit for documentation and clarity.
            if (dto == null)
            {
                throw new System.ArgumentNullException(nameof(dto));
            }

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