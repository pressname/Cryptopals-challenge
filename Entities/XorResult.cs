using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cryptopals.Data
{
    public class XorResult
    {
        public char Key { get; set; }
        public string Result { get; set; }
        public double CharFrequencyRating { get; set; }
        public Dictionary<char, double> LanguageCharFrequencyDict { get; set; }

        public XorResult(char key, string result, Dictionary<char, double> languageCharFrequencyDict)
        {
            Key = key;
            Result = result;
            LanguageCharFrequencyDict = languageCharFrequencyDict;
            CharFrequencyRating = CalcCharFrequencyRating();
        }

        internal double CalcCharFrequencyRating()
        {
            //make char frequency dict
            var charFrequency = new Dictionary<char, double>();
            var valueLenght = Result.Length;
            var lowerCaseResult = Result.ToLower();

            //calulate char occurents
            foreach (char c in lowerCaseResult)
            {
                if (charFrequency.ContainsKey(c))
                {
                    charFrequency[c] = +1;

                }
                else
                {
                    charFrequency.Add(c, 1);
                }
            }

            //calculate relative char frequencies
            foreach (var key in charFrequency.Keys)
            {
                charFrequency[key] = charFrequency[key] / valueLenght;
            }


            var result = 0.0;
            foreach (var key in LanguageCharFrequencyDict.Keys)
            {
                if (charFrequency.ContainsKey(key))
                {
                    result += (double)(LanguageCharFrequencyDict[key] - charFrequency[key]);
                }
            }

            return result;
        }

        public override string ToString()
        {
            return $"key:{Key} - result:{Result} - frequency score:{CharFrequencyRating}";
        }
    }
}
