using System;

namespace DogWalking.DL.Entities
{
    /// <summary>
    /// Represents a single walking session for a dog.
    /// </summary>
    /// <remarks>
    /// This entity contains the primary key, a foreign key to the associated <see cref="Dog"/>,
    /// the date/time when the walk occurred and the duration in minutes.
    /// </remarks>
    public class Walk
    {
        /// <summary>
        /// Gets or sets the unique identifier for the walk.
        /// </summary>
        /// <value>The primary key of the walk record.</value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the dog that was walked.
        /// </summary>
        /// <value>Foreign key referencing the associated <see cref="Dog"/>.</value>
        public int DogId { get; set; }

        /// <summary>
        /// Gets or sets the date and time when the walk started.
        /// </summary>
        /// <value>The start date and time of the walk.</value>
        /// <remarks>
        /// Store times consistently (for example, UTC) according to application conventions.
        /// </remarks>
        public DateTime WalkDate { get; set; }

        /// <summary>
        /// Gets or sets the duration of the walk in whole minutes.
        /// </summary>
        /// <value>The length of the walk expressed in minutes.</value>
        public int DurationMinutes { get; set; }

        /// <summary>
        /// Gets or sets the navigation property for the associated dog.
        /// </summary>
        /// <value>The <see cref="Dog"/> entity that was walked.</value>
        public Dog Dog { get; set; }
    }
}