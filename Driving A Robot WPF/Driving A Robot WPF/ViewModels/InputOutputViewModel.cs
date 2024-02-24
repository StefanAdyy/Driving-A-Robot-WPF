using Driving_A_Robot_WPF.Models;
using Driving_A_Robot_WPF.ViewModels.Commands;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Driving_A_Robot_WPF.ViewModels
{
    public class InputOutputViewModel : ViewModelBase
    {
		private string _consoleInput;
		public string ConsoleInput
		{
			get
			{
				return _consoleInput;
			}
			set
			{
                _consoleInput = value;
                OnPropertyChanged(nameof(ConsoleInput));
			}
		}

		private string _output = "";
		public string Output
		{
			get
			{
				return _output;
			}
			set
			{
				_output = value;
				OnPropertyChanged(nameof(Output));
			}
		}

		private string _consoleHistory = "";
		public string ConsoleHistory
		{
			get
			{
				return _consoleHistory;
			}
			set
			{
				_consoleHistory = value;
				OnPropertyChanged(nameof(ConsoleHistory));
			}
		}

		public ICommand EnterCommand { get; }

        public InputOutputViewModel(ThreeDimensionalSpaceModel threeDimensionalSpaceModel)
		{
			EnterCommand = new EnterInstructionCommand(threeDimensionalSpaceModel, this);
		}
    }
}
