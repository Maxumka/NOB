using NOB.Generator.Information;
using System.Text;
using NOB.Generator.Helpers;

namespace NOB.Generator.CodeBuilders
{
    public class CommandBuilder
    {
        private readonly StringBuilder _codeBuilder;
        private readonly string _methodName;

        public CommandBuilder(StringBuilder codeBuilder, MethodInformation methodInformation) 
        {
            _codeBuilder = codeBuilder;
            _methodName = methodInformation.Name;
        }

        public void Build()
        {
            var commandFieldName = $"command{_methodName.CapitalizeFirstChar()}";
            var commandPropertyName = $"Command{_methodName.CapitalizeFirstChar()}";

            _codeBuilder.AppendLine($"private Command {commandFieldName};");
            _codeBuilder.AppendLine($"public Command {commandPropertyName}");
            _codeBuilder.AppendLine("{");
            _codeBuilder.AppendLine($"get => new({_methodName});");
            _codeBuilder.AppendLine("}");
        }
    }
}
