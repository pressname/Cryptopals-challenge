using System.Collections.Generic;
using System.Linq;
using Cryptopals.Entities;
using Cryptopals.Services;

namespace Cryptopals.Pages
{
    public partial class UniKryptoCeasar
    {
        private string ChiperText = "QVR PNRFNE PUVSSER FBYYGR VA QRE CENKVF AVPUG IREJRAQRG JREQRA";
        private readonly Dictionary<char, double> FrequencyDict = new()
        {
            { 'e', 17.40 },
            { 'n', 9.78 },
            { 'i', 7.55 },
            { 's', 7.27 },
            { 'r', 7.00 },
            { 'a', 6.51 },
            { 't', 6.15 },
            { 'd', 5.08 },
            { 'h', 4.76 },
            { 'u', 4.35 },
            { 'l', 3.44 },
            { 'c', 3.06 },
            { 'g', 3.01 },
            { 'm', 2.53 },
            { 'o', 2.51 },
            { 'b', 1.89 },
            { 'w', 1.89 },
            { 'f', 1.66 },
            { 'k', 1.21 },
            { 'z', 1.13 },
            { 'p', 0.79 },
            { 'v', 0.67 },
            { 'ẞ', 0.31 },
            { 'j', 0.27 },
            { 'y', 0.04 },
            { 'x', 0.03 },
            { 'q', 0.02 }
        };

        private List<CeasarResult> BruteForceCeaser(string cipherText)
        {
            var erg = new List<CeasarResult>();
            for (int i = 0; i < 27; i++)
            {
                erg.Add(Caesar(cipherText,i));
            }
            erg = erg.OrderByDescending(x => x.CharFrequencyRating).ToList();
            return erg;
        }

        private string DoubleCeasar(string value, int shift1, int shift2)
        {
            return Caesar(Caesar(value, shift1).Text,shift2).Text;
        }


        private CeasarResult Caesar(string value, int shift)
        {
            char[] result = value.ToCharArray();
            for (int i = 0; i < result.Length; i++)
            {
                char letter = result[i];
                if (letter != ' ')
                {
                    letter = (char)(letter + shift);
                    if (letter > 'z')
                    {
                        letter = (char)(letter - 26);
                    }
                    else if (letter < 'a')
                    {
                        letter = (char)(letter + 26);
                    }
                }
                result[i] = letter;
            }
            return new CeasarResult(shift, new string(result), FrequencyDict);
        }




        private const string TableStart = @"\begin{center}" + "\n" + @"\begin{tabular}";
        private const string TableEnd = @"\end{tabular}" + "\n" + @"\end{center}" + "\n";
        public string DictToLatexTable<TKey, TValue>(Dictionary<TKey, TValue> value, string keyName, string valueName)
        {
            var tableStart = TableStart + @"{|c|c|}" + "\n";
            var tableHeader = @$"\hline {keyName} & {valueName}\\ " + "\n";
            var tableContent = "";
            foreach (var item in value)
            {
                tableContent += @$"\hline {item.Key} & {item.Value}\\ " + "\n";
            }
            return tableStart + tableHeader + tableContent + TableEnd;
        }

        public string ListToLatexTable<T>(List<T> value, string name)
        {
            var tableStart = TableStart + @"{|c|} " + "\n";
            var tableHeader = @$"\hline {name} \\ " + "\n";
            var tableContent = "";
            foreach (var item in value)
            {
                tableContent += @"\hline " + SanitizeString(item.ToString()) + @" \\ " + "\n";
            }
            return tableStart + tableHeader + tableContent + TableEnd;
        }

        private string SanitizeString(string value)
        {
            Dictionary<string, string> replacements = new()
            {
                {@"\", @"\textbackslash "},
                {"_", @"\textunderscore " },
                {"`", "" },
                {"´", "" },
                {"^", "" }
            };

            foreach (var item in replacements)
            {
                value = value.Replace(item.Key, item.Value);
            }
            return value;
        }
    }
}


