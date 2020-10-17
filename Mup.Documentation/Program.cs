using System.IO;
using System.Text.RegularExpressions;
using CodeMap.DeclarationNodes;

namespace Mup.Documentation
{
    internal static class Program
    {
        internal static void Main()
        {
            var mupAssembly = typeof(ParseTreeVisitor).Assembly;
            var mupAssemblyDeclration = DeclarationNode.Create(mupAssembly).Apply(new MupAssemblyDocumentationAddition());

            var memberReferenceResolver = new CodeMapMemberReferenceResolver();
            var templateWriter = new CodeMapHandlebarsTemplateWriter(memberReferenceResolver);

            var declarationNodeVisitor = new FileWriterTemplateWriterDeclarationNodeVisitor(memberReferenceResolver, templateWriter);

            foreach (var assetResourceName in typeof(Program).Assembly.GetManifestResourceNames())
            {
                var match = Regex.Match(assetResourceName, @"\.Assets\.(?<assetName>\w+\.\w+)$", RegexOptions.IgnoreCase);
                if (match.Success)
                    using (var assetFileStream = new FileStream(match.Groups["assetName"].Value, FileMode.Create, FileAccess.Write, FileShare.Read))
                    using (var assetResourceStream = typeof(Program).Assembly.GetManifestResourceStream(assetResourceName))
                        assetResourceStream.CopyTo(assetFileStream);
            }

            using (var indexPageWriter = new StreamWriter(new FileStream("index.html", FileMode.Create, FileAccess.Write, FileShare.Read)))
                templateWriter.Write(indexPageWriter, "Index", new { isHomeSelected = true });
            using (var indexPageWriter = new StreamWriter(new FileStream("license.html", FileMode.Create, FileAccess.Write, FileShare.Read)))
                templateWriter.Write(indexPageWriter, "License", new { isLicenseSelected = true });
            mupAssemblyDeclration.Accept(declarationNodeVisitor);
        }
    }
}