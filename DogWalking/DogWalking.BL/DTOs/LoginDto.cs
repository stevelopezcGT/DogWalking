namespace DogWalking.BL.DTOs
{
    /// <summary>
    /// Represents the credentials required to authenticate a user.
    /// </summary>
    /// <remarks>
    /// This Data Transfer Object (DTO) carries the username and password from a client
    /// to the authentication service. Passwords should be transmitted over secure
    /// channels (for example, HTTPS) and should not be logged or stored in plaintext.
    /// </remarks>
    public class LoginDto
    {
        /// <summary>
        /// Gets or sets the username of the user attempting to authenticate.
        /// </summary>
        /// <value>
        /// A non-empty string that identifies the user (for example, a username or email).
        /// </value>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the user's password in plaintext as supplied for authentication.
        /// </summary>
        /// <value>
        /// A string containing the user's password. Handle securely and clear from memory
        /// as soon as it is no longer needed.
        /// </value>
        public string Password { get; set; }
    }
}