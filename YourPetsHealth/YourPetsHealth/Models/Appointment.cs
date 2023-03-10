using System;
using System.Collections.Generic;
using System.Text;

namespace YourPetsHealth.Models
{
    public class Appointment
    {
        public Guid Id { get; set; }
        public DateTime StartDateTime { get; set; }
        public Guid ClinicId { get; set; }
        public Guid UserId { get; set; }
        public string Procedures { get; set; }
        public int TotalTime { get; set; }
        public double TotalPrice { get; set; }
        public Pet ActivePet { get; set; } = new Pet();
        public bool IsActive { get; set; }
    }
}
