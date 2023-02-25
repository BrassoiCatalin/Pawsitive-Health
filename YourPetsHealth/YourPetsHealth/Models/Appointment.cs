using System;
using System.Collections.Generic;
using System.Text;

namespace YourPetsHealth.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public DateTime StartDateTime { get; set; }
        public int ClinicId { get; set; }
        public int UserId { get; set; }
        public string Procedures { get; set; }
        public int TotalTime { get; set; }
        public double TotalPrice { get; set; }
        public bool IsActive { get; set; }
    }
}
