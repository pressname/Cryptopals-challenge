using System.Collections.Generic;

namespace Cryptopals.Entities
{
    public class CeasarResult
    {
        public string Text { get; set; }
        public int Shift { get; set; }
        public double CharFrequencyRating { get; set; }
        private Dictionary<char, double> LanguageCharFrequencyDict { get; set; }

        public CeasarResult(int shift, string text, Dictionary<char, double> languageCharFrequencyDict)
        {
            Shift = shift;
            Text = text;
            LanguageCharFrequencyDict = languageCharFrequencyDict;
            CharFrequencyRating = CalcCharFrequencyRating();
        }

        private double CalcCharFrequencyRating()
        {
            //make char frequency dict
            var charFrequency = new Dictionary<char, double>();
            var valueLenght = Text.Length;
            var lowerCaseResult = Text.ToLower();

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
    }
}
