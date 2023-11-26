using E_Commerce.DAL.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Entitites
{
    public class Actor:IEntity
    {
        [Display(Name ="Full Name")]
        [Required(ErrorMessage = "Name is Required")]
        [StringLength(20,MinimumLength =3,ErrorMessage ="Name lenght should be between 3 and 20")]
        public string Name { get; set; }
        [Display(Name="Biography")]
        [Required(ErrorMessage = "Biography is Required")]
        public string Bio { get; set; }
        [Display(Name="Profile Picture")]
        [Required(ErrorMessage ="Picture is Required")]
        public string ProfilePicture { get; set; }
        public List<Actor_Movie> Actor_Movies { get; set; }
        [Key]
        public int ID { get; set; }
    }
}
