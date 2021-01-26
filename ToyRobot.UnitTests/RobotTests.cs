using NUnit.Framework;
using System;
using ToyRobot;

namespace ToyRobot.UnitTests
{
    public class RobotTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [TestCase("LEFT")]
        [TestCase("MOVE\r\nLEFT\r\nREPORT")]
        public void Execute_WhenInputCommandsWithoutPlacement_ThrowArgumentException(string commands)
        {
            Robot robot = new Robot(commands);
            Assert.That(() => robot.Execute(), Throws.ArgumentException);
        }

        [Test]
        [TestCase("PLACE 0,0,NORTH\r\nLEFT\r\nREPORT", "0,0,WEST")]
        [TestCase("PLACE 0,0,WEST\r\nLEFT\r\nREPORT", "0,0,SOUTH")]
        [TestCase("PLACE 0,0,SOUTH\r\nLEFT\r\nREPORT", "0,0,EAST")]
        [TestCase("PLACE 0,0,EAST\r\nLEFT\r\nREPORT", "0,0,NORTH")]
        public void Execute_WhenInputTurnLeftCommand_TurnLeft(string commands, string expectedResult)
        {
            Robot robot = new Robot(commands);
            string result = robot.Execute();
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        [TestCase("PLACE 0,0,NORTH\r\nRIGHT\r\nREPORT", "0,0,EAST")]
        [TestCase("PLACE 0,0,EAST\r\nRIGHT\r\nREPORT", "0,0,SOUTH")]
        [TestCase("PLACE 0,0,SOUTH\r\nRIGHT\r\nREPORT", "0,0,WEST")]
        [TestCase("PLACE 0,0,WEST\r\nRIGHT\r\nREPORT", "0,0,NORTH")]
        public void Execute_WhenInputTurnRightCommand_TurnRight(string commands, string expectedResult)
        {
            Robot robot = new Robot(commands);
            string result = robot.Execute();
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        [TestCase("PLACE 0,0,NORTH\r\nMOVE\r\nREPORT", "0,1,NORTH")]
        [TestCase("PLACE 0,0,EAST\r\nMOVE\r\nREPORT", "1,0,EAST")]
        [TestCase("PLACE 0,0,SOUTH\r\nMOVE\r\nREPORT", "0,0,SOUTH")]
        [TestCase("PLACE 0,0,WEST\r\nMOVE\r\nREPORT", "0,0,WEST")]
        public void Execute_WhenInputMoveCommand_MoveOneStepForward(string commands, string expectedResult)
        {
            Robot robot = new Robot(commands);
            string result = robot.Execute();
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        [TestCase("PLACE 1,2,EAST\r\nMOVE\r\nMOVE\r\nLEFT\r\nMOVE\r\nREPORT", "3,3,NORTH")]
        [TestCase("PLACE 1,1,EAST\r\nMOVE\r\nLEFT\r\nMOVE\r\nRIGHT\r\nRIGHT\r\nREPORT", "2,2,SOUTH")]
        public void Execute_WhenInputMoveAndTurnCommands_MoveAndTurn(string commands, string expectedResult)
        {
            Robot robot = new Robot(commands);
            string result = robot.Execute();
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        [TestCase("PLACE -1,-1,NORTH\r\nREPORT", "0,0,NORTH")]
        [TestCase("PLACE 6,6,NORTH\r\nMOVE\r\nREPORT", "0,0,NORTH")]
        public void Execute_WhenInputInvalidInitialPlacement_StaysAtTheOrigin(string commands, string expectedResult)
        {
            Robot robot = new Robot(commands);
            string result = robot.Execute();
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        [TestCase("PLACE 5,5,NORTH\r\nMOVE\r\nREPORT", "5,5,NORTH")]
        [TestCase("PLACE 5,5,EAST\r\nMOVE\r\nREPORT", "5,5,EAST")]
        [TestCase("PLACE 5,5,SOUTH\r\nMOVE\r\nREPORT", "5,4,SOUTH")]
        [TestCase("PLACE 5,5,WEST\r\nMOVE\r\nREPORT", "4,5,WEST")]
        public void Execute_WhenStandAtEndPoint_CanOnlyMoveToSouthOrWest(string commands, string expectedResult)
        {
            Robot robot = new Robot(commands);
            string result = robot.Execute();
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        [TestCase("PLACE 2,2,NORTH\r\nREPORT\r\nPLACE 1,2,EAST\r\nREPORT", "1,2,EAST")]
        [TestCase("PLACE 1,1,NORTH\r\nMOVE\r\nRIGHT\r\nMOVE\r\nREPORT\r\nPLACE 1,2,EAST\r\nMOVE\r\nMOVE\r\nLEFT\r\nMOVE\r\nREPORT", "3,3,NORTH")]
        public void Execute_WhenPlacedMultipleTimes_FinalPositionIsBasedOnTheLastPlacement(string commands, string expectedResult)
        {
            Robot robot = new Robot(commands);
            string result = robot.Execute();
            Assert.That(result, Is.EqualTo(expectedResult));
        }
    }
}