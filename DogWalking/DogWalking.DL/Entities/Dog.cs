using System.Collections.Generic;

namespace DogWalking.DL.Entities
{
    /// <summary>
    /// Represents a dog that belongs to a client in the system.
    /// Contains identifying information and related walks.
    /// </summary>
    public class Dog
    {
        /// <summary>
        /// Gets or sets the unique identifier for the dog.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the identifier of the client that owns the dog.
        /// This is a foreign key to the <see cref="Client"/> entity.
        /// </summary>
        public int ClientId { get; set; }

        /// <summary>
        /// Gets or sets the dog's name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the dog's breed.
        /// </summary>
        public string Breed { get; set; }

        /// <summary>
        /// Gets or sets the dog's age in years.
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// Gets or sets the client that owns the dog.
        /// Navigation property to the <see cref="Client"/> entity.
        /// </summary>
        public Client Client { get; set; }

        /// <summary>
        /// Gets or sets the collection of walks associated with the dog.
        /// Navigation property containing zero or more <see cref="Walk"/> entries.
        /// </summary>
        public ICollection<Walk> Walks { get; set; }
    }
}