using System.Collections.Generic;

namespace DogWalking.DL.Entities
{
    /// <summary>
    /// Represents a client of the dog walking service.
    /// </summary>
    /// <remarks>
    /// A <see cref="Client"/> may own zero or more <see cref="Dog"/> entities.
    /// This class is a simple data entity used by the data layer.
    /// </remarks>
    public class Client
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Client"/> class.
        /// </summary>
        /// <remarks>
        /// The constructor ensures the <see cref="Dogs"/> collection is initialized to an empty
        /// <see cref="HashSet{Dog}"/> so callers can safely add or enumerate dogs without
        /// checking for <c>null</c>.
        /// </remarks>
        public Client() 
        {
            Dogs = new HashSet<Dog>();
        }

        /// <summary>
        /// Gets or sets the unique identifier for the client.
        /// </summary>
        /// <value>An integer that uniquely identifies the client.</value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the client's full name.
        /// </summary>
        /// <value>A non-null string containing the client's name. Defaults to an empty string.</value>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the client's phone number.
        /// </summary>
        /// <value>A non-null string containing the client's phone number. Defaults to an empty string.</value>
        public string Phone { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the collection of dogs owned by the client.
        /// </summary>
        /// <value>
        /// A collection of <see cref="Dog"/> instances associated with the client.
        /// This property may be null if the collection has not been initialized by the data layer.
        /// </value>
        public ICollection<Dog> Dogs { get; set; }
    }
}