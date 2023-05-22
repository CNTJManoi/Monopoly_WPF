using System;
using Monopoly.Logic;
using Monopoly.Logic.Properties;
using Monopoly.Logic.Tiles;
using Xunit;

namespace Monopoly.Tests.Tiles;

public class TileStartTests
{
    [Fact]
    public void DoAction_AddsMovesToGameInfoQueue()
    {
        // Arrange
        var game = new Game(2);
        game.InitNewGame(2);
        var player = game.CurrentPlayer;
        var tileStart = new TileStart(game, "Start");
        // Act
        tileStart.DoAction(player);

        // Assert

        Assert.Contains(string.Format(Language.moves, player.Name, tileStart.Description),
            game.GameInfo);
    }

    [Fact]
    public void DoAction_AddsStartMoneyToPlayer()
    {
        // Arrange
        var game = new Game(2);
        game.InitNewGame(2);
        var player = game.CurrentPlayer;
        var tileStart = new TileStart(game, "Start");

        // Act
        tileStart.DoAction(player);

        // Assert
        Assert.Equal(1900, player.Money);
    }

    [Fact]
    public void GetCardInformation_ReturnsStartCardInformation()
    {
        // Arrange
        var game = new Game(2);
        game.InitNewGame(2);
        var tileStart = new TileStart(game, "Start");

        // Act
        var result = tileStart.GetCardInformation();

        // Assert
        Assert.Equal(string.Format(Language.start, tileStart.Description, Environment.NewLine), result);
    }
}