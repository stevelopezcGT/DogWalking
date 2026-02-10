namespace DogWalking.BL.DTOs
{
    /// <summary>
    /// Represents a data transfer object that carries basic information about a dog.
    /// </summary>
    /// <remarks>
    /// Use this DTO to transfer non-domain-specific dog information between
    /// application layers (for example, between services and controllers).
    /// It intentionally excludes domain behavior and persistence concerns.
    /// </remarks>
    public class DogDto
    {
        /// <summary>
        /// Gets or sets the identifier of the client who owns the dog.
        /// </summary>
        /// <value>The client's unique identifier.</value>
        public int ClientId { get; set; }

        /// <summary>
        /// Gets or sets the name of the dog.
        /// </summary>
        /// <value>A human-readable name for the dog.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the breed of the dog.
        /// </summary>
        /// <value>The dog's breed or type.</value>
        public string Breed { get; set; }

        /// <summary>
        /// Gets or sets the age of the dog in years.
        /// </summary>
        /// <value>The dog's age expressed in whole years.</value>
        public int Age { get; set; }
    }
}