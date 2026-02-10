using System;

namespace DogWalking.BL.DTOs
{
    /// <summary>
    /// Data transfer object that represents a single dog walk.
    /// </summary>
    public class WalkDto
    {
        /// <summary>
        /// Gets or sets the identifier of the dog that was walked.
        /// </summary>
        /// <value>The unique identifier of the dog.</value>
        public int DogId { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the walk occurred.
        /// </summary>
        /// <value>A <see cref="DateTime"/> representing when the walk took place.</value>
        /// <remarks>
        /// The value represents the local date and time for the walk by default.
        /// Consumers may choose to use UTC or convert as needed.
        /// </remarks>
        public DateTime WalkDate { get; set; }

        /// <summary>
        /// Gets or sets the duration of the walk in minutes.
        /// </summary>
        /// <value>An integer representing the duration of the walk in minutes.</value>
        public int DurationMinutes { get; set; }
    }
}