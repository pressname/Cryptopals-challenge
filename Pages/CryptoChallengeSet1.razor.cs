using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cryptopals.Pages
{
    public partial class CryptoChallengeSet1
    {
        private string HexString { get; set; } = "49276d206b696c6c696e6720796f757220627261696e206c696b65206120706f69736f6e6f7573206d757368726f6f6d";

        private static byte[] ConvertHexStringToByteArray(string value)
        {
            if (value.Length % 2 == 0)
            {
                throw new Exception("cannot have an odd number of digits");
            }
        }

        private static string ConvertHexStringToBase64String(string value)
        {

        }
    }
}
