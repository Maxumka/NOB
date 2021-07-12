using GeneratedAttribute;

namespace NOB.SampleProject.ViewModels
{
    [ViewModel]
    public partial class MainWindowViewModel
    {
        [Property]
        private string text1;

        [Property]
        private string text2;

        public string Text3 => text1 + text2;

        public MainWindowViewModel() { }
    }
}
