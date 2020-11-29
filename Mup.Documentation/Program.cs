using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using CodeMap.DeclarationNodes;
using CodeMap.Handlebars;

namespace Mup.Documentation
{
    internal static class Program
    {
        internal static void Main()
        {
            var templateWriter = new HandlebarsTemplateWriter(
                new MemberReferenceResolver(
                    new Dictionary<Assembly, IMemberReferenceResolver>
                    {
                        { typeof(ParseTreeVisitor).Assembly, new CodeMapMemberReferenceResolver() }
                    },
                    new MicrosoftDocsMemberReferenceResolver("netstandard-1.0")
                )
            );

            DeclarationNode
                .Create(typeof(ParseTreeVisitor).Assembly)
                .Apply(new MupAssemblyDocumentationAddition())
                .Accept(new HandlebarsWriterDeclarationNodeVisitor(new DirectoryInfo(Environment.CurrentDirectory), templateWriter));
            File.Move(Path.Combine(Environment.CurrentDirectory, "index.html"), Path.Combine(Environment.CurrentDirectory, "documentation.html"), overwrite: true);

            using (var indexPageWriter = new StreamWriter(new FileStream("index.html", FileMode.Create, FileAccess.Write, FileShare.Read)))
                templateWriter.Write(indexPageWriter, "Index", new { isHomeSelected = true });
            using (var indexPageWriter = new StreamWriter(new FileStream("license.html", FileMode.Create, FileAccess.Write, FileShare.Read)))
                templateWriter.Write(indexPageWriter, "License", new { isLicenseSelected = true });
        }
    }
}