using System;
using System.Collections.Generic;
using System.Text;

namespace YourPetsHealth.Models
{
    internal class Clinic
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; }
        public DateTime StartHour { get; set; }
        public DateTime EndHour { get; set; }
    }
}
