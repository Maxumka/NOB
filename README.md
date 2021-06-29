# NOB
NOB - its a source generator that allows you not to write boilerplate code in the WPF.
## Features
1. Generate property
2. Generate INotifyPropertyChanged
3. Generate ICommand
4. Generate Command (only without parameters)
## Example
Make your viewmodel a partial class, add the viewmodel attribute to the class, the property attribute to the fields, the command attribute to the methods.
```CSharp    
    [ViewModel]
    public partial class MainWindowViewModel
    {
        [Property]
        private string text1;

        [Command]
        private void ClearText1() => PropertyText1 = "";

        public MainWindowViewModel() { }
    }
```
And this is what your viewmodel will become
```CSharp    
using System;
using System.ComponentModel;
using System.Windows.Input;
using GeneratedCommand;
namespace GeneratedAttribute
{
  [AttributeUsage(AttributeTargets.Class)]
  public class ViewModelAttribute : Attribute { }
  [AttributeUsage(AttributeTargets.Field)]
  public class PropertyAttribute : Attribute { }
  [AttributeUsage(AttributeTargets.Method)]
  public class CommandAttribute : Attribute { }
}

namespace GeneratedCommand
{
  public class Command : ICommand
  {
  private readonly Action _execute;
  private readonly Func<bool> _canExecute;
  public event EventHandler CanExecuteChanged
  {
    add { CommandManager.RequerySuggested += value; }
    remove { CommandManager.RequerySuggested -= value; }
  }
  public Command(Action execute, Func<bool> canExecute = null) => (_execute, _canExecute) = (execute, canExecute);
  public bool CanExecute(object parameter) => true;
  public void Execute(object parameter) => _execute();
  }
}
namespace NOB.SampleProject.ViewModels
{
  public partial class MainWindowViewModel : INotifyPropertyChanged
  {
    public string PropertyText1
    {
      get => text1;
      set 
      {
        text1 = value; OnPropertyChanged(nameof(PropertyText1));
      }
    }
  private Command commandClearText1;
  public Command CommandClearText1
  {
    get => new(ClearText1);
  }
  public event PropertyChangedEventHandler PropertyChanged;
  public void OnPropertyChanged(string propertyName = null) { PropertyChanged?.Invoke(this, new(propertyName)); }
  }
}
```


