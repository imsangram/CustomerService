using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Customer.Entity
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Date of Birth")]
        public DateTime DateOfBirth { get; set; }

        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [DisplayName("Last Name")]
        public string LastName { get; set; }

    }
}
