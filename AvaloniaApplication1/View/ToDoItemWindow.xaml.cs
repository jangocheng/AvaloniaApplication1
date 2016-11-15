using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace AvaloniaApplication1.View
{
    public class ToDoItemWindow : Window
    {
        public ToDoItemWindow()
        {
            this.InitializeComponent();
            App.AttachDevTools(this);
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
