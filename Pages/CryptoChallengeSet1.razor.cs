using Cryptopals.Data;
using Cryptopals.Entities;
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
        public List<string> Challenge6List { get; set; } = new();
        private string HexStringChallange1 { get; set; } = "49276d206b696c6c696e6720796f757220627261696e206c696b65206120706f69736f6e6f7573206d757368726f6f6d";
        private readonly Dictionary<char, double> MostUsedCharFrequencyDict = new()
        {
            { 'e', 12.02 },
            { 't', 9.10 },
            { 'a', 8.12 },
            { 'o', 7.68 },
            { 'i', 7.31 },
            { 'n', 6.95 },
            { 's', 6.28 },
            { 'r', 6.02 },
            { 'h', 5.92 },
            { 'd', 4.32 },
            { 'l', 3.98 },
            { 'u', 2.88 },
            { 'c', 2.71 },
            { 'm', 2.61 },
            { 'f', 2.30 },
            { 'y', 2.11 },
            { 'w', 2.09 },
            { 'g', 2.03 },
            { 'p', 1.82 },
            { 'b', 1.49 },
            { 'v', 1.11 },
            { 'k', 0.69 },
            { 'x', 0.17 },
            { 'q', 0.11 },
            { 'j', 0.10 },
            { 'z', 0.07 },
            { ' ', 0.19 }
        };

        protected override async Task OnInitializedAsync()
        {
            await ImportTextFile();
        }

        private string HexStringToBase64String(string value)
        {
            return Convert.ToBase64String(StringToByteArray(value));
        }        
        private byte[] Base64StringToByteArray(string value)
        {
            return Convert.FromBase64String(value);
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
            data = await File.ReadAllLinesAsync("C:/Users/cleme/source/repos/Cryptopals/Data/challenge_6_data.txt");
            Challenge6List = data.ToList();
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

        private int HammingDistance(string s1, string s2)
        {
            if (s1.Length != s2.Length)
            {
                throw new Exception();
            }
            var result = Xor.XorWithKey(Encoding.Default.GetBytes(s1), Encoding.Default.GetBytes(s2));
            var s1bits = string.Join("", result.Select(n => Convert.ToString(n, 2).PadLeft(8, '0')));
            return s1bits.Where(x => x.ToString() == "1").Count();
        }

        private int HammingDistance(byte[] s1, byte[] s2)
        {
            //if (s1.Length != s2.Length)
            //{
            //    throw new Exception();
            //}
            var result = Xor.XorWithKey(s1, s2);
            var s1bits = string.Join("", result.Select(n => Convert.ToString(n, 2).PadLeft(8, '0')));
            return s1bits.Where(x => x.ToString() == "1").Count();
        }

        private List<ValuePair> TestKeysize(int minSize, int maxSize, List<string> strings)
        {
            List<ValuePair> results = new();
            foreach (var item in strings)
            {
                try
                {
                    var s1 = Base64StringToByteArray(item);
                    for (int currentKeysize = minSize; currentKeysize <= maxSize; currentKeysize++)
                    {
                        var part1 = s1.Take(currentKeysize).ToArray();
                        var part2 = s1.Skip(currentKeysize).Take(currentKeysize).ToArray();
                        var editDistance = HammingDistance(part1, part2) / currentKeysize;
                        results.Add(new ValuePair(currentKeysize, editDistance ));
                    }
                }
                catch (Exception)
                {

                }

            }

            return results.OrderBy(x => x.EditDistance).ToList();
        }

       
    }
}
