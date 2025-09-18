using BCrypt.Net;

namespace Bycript;

public class BCryptService : IBCryptService
{
    public string HashText(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            throw new ArgumentException("el texto no puede ser nulo o vacío", nameof(text));
        }

        return BCrypt.Net.BCrypt.HashPassword(text, workFactor: 12);
    }

    public bool VerifyText(string text, string hash)
    {
        if (string.IsNullOrEmpty(text))
        {
            throw new ArgumentException("la contraseña no puede estar nula o vacía", nameof(text));
        }

        if (string.IsNullOrEmpty(hash))
        {
            throw new ArgumentException("Hash no puede ser nulo o vacío", nameof(hash));
        }

        try
        {
            return BCrypt.Net.BCrypt.Verify(text, hash);
        }
        catch (SaltParseException)
        {
            return false;
        }
    }
}