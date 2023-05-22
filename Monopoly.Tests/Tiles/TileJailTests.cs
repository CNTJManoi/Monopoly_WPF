using Monopoly.Logic;
using Monopoly.Logic.Properties;
using Monopoly.Logic.Tiles;
using Xunit;

namespace Monopoly.Tests.Tiles;

public class TileJailTests
{
    [Fact]
    public void DoAction_WhenPlayerIsNotInJail_AddsCorrectMessageToGameInfoQueue()
    {
        // Arrange
        var game = new Game(2);
        game.InitNewGame(2);
        var tile = new TileJail(game, "Jail");

        // Act
        tile.DoAction(game.CurrentPlayer);

        // Assert
        Assert.Contains(string.Format(Language.moves, game.CurrentPlayer.Name, tile.Description),
            game.GameInfo);
    }

    [Fact]
    public void DoAction_WhenPlayerIsInJail_AddsCorrectMessageToGameInfoQueue()
    {
        // Arrange
        var game = new Game(2);
        game.InitNewGame(2);
        var tile = new TileJail(game, "Jail");

        game.CurrentPlayer.IsInJail = true;
        game.CurrentPlayer.JailCounter = 1;
        game.CurrentPlayer.CurrentTile = tile;

        // Act
        tile.DoAction(game.CurrentPlayer);

        // Assert
        Assert.Contains(string.Format(Language.jailperson, game.CurrentPlayer.Name),
            game.GameInfo);
    }

    [Fact]
    public void DoAction_WhenPlayerRollsDoubles_MovesPlayerToJailVisitAndSetsIsInJailToFalse()
    {
        // Arrange
        var game = new Game(2);
        game.InitNewGame(2);
        var tile = new TileJail(game, "Jail");

        game.CurrentPlayer.IsInJail = true;
        game.CurrentPlayer.JailCounter = 1;

        game.PlayerDice.FirstDice = 2;
        game.PlayerDice.SecondDice = 2;

        // Act
        tile.DoAction(game.CurrentPlayer);

        // Assert
        Assert.Equal(game.JailVisit, game.CurrentPlayer.CurrentTile);
        Assert.False(game.CurrentPlayer.IsInJail);
        Assert.Equal(0, game.CurrentPlayer.JailCounter);
    }

    [Fact]
    public void DoAction_WhenPlayerHasRolledThreeTimes_MovesPlayerToJailVisitAndSetsIsInJailToFalse()
    {
        // Arrange
        var game = new Game(2);
        game.InitNewGame(2);
        var tile = new TileJail(game, "Jail");

        game.CurrentPlayer.IsInJail = true;
        game.CurrentPlayer.JailCounter = 2;

        game.PlayerDice.FirstDice = 1;
        game.PlayerDice.SecondDice = 2;

        // Act
        tile.DoAction(game.CurrentPlayer);

        // Assert
        Assert.Equal(game.JailVisit, game.CurrentPlayer.CurrentTile);
        Assert.False(game.CurrentPlayer.IsInJail);
        Assert.Equal(0, game.CurrentPlayer.JailCounter);
    }

    [Fact]
    public void GetCardInformation_ReturnsCorrectValue()
    {
        // Arrange
        var game = new Game(2);
        game.InitNewGame(2);
        var tile = new TileJail(game, "Jail");

        // Act
        var result = tile.GetCardInformation();

        // Assert
        Assert.Equal(Language.jail, result);
    }
}