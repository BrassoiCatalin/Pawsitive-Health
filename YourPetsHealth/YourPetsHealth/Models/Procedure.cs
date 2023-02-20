using System;
using System.Collections.Generic;
using System.Text;

namespace YourPetsHealth.Models
{
    internal class Procedure
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public DateTime Time { get; set; }
        public int ClinicId { get; set; }
    }
}
