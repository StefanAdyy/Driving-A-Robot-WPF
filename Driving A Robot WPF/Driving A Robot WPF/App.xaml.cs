using Driving_A_Robot_WPF.Exceptions;
using Driving_A_Robot_WPF.Models;
using Driving_A_Robot_WPF.ViewModels;
using System.Configuration;
using System.Data;
using System.Windows;

namespace Driving_A_Robot_WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            try
            {

                string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
                string filePath = currentDirectory + @"..\..\..\Assets\3DSpaceLimits.txt";
                ThreeDimensionalSpaceModel threeDimensionalSpaceModel = new ThreeDimensionalSpaceModel(filePath);

                MainWindow = new MainWindow()
                {
                    DataContext = new MainViewModel()
                };

                MainWindow.Show();
            }
            catch (FileOperationException ex)
            {
                Utils.Logger.LogError(ex.Message);
                MessageBox.Show($"An exception occured while fetching the space coordinates from the file:\n\n{ex.Message}", "Ooops..", MessageBoxButton.OK, MessageBoxImage.Information);

                Shutdown();
            }
            catch (RangeException.InvalidRangeFormatException ex)
            {
                Utils.Logger.LogError(ex.Message);
                MessageBox.Show($"An exception occured while initializing the space limits from the file:\n\n{ex.Message}", "Ooops..", MessageBoxButton.OK, MessageBoxImage.Information);

                Shutdown();
            }
            catch(ThreeDimensionalSpaceException.InvalidAxis ex)
            {
                Utils.Logger.LogError(ex.Message);
                MessageBox.Show($"A wrong axis was given in the file:\n\n{ex.Message}", "Ooops..", MessageBoxButton.OK, MessageBoxImage.Information);

                Shutdown();
            }
            catch (Exception ex)
            {
                Utils.Logger.LogError(ex.Message);
                MessageBox.Show($"An unexpected exception occured:\n\n{ex.Message}", "Ooops..", MessageBoxButton.OK, MessageBoxImage.Information);

                Shutdown();
            }


            base.OnStartup(e);
        }
    }

}
