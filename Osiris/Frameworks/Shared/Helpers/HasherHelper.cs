using System.Security.Cryptography;
using System.Text;

namespace Shared.Helpers
{
    public interface IHasherHelper
    {
        string Hash(string plainText);
        bool Verify(string? plainText, string? hashText);
    }

    public sealed class HasherHelper : IHasherHelper
    {
        private const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        private const int saltSize = 16;
        private const int hashSize = 32;
        private const int iterations = 500000; // OWASP recommendation, iterations is at least 100,000
        private static readonly HashAlgorithmName algorithm = HashAlgorithmName.SHA512;

        public string Hash(string? plainText)
        {
            byte[] salt = GenerateRandomSalt(saltSize);
            byte[] hash = GeneratePbkdf2Hash(plainText, salt);

            return $"{ByteArrayToHexString(hash)}-{ByteArrayToHexString(salt)}";
        }

        public bool Verify(string? plainText, string? hashText)
        {
            if (string.IsNullOrEmpty(plainText) || string.IsNullOrEmpty(hashText))
            {
                return false;
            }

            string[] parts = hashText.Split('-');
            byte[] hash = HexStringToByteArray(parts[0]);
            byte[] salt = HexStringToByteArray(parts[1]);

            byte[] inputHash = GeneratePbkdf2Hash(plainText, salt);

            return CryptographicOperations.FixedTimeEquals(hash, inputHash);
        }

        #region Private Methods
        private byte[] GenerateRandomSalt(int saltSize)
        {
            var rawSalt = new StringBuilder(saltSize);
            byte[] randomBytes = new byte[1];

            using (var rng = RandomNumberGenerator.Create())
            {
                while (rawSalt.Length < saltSize)
                {
                    rng.GetBytes(randomBytes);

                    // Get random index from chars
                    int index = randomBytes[0] % chars.Length;

                    rawSalt.Append(chars[index]);
                }
            }

            byte[] salt = Encoding.UTF8.GetBytes(rawSalt.ToString());

            return salt;
        }

        private byte[] GeneratePbkdf2Hash(string? plainText, byte[] salt)
        {
            using (var rfc2898 = new Rfc2898DeriveBytes(plainText, salt, iterations, algorithm))
            {
                return rfc2898.GetBytes(hashSize);
            }
        }

        private string ByteArrayToHexString(byte[] byteArray)
        {
            var hex = new StringBuilder(byteArray.Length * 2);
            foreach (byte b in byteArray)
            {
                hex.AppendFormat("{0:x2}", b);
            }
            return hex.ToString();
        }

        private byte[] HexStringToByteArray(string hexString)
        {
            int length = hexString.Length;
            byte[] byteArray = new byte[length / 2];
            for (int i = 0; i < length; i += 2)
            {
                byteArray[i / 2] = Convert.ToByte(hexString.Substring(i, 2), 16);
            }
            return byteArray;
        }
        #endregion Private Methods
    }
}
