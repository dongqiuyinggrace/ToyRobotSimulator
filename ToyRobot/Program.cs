using System;

namespace ToyRobot
{
    class Program
    {
        static void Main(string[] args)
        {
            Robot robot = new Robot("PLACE 2,2,NORTH\nREPORT\nPLACE 1,2,EAST\nREPORT\nPLACE 1,1,SOUTH\nMOVE\nMOVE\nREPORT");
            robot.Execute();
        }
    }
}
