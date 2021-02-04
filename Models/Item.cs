using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaDeliveryManagement.Models
{
    public class Item
    {
        [Key]
        public int Id { get; set; }
        public int Quantity { get; set; }


        public int MenuId { get; set; }
        [NotMapped]
        [ForeignKey("MenuId")]
        public virtual Menu Menu { get; set; }

      
    }
}
