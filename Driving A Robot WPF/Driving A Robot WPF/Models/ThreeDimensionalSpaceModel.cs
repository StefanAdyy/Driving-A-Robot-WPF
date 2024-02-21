using Driving_A_Robot_WPF.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                            throw new ArgumentException("Invalid values in the string.");
                    }

                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"Error processing limit: {ex.Message}");
                }
            }

            ObjectInSpace = robot;
        }

        public string Move(double value, string axis)
        {
            string message = "";

            if (ObjectInSpace == null)
                message = "Object to move is not initialized";
            else
            {
                switch (axis.ToLower())
                {
                    case "x":
                        double computedXValue = ObjectInSpace.XCoordinate + value;
                        
                        if(XRange.HasInRange(computedXValue))
                            ObjectInSpace.XCoordinate = computedXValue;
                        else
                            message = "Invalid move. Robot would go outside space bounds.";
                    break;

                    case "y":
                        double computedYValue = ObjectInSpace.YCoordinate + value;

                        if (YRange.HasInRange(computedYValue))
                            ObjectInSpace.YCoordinate = computedYValue;
                        else
                            message = "Invalid move. Robot would go outside space bounds.";
                        break;

                    case "z":
                        double computedZValue = ObjectInSpace.ZCoordinate + value;

                        if (ZRange.HasInRange(computedZValue))
                            ObjectInSpace.ZCoordinate = computedZValue;
                        else
                            message = "Invalid move. Robot would go outside space bounds.";
                        break;

                    default:
                        message = $"Input axis \"{axis}\" is: INVALID";
                        break;
                }
            }

            return message;
        }

    }
}
