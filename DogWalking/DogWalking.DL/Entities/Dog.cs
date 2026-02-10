using System.Collections.Generic;

namespace DogWalking.DL.Entities
{
    /// <summary>
    /// Data entity for a dog.
    /// </summary>
    public class Dog
    {
        /// <summary>
        /// Gets or sets the dog id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the client id.
        /// </summary>
        public int ClientId { get; set; }

        /// <summary>
        /// Gets or sets the dog name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the dog breed.
        /// </summary>
        public string Breed { get; set; }

        /// <summary>
        /// Gets or sets the dog age.
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// Gets or sets the client navigation property.
        /// </summary>
        public Client Client { get; set; }

        /// <summary>
        /// Gets or sets dog walks.
        /// </summary>
        public ICollection<Walk> Walks { get; set; }
    }
}
