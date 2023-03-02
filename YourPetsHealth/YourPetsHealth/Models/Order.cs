using System;
using System.Collections.Generic;
using System.Text;

namespace YourPetsHealth.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public List<string> AllProducts { get; set; }
        public double TotalPrice { get; set; }
        public Guid UserId { get; set; }
        public Guid ClinicId { get; set; }
    }
}
