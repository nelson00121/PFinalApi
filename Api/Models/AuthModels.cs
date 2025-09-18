using System.ComponentModel.DataAnnotations;

namespace Api.Models;

public class LoginResponseDto
{
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public DateTime AccessTokenExpiry { get; set; }
    public DateTime RefreshTokenExpiry { get; set; }
    public UserInfoDto User { get; set; } = new();
}

public class RefreshTokenRequestDto
{
    [Required(ErrorMessage = "El token de actualización es obligatorio")]
    public string RefreshToken { get; set; } = string.Empty;
}

public class RefreshTokenResponseDto
{
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public DateTime AccessTokenExpiry { get; set; }
    public DateTime RefreshTokenExpiry { get; set; }
}

public class UserInfoDto
{
    public int Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public bool IsAdmin { get; set; }
    public bool Activo { get; set; }
    public int EmpleadoId { get; set; }
}

public class RefreshToken
{
    public int Id { get; set; }
    public string Token { get; set; } = string.Empty;
    public int UsuarioId { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime ExpiresAt { get; set; }
    public bool IsRevoked { get; set; }
    public string? RevokedBy { get; set; }
    public DateTime? RevokedAt { get; set; }
    public string? ReplacedByToken { get; set; }
    
    public bool IsExpired => DateTime.UtcNow >= ExpiresAt;
    public bool IsActive => !IsRevoked && !IsExpired;
}