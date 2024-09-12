using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Eapproval.Helpers.IHelpers;


namespace Eapproval.Helpers;

public class Hasher:IHasher
{
     private static readonly byte[] FixedSalt = new byte[] { 0x53, 0x61, 0x6C, 0x74, 0x53, 0x61, 0x6C, 0x74, 0x53, 0x61, 0x6C, 0x74, 0x53, 0x61, 0x6C, 0x74 }; // Example fixed salt
    private const int KeySize = 32; // 256 bits
    private const int Iterations = 10000;

    public string HashPassword(string password)
    {
        // Hash the password using the fixed salt
        byte[] hash = KeyDerivation.Pbkdf2(password, FixedSalt, KeyDerivationPrf.HMACSHA512, Iterations, KeySize);

        // Combine salt and hash into a single string
        byte[] combinedBytes = new byte[FixedSalt.Length + KeySize];
        Array.Copy(FixedSalt, 0, combinedBytes, 0, FixedSalt.Length);
        Array.Copy(hash, 0, combinedBytes, FixedSalt.Length, KeySize);

        // Convert to Base64 and return
        return Convert.ToBase64String(combinedBytes);
    }

      public bool VerifyPassword(string password, string hashedPassword)
    {
        // Extract the salt and hash from the combined bytes
        byte[] combinedBytes = Convert.FromBase64String(hashedPassword);
        byte[] salt = new byte[FixedSalt.Length];
        byte[] hash = new byte[KeySize];
        Array.Copy(combinedBytes, 0, salt, 0, FixedSalt.Length);
        Array.Copy(combinedBytes, FixedSalt.Length, hash, 0, KeySize);

        // Compute the hash of the input password with the extracted salt
        byte[] testHash = KeyDerivation.Pbkdf2(password, salt, KeyDerivationPrf.HMACSHA512, Iterations, KeySize);

        // Compare the computed hash with the stored hash
        return hash.SequenceEqual(testHash);
    }
}
