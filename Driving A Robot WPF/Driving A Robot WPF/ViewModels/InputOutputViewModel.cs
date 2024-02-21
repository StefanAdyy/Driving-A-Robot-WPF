using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Driving_A_Robot_WPF.ViewModels
{
    public class InputOutputViewModel : ViewModelBase
    {
		private string _input;
		public string Input
		{
			get
			{
				return _input;
			}
			set
			{
				_input = value;
				//Output = value;
				OnPropertyChanged(nameof(Input));
			}
		}

		private string _output;
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
	}
}
