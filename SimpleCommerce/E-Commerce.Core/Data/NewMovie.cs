

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Data
{
    public class NewMovie
    {
        public int ID { get; set; }
        [Display(Name ="Movie Name")]
        [Required(ErrorMessage ="Name is Required")]
        public string Name { get; set; }
        [Display(Name = "Movie Description")]
        [Required(ErrorMessage = "Description is Required")]
        public string Description { get; set; }
        [Display(Name = "Movie Photo")]
        [Required(ErrorMessage = "Photo is Required")]
        public string ImageURL { get; set; }
        [Display(Name = "Movie Price $")]
        [Required(ErrorMessage = "Price is Required")]
        public double Price { get; set; }
        [Display(Name = "Movie StartDate")]
        [Required(ErrorMessage = "StartDate is Required")]
        public DateTime StartTime { get; set; }
        [Display(Name = "Movie EndDate")]
        [Required(ErrorMessage = "EndDate is Required")]
        public DateTime EndTime { get; set; }
        [Display(Name = "Movie Category")]
        [Required(ErrorMessage = "Category is Required")]
        public MovieCategory Category { get; set; }
        [Display(Name = "Movie Actors")]
        [Required(ErrorMessage = "Actors is Required")]
        public List<int> ActorIDs{ get; set; }
        [Display(Name = "Select a Cinema")]
        [Required(ErrorMessage = "Cinema is Required")]
        public int CinemaID { get; set; }
        [Display(Name = "Select Producer")]
        [Required(ErrorMessage = "Producer is Required")]

        public int ProducerID { get; set; }
    }
}
