using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaDeliveryManagement.Models
{
    public class Salary
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Salary")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [Required(ErrorMessage = "Salary is required")]
        public double SalaryPerHour { get; set; }

        [Display(Name = "Employee's Name")]
        [Required(ErrorMessage = "Select Employee Name")]
        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }


    }
}
