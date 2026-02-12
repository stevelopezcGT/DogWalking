namespace DogWalking.BL.DTOs
{
    /// <summary>
    /// DTO for client data.
    /// </summary>
    public class ClientDto
    {
        public  int  Id { get; set; }
        /// <summary>
        /// Gets or sets the client name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the client phone number.
        /// </summary>
        public string Phone { get; set; }
    }
}
