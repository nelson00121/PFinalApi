using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models
{
    public class LoginDTO
    {

        [Required(ErrorMessage = "el usuario es obligatorio")]
        [StringLength(1000, MinimumLength = 1, ErrorMessage = "EL usuario debe tener entre 1 y 100 caracteres")]
        [EmailAddress]
        public string User { get; set; }

         [Required(ErrorMessage = "la contraseña obligatoria")]
        [StringLength(1000, MinimumLength = 1, ErrorMessage = "La contraseña debe tener entre 1 y 100 caracteres")]
         public string Password { get; set; }

    }
}