using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaDeliveryManagement.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }

        [Display(Name = "Terminate Employee? (Check if applicable)")]
        public bool Terminated { get; set; }

        [Display(Name = "Full Name")]
        public string FriendlyName => $"{FirstName} {LastName}";
    }
}
