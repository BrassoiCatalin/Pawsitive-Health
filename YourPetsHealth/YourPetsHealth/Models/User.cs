using System;
using System.Collections.Generic;
using System.Text;

namespace YourPetsHealth.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Image { get; set; }
        public string Role { get; set; }
        public Guid ClinicId { get; set; }
        public Address Address { get; set; }
    }
}
