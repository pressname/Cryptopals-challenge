using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cryptopals.Services
{
    public class LatexGenerator
    {

        private const string TableStart = @"\begin{center}"+ "\n"+ @"\begin{tabular}";
        private const string TableEnd = @"\end{tabular}"+ "\n" + @"\end{center}"+ "\n";
        public string DictToLatexTable<TKey, TValue>(Dictionary<TKey, TValue> value, string keyName, string valueName)
        {
            var tableStart = TableStart + @"{|c|c|}" +"\n";
            var tableHeader = @$"\hline {keyName} & {valueName}\\ " + "\n";
            var tableContent =  "";
            foreach (var item in value)
            {
                tableContent += @$"\hline {item.Key} & {item.Value}\\ "+ "\n";
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
                tableContent += @"\hline \text{"+item+@"} \\ " + "\n";
            }
            return tableStart + tableHeader + tableContent + TableEnd;
        }
        //private string SanitizeStringForLatex(string value)
        //{
        //    var regex;
        //    return $"";
        //}
    }
}
