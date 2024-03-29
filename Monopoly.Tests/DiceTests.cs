﻿using Monopoly.Logic;
using Xunit;

namespace Monopoly.Tests;

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
        do
        {
            dice.ThrowDice();
        } while (dice.FirstDice != dice.SecondDice);

        // Act
        var result = dice.IsDouble();

        // Assert
        Assert.True(result);
        do
        {
            dice.ThrowDice();
        } while (dice.FirstDice == dice.SecondDice);
        // Arrange

        // Act
        result = dice.IsDouble();

        // Assert
        Assert.False(result);
    }
}