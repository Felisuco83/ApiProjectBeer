using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectBeer.Models
{
    [Table("BeerCategory")]
    public class BeerCategory
    {
        [Key]
        public int BeerCategoryId { get; set; }
        [StringLength(150, ErrorMessage = "Este campo debe tener 150 caracteres como máximo")]
        public string Description { get; set; }
        [StringLength(50, ErrorMessage = "Este campo debe tener 150 caracteres como máximo")]
        public string ShortName { get; set; }
    }
}
