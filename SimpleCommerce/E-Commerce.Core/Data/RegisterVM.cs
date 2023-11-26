using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace E_Commerce.Core.Data
{
    public class RegisterVM
    {
        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Mail address Required")]
        public string EmailAddress { get; set; }
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First Name Required")]
        public string Name { get; set; }
        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last Name Required")]
        public string SurName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name ="Confirm Password")]
        [Required(ErrorMessage ="Confirm Password Required")]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="Password do not matchs")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage ="Phone Number Required")]
        [Display(Name="Gsm Number")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage ="Identity Number Required")]
        [Display(Name="Identity Number")]
        [MaxLength(11)]
        [MinLength(11)]
        public string IdentityNumber { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string ZipCode { get; set; }
    }
}
