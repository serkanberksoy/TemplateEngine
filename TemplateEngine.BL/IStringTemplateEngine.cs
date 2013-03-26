using System.Collections.Generic;

namespace TemplateEngine.BL
{
    public interface IStringTemplateEngine
    {
        string Replace(string template, IDictionary<string, string> values);
    }
}