using NOB.Generator.Information;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace NOB.Generator.CodeBuilders
{
    public class ClassBuilder
    {
        private readonly StringBuilder _codeBuilder;
        private readonly string _className;
        private readonly string _classNameSpace;
        private readonly IEnumerable<FieldInformation> _fieldsInformation;
        private readonly IEnumerable<MethodInformation> _methodsInformation;

        public ClassBuilder(StringBuilder codeBuilder, ClassInformation classInformation)
        {
            _codeBuilder = codeBuilder;
            _className = classInformation.Name;
            _classNameSpace = classInformation.NameSpace;
            _fieldsInformation = classInformation.FieldsInformation;
            _methodsInformation = classInformation.MethodsInformation;
        }
        
        public void Build()
        {
            _codeBuilder.AppendLine($"namespace {_classNameSpace}");
            _codeBuilder.AppendLine("{");

            _codeBuilder.AppendLine($"public partial class {_className} : INotifyPropertyChanged");
            _codeBuilder.AppendLine("{");
            BuildProperties();
            BuildCommands();
            BuildImplementationINotifyPropertyChanged();
            _codeBuilder.AppendLine("}");

            _codeBuilder.AppendLine("}");
        }

        private void BuildProperties()
        {
            if (_fieldsInformation.Count() is 0) return;

            foreach (var field in _fieldsInformation)
            {
                var propertyBuilder = new PropertyBuilder(_codeBuilder, field);
                propertyBuilder.Build();
            }
        }

        private void BuildCommands()
        {
            if (_methodsInformation.Count() is 0) return;

            foreach (var method in _methodsInformation)
            {
                var methodBuilder = new CommandBuilder(_codeBuilder, method);
                methodBuilder.Build();
            }
        }

        private void BuildImplementationINotifyPropertyChanged()
        {
            _codeBuilder.AppendLine("public event PropertyChangedEventHandler PropertyChanged;");
            _codeBuilder.AppendLine(@"public void OnPropertyChanged(string propertyName = null)");
            _codeBuilder.AppendLine("{ PropertyChanged?.Invoke(this, new(propertyName)); }");
        }
    }
}