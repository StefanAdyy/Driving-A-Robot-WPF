using Driving_A_Robot_WPF.Models;

namespace Driving_A_Robot_WPF.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public ViewModelBase CurrentViewModel { get; }

        public MainViewModel(ThreeDimensionalSpaceModel threeDimensionalSpaceModel) 
        {
            CurrentViewModel = new InputOutputViewModel(threeDimensionalSpaceModel);
        }
    }
}
