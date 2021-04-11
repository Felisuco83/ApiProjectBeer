using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectBeer.Models
{
    [Table("Beer")]
    public class Beer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int BeerId { get; set; }
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Este campo es obligatorio")]
        [StringLength(150, ErrorMessage = "Este campo debe tener 150 caracteres como máximo")]
        public string Name { get; set; }
        [Column(TypeName = "decimal(3, 1)")]
        [Display(Name = "Gradación")]
        public decimal Proof { get; set; }
        [Column("CategoryIdFK")]
        [Display(Name = "Categoría")]
        public int Category { get; set; }
        [StringLength(150, ErrorMessage = "Este campo debe tener 150 caracteres como máximo")]
        [Display(Name = "Imagen")]
        public string ImagePath { get; set; }
        [StringLength(100, ErrorMessage = "Este campo debe tener 100 caracteres como máximo")]
        [Display(Name = "País")]
        public string Country { get; set; }
        [StringLength(60, ErrorMessage = "Este campo debe tener 60 caracteres como máximo")]
        [Display(Name = "Marca")]
        public string Company { get; set; }
        [StringLength(1500, ErrorMessage = "Este campo debe tener 1500 caracteres como máximo")]
        [Display(Name = "Descripción")]
        public string Description { get; set; }
        public bool IsApproved { get; set; }
    }
}
