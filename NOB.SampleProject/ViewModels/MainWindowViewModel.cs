using GeneratedAttribute;

namespace NOB.SampleProject.ViewModels
{
    [ViewModel]
    public partial class MainWindowViewModel
    {
        [Property]
        private string text1;

        [Command]
        private void ClearText1() => PropertyText1 = "";

        public MainWindowViewModel() { }
    }
}
