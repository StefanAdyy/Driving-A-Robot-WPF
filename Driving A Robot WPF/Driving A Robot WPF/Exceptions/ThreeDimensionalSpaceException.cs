using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Driving_A_Robot_WPF.Exceptions
{
    public class ThreeDimensionalSpaceException : Exception
    {
        public ThreeDimensionalSpaceException(string message) : base(message) { }

        public class ObjectMoveException : ThreeDimensionalSpaceException
        {
            public ObjectMoveException(string message) : base(message) { }
        }

        public class InvalidAxis : ThreeDimensionalSpaceException
        {
            public InvalidAxis(string message) : base(message) { }
        }
    }
}
