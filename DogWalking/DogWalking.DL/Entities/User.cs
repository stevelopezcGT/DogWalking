namespace DogWalking.DL.Entities
{
    /// <summary>
    /// Data entity for a user.
    /// </summary>
    public class User : BaseEntity
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
