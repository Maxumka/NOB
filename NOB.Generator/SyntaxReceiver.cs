using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NOB.Generator.Information;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NOB.Generator
{
    public class SyntaxReceiver : ISyntaxReceiver
    {
        private const string _methodAttributeName = "[Command]";
        private const string _propertyAttributeName = "[Property]";
        private const string _viewModelClassAttributeName = "ViewModel";

        private readonly ISourceGenerator _sourceGenerator;

        public List<ClassInformation> ClassesInforamtion { get; private set; } = new();

        public SyntaxReceiver(ISourceGenerator sourceGenerator)
            => _sourceGenerator = sourceGenerator;

        public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
        {
            if (syntaxNode is not NamespaceDeclarationSyntax namespaceDeclaration) return;

            var classesDeclaration = namespaceDeclaration.DescendantNodes()
                                                         .OfType<ClassDeclarationSyntax>()
                                                         .Where(c => c.DescendantNodes()
                                                                      .OfType<AttributeSyntax>()
                                                                      .Any(a => a.ToString() is _viewModelClassAttributeName));

            foreach (var classDeclaration in classesDeclaration)
            {
                var fieldsInformation = classDeclaration.Members
                                                        .OfType<FieldDeclarationSyntax>()
                                                        .Where(f => f.AttributeLists.Any(a => a.ToString() is _propertyAttributeName))
                                                        .Select(f => GetAdditionalProperties(f, classDeclaration));

                var methodsInformation = classDeclaration.Members
                                                         .OfType<MethodDeclarationSyntax>()
                                                         .Where(m => m.AttributeLists.Any(a => a.ToString() is _methodAttributeName))
                                                         .Select(m => new MethodInformation(m));

                var classInformation = new ClassInformation
                {
                    Name = classDeclaration.Identifier.ValueText,
                    NameSpace = namespaceDeclaration.Name.ToString(),
                    FieldsInformation = fieldsInformation,
                    MethodsInformation = methodsInformation
                };

                ClassesInforamtion.Add(classInformation);
            }
        }

        private FieldInformation GetAdditionalProperties(FieldDeclarationSyntax fieldDeclaration, ClassDeclarationSyntax classDeclaration)
        {
            var properties = classDeclaration.Members
                                             .OfType<PropertyDeclarationSyntax>()
                                             .Where(p => p.ExpressionBody.Expression.ToFullString().Contains(fieldDeclaration.Declaration.Variables.First().Identifier.ValueText))
                                             .Select(p => p.Identifier.ValueText)
                                             .ToArray();

            return properties switch
            {
                { Length: var length } when length > 0 => new(fieldDeclaration, properties),
                _                                      => new(fieldDeclaration)
            };
        }
    }
}
