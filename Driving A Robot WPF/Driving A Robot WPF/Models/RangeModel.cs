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

        public static RangeModel GetRangeFromString(string valuesString)
        {
            string[] values = valuesString.Split(' ');

            if (values.Length != 2)
            {
                throw new ArgumentException("String does not contain the right data.");
            }

            if (double.TryParse(values[0], out double minValue) && double.TryParse(values[1], out double maxValue))
            {
                return new RangeModel(minValue, maxValue);
            }
            else
            {
                throw new ArgumentException("Invalid values in the string.");
            }
        }

    }
}
