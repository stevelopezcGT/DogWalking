namespace DogWalking.DL.Entities
{
    /// <summary>
    /// Represents an application user in the dog walking system.
    /// </summary>
    /// <remarks>
    /// A <see cref="User"/> contains the minimal identity and authentication
    /// information required by the data layer. Higher-level layers may
    /// extend this model or encapsulate additional user profile data.
    /// </remarks>
    public class User
    {
        /// <summary>
        /// Gets or sets the unique identifier for the user.
        /// </summary>
        /// <value>An integer that uniquely identifies the user.</value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the username for the user.
        /// </summary>
        /// <value>
        /// A string that represents the user's login name or display identifier.
        /// </value>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the password used for authentication.
        /// </summary>
        /// <remarks>
        /// Implementations should store a securely hashed and salted representation
        /// of the password rather than plain text.
        /// </remarks>
        /// <value>A string containing the user's password or its hashed form.</value>
        public string Password { get; set; }
    }
}