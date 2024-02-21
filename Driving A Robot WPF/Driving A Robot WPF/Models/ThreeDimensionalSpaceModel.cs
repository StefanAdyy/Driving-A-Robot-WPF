using Driving_A_Robot_WPF.Exceptions;
using Driving_A_Robot_WPF.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Driving_A_Robot_WPF.Models
{
    public class ThreeDimensionalSpaceModel
    {
        private RangeModel _xRange;
        private RangeModel _yRange;
        private RangeModel _zRange;

        public RangeModel XRange { get { return _xRange; } }
        public RangeModel YRange { get { return _yRange; } }
        public RangeModel ZRange { get { return _zRange; } }

        public RobotModel ObjectInSpace { get; set; }
        public PointModel DefaultCoordinates { get; set; }

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

                foreach (string limit in limits)
                {
                    try
                    {
                        string[] tokens = limit.Split(' ');
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
                        MessageBox.Show($"An error occured while processing the limits:\n{ex.Message}", "Ooops..", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (FileOperationException ex)
            {
                MessageBox.Show($"An exception was thrown while reading the file:\n{ex.Message}", "Ooops..", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected exception occured:\n{ex.Message}", "Ooops..", MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }

        public void SetObjectDefaultPosition(PointModel defaultCoordinates)
        {
            if (ObjectInSpace == null)
                throw new NullReferenceException("Object to move is not initialized");

            if (ObjectInSpace == null)
                throw new NullReferenceException("Default coordinates are not initialized");

            if (XRange.HasInRange(defaultCoordinates.X) && YRange.HasInRange(defaultCoordinates.Y) && ZRange.HasInRange(defaultCoordinates.Z)) 
                throw new ThreeDimensionalSpaceException.ObjectPositionException("Invalid move. Robot would go outside space bounds.");

            DefaultCoordinates = defaultCoordinates;
        }

        public void MoveObject(double value, string axis)
        {
            if (ObjectInSpace == null)
                throw new NullReferenceException("Object to move is not initialized");

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
                throw new NullReferenceException("Object to move is not initialized.");

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
