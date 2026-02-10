using System;

namespace DogWalking.BL.DTOs
{
    /// <summary>
    /// DTO for walk data.
    /// </summary>
    public class WalkDto
    {
        /// <summary>
        /// Gets or sets the dog id.
        /// </summary>
        public int DogId { get; set; }

        /// <summary>
        /// Gets or sets the walk date.
        /// </summary>
        public DateTime WalkDate { get; set; }

        /// <summary>
        /// Gets or sets the walk duration in minutes.
        /// </summary>
        public int DurationMinutes { get; set; }
    }
}
