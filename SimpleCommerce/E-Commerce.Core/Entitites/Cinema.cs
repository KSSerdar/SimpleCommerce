using E_Commerce.DAL.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Entitites
{
    public class Cinema:IEntity
    {
        [Display(Name = "Cinema Logo")]
        [Required(ErrorMessage = "Logo is Required")]
        public string Logo { get; set; }
        [Display(Name = "Cinema Name")]
        [Required(ErrorMessage = "Name is Required")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Name lenght should be between 3 and 20")]
        public string Name { get; set; }
        [Display(Name = "Cinema Description")]
        [Required(ErrorMessage = "Description is Required")]
        public string Description { get; set; }
        public List<Movie> Movies { get; set; }
        [Key]
        public int ID { get ; set ; }
    }
}
