using Driving_A_Robot_WPF.Exceptions;

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

        public bool HasInRange(double value)
        {
            if(value >= MinValue && value <= MaxValue) 
            {
                return true;
            }

            return false;
        }

        public static RangeModel GetRangeFromString(string valuesString)
        {
            char[] delimiterChars = { ' ', ',', '\t' };
            string[] values = valuesString.Split(delimiterChars, StringSplitOptions.RemoveEmptyEntries);

            if (values.Length != 2)
            {
                throw new RangeException.InvalidRangeFormatException("String does not contain data in a right way.");
            }

            if (double.TryParse(values[0], out double minValue) && double.TryParse(values[1], out double maxValue))
            {
                return new RangeModel(minValue, maxValue);
            }

            throw new RangeException.InvalidRangeFormatException("Invalid values in the string.");
        }

    }
}
