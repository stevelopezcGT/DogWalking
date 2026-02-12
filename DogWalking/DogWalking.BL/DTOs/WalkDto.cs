using System;

namespace DogWalking.BL.DTOs
{
    /// <summary>
    /// DTO for walk data.
    /// </summary>
    public class WalkDto
    {
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the dog id.
        /// </summary>
        public int DogId { get; set; }

        /// <summary>
        /// Gets or sets the client id.
        /// </summary>
        public int ClientId { get; set; }

        /// <summary>
        /// Gets or sets the client name.
        /// </summary>
        public string ClientName { get; set; }

        /// <summary>
        /// Gets or sets the dog name.
        /// </summary>
        public string DogName { get; set; }

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
