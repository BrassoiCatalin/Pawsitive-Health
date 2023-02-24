using System;
using System.Collections.Generic;
using System.Text;

namespace YourPetsHealth.Models
{
    public class Clinic
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; }
        public TimeSpan StartHour { get; set; }
        public TimeSpan EndHour { get; set; }
    }
}
