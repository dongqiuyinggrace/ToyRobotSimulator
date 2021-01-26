using System;
using System.IO;

namespace ToyRobot
{
    class Program
    {
        static void Main(string[] args)
        {
            var commands = File.ReadAllText("../../../commands.txt");
            Robot robot = new Robot(commands);
            robot.Execute();
        }
    }
}
