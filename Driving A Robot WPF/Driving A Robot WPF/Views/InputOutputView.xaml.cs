using System.Windows;
using System.Windows.Controls;

namespace Driving_A_Robot_WPF.Views
{
    public partial class InpuOutputView : UserControl
    {
        public InpuOutputView()
        {
            InitializeComponent();
        }
        private void ConsoleHistoryTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            ConsoleInputTextBox.Focus();
        }

        private void ConsoleHistoryTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ConsoleHistoryTextBox.ScrollToEnd();

            
        }
    }
}
