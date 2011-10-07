using System.Text.RegularExpressions;

namespace Lesk
{
    public class RegexInfo
    {
        public Regex Regex { get; set; }

        public RegexOptions Options { get; set; } 

        public string Pattern { get; set; }
    }
}