using System;

namespace DogWalking.DL.Entities
{
    /// <summary>
    /// Data entity for a walk.
    /// </summary>
    public class Walk
    {
        /// <summary>
        /// Gets or sets the walk id.
        /// </summary>
        public int Id { get; set; }

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

        /// <summary>
        /// Gets or sets the dog navigation property.
        /// </summary>
        public Dog Dog { get; set; }
    }
}
