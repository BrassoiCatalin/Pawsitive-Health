using System;
using System.Collections.Generic;
using System.Text;

namespace YourPetsHealth.Models
{
    internal class Pet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Species { get; set; }
        public string Breed { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public int UserId { get; set; }
    }
}
