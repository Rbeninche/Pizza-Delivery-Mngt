using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaDeliveryManagement.Models
{
    public class Menu
    {
        [Key]
        public int MenuId { get; set; }

        [Display(Name = "Pizza Name")]
        [Required]
        public string PizzaName { get; set; }

        [Display(Name = "Price")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [Required]
        public decimal Price { get; set; }

        [Display(Name = "Pizza Size")]
        [Required]
        public string PizzaSizeId { get; set; }

        [ForeignKey("PizzaSizeId")]
        public virtual PizzaSize PizzaSize { get; set; }
    }
}
