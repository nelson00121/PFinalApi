using System.ComponentModel.DataAnnotations;

namespace BackOffice.Models;

public class LoginModel
{
    [Required(ErrorMessage = "El usuario es requerido")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "El usuario debe tener entre 3 y 50 caracteres")]
   // [RegularExpression(@"^[a-zA-Z0-9_.]+$", ErrorMessage = "El usuario solo puede contener letras, números, puntos y guiones bajos")]
    public string Username { get; set; } = string.Empty;

    [Required(ErrorMessage = "La contraseña es requerida")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "La contraseña debe tener entre 6 y 100 caracteres")]
    public string Password { get; set; } = string.Empty;

    public bool RememberMe { get; set; } = false;
}