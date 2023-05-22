using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monopoly.Logic;
using Xunit;

namespace Monopoly.Tests
{
    public class DiceTests
    {

        [Fact]
        public void TestThrowDice()
        {
            // Arrange
            var dice = new Dice();

            // Act
            var result = dice.ThrowDice();

            // Assert
            Assert.InRange(result, 2, 12);
            Assert.InRange(dice.FirstDice, 1, 6);
            Assert.InRange(dice.SecondDice, 1, 6);
        }

        [Fact]
        public void TestIsDouble()
        {
            // Arrange
            var dice = new Dice();
            dice.FirstDice = 3;
            dice.SecondDice = 3;

            // Act
            var result = dice.IsDouble();

            // Assert
            Assert.True(result);

            // Arrange
            dice.FirstDice = 1;
            dice.SecondDice = 2;

            // Act
            result = dice.IsDouble();

            // Assert
            Assert.False(result);
        }
    }
}

