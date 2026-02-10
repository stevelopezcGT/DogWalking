namespace DogWalking.BL.DTOs
{
    /// <summary>
    /// DTO for dog data.
    /// </summary>
    public class DogDto
    {
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
    }
}
