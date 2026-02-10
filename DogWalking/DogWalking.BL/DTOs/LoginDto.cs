namespace DogWalking.BL.DTOs
{
    /// <summary>
    /// DTO for login credentials.
    /// </summary>
    public class LoginDto
    {
        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        public string Password { get; set; }
    }
}
