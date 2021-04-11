using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectBeer.Models
{
    [Table("BeerRating")]
    public class BeerRating
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BeerId { get; set; }
        public string Name { get; set; }
        public decimal Proof { get; set; }
        [Column("CategoryIdFK")]
        public int Category { get; set; }
        public string ImagePath { get; set; }
        public string Country { get; set; }
        public string Company { get; set; }
        public bool IsApproved { get; set; }
        public int Qualification { get; set; }
    }
}
