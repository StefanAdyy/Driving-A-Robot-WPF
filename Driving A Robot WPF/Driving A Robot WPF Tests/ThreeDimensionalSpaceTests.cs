using Driving_A_Robot_WPF.Exceptions;
using Driving_A_Robot_WPF.Models;

namespace Driving_A_Robot_WPF_Tests
{
    public class Tests
    {
        private RangeModel xRange;
        private RangeModel yRange;
        private RangeModel zRange;
        private RobotModel robot;
        private ThreeDimensionalSpaceModel threeDimensionalSpace;

        [SetUp]
        public void SetUp()
        {
            xRange = new RangeModel(0, 10);
            yRange = new RangeModel(0, 10);
            zRange = new RangeModel(0, 10);
            robot = new RobotModel(new PointModel(0, 0, 0));
            threeDimensionalSpace = new ThreeDimensionalSpaceModel(xRange, yRange, zRange, new PointModel(0, 0, 0), robot);
        }

        [Test]
        public void SetObjectPosition_ValidCoordinates()
        {
            PointModel validCoordinates = new PointModel(5, 5, 5);

            threeDimensionalSpace.SetObjectPosition(validCoordinates);

            Assert.AreEqual(threeDimensionalSpace.ObjectInSpace.Coordinates, validCoordinates);
        }

        [Test]
        public void ResetObjectPosition_Valid()
        {
            threeDimensionalSpace.ObjectInSpace.Coordinates = new PointModel(5, 5, 5);

            threeDimensionalSpace.ResetObjectPosition();

            PointModel expectedCoordinates = new PointModel(0, 0, 0);
            Assert.AreEqual(expectedCoordinates, threeDimensionalSpace.ObjectInSpace.Coordinates);
        }

        [Test]
        public void ResetObjectPosition_NullObjectInSpace()
        {
            threeDimensionalSpace.ObjectInSpace = null;

            Assert.Throws<ThreeDimensionalSpaceException.ObjectNullException>(() => threeDimensionalSpace.ResetObjectPosition());
        }

        [Test]
        public void ResetObjectPosition_NullDefaultCoordinates()
        {
            threeDimensionalSpace.DefaultCoordinates = null;

            Assert.Throws<ThreeDimensionalSpaceException.UnsetDefaultCoordinates>(() => threeDimensionalSpace.ResetObjectPosition());
        }

        [Test]
        public void MoveObject_ValidMove()
        {
            threeDimensionalSpace.MoveObject(2, "x");
            threeDimensionalSpace.MoveObject(2, "y");
            threeDimensionalSpace.MoveObject(2, "z");

            Assert.AreEqual(2, threeDimensionalSpace.ObjectInSpace.Coordinates.X);
            Assert.AreEqual(2, threeDimensionalSpace.ObjectInSpace.Coordinates.Y);
            Assert.AreEqual(2, threeDimensionalSpace.ObjectInSpace.Coordinates.Z);

            threeDimensionalSpace.MoveObject(1, "X");
            threeDimensionalSpace.MoveObject(1, "Y");
            threeDimensionalSpace.MoveObject(1, "Z");

            Assert.AreEqual(3, threeDimensionalSpace.ObjectInSpace.Coordinates.X);
            Assert.AreEqual(3, threeDimensionalSpace.ObjectInSpace.Coordinates.Y);
            Assert.AreEqual(3, threeDimensionalSpace.ObjectInSpace.Coordinates.Z);
        }

        [Test]
        public void MoveObject_InvalidMoveOutsideBounds()
        {
            Assert.Throws<ThreeDimensionalSpaceException.ObjectPositionException>(() => threeDimensionalSpace.MoveObject(20, "x"));
        }

        [Test]
        public void MoveObject3Axis_ValidMove()
        {
            threeDimensionalSpace.MoveObject(7, 7, 7);

            Assert.AreEqual(7, threeDimensionalSpace.ObjectInSpace.Coordinates.X);
            Assert.AreEqual(7, threeDimensionalSpace.ObjectInSpace.Coordinates.Y);
            Assert.AreEqual(7, threeDimensionalSpace.ObjectInSpace.Coordinates.Z);
        }

        [Test]
        public void MoveObject3Axis_InvalidMoveOutsideBounds()
        {
            Assert.Throws<ThreeDimensionalSpaceException.ObjectPositionException>(() => threeDimensionalSpace.MoveObject(30, 30, 30));
            Assert.Throws<ThreeDimensionalSpaceException.ObjectPositionException>(() => threeDimensionalSpace.MoveObject(-30, -30, -30));
            Assert.Throws<ThreeDimensionalSpaceException.ObjectPositionException>(() => threeDimensionalSpace.MoveObject(-1, 11, 5));
        }
    }
}