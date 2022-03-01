using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Auth.Services
{
    public class PasswordHasherService
    {
        /// <summary>
        /// Size of salt
        /// </summary>
        private const int saltSize = 16;

        /// <summary>
        /// Size of hash
        /// </summary>
        private const int HashSize = 20;

        /// <summary>
        /// Creates a hash from a password
        /// </summary>
        /// <param name="password">The password</param>
        /// <param name="iterations">Number of iterations</param>
        /// <returns>The hash</returns>
        public string Hash(string password, int iterations)
        {
            // Create salt
            byte[] salt = RandomNumberGenerator.GetBytes(saltSize);

            // Create hash
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations);
            var hash = pbkdf2.GetBytes(HashSize);

            // Combine salt and hash
            var hashBytes = new byte[saltSize + HashSize];
            Array.Copy(salt, 0, hashBytes, 0, saltSize);
            Array.Copy(hash, 0, hashBytes, saltSize, HashSize);

            // Convert to base64
            var base64Hash = Convert.ToBase64String(hashBytes);

            // Format hash with extra information
            return string.Format("$MAGIK$V1${0}${1}", iterations, base64Hash);
        }

        /// <summary>
        /// Creates a hash from a password with 10000 iterations
        /// </summary>
        /// <param name="password">The password</param>
        /// <returns>The hash</returns>
        public string Hash(string password)
        {
            return Hash(password, 10000);
        }

        /// <summary>
        /// Checks if hash is supported
        /// </summary>
        /// <param name="hashString">The hash</param>
        /// <returns>Is it hash supported</returns>
        public bool IsHashSupported(string hashString)
        {
            return hashString.Contains("$MAGIK$V1$");
        }

        /// <summary>
        /// Verifies a password against a hash
        /// </summary>
        /// <param name="password">The password</param>
        /// <param name="hashedPassword">The hash</param>
        /// <returns>Is it password verified</returns>
        public bool Verify(string password, string hashedPassword)
        {
            // Check hash
            if (!IsHashSupported(hashedPassword)) throw new NotSupportedException("The hashtype is not supported");

            // Extract iteration and Base64 string
            var splittedHashString = hashedPassword.Replace("$MAGIK$V1$", "").Split('$');
            var iterations = int.Parse(splittedHashString[0]);
            var base64Hash = splittedHashString[1];

            // Get hash bytes
            var hashBytes = Convert.FromBase64String(base64Hash);

            // Get salt
            var salt = new byte[saltSize];
            Array.Copy(hashBytes, 0, salt, 0, saltSize);

            // Create hash with given salt
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations);
            byte[] hash = pbkdf2.GetBytes(HashSize);

            // Get result
            for (var i = 0; i < HashSize; i++)
            {
                if (hashBytes[i + saltSize] != hash[i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
