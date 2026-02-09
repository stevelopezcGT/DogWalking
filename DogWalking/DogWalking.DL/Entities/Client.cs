using System.Collections.Generic;

namespace DogWalking.DL.Entities
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public ICollection<Dog> Dogs { get; set; }
    }
}