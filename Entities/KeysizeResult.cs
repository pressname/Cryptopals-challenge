//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Cryptopals.Entities;

//namespace Cryptopals.Entities
//{
//    public class KeysizeResult
//    {
//        public int EditDistance { get; set; }
//        public int Keysize { get; set; }
//        public string StringValue { get; set; }
//        public string RepeatingXORKey { get; private set; }

//        public KeysizeResult(int keysize, int distance, string value)
//        {
//            Keysize = keysize;
//            EditDistance = distance;
//            StringValue = value;
//        }


//        public void ExtracKey()
//        {
//            var blocks = GenerateKeysizeBlocks();
//            var transformedBlocks = TransformBlocks(blocks);
//            //var res = CrackSingleByteXORFromFile
//        }

//        private string CrackBlocks(List<string> data)
//        {
//            var xordValues = new List<XorResult>();
//            for (int y = 0; y < importedList.Count; y++)
//            {
//                var hexValue = StringToByteArray(importedList[y].Trim().ToLower());
//                foreach (var key in Enumerable.Range(0, 127))
//                {
//                    var keyAsHex = Convert.ToChar(key);
//                    var result = new byte[hexValue.Length];
//                    for (int x = 0; x < hexValue.Length; x++)
//                    {
//                        result[x] = (byte)(hexValue[x] ^ Convert.ToByte(keyAsHex));
//                    }
//                    var plaintext = Encoding.UTF8.GetString(result);
//                    //todo add scoring methode 
//                    xordValues.Add(new XorResult(keyAsHex, plaintext, MostUsedCharFrequencyDict));
//                }
//            }

//            return xordValues.OrderByDescending(x => x.CharFrequencyRating).ToList();
//        }
//        private List<string> GenerateKeysizeBlocks()
//        {
//            List<string> blocks = new();
//            var blockCount = StringValue.Length / Keysize;
//            int indexOffset = 0;
//            for (int i = 0; i < blockCount; i++)
//            {
//                blocks.Add(StringValue.Substring(indexOffset, Keysize));
//                indexOffset += Keysize;x
//            }
//            return blocks;
//        }

//        private List<string> TransformBlocks(List<string> blocks)
//        {
//            List<string> transformedBlocks = new();
//            for (int i = 0; i < Keysize; i++)
//            {
//                var transform = "";
//                foreach (var block in blocks)
//                {
//                    String.Join(transform, block.ElementAt(i));
//                }
//                transformedBlocks.Add(transform);
//            }

//            return transformedBlocks;
//        }
        
//    }
//}
