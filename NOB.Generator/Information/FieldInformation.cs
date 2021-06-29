using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Text;

namespace NOB.Generator.Information
{
    public class FieldInformation
    {
        public string Name { get; set; }

        public string Type { get; set; }

        public FieldInformation(FieldDeclarationSyntax fieldDeclarationSyntax)
        {
            Name = fieldDeclarationSyntax.Declaration.Variables.First().Identifier.ValueText;
            Type = fieldDeclarationSyntax.Declaration.Type.ToString();
        }

        public void Deconstruct(out string name, out string type)
        {
            name = Name;
            type = Type;
        }
    }
}
