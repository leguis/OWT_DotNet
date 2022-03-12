namespace Contact;

public class Encryption
{
    // TODO Better encryption (Not Base64)
    public static string? Encrypt(string str) {
        if (str == null)
            return null;
        var bytes = System.Text.Encoding.UTF8.GetBytes(str);
        return System.Convert.ToBase64String(bytes);
    }

    public static string? Decrypt(string str) {
        if (str == null)
            return null;
        var bytes = System.Convert.FromBase64String(str);
        return System.Text.Encoding.UTF8.GetString(bytes);
    }
}