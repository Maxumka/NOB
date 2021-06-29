using Microsoft.CodeAnalysis;
using NOB.Generator.CodeBuilders;

namespace NOB.Generator
{
    [Generator]
    public class SourceGenerator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            if (context.SyntaxReceiver is not SyntaxReceiver syntaxReceiver) return;

            var codeBuilder = new CodeBuilder(syntaxReceiver.ClassesInforamtion);

            var sourceCode = codeBuilder.Build();

            context.AddSource("GeneratedViewModel", sourceCode);
        }

        public void Initialize(GeneratorInitializationContext context)
        {
            context.RegisterForSyntaxNotifications(() => new SyntaxReceiver(this));
        }
    }
}
