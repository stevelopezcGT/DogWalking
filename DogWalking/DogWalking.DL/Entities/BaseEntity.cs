using System;

namespace DogWalking.DL.Entities
{
    /// <summary>
    /// Base entity with audit and soft-delete fields.
    /// </summary>
    public abstract class BaseEntity
    {
        /// <summary>
        /// Gets or sets the entity id.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the creation date.
        /// </summary>
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// Gets or sets the creator user.
        /// </summary>
        public string CreatedBy { get; set; }
        /// <summary>
        /// Gets or sets the last update date.
        /// </summary>
        public DateTime? UpdatedAt { get; set; }
        /// <summary>
        /// Gets or sets the last updater user.
        /// </summary>
        public string UpdatedBy { get; set; }
        /// <summary>
        /// Gets or sets whether the entity is active.
        /// </summary>
        public bool IsActive { get; set; }
    }
}
