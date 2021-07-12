using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;

namespace NOB.Generator.Information
{
    public class FieldInformation
    {
        public string Name { get; set; }

        public string Type { get; set; }

        public string[] AdditionalProperties { get; set; } = new string[] { };

        public FieldInformation(FieldDeclarationSyntax fieldDeclarationSyntax)
        {
            Name = fieldDeclarationSyntax.Declaration.Variables.First().Identifier.ValueText;
            Type = fieldDeclarationSyntax.Declaration.Type.ToString();
        }

        public FieldInformation(FieldDeclarationSyntax fieldDeclarationSyntax, string[] additionalProperties)
        {
            Name = fieldDeclarationSyntax.Declaration.Variables.First().Identifier.ValueText;
            Type = fieldDeclarationSyntax.Declaration.Type.ToString();
            AdditionalProperties = additionalProperties;
        }

        public void Deconstruct(out string name, out string type)
        {
            name = Name;
            type = Type;
        }
    }
}
