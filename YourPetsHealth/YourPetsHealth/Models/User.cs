using System;
using System.Collections.Generic;
using System.Text;

namespace YourPetsHealth.Models
{
    internal class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public byte[] Image { get; set; }
        public string Role { get; set; }
        public int ClinicId { get; set; }
        public int AddressId { get; set; }
    }
}
