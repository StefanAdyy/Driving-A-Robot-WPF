namespace Driving_A_Robot_WPF.Exceptions
{
    public class RangeException : Exception
    {
        public RangeException(string message) : base(message) {}

        public class InvalidRangeFormatException : RangeException
        {
            public InvalidRangeFormatException(string message) : base(message) { }
        }
    }
}
