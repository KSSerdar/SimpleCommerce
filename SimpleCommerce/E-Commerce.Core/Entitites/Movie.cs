using E_Commerce.Core.Data;
using E_Commerce.DAL.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Entitites
{
    public class Movie:IEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }
        public double Price { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public MovieCategory Category { get; set; }
        public List<Actor_Movie> Actor_Movies { get; set; }
        public int CinemaID { get; set; }
        [ForeignKey("CinemaID")]
        public Cinema Cinema { get; set; }
        public int ProducerID { get; set; }
        [ForeignKey("ProducerID")]
        public Producer Produer { get; set; }
        [Key]
        public int ID { get ; set ; }
    }
}
