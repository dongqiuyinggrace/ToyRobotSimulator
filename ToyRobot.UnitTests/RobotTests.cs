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
        [TestCase("MOVE\nLEFT\nREPORT")]
        public void Execute_WhenInputCommandsWithoutPlacement_ThrowArgumentException(string commands)
        {
            Robot robot = new Robot(commands);
            Assert.That(() => robot.Execute(), Throws.ArgumentException);
        }

        [Test]
        [TestCase("PLACE 0,0,NORTH\nLEFT\nREPORT", "0,0,WEST")]
        [TestCase("PLACE 0,0,WEST\nLEFT\nREPORT", "0,0,SOUTH")]
        [TestCase("PLACE 0,0,SOUTH\nLEFT\nREPORT", "0,0,EAST")]
        [TestCase("PLACE 0,0,EAST\nLEFT\nREPORT", "0,0,NORTH")]
        public void Execute_WhenInputTurnLeftCommand_TurnLeft(string commands, string expectedResult)
        {
            Robot robot = new Robot(commands);
            string result = robot.Execute();
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        [TestCase("PLACE 0,0,NORTH\nRIGHT\nREPORT", "0,0,EAST")]
        [TestCase("PLACE 0,0,EAST\nRIGHT\nREPORT", "0,0,SOUTH")]
        [TestCase("PLACE 0,0,SOUTH\nRIGHT\nREPORT", "0,0,WEST")]
        [TestCase("PLACE 0,0,WEST\nRIGHT\nREPORT", "0,0,NORTH")]
        public void Execute_WhenInputTurnRightCommand_TurnRight(string commands, string expectedResult)
        {
            Robot robot = new Robot(commands);
            string result = robot.Execute();
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        [TestCase("PLACE 0,0,NORTH\nMOVE\nREPORT", "0,1,NORTH")]
        [TestCase("PLACE 0,0,EAST\nMOVE\nREPORT", "1,0,EAST")]
        [TestCase("PLACE 0,0,SOUTH\nMOVE\nREPORT", "0,0,SOUTH")]
        [TestCase("PLACE 0,0,WEST\nMOVE\nREPORT", "0,0,WEST")]
        public void Execute_WhenInputMoveCommand_MoveOneStepForward(string commands, string expectedResult)
        {
            Robot robot = new Robot(commands);
            string result = robot.Execute();
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        [TestCase("PLACE 1,2,EAST\nMOVE\nMOVE\nLEFT\nMOVE\nREPORT", "3,3,NORTH")]
        [TestCase("PLACE 1,1,EAST\nMOVE\nLEFT\nMOVE\nRIGHT\nRIGHT\nREPORT", "2,2,SOUTH")]
        public void Execute_WhenInputMoveAndTurnCommands_MoveAndTurn(string commands, string expectedResult)
        {
            Robot robot = new Robot(commands);
            string result = robot.Execute();
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        [TestCase("PLACE -1,-1,NORTH\nREPORT", "0,0,NORTH")]
        [TestCase("PLACE 6,6,NORTH\nMOVE\nREPORT", "0,0,NORTH")]
        public void Execute_WhenInputInvalidInitialPlacement_StaysAtTheOrigin(string commands, string expectedResult)
        {
            Robot robot = new Robot(commands);
            string result = robot.Execute();
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        [TestCase("PLACE 5,5,NORTH\nMOVE\nREPORT", "5,5,NORTH")]
        [TestCase("PLACE 5,5,EAST\nMOVE\nREPORT", "5,5,EAST")]
        [TestCase("PLACE 5,5,SOUTH\nMOVE\nREPORT", "5,4,SOUTH")]
        [TestCase("PLACE 5,5,WEST\nMOVE\nREPORT", "4,5,WEST")]
        public void Execute_WhenStandAtEndPoint_CanOnlyMoveToSouthOrWest(string commands, string expectedResult)
        {
            Robot robot = new Robot(commands);
            string result = robot.Execute();
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        [TestCase("PLACE 2,2,NORTH\nREPORT\nPLACE 1,2,EAST\nREPORT", "1,2,EAST")]
        [TestCase("PLACE 1,1,NORTH\nMOVE\nRIGHT\nMOVE\nREPORT\nPLACE 1,2,EAST\nMOVE\nMOVE\nLEFT\nMOVE\nREPORT", "3,3,NORTH")]
        public void Execute_WhenPlacedMultipleTimes_FinalPositionIsBasedOnTheLastPlacement(string commands, string expectedResult)
        {
            Robot robot = new Robot(commands);
            string result = robot.Execute();
            Assert.That(result, Is.EqualTo(expectedResult));
        }
    }
}