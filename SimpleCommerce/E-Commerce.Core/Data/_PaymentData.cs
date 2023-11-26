using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Data
{
    public class _PaymentData
    {
        [Key] 
        public int Id { get; set; }
        public string PaymentID { get; set; }
        public string IpAddress { get; set; }
    }
}
