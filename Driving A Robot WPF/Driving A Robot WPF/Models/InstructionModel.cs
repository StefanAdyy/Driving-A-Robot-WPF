using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Driving_A_Robot_WPF.Models
{
    public class InstructionModel
    {      
        public string Name { get; }
        public string Parameters { get; }

        public InstructionModel(string name, string parameters = "")
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Command name cannot be null or empty.", nameof(name));
            }

            Name = name;
            Parameters = parameters;
        }
    }
}
