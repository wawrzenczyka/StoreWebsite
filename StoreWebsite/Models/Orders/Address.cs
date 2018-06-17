using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StoreWebsite.Models
{
    public class Address
    {
        [Key]
        public Guid Id { get; set; }
        [Required, Display(Name = "First name")]
        public string FirstName { get; set; }
        [Required, Display(Name = "Last name")]
        public string LastName { get; set; }
        [Required]
        public string Street { get; set; }
        [Required]
        public string Building { get; set; }
        public string Apartment { get; set; }
        [Required, Display(Name = "Postal code")]
        public string PostalCode { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Country { get; set; }
    }
}