using System;
using System.Security.Cryptography;
using System.Text;

namespace Musicfy.Infrastructure.Utils
{
    public static class HashUtils
    {
        private const string Sha1HashProvider = "SHA1";
        /// <summary>
        /// Using SHA1 algorithm, generates the hash code of the input string.
        /// </summary>
        /// <param name="inputString">
        /// The input string.
        /// </param>
        /// <returns>
        /// Hash content of the encrypted string
        /// </returns>
        public static string EncodeString(string inputString)
        {
            // convert the input string to byte array
            byte[] byteInput = ConvertStringToByteArray(inputString);

            // Create hash value from inputString using SHA1 instance returned by Crypto Config system
            byte[] hashValue = ((HashAlgorithm)CryptoConfig.CreateFromName(Sha1HashProvider)).ComputeHash(byteInput);

            // return the string representation of the hash
            return BitConverter.ToString(hashValue);
        }

        /// <summary>
        /// Converts to byte array the string argument
        /// </summary>
        /// <param name="s">
        /// The string to be converted
        /// </param>
        /// <returns>
        /// byte array from string
        /// </returns>
        private static byte[] ConvertStringToByteArray(string s)
        {
            // returns the binary representation of the string s (treated as unicode)
            return (new UnicodeEncoding()).GetBytes(s);
        }
    }
}