using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectBeer.Models
{
    [Table("Rating")]
    public class Rating
    {
        [Key]
        public int RatingId { get; set; }
        public int BeerIdFK { get; set; }
        public int UserIdFK { get; set; }
        [Range(1,10)]
        [Display(Name = "Calificación de 1 a 10")]
        public int Qualification { get; set; }
        [StringLength(2000, ErrorMessage = "Este campo debe tener 2000 caracteres como máximo")]
        [Display(Name = "Descripción")]
        public string Description { get; set; }
    }
}
