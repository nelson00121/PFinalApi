using System.ComponentModel.DataAnnotations;

namespace Database.DTOS;

public class UsuarioCreateDto
{
    public int? Id { get; set; }

    [Required(ErrorMessage = "El email es obligatorio")]
    [EmailAddress(ErrorMessage = "Debe ser un email válido")]
    [StringLength(200, MinimumLength = 5, ErrorMessage = "El email debe tener entre 5 y 200 caracteres")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "La contraseña es obligatoria")]
    [StringLength(100, MinimumLength = 8, ErrorMessage = "La contraseña debe tener entre 8 y 100 caracteres")]
    public string Password { get; set; } = null!;

    [Required(ErrorMessage = "Confirme la contraseña")]
    [Compare("Password", ErrorMessage = "Las contraseñas no coinciden")]
    public string ConfirmPassword { get; set; } = null!;

    [Required(ErrorMessage = "Debe especificar si es administrador")]
    public bool IsAdmin { get; set; }

    [Required(ErrorMessage = "Debe especificar si está activo")]
    public bool Activo { get; set; }

    [Required(ErrorMessage = "El ID del empleado es obligatorio")]
    [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un empleado válido")]
    public int EmpleadoId { get; set; }
}

public class UsuarioUpdateDto
{
    [Required(ErrorMessage = "El email es obligatorio")]
    [EmailAddress(ErrorMessage = "Debe ser un email válido")]
    [StringLength(200, MinimumLength = 5, ErrorMessage = "El email debe tener entre 5 y 200 caracteres")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "Debe especificar si es administrador")]
    public bool IsAdmin { get; set; }

    [Required(ErrorMessage = "Debe especificar si está activo")]
    public bool Activo { get; set; }

    [Required(ErrorMessage = "El ID del empleado es obligatorio")]
    [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un empleado válido")]
    public int EmpleadoId { get; set; }
}

public class ChangePasswordDto
{
    [Required(ErrorMessage = "La contraseña actual es obligatoria")]
    public string CurrentPassword { get; set; } = null!;

    [Required(ErrorMessage = "La nueva contraseña es obligatoria")]
    [StringLength(100, MinimumLength = 8, ErrorMessage = "La contraseña debe tener entre 8 y 100 caracteres")]
    public string NewPassword { get; set; } = null!;

    [Required(ErrorMessage = "Confirme la nueva contraseña")]
    [Compare("NewPassword", ErrorMessage = "Las contraseñas no coinciden")]
    public string ConfirmNewPassword { get; set; } = null!;
}