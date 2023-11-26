using E_Commerce.Core.Entitites;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Data
{
    public class LoginVM
    {
        [Display(Name ="Email Address")]
        [Required(ErrorMessage ="Mail address Required")]
        public string EmailAddress { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
