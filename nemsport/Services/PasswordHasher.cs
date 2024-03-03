using System;
using System.Security.Cryptography;

public class PasswordHasher
{
    // The following constants may be changed without breaking existing hashes.
    public const int SaltSize = 16; // 128-bit salt
    public const int HashSize = 20; // 160-bit hash
    public const int Iterations = 10000; // Number of iterations

    public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        // Use a cryptographically strong random number generator for the salt
        using (var rng = new RNGCryptoServiceProvider())
        {
            passwordSalt = new byte[SaltSize];
            rng.GetBytes(passwordSalt);
        }

        // Create the Rfc2898DeriveBytes and get the hash value
        using (var pbkdf2 = new Rfc2898DeriveBytes(password, passwordSalt, Iterations))
        {
            passwordHash = pbkdf2.GetBytes(HashSize);
        }
    }

    public static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
    {
        // Generate the hash on the password the user entered
        using (var pbkdf2 = new Rfc2898DeriveBytes(password, storedSalt, Iterations))
        {
            byte[] hash = pbkdf2.GetBytes(HashSize);
            // Compare the results
            for (int i = 0; i < HashSize; i++)
            {
                if (storedHash[i] != hash[i])
                {
                    return false;
                }
            }
        }

        return true;
    }
}