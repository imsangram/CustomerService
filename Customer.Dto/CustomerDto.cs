using System;
using System.ComponentModel.DataAnnotations;

namespace Customer.Dto
{
    public class CustomerDto : CustomerCreateDto
    {
        [Required]
        public int Id { get; set; }
    }
}
