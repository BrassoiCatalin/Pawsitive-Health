using System;
using System.Collections.Generic;
using System.Text;
using YourPetsHealth.Models;

namespace YourPetsHealth.Utility
{
    public static class ActiveUser
    {
        public static User User { get; set; }
        public static Clinic Clinic { get; set; }
        public static List<Product> ProductsToBuy { get; set; }
        public static List<Order> Orders { get; set; }
        public static List<Appointment> Appointments { get; set; }
    }
}
