namespace Bycript;

public interface IBCryptService
{
    string HashText(string text);

    /// <summary>
    ///  compara el valor del texto hash y el texto normal y me devuelve un bool true si coinciden o un false si no coinciden
    /// </summary>
    /// <param name="text"></param>
    /// <param name="hash"></param>
    /// <returns></returns>
    bool VerifyText(string text, string hash);
}