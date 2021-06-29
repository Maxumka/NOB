using NOB.Generator.Information;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace NOB.Generator.CodeBuilders
{
    public class CodeBuilder
    {
        private readonly StringBuilder _codeBuilder;
        private readonly IEnumerable<ClassInformation> _classesInformation;

        public CodeBuilder(IEnumerable<ClassInformation> classInformation)
        {
            _codeBuilder = new();
            _classesInformation = classInformation; 
        }

        public string Build()
        {
            BuildUsing();
            BuildAttribute();
            BuildImplementationICommand();
            BuildClassess();

            return _codeBuilder.ToString();
        }

        private void BuildUsing()
        {
            _codeBuilder.AppendLine("using System;");
            _codeBuilder.AppendLine("using System.ComponentModel;");
            _codeBuilder.AppendLine("using System.Windows.Input;");
            _codeBuilder.AppendLine("using GeneratedCommand;");
        }

        private void BuildAttribute()
        {
            _codeBuilder.AppendLine("namespace GeneratedAttribute");
            _codeBuilder.AppendLine("{");
            _codeBuilder.AppendLine("[AttributeUsage(AttributeTargets.Class)]\npublic class ViewModelAttribute : Attribute { }");
            _codeBuilder.AppendLine("[AttributeUsage(AttributeTargets.Field)]\npublic class PropertyAttribute : Attribute { }");
            _codeBuilder.AppendLine("[AttributeUsage(AttributeTargets.Method)]\npublic class CommandAttribute : Attribute { }");
            _codeBuilder.AppendLine("}");
        }

        private void BuildImplementationICommand()
        {
            _codeBuilder.AppendLine("namespace GeneratedCommand");
            _codeBuilder.AppendLine("{");
            _codeBuilder.AppendLine("public class Command : ICommand");
            _codeBuilder.AppendLine("{");
            _codeBuilder.AppendLine("private readonly Action _execute;");
            _codeBuilder.AppendLine("private readonly Func<bool> _canExecute;");
            _codeBuilder.AppendLine("public event EventHandler CanExecuteChanged");
            _codeBuilder.AppendLine("{");
            _codeBuilder.AppendLine("add { CommandManager.RequerySuggested += value; }");
            _codeBuilder.AppendLine("remove { CommandManager.RequerySuggested -= value; }");
            _codeBuilder.AppendLine("}");
            _codeBuilder.AppendLine(@"public Command(Action execute, Func<bool> canExecute = null) 
                                            => (_execute, _canExecute) = (execute, canExecute);");
            _codeBuilder.AppendLine("public bool CanExecute(object parameter) => true;");
            _codeBuilder.AppendLine("public void Execute(object parameter) => _execute();");
            _codeBuilder.AppendLine("}");
            _codeBuilder.AppendLine("}");
        }

        private void BuildClassess()
        {
            if (_classesInformation.Count() is 0) return;

            foreach (var classess in _classesInformation)
            {
                var classBuilder = new ClassBuilder(_codeBuilder, classess);
                classBuilder.Build();
            }
        }
    }
}
