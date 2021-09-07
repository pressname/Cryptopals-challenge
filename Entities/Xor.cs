using Cryptopals.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cryptopals.Entities
{
    public static class Xor
    {
        public static string ExpandKey(string hex, string key)
        {
            var expandedKey = string.Empty;

            while (expandedKey.Length < hex.Length)
            {
                expandedKey += key;
            }

            return expandedKey;
        }
        public static byte[] XorWithKey(byte[] value, byte[] key)
        {
            var ciphertext = new byte[value.Length];
            if (value.Length != key.Length)
            {
                throw new ArgumentException("value and key need to be the same length; Expand the key with the ExpandKey methode");

            }

            for (int q = 0; q < value.Length; q++)
            {
                ciphertext[q] = (byte)(value[q] ^ key[q]);
            }
            return ciphertext;
        }
    }
}
