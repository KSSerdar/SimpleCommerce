using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Entitites
{
    public class Order
    {
        [Key]
        public int ID { get; set; }
        public string Email { get; set; }
        public string UserID { get; set; }
        [ForeignKey(nameof(UserID))]
        public ApplicationUser ApplicationUser { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }
}
