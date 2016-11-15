using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace AvaloniaApplication1.View
{
    public class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
            this.DataContext = new ViewModel.MainVM();
            App.AttachDevTools(this);
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
