using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Entitites
{
    public class ApplicationUser:IdentityUser
    {
        [Display(Name ="User Full Name")]
        public string FullName { get; set; }

        public string Name { get; set; }
        public string SurName { get; set; }
        public string IdentityNumber { get; set; }
        public string LastLoginDate { get; set; } 
        public string RegistrationDate { get; set; }
        public string RegistrationAdress { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
    }
}
