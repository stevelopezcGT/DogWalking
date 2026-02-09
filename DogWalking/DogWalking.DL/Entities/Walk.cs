using System;

namespace DogWalking.DL.Entities
{
    public class Walk
    {
        public int Id { get; set; }
        public int DogId { get; set; }
        public DateTime WalkDate { get; set; }
        public int DurationMinutes { get; set; }

        public Dog Dog { get; set; }
    }
}