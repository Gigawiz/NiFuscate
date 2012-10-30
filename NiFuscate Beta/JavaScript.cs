using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace NiFuscate_Beta
{
    class JavaScript
    {
        public string remcom(string input)
        {
            var blockComments = @"/\*(.*?)\*/";
            var lineComments = @"//(.*?)\r?\n";
            var strings = @"""((\\[^\n]|[^""\n])*)""";
            var verbatimStrings = @"@(""[^""]*"")+";
            string noComments = Regex.Replace(input, blockComments + "|" + lineComments + "|" + strings + "|" + verbatimStrings, me =>
            {
                 if (me.Value.StartsWith("/*") || me.Value.StartsWith("//"))
                 return me.Value.StartsWith("//") ? Environment.NewLine : "";
                // Keep the literal strings
                return me.Value;
            },
         RegexOptions.Singleline);
            return noComments;
        }
    }
}
