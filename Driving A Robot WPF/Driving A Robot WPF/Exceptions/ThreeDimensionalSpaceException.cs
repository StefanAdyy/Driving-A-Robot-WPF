namespace Driving_A_Robot_WPF.Exceptions
{
    public class ThreeDimensionalSpaceException : Exception
    {
        public ThreeDimensionalSpaceException(string message) : base(message) { }

        public class ObjectNullException : ThreeDimensionalSpaceException
        {
            public ObjectNullException(string message) : base(message) { }
        }

        public class ObjectPositionException : ThreeDimensionalSpaceException
        {
            public ObjectPositionException(string message) : base(message) { }
        }

        public class InvalidAxis : ThreeDimensionalSpaceException
        {
            public InvalidAxis(string message) : base(message) { }
        }

        public class UnsetDefaultCoordinates : ThreeDimensionalSpaceException
        {
            public UnsetDefaultCoordinates(string message) : base(message) { }
        }
    }
}
