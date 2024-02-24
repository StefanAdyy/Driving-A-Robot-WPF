using Driving_A_Robot_WPF.Exceptions;
using Driving_A_Robot_WPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

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
                string command = _viewModel.ConsoleInput.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries)[0];
                string commandParameters = _viewModel.ConsoleInput.Remove(0, _viewModel.ConsoleInput.IndexOf(command) + command.Length);
                string[] commandTokens;

                switch (command, commandParameters.Trim(new char[] {' ', '\t'}).Length)
                {
                    case ("quit", 0):
                        Environment.Exit(0);
                        break;

                    case ("help", 0):
                        responseMessage = "Available commands: QUIT | SET x, y, z | RESET | MOVE axis, value | 3DMOVE ValueOnX, ValueOnY, ValueOnZ | PRINT";
                        break;

                    case ("print", 0):
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

                    case ("move", > 0):
                        commandTokens = commandParameters.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                        
                        if (commandTokens.Length != 2)
                        {
                            responseMessage = $"Invalid command: \"{_viewModel.ConsoleInput}\"";
                            newConsoleHisoryLine += $"(INVALID) ";
                            Utils.Logger.LogError(responseMessage);
                        }
                        else
                            try
                            {
                                double value;

                                if (double.TryParse(commandTokens[0].Trim(), out value))
                                {
                                    _threeDimensionalSpace.MoveObject(value, commandTokens[1].Trim());
                                }
                                else
                                {
                                    responseMessage = $"Invalid parameters in command: \"{_viewModel.ConsoleInput}\"";
                                    newConsoleHisoryLine += $"(INVALID) ";
                                    Utils.Logger.LogError(responseMessage);
                                }
                            }
                            catch (ThreeDimensionalSpaceException.ObjectNullException)
                            {
                                responseMessage = $"Invalid command. The object's position must be set first. Use SET x, y, z before.";
                                newConsoleHisoryLine += $"(INVALID) ";
                                Utils.Logger.LogError(responseMessage);
                            }
                            catch (ThreeDimensionalSpaceException.ObjectPositionException)
                            {
                                responseMessage = $"Invalid command. Object would be placed out of bounds. Command: \"{_viewModel.ConsoleInput}\"";
                                newConsoleHisoryLine += $"(INVALID) ";
                                Utils.Logger.LogError(responseMessage);
                            }
                            catch (ThreeDimensionalSpaceException.InvalidAxis)
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

                    case ("3dmove", > 0):
                        commandTokens = commandParameters.Split(',');

                        if (commandTokens.Length != 3)
                        {
                            responseMessage = $"Invalid command: \"{_viewModel.ConsoleInput}\"";
                            newConsoleHisoryLine += $"(INVALID) ";
                            Utils.Logger.LogError(responseMessage);
                        }
                        else
                            try
                            {
                                double x, y, z;

                                if (double.TryParse(commandTokens[0].Trim(), out x) &&
                                    double.TryParse(commandTokens[1].Trim(), out y) &&
                                    double.TryParse(commandTokens[2].Trim(), out z))
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
                            catch (ThreeDimensionalSpaceException.ObjectNullException)
                            {
                                responseMessage = $"Invalid command. The object's position must be set first. Use SET x, y, z before.";
                                newConsoleHisoryLine += $"(INVALID) ";
                                Utils.Logger.LogError(responseMessage);
                            }
                            catch (ThreeDimensionalSpaceException.ObjectPositionException)
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

                    case ("set", > 0):
                        commandTokens = commandParameters.Split(',');

                        if (commandTokens.Length != 3)
                        {
                            responseMessage = $"Invalid command: \"{_viewModel.ConsoleInput}\"";
                            newConsoleHisoryLine += $"(INVALID) ";
                            Utils.Logger.LogError(responseMessage);
                        }
                        else
                            try
                            {
                                double x, y, z;

                                if (double.TryParse(commandTokens[0].Trim(), out x) &&
                                    double.TryParse(commandTokens[1].Trim(), out y) &&
                                    double.TryParse(commandTokens[2].Trim(), out z))
                                {
                                    PointModel coordinates = new PointModel(x, y, z);

                                    if (_threeDimensionalSpace.ObjectInSpace != null)
                                    {
                                        _threeDimensionalSpace.SetObjectPosition(coordinates);
                                    }
                                    else
                                    {
                                        RobotModel robot = new RobotModel(coordinates);
                                        _threeDimensionalSpace.ObjectInSpace = robot;
                                    }
                                }
                                else
                                {
                                    responseMessage = $"Invalid parameters in command: \"{_viewModel.ConsoleInput}\"";
                                    newConsoleHisoryLine += $"(INVALID) ";
                                    Utils.Logger.LogError(responseMessage);
                                }
                            }
                            catch (NullReferenceException)
                            {
                                Utils.Logger.LogError("Object to move does not exist");
                            }
                            catch (ThreeDimensionalSpaceException.ObjectPositionException)
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

                    case ("reset", 0):
                        try
                        {
                            _threeDimensionalSpace.ResetObjectPosition();
                        }
                        catch (ThreeDimensionalSpaceException.ObjectNullException)
                        {
                            responseMessage = $"Invalid command. The object's position must be set first. Use SET x, y, z before.";
                            newConsoleHisoryLine += $"(INVALID) ";
                            Utils.Logger.LogError(responseMessage);
                        }
                        catch (ThreeDimensionalSpaceException.UnsetDefaultCoordinates)
                        {
                            responseMessage = $"Invalid command. The object's position must be set first. Use SET x, y, z before.";
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
                        responseMessage = $"Unknown command: \"{_viewModel.ConsoleInput}\" Type \"Help\" to see a list of available commands.";
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
