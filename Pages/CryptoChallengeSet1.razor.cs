using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cryptopals.Pages
{
    public partial class CryptoChallengeSet1
    {
        private string HexStringChallange1 { get; set; } = "49276d206b696c6c696e6720796f757220627261696e206c696b65206120706f69736f6e6f7573206d757368726f6f6d";
        private string HexStringToBase64String(string value)
        {
            return Convert.ToBase64String(StringToByteArray(value));
        }

        private byte[] StringToByteArray(string value)
        {
            var result = new byte[value.Length / 2];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = Convert.ToByte(value.Substring(i * 2, 2), 16);
            }
            return result;
        }

        private string FixedXOR(string value, string key)
        {
            if (value.Length != key.Length)
            {
                throw new ArgumentException("the string and the key need to have the same length");
            }
            else
            {
                //convert value and key to a byte array
                var hexValue = StringToByteArray(value);
                var hexKey = StringToByteArray(key);
                var result = new byte[hexValue.Length];
                for (int i = 0; i < hexValue.Length; i++)
                {
                    result[i] = (byte)(hexValue[i] ^ hexKey[i]);
                }
                return Convert.ToHexString(result);
            }
        }



    }
}
