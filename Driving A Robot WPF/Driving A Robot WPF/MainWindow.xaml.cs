using Driving_A_Robot_WPF.Exceptions;
using Driving_A_Robot_WPF.Models;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Driving_A_Robot_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            //RobotModel robot = new RobotModel(0, 1, 2);
            //robot.ToString();
            //string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            //try
            //{
            //    string filePath = currentDirectory + @"..\..\..\Assets\3DSpaceLimits.txt";
            //    ThreeDimensionalSpaceModel threeDimensionalSpaceModel = new ThreeDimensionalSpaceModel (filePath, robot);
            //    threeDimensionalSpaceModel.Move(2000, "y");
            //}
            //catch (ThreeDimensionalSpaceException ex)
            //{
            //    string s = ex.Message;
            //}

            Utils.Logger.LogError("Ceva eroare");

            InitializeComponent();
        }
    }
}