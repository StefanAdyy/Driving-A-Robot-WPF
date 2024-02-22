using Driving_A_Robot_WPF.Exceptions;
using Driving_A_Robot_WPF.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace Driving_A_Robot_WPF.Models
{
    public class ThreeDimensionalSpaceModel
    {
        private RangeModel _xRange;
        private RangeModel _yRange;
        private RangeModel _zRange;
        private PointModel _defaultCoordinates;

        public RangeModel XRange { get { return _xRange; } }
        public RangeModel YRange { get { return _yRange; } }
        public RangeModel ZRange { get { return _zRange; } }
        public PointModel DefaultCoordinates { get { return _defaultCoordinates; } }

        public RobotModel ObjectInSpace { get; set; }

        public ThreeDimensionalSpaceModel(RangeModel xRange, RangeModel yRange, RangeModel zRange, RobotModel robot = null)
        {
            _xRange = xRange;
            _yRange = yRange;
            _zRange = zRange;
            ObjectInSpace = robot;
        }

        public ThreeDimensionalSpaceModel(string rangesFilePath, RobotModel robot = null)
        {
            ObjectInSpace = robot;

            try
            {
                List<string> limits = FileUtils.ReadLinesFromFile(rangesFilePath);

                if(limits.Count != 3) 
                {
                    throw new RangeException.InvalidRangeFormatException("The file that should contain 3 lines with the axis and their limits");
                }

                foreach (string limit in limits)
                {
                    try
                    {
                        string[] tokens = Regex.Split(limit, @" +");

                        if (tokens.Length != 3)
                        {
                            throw new RangeException.InvalidRangeFormatException($"Invalid range input. It should respect the following format:\n \"x 1 2\" (axis value value)\n \"y 1 2\" (axis value value)\n \"z 1 2\" (axis value value)");
                        }

                        RangeModel range = RangeModel.GetRangeFromString(tokens[1] + " " + tokens[2]);

                        switch (tokens[0].ToLower())
                        {
                            case "x":
                                _xRange = range;
                                break;

                            case "y":
                                _yRange = range;
                                break;

                            case "z":
                                _zRange = range;
                                break;

                            default:
                                throw new ThreeDimensionalSpaceException.InvalidAxis($"Axis \"{tokens[0]}\" is NOT valid.");
                        }
                    }
                    catch (RangeException.InvalidRangeFormatException ex)
                    {
                        throw new RangeException.InvalidRangeFormatException(ex.Message);
                    }
                }
            }
            catch (FileOperationException ex)
            {
                throw new FileOperationException(ex.Message);
            }
        }

        public void SetObjectDefaultPosition(PointModel defaultCoordinates)
        {
            if (ObjectInSpace == null)
                throw new ThreeDimensionalSpaceException.ObjectNullException("Object to move is not initialized");

            if (!XRange.HasInRange(defaultCoordinates.X) || !YRange.HasInRange(defaultCoordinates.Y) || !ZRange.HasInRange(defaultCoordinates.Z)) 
                throw new ThreeDimensionalSpaceException.ObjectPositionException("Invalid move. Robot would go outside space bounds.");

            _defaultCoordinates = defaultCoordinates;
            ObjectInSpace.Coordinates = DefaultCoordinates;
        }

        public void ResetObjectPosition()
        {
            if (ObjectInSpace == null)
                throw new ThreeDimensionalSpaceException.ObjectNullException("Object to move is not initialized");

            if (DefaultCoordinates == null)
                throw new ThreeDimensionalSpaceException.UnsetDefaultCoordinates("Default coordinates are not initialized");

            ObjectInSpace.Coordinates = DefaultCoordinates;
        }

        public void MoveObject(double value, string axis)
        {
            if (ObjectInSpace == null)
                throw new ThreeDimensionalSpaceException.ObjectNullException("Object to move is not initialized");

            switch (axis.ToLower())
            {
                case "x":
                    double computedXValue = ObjectInSpace.Coordinates.X + value;

                    if (XRange.HasInRange(computedXValue))
                        ObjectInSpace.Coordinates.X = computedXValue;
                    else
                        throw new ThreeDimensionalSpaceException.ObjectPositionException("Invalid move. Robot would go outside space bounds.");
                    break;

                case "y":
                    double computedYValue = ObjectInSpace.Coordinates.Y + value;

                    if (YRange.HasInRange(computedYValue))
                        ObjectInSpace.Coordinates.Y = computedYValue;
                    else
                        throw new ThreeDimensionalSpaceException.ObjectPositionException("Invalid move. Robot would go outside space bounds.");
                    break;

                case "z":
                    double computedZValue = ObjectInSpace.Coordinates.Z + value;

                    if (ZRange.HasInRange(computedZValue))
                        ObjectInSpace.Coordinates.Z = computedZValue;
                    else
                        throw new ThreeDimensionalSpaceException.ObjectPositionException("Invalid move. Robot would go outside space bounds.");
                    break;

                default:
                    throw new ThreeDimensionalSpaceException.InvalidAxis($"Input axis \"{axis}\" is: INVALID");
            }
        }

        public void MoveObject(double valueOnX, double valueOnY, double valueOnZ)
        {
            if (ObjectInSpace == null)
                throw new ThreeDimensionalSpaceException.ObjectNullException("Object to move is not initialized.");

            if (XRange.HasInRange(valueOnX + ObjectInSpace.Coordinates.X) &&
                   YRange.HasInRange(valueOnY + ObjectInSpace.Coordinates.Y) &&
                   ZRange.HasInRange(valueOnZ + ObjectInSpace.Coordinates.Z))
            {
                ObjectInSpace.Coordinates.X += valueOnX;
                ObjectInSpace.Coordinates.Y += valueOnY;
                ObjectInSpace.Coordinates.Z += valueOnZ;
            }
            else
                throw new ThreeDimensionalSpaceException.ObjectPositionException("Invalid move. Robot would go outside space bounds.");
        }
    }
}
