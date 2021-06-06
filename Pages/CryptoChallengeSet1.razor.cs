using Cryptopals.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cryptopals.Pages
{
    public partial class CryptoChallengeSet1
    {
        private double ElapsedMs { get; set; }
        public List<string> Challenge4List { get; set; } = new();
        private string HexStringChallange1 { get; set; } = "49276d206b696c6c696e6720796f757220627261696e206c696b65206120706f69736f6e6f7573206d757368726f6f6d";
        private readonly Dictionary<char, double> MostUsedCharFrequencyDict = new()
        {
            {'e',12.60},
            {'t',9.37 },
            {'a',8.34 },
            {'o',7.70 },
            {'n',6.80 },
            {'i',6.71 },
            {'h',6.11 },
            {'s',6.11 },
            {'r',5.68 },
            {'l',4.24 },
            {'d',4.14 },
            {'u',2.85 }
        };

        protected override async Task OnInitializedAsync()
        {
            await ImportTextFile();
        }

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

        private List<XorResult> CrackSingleByteXOR(string value)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();

            var hexValue = StringToByteArray(value);
            var xordValues = new List<XorResult>();
            foreach (var key in Enumerable.Range(0, 127))
            {
                var keyAsHex = Convert.ToChar(key);
                var result = new byte[hexValue.Length];
                for (int x = 0; x < hexValue.Length; x++)
                {
                    result[x] = (byte)(hexValue[x] ^ Convert.ToByte(keyAsHex));
                }
                var plaintext = Encoding.UTF8.GetString(result);
                xordValues.Add(new XorResult(keyAsHex, plaintext, MostUsedCharFrequencyDict));
            }
            watch.Stop();
            ElapsedMs = watch.Elapsed.TotalMilliseconds;
            return xordValues.OrderByDescending(x => x.CharFrequencyRating).ToList();
        }

        private async Task ImportTextFile()
        {
            var data = await File.ReadAllLinesAsync("C:/Users/cleme/source/repos/Cryptopals/Data/challenge_4_data.txt");
            Challenge4List = data.ToList();
        }
        private List<XorResult> CrackSingleByteXORFromFile(List<string> importedList)
        {

            var watch = System.Diagnostics.Stopwatch.StartNew();
            var xordValues = new List<XorResult>();
            for (int y = 0; y < importedList.Count; y++)
            {
                var hexValue = StringToByteArray(importedList[y].Trim().ToLower());
                foreach(var key in Enumerable.Range(0, 127))
                {
                    var keyAsHex = Convert.ToChar(key);
                    var result = new byte[hexValue.Length];
                    for (int x = 0; x < hexValue.Length; x++)
                    {
                        result[x] = (byte)(hexValue[x] ^ Convert.ToByte(keyAsHex));
                    }
                    var plaintext = Encoding.UTF8.GetString(result);
                    //todo add scoring methode 
                    xordValues.Add(new XorResult(keyAsHex, plaintext, MostUsedCharFrequencyDict));
                }
            }

            
            watch.Stop();
            ElapsedMs = watch.Elapsed.TotalMilliseconds;
            return xordValues.OrderByDescending(x => x.CharFrequencyRating).ToList();
        }

    }
}
