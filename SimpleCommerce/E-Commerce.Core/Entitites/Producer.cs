using E_Commerce.DAL.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace E_Commerce.Core.Entitites
{
    public class Producer:IEntity
    {
        [Display(Name = "Full Name")]
        [Required(ErrorMessage ="Name is Required")]
        [StringLength(50,MinimumLength =3,ErrorMessage ="Name should be between 3 and 50 lenght")]
        public string Name { get; set; }
        [Display(Name = "Biography")]
        [Required(ErrorMessage = "Biography is Required")]
        public string Bio { get; set; }
        [Display(Name = "Photo")]
        [Required(ErrorMessage = "Picture is Required")]
        public string ProfilePicture { get; set; }
        public List<Movie> Movies { get; set; }
        [Key]
        public int ID { get; set; }
    }
}
