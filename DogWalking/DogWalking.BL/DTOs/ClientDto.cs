namespace DogWalking.BL.DTOs
{
    /// <summary>
    /// Represents a client in the DogWalking domain as a data transfer object (DTO).
    /// </summary>
    /// <remarks>
    /// This DTO is used to transfer client data between application layers.
    /// It contains only data required by consumers and does not include behavior.
    /// </remarks>
    public class ClientDto
    {
        /// <summary>
        /// Gets or sets the client's full name.
        /// </summary>
        /// <value>
        /// A <see cref="string"/> containing the client's display name. 
        /// Typical examples: "Jane Doe", "Acme Corp (Contact)".
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the client's phone number.
        /// </summary>
        /// <value>
        /// A <see cref="string"/> containing the client's phone number in a human-readable format.
        /// Prefer E.164 or a consistent local format (for example: "+15551234567" or "(555) 123-4567").
        /// </value>
        public string Phone { get; set; }
    }
}