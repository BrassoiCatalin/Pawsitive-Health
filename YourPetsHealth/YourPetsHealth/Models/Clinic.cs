using System;
using System.Collections.Generic;
using System.Text;

namespace YourPetsHealth.Models
{
    internal class Clinic
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int AddressId { get; set; }
        public DateTime StartHour { get; set; }
        public DateTime EndHour { get; set; }
    }
}
