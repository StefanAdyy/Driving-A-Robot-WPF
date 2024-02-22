using Driving_A_Robot_WPF.Exceptions;
using Driving_A_Robot_WPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Driving_A_Robot_WPF.ViewModels.Commands
{
    class EnterInstructionCommand : CommandBase
    {
        private ThreeDimensionalSpaceModel _threeDimensionalSpace;
        private InputOutputViewModel _viewModel;
        public EnterInstructionCommand(ThreeDimensionalSpaceModel threeDimensionalSpace, InputOutputViewModel viewModel)
        {
            _threeDimensionalSpace = threeDimensionalSpace;
            _viewModel = viewModel;
        }

        public override void Execute(object parameter)
        {
            string newConsoleHisoryLine = $"[{DateTime.Now.Hour}:{DateTime.Now.Minute}]>";
            string responseMessage = "";

            if (!string.IsNullOrWhiteSpace(_viewModel.ConsoleInput))
            {
                string[] commandTokens = _viewModel.ConsoleInput.Split(" ", StringSplitOptions.RemoveEmptyEntries);

                switch (commandTokens[0].ToLower())
                {
                    case "quit":
                        //????????????cum e mai bine
                        break;

                    case "help":
                        responseMessage = "Available commands: QUIT, SET(x, y, z), RESET, MOVE (axis, value), 3DMOVE(ValueOnX, ValueOnY, ValueOnZ), PRINT";
                        break;

                    case "print":
                        try
                        {
                            if (_threeDimensionalSpace.ObjectInSpace != null)
                                responseMessage = $"Robot's current coordinates: {_threeDimensionalSpace.ObjectInSpace.Coordinates.X}, " +
                                                  $"{_threeDimensionalSpace.ObjectInSpace.Coordinates.Y}, " +
                                                  $"{_threeDimensionalSpace.ObjectInSpace.Coordinates.Z}";
                            else
                                responseMessage = $"Robot's current coordinates: ???, ???, ??? (NOT SET)";
                        }
                        catch (Exception ex)
                        {
                            responseMessage = $"An unexpected exception occured while printing the robot's current coordinates.";
                            Utils.Logger.LogError($"An unexpected exception occured while printing the robot's current coordinates: {ex.Message}");
                        }
                        break;

                    case "move":
                        try
                        {
                            if (_threeDimensionalSpace.ObjectInSpace != null && _threeDimensionalSpace.ObjectInSpace.AreCoordinatesSet)
                                //verifica mai intai daca are destui parametri si tryparse parametri
                                try
                                {
                                    //_threeDimensionalSpace.MoveObject
                                }
                                catch (ThreeDimensionalSpaceException.ObjectPositionException ex)
                                {
                                    //log
                                }
                                catch (ThreeDimensionalSpaceException.InvalidAxis ex)
                                {

                                }
                                catch (Exception ex)
                                {
                                    responseMessage = $"An unexpected exception occured while moving the robot.";
                                    Utils.Logger.LogError($"An unexpected exception occured while moving the robot: {ex.Message}");
                                }

                            else
                                responseMessage = $"The robot initial coordinates must be set first! (Command: SET x, y, z)";
                        }
                        catch (Exception ex)
                        {
                            responseMessage = $"An unexpected exception occured while moving the robot";
                            Utils.Logger.LogError($"An unexpected exception occured while moving the robot: {ex.Message}");
                        }
                        break;

                    case "3dmove": //vezi set
                        try
                        {
                            if (_threeDimensionalSpace.ObjectInSpace != null && _threeDimensionalSpace.ObjectInSpace.AreCoordinatesSet)
                                //move
                                try
                                {
                                    //_threeDimensionalSpace.MoveObject
                                }
                                catch (ThreeDimensionalSpaceException.ObjectPositionException ex)
                                {
                                    //log
                                }
                                catch (Exception ex)
                                {
                                    responseMessage = $"An unexpected exception occured while moving the robot.";
                                    Utils.Logger.LogError($"An unexpected exception occured while moving the robot: {ex.Message}");
                                }

                            else
                                responseMessage = $"The robot initial coordinates must be set first! (Command: SET x, y, z)";
                        }
                        catch (Exception ex)
                        {
                            responseMessage = $"An unexpected exception occured while moving the robot";
                            Utils.Logger.LogError($"An unexpected exception occured while moving the robot: {ex.Message}");
                        }
                        break;

                    case "set":
                        if (commandTokens.Length != 4)
                        {
                            responseMessage = $"Invalid command: \"{_viewModel.ConsoleInput}\""; //strike through?
                            newConsoleHisoryLine += $"(INVALID COMMAND) ";
                            Utils.Logger.LogError(responseMessage);
                        }
                        else
                            try
                            {
                                double x, y, z;

                                if (double.TryParse(commandTokens[1], out x) && 
                                    double.TryParse(commandTokens[2], out y) && 
                                    double.TryParse(commandTokens[3], out z))
                                {
                                    PointModel coordinates = new PointModel(x, y, z);
                                    RobotModel robot = new RobotModel();
                                    _threeDimensionalSpace.ObjectInSpace = robot;
                                    _threeDimensionalSpace.SetObjectDefaultPosition(coordinates);
                                }
                                else
                                {
                                    responseMessage = $"Invalid command: \"{_viewModel.ConsoleInput}\""; //strike through?
                                    newConsoleHisoryLine += $"(INVALID COMMAND) ";
                                    Utils.Logger.LogError(responseMessage);
                                }

                            }
                            catch (NullReferenceException ex)
                            {
                                Utils.Logger.LogError("Object to move does not exist");
                            }
                            catch (ThreeDimensionalSpaceException.ObjectPositionException ex)
                            {

                                responseMessage = $"Wrong coordonates. Object would be placed out of bounds. Command: \"{_viewModel.ConsoleInput}\""; //strike through?
                                newConsoleHisoryLine += $"(INVALID COMMAND) ";
                                Utils.Logger.LogError(responseMessage);
                            }
                            catch (Exception ex)
                            {
                                responseMessage = $"An unexpected exception occured while setting the robot's default coordinates.";
                                Utils.Logger.LogError($"An unexpected exception occured while setting the robot's default coordinates: {ex.Message}");
                            }
                        break;

                    case "reset":
                        break;

                    default:
                        responseMessage = $"Unknown command: \"{commandTokens[0]}\" Type \"Help\" to see a list of available commands."; //strike through?
                        newConsoleHisoryLine += $"(UNKNOWN COMMAND) ";
                        Utils.Logger.LogError(responseMessage);
                        break;
                }

                newConsoleHisoryLine += $"{_viewModel.ConsoleInput}";
            }

            _viewModel.ConsoleHistory += $"\n{newConsoleHisoryLine}";
            _viewModel.ConsoleInput = string.Empty;
            _viewModel.Output = responseMessage;
        }
    }
}
