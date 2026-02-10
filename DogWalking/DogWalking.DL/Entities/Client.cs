using System.Collections.Generic;

namespace DogWalking.DL.Entities
{
    /// <summary>
    /// Data entity for a client.
    /// </summary>
    public class Client
    {
        /// <summary>
        /// Initializes a new instance of <see cref="Client"/>.
        /// </summary>
        public Client()
        {
            Dogs = new HashSet<Dog>();
        }

        /// <summary>
        /// Gets or sets the client id.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the client name.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the client phone number.
        /// </summary>
        public string Phone { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets client dogs.
        /// </summary>
        public ICollection<Dog> Dogs { get; set; }
    }
}
