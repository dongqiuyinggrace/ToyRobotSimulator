using System;
using System.Collections.Generic;
using System.Text;

namespace ToyRobot
{
    public class Robot
    {
        private const int ENDPOINT_X = 5;
        private const int ENDPOINT_Y = 5;

        private readonly string _commands;
        private string _direction;
        private Coordinate _coordinate;

        public Robot(string commands)
        {
            _commands = commands;
            _coordinate = new Coordinate(0, 0);
            _direction = "";
        }

        public string Execute()
        {
            string[] commandLines = _commands.Split("\r\n");
            var indexesOfPlacement = new List<int>();
            for (int i = 0; i < commandLines.Length; i++)
            {
                if (commandLines[i].StartsWith("PLACE"))
                {
                    indexesOfPlacement.Add(i);
                }
            }

            if (indexesOfPlacement.Count == 0)
            {
                throw new ArgumentException();
            }

            foreach (var index in indexesOfPlacement)
            {
                ExecuteBasedOnOnePlacement(commandLines, index);
            }

            return _coordinate.X.ToString() + "," + _coordinate.Y.ToString() + "," + _direction;
        }

        private void ExecuteBasedOnOnePlacement(string[] commandLines, int index)
        {
            GetPositionAndDireciton(commandLines[index]);

            if (_coordinate.X < 0 || _coordinate.X > ENDPOINT_X || _coordinate.Y < 0 || _coordinate.Y > ENDPOINT_Y)
            {
                _coordinate.X = 0;
                _coordinate.Y = 0;
                return;
            }

            for (int i = index + 1; i < commandLines.Length; i++)
            {
                if (commandLines[i].StartsWith("PLACE"))
                {
                    break;
                }

                if (commandLines[i] == "LEFT")
                {
                    RotateLeft();
                }
                else if (commandLines[i] == "RIGHT")
                {
                    RotateRight();
                }
                else if (commandLines[i] == "MOVE")
                {
                    MoveForward();
                }
                else if (commandLines[i] == "REPORT")
                {
                    Console.WriteLine(_coordinate.X.ToString() + "," + _coordinate.Y.ToString() + "," + _direction);
                }
            }
        }

        private void GetPositionAndDireciton(string commandString)
        {
            int indexOfFirstComma = commandString.IndexOf(',');
            int indexOfSecondComma = commandString.IndexOf(',', indexOfFirstComma + 1);

            int indexOfXCoordinate = commandString.IndexOf(' ') + 1;
            int indexOfYCoordinate = indexOfFirstComma + 1;

            string xCoordinate = commandString.Substring(indexOfXCoordinate, indexOfFirstComma - indexOfXCoordinate);
            string yCoordinate = commandString.Substring(indexOfYCoordinate, indexOfSecondComma - indexOfYCoordinate);

            _coordinate.X = Convert.ToInt32(xCoordinate);
            _coordinate.Y = Convert.ToInt32(yCoordinate);

            _direction = commandString.Substring(commandString.LastIndexOf(',') + 1);
        }

        private void MoveForward()
        {
            switch (_direction)
            {
                case "NORTH":
                    _coordinate.Y = Math.Min(_coordinate.Y + 1, ENDPOINT_Y);
                    break;
                case "EAST":
                    _coordinate.X = Math.Min(_coordinate.X + 1, ENDPOINT_X);
                    break;
                case "SOUTH":
                    _coordinate.Y = Math.Max(_coordinate.Y - 1, 0);
                    break;
                case "WEST":
                    _coordinate.X = Math.Max(_coordinate.X - 1, 0);
                    break;
                default:
                    break;
            }
        }

        private void RotateRight()
        {
            string result;
            switch (_direction)
            {
                case "NORTH":
                    result = "EAST";
                    break;
                case "EAST":
                    result = "SOUTH";
                    break;
                case "SOUTH":
                    result = "WEST";
                    break;
                case "WEST":
                    result = "NORTH";
                    break;
                default:
                    result = "";
                    break;
            }

            _direction = result;
        }

        private void RotateLeft()
        {
            string result;
            switch (_direction)
            {
                case "NORTH":
                    result = "WEST";
                    break;
                case "WEST":
                    result = "SOUTH";
                    break;
                case "SOUTH":
                    result = "EAST";
                    break;
                case "EAST":
                    result = "NORTH";
                    break;
                default:
                    result = "";
                    break;
            }

            _direction = result;
        }
    }
}
