using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CRUD_Application.Models
{
    public class Account
    {
        public int AccountNumber { get; set; }
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        [Required]
        public string? City { get; set;}
        [Required]
        public string? State { get; set; }
        [Required]
        public DateTime DateOfOpening { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public string? AccountType { get; set; }
        [Required]
        public string? Status { get; set; }
    }
}
