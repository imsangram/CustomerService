using System;
using System.ComponentModel.DataAnnotations;

namespace Customer.Dto
{
    public class CustomerCreateDto
    {
        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

    }
}
