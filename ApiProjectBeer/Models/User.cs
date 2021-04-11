using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectBeer.Models
{
    [Table("APPUSER")]
    public class User
    {
        [Key]
        public int UserId { get; set; }
        [Display(Name = "Nombre de usuario")]
        public string Username { get; set; }
        public byte[] Password { get; set; }
        [NotMapped]
        [Display(Name = "Contraseña")]
        [DataType(DataType.Password)]
        public string PasswordString { get; set; }
        [NotMapped] 
        [Compare("PasswordString", ErrorMessage = "Las contraseñas deben coincidir")]
        [Display(Name = "Repite la contraseña")]
        public string ConfirmPassword { get; set; }
        public string Salt { get; set; }
        public string Email { get; set; }
        [Display(Name = "Género")]
        public string Gender { get; set; }
        [Display(Name = "Fecha de nacimiento")]
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime BirthDate { get; set; }
        [Display(Name = "Descripción personal")]
        public string UserBio { get; set; }
        public string Role { get; set; }
    }
}
