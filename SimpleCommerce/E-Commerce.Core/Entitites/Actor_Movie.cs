using E_Commerce.DAL.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Core.Entitites
{
    public class Actor_Movie
    {
        public int ActorID { get; set; }
        public Actor Actor { get; set; }
        public int MovieID { get; set; }
        public Movie Movie { get; set; }
        //[Key]
        //public int ID { get; set; }
    }
}
