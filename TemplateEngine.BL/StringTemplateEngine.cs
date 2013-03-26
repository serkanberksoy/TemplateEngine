using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace TemplateEngine.BL
{
    public class StringTemplateEngine : IStringTemplateEngine
    {
        private const string PATTERN = @"\$\{([a-zA-Z0-9_þÞýÝçÇöÖüÜÐð]*)\}";

        public IEnumerable<string> FindTokenList(string template)
        {
            List<string> result = new List<string>();

            Regex re = new Regex(PATTERN,
                                 RegexOptions.IgnoreCase |
                                 RegexOptions.CultureInvariant |
                                 RegexOptions.Multiline);

            MatchCollection mc = re.Matches(template);

            foreach (Match m in mc)
            {
                for (int i = 1; i < m.Groups.Count; i = i + 2)
                {
                    result.Add(m.Groups[i].Value.ToLowerInvariant());
                }
            }

            return result;
        }
        public string Replace(string template, IDictionary<string, string> values)
        {
            string result = template;

            IEnumerable<string> tokenList = FindTokenList(template);

            if (tokenList.Any())
            {
                foreach (string token in tokenList)
                {
                    string value = string.Empty;

                    if (values != null)
                    {
                        value = (from item in values
                                 where item.Key.ToLowerInvariant() == token
                                 select item.Value).FirstOrDefault();
                    }

                    Regex regex = new Regex(@"\$\{" + token + @"\}",
                                            RegexOptions.IgnoreCase |
                                            RegexOptions.CultureInvariant |
                                            RegexOptions.Multiline);

                    result = regex.Replace(result, value ?? string.Empty);
                }
            }

            return result;
        }
    }
}