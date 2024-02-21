using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Driving_A_Robot_WPF.Models
{
    public class RangeModel
    {
        public double MinValue { get; }
        public double MaxValue { get; }

        public RangeModel(double minValue, double maxValue)
        {
            if (minValue > maxValue)
            {
                throw new ArgumentException("MinValue must be less than or equal to MaxValue.");
            }

            MinValue = minValue; 
            MaxValue = maxValue;
        }

    }
}
