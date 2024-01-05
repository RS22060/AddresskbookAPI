﻿namespace AdressbookApp.Models
{
    public class Address
    {
        public int AddressId { get; set; }

        public string? FullName { get; set; }

        public string? Email { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Country { get; set; }

        public string? City { get; set; }

        public string? Region { get; set; }

        public string? Street { get; set; }

        public string? ApartmentNumber { get; set; }
    }
}
