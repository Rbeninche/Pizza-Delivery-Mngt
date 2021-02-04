using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaDeliveryManagement.Models
{
    public class PizzaSize
    {
        [Key]
        public string PizzaSizeId { get; set; }
        [Required]
        public string Size { get; set; }

    }
}
