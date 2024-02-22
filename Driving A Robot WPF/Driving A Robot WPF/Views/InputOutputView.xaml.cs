using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Driving_A_Robot_WPF.Views
{
    /// <summary>
    /// Interaction logic for InpuOutputView.xaml
    /// </summary>
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
