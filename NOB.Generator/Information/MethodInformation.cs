using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace NOB.Generator.Information
{
    public class MethodInformation
    {
        public string Name { get; set; }

        public MethodInformation(MethodDeclarationSyntax methodDeclarationSyntax)
        {
            Name = methodDeclarationSyntax.Identifier.ValueText;
        }
    }
}
