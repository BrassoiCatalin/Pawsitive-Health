using System;
using System.Collections.Generic;
using System.Text;

namespace YourPetsHealth.Models
{
    public class Pet
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Species { get; set; }
        public string Breed { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public Guid UserId { get; set; }
    }
}
