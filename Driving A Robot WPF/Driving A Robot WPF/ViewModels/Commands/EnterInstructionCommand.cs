using Driving_A_Robot_WPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Driving_A_Robot_WPF.ViewModels.Commands
{
    class EnterInstructionCommand : CommandBase
    {
        private ThreeDimensionalSpaceModel _threeDimensionalSpace;
        private InputOutputViewModel _viewModel;
        public EnterInstructionCommand(ThreeDimensionalSpaceModel threeDimensionalSpace, InputOutputViewModel viewModel)
        {
            _threeDimensionalSpace = threeDimensionalSpace;
            _viewModel = viewModel;
        }

        public override void Execute(object parameter)
        {
            if (!string.IsNullOrWhiteSpace(_viewModel.ConsoleInput))
            {
                _viewModel.ConsoleHistory += $"\n{_viewModel.ConsoleInput}";
                _viewModel.ConsoleInput = string.Empty;
            }

        }
    }
}
