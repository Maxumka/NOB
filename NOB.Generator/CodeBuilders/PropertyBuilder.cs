using NOB.Generator.Information;
using System.Text;
using NOB.Generator.Helpers;

namespace NOB.Generator.CodeBuilders
{
    public class PropertyBuilder
    {
        private readonly StringBuilder _codeBuilder;
        private readonly string _fieldName;
        private readonly string _fieldType;
        private readonly string[] _additionalProperties;

        public PropertyBuilder(StringBuilder codeBuilder, FieldInformation fieldInformation)
        {
            _codeBuilder = codeBuilder;
            _fieldName = fieldInformation.Name;
            _fieldType = fieldInformation.Type;
            _additionalProperties = fieldInformation.AdditionalProperties;
        }

        public void Build()
        {
            var propertyName = $"Property{_fieldName.CapitalizeFirstChar()}";

            _codeBuilder.AppendLine($"public {_fieldType} {propertyName}");
            _codeBuilder.AppendLine("{");
            _codeBuilder.AppendLine($"get => {_fieldName};");
            _codeBuilder.AppendLine("set {");
            _codeBuilder.AppendLine($"{_fieldName} = value; OnPropertyChanged(nameof({propertyName}));");
            _codeBuilder.AppendLine($"//count props: {_additionalProperties.Length}");
            foreach (var prop in _additionalProperties)
            {
                _codeBuilder.AppendLine($"OnPropertyChanged(nameof({prop}));");
                _codeBuilder.AppendLine($"//{prop}");
            }
            _codeBuilder.AppendLine("}");
            _codeBuilder.AppendLine("}");
        }
    }
}
