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

        public ThreeDimensionalSpaceModel(RangeModel xRange, RangeModel yRange, RangeModel zRange, RobotModel robot = null)
        {
            _xRange = xRange;
            _yRange = yRange;
            _zRange = zRange;
            ObjectInSpace = robot;
        }

        public ThreeDimensionalSpaceModel(string rangesFilePath, RobotModel robot = null)
        {
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
                                throw new ThreeDimensionalSpaceException.InvalidAxis($"Axis \"{tokens[0]}\" is: INVALID");
                        }
                    }
                    catch (RangeException.InvalidRangeFormatException ex)
                    {
                        MessageBox.Show($"An error occured while processing the limits:\n{ex.Message}", "Ooops..", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }

                ObjectInSpace = robot;
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

        public void Move(double value, string axis)
        {
            if (ObjectInSpace == null)
                throw new NullReferenceException("Object to move is not initialized");

            switch (axis.ToLower())
            {
                case "x":
                    double computedXValue = ObjectInSpace.XCoordinate + value;

                    if (XRange.HasInRange(computedXValue))
                        ObjectInSpace.XCoordinate = computedXValue;
                    else
                        throw new ThreeDimensionalSpaceException.ObjectMoveException("Invalid move. Robot would go outside space bounds.");
                    break;

                case "y":
                    double computedYValue = ObjectInSpace.YCoordinate + value;

                    if (YRange.HasInRange(computedYValue))
                        ObjectInSpace.YCoordinate = computedYValue;
                    else
                        throw new ThreeDimensionalSpaceException.ObjectMoveException("Invalid move. Robot would go outside space bounds.");
                    break;

                case "z":
                    double computedZValue = ObjectInSpace.ZCoordinate + value;

                    if (ZRange.HasInRange(computedZValue))
                        ObjectInSpace.ZCoordinate = computedZValue;
                    else
                        throw new ThreeDimensionalSpaceException.ObjectMoveException("Invalid move. Robot would go outside space bounds.");
                    break;

                default:
                    throw new ThreeDimensionalSpaceException.InvalidAxis($"Input axis \"{axis}\" is: INVALID");
            }
        }

        public void Move(double valueOnX, double valueOnY, double valueOnZ)
        {
            if (ObjectInSpace == null)
                throw new NullReferenceException("Object to move is not initialized.");

            if (XRange.HasInRange(valueOnX + ObjectInSpace.XCoordinate) &&
                   YRange.HasInRange(valueOnY + ObjectInSpace.YCoordinate) &&
                   ZRange.HasInRange(valueOnZ + ObjectInSpace.ZCoordinate))
            {
                ObjectInSpace.XCoordinate += valueOnX;
                ObjectInSpace.YCoordinate += valueOnY;
                ObjectInSpace.ZCoordinate += valueOnZ;
            }
            else
                throw new ThreeDimensionalSpaceException.ObjectMoveException("Invalid move. Robot would go outside space bounds.");
        }
    }
}
