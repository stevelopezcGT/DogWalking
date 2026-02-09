using System.Collections.Generic;

namespace DogWalking.DL.Entities
{
    public class Dog
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string Name { get; set; }
        public string Breed { get; set; }
        public int Age { get; set; }

        public Client Client { get; set; }
        public ICollection<Walk> Walks { get; set; }
    }
}