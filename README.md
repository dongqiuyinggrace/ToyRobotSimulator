# ToyRobotSimulator
 There are two projects in this ToyRobotSimulator solution: ToyRobot and ToyRobot.UnitTests.

## ToyRobot 
- Robot.cs: Main class to simulate robot moving on a square tabletop 
- Coordinate.cs: The coordinate (X and Y) of the robot
- Program.cs: In the Main method, Robot is instantiated with commands read from commands.txt as the input parameter, then call Execute method on that instance, result can be shown in the console. 
- commands.txt: Commands that instruct how to place and move the robot are in this file

## ToyRobot.UnitTests
- RobotTests.cs: UnitTests for Robot.cs. 
