using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace Acrolinx.Sdk.Sidebar.Util.Changetracking
{
    public static class TextExtraction
    {
        private static string REPLACE_SCRIPTS_REGEXP = "<script\\b[^<]*(?:(?!<\\/script>)<[^<]*)*</script>";
        private static string REPLACE_STYLES_REGEXP = "<style\\b[^<]*(?:(?!<\\/style>)<[^<]*)*</style>";
        private static string REPLACE_TAG_REGEXP = "<([^>]+)>";
        private static string REPLACE_ENTITY_REGEXP = "&.*?;";

        private static string[] REPLACE_TAGS_PARTS = { REPLACE_SCRIPTS_REGEXP, REPLACE_STYLES_REGEXP, REPLACE_TAG_REGEXP, REPLACE_ENTITY_REGEXP };
        private static string REPLACE_TAGS_REGEXP = '(' + string.Join("|", REPLACE_TAGS_PARTS) + ')';

        private static HashSet<string> NEW_LINE_TAGS = new HashSet<string> { "BR", "P", "DIV" };
        private static HashSet<string> AUTO_SELF_CLOSING_LINE_TAGS = new HashSet<string> { "BR" };

        private static string getTagReplacement(string completeTag)
        {
            Regex regex = new Regex(@"^<(\/?)(\w+)");

            Match match = regex.Match(completeTag.ToUpper());
            string[] matches = new string[3] {"","",""};
            for (int i = 0; i < match.Groups.Count; i++)
            {
                if (i == 0)
                {
                    continue;
                }
                matches[i - 1] = match.Groups[i].Value;
            }

            string slash1 = matches[0];
            string tagName = matches[1];
            string slash2 = matches[2];

            if (!string.IsNullOrEmpty(tagName))
            {
                if (AUTO_SELF_CLOSING_LINE_TAGS.Contains(tagName) || (NEW_LINE_TAGS.Contains(tagName)
                    && (!string.IsNullOrEmpty(slash1) || !string.IsNullOrEmpty(slash2))))
                {
                    return "\n";
                }
            }
            return string.Empty;

        }
        public static Tuple<string, List<Tuple<double, double>>> extractText(string content)
        {
            Regex regex = new Regex(REPLACE_TAGS_REGEXP, RegexOptions.IgnoreCase);
            List<Tuple<double, double>> offsetMapping = new List<Tuple<double, double>>();
            int currentDiffOffset = 0;
            string resultText = regex.Replace(content, delegate (Match m)
            {
                int offset = m.Index;
                string tagOrEntity = m.Value;
                string rep = tagOrEntity.StartsWith("&") ?
                                HttpUtility.HtmlDecode(tagOrEntity) :
                                getTagReplacement(tagOrEntity);

                currentDiffOffset -= tagOrEntity.Length - rep.Length;
                offsetMapping.Add(new Tuple<double, double>(offset + tagOrEntity.Length, currentDiffOffset));

                return rep;
            });

            return new Tuple<string, List<Tuple<double, double>>>(resultText, offsetMapping);
        }

    }
}
