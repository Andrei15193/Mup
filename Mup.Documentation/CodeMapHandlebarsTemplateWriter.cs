using System;
using System.Collections.Generic;
using CodeMap.Handlebars;

namespace Mup.Documentation
{
    public class CodeMapHandlebarsTemplateWriter : HandlebarsTemplateWriter
    {
        public CodeMapHandlebarsTemplateWriter(IMemberReferenceResolver memberReferenceResolver)
            : base(memberReferenceResolver)
        {
        }

        protected override IReadOnlyDictionary<string, string> GetPartials()
            => new Dictionary<string, string>(base.GetPartials(), StringComparer.OrdinalIgnoreCase)
            {
                ["Layout"] = ReadFromEmbeddedResource(typeof(Program).Assembly, "Mup.Documentation.Partials.Layout.hbs")
            };

        protected override IReadOnlyDictionary<string, string> GetTemplates()
            => new Dictionary<string, string>(base.GetTemplates(), StringComparer.OrdinalIgnoreCase)
            {
                ["Index"] = ReadFromEmbeddedResource(typeof(Program).Assembly, "Mup.Documentation.Templates.Index.hbs"),
                ["License"] = ReadFromEmbeddedResource(typeof(Program).Assembly, "Mup.Documentation.Templates.License.hbs")
            };
    }
}