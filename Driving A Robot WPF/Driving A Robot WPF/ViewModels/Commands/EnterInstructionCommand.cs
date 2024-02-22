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
                char[] delimiterChars = { ' ', ',', '\t' };
                string[] commandTokens = _viewModel.ConsoleInput.Split(delimiterChars, StringSplitOptions.RemoveEmptyEntries);

                switch (commandTokens[0].ToLower())
                {
                    case "quit":
                        //????????????cum e mai bine
                        break;

                    case "help":
                        responseMessage = "Available commands: QUIT | SET x, y, z | RESET | MOVE axis, value | 3DMOVE ValueOnX, ValueOnY, ValueOnZ | PRINT";
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
                        if (commandTokens.Length != 3)
                        {
                            responseMessage = $"Invalid command: \"{_viewModel.ConsoleInput}\"";
                            newConsoleHisoryLine += $"(INVALID) ";
                            Utils.Logger.LogError(responseMessage);
                        }
                        else
                            try
                            {
                                double value;

                                if (double.TryParse(commandTokens[1], out value))
                                {
                                    _threeDimensionalSpace.MoveObject(value, commandTokens[2]);
                                }
                                else
                                {
                                    responseMessage = $"Invalid parameters in command: \"{_viewModel.ConsoleInput}\"";
                                    newConsoleHisoryLine += $"(INVALID) ";
                                    Utils.Logger.LogError(responseMessage);
                                }
                            }
                            catch (ThreeDimensionalSpaceException.ObjectNullException ex)
                            {
                                responseMessage = $"Invalid command. The default coordinates must be set first. Use SET x, y, z before.";
                                newConsoleHisoryLine += $"(INVALID) ";
                                Utils.Logger.LogError(responseMessage);
                            }
                            catch (ThreeDimensionalSpaceException.ObjectPositionException ex)
                            {
                                responseMessage = $"Invalid command. Object would be placed out of bounds. Command: \"{_viewModel.ConsoleInput}\"";
                                newConsoleHisoryLine += $"(INVALID) ";
                                Utils.Logger.LogError(responseMessage);
                            }
                            catch (ThreeDimensionalSpaceException.InvalidAxis ex)
                            {
                                responseMessage = $"Invalid command. Input axis should be X, Y or Z. Command: \"{_viewModel.ConsoleInput}\"";
                                newConsoleHisoryLine += $"(INVALID) ";
                                Utils.Logger.LogError(responseMessage);
                            }
                            catch (Exception ex)
                            {
                                responseMessage = $"An unexpected exception occured while moving the robot";
                                Utils.Logger.LogError($"An unexpected exception occured while moving the robot: {ex.Message}");
                            }
                        break;

                    case "3dmove":
                        if (commandTokens.Length != 4)
                        {
                            responseMessage = $"Invalid command: \"{_viewModel.ConsoleInput}\"";
                            newConsoleHisoryLine += $"(INVALID) ";
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
                                    _threeDimensionalSpace.MoveObject(x, y, z);
                                }
                                else
                                {
                                    responseMessage = $"Invalid parameters in command: \"{_viewModel.ConsoleInput}\"";
                                    newConsoleHisoryLine += $"(INVALID) ";
                                    Utils.Logger.LogError(responseMessage);
                                }
                            }
                            catch (ThreeDimensionalSpaceException.ObjectNullException ex)
                            {
                                responseMessage = $"Invalid command. The default coordinates must be set first. Use SET x, y, z before.";
                                newConsoleHisoryLine += $"(INVALID) ";
                                Utils.Logger.LogError(responseMessage);
                            }
                            catch (ThreeDimensionalSpaceException.ObjectPositionException ex)
                            {
                                responseMessage = $"Invalid command. Object would be placed out of bounds. Command: \"{_viewModel.ConsoleInput}\"";
                                newConsoleHisoryLine += $"(INVALID) ";
                                Utils.Logger.LogError(responseMessage);
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
                            responseMessage = $"Invalid command: \"{_viewModel.ConsoleInput}\"";
                            newConsoleHisoryLine += $"(INVALID) ";
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
                                    responseMessage = $"Invalid parameters in command: \"{_viewModel.ConsoleInput}\"";
                                    newConsoleHisoryLine += $"(INVALID) ";
                                    Utils.Logger.LogError(responseMessage);
                                }
                            }
                            catch (NullReferenceException ex)
                            {
                                Utils.Logger.LogError("Object to move does not exist");
                            }
                            catch (ThreeDimensionalSpaceException.ObjectPositionException ex)
                            {
                                responseMessage = $"Invalid command. Object would be placed out of bounds. Command: \"{_viewModel.ConsoleInput}\"";
                                newConsoleHisoryLine += $"(INVALID) ";
                                Utils.Logger.LogError(responseMessage);
                            }
                            catch (Exception ex)
                            {
                                responseMessage = $"An unexpected exception occured while setting the robot's default coordinates.";
                                Utils.Logger.LogError($"An unexpected exception occured while setting the robot's default coordinates: {ex.Message}");
                            }
                        break;

                    case "reset":
                        if (commandTokens.Length != 1)
                        {
                            responseMessage = $"Invalid command: \"{_viewModel.ConsoleInput}\"";
                            newConsoleHisoryLine += $"(INVALID) ";
                            Utils.Logger.LogError(responseMessage);
                        }
                        else
                            try
                            {
                                _threeDimensionalSpace.ResetObjectPosition();
                            }
                            catch (ThreeDimensionalSpaceException.ObjectNullException ex)
                            {
                                responseMessage = $"Invalid command. The default coordinates must be set first. Use SET x, y, z before.";
                                newConsoleHisoryLine += $"(INVALID) ";
                                Utils.Logger.LogError(responseMessage);
                            }
                            catch (ThreeDimensionalSpaceException.UnsetDefaultCoordinates ex)
                            {
                                responseMessage = $"Invalid command. The default coordinates must be set first. Use SET x, y, z before.";
                                newConsoleHisoryLine += $"(INVALID) ";
                                Utils.Logger.LogError(responseMessage);
                            }
                            catch (Exception ex)
                            {
                                responseMessage = $"An unexpected exception occured while resetting the robot's coordinates.";
                                Utils.Logger.LogError($"An unexpected exception occured while resetting the robot's coordinates: {ex.Message}");
                            }
                        break;

                    default:
                        responseMessage = $"Unknown command: \"{commandTokens[0]}\" Type \"Help\" to see a list of available commands.";
                        newConsoleHisoryLine += $"(INVALID) ";
                        Utils.Logger.LogError(responseMessage);
                        break;
                }

                newConsoleHisoryLine += $"{_viewModel.ConsoleInput}";
            }

            _viewModel.ConsoleHistory += $"\n{newConsoleHisoryLine}"; //strikethrough daca e (invalid)
            _viewModel.ConsoleInput = string.Empty;
            _viewModel.Output = responseMessage;
        }
    }
}
