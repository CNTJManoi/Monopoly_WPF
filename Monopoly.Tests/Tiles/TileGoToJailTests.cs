using Monopoly.Logic;
using Monopoly.Logic.Properties;
using Monopoly.Logic.Tiles;
using Xunit;

namespace Monopoly.Tests.Tiles;

public class TileGoToJailTests
{
    [Fact]
    public void DoAction_SetsPlayerCurrentTileToJail()
    {
        // Arrange
        var game = new Game(2);
        game.InitNewGame(2);
        var tile = new TileGoToJail(game, "Go to Jail");

        // Act
        tile.DoAction(game.CurrentPlayer);

        // Assert
        Assert.Equal(game.Jail, game.CurrentPlayer.CurrentTile);
    }

    [Fact]
    public void DoAction_SetsPlayerIsInJailToTrue()
    {
        // Arrange
        var game = new Game(2);
        game.InitNewGame(2);
        var tile = new TileGoToJail(game, "Go to Jail");

        // Act
        tile.DoAction(game.CurrentPlayer);

        // Assert
        Assert.True(game.CurrentPlayer.IsInJail);
    }

    [Fact]
    public void GetCardInformation_ReturnsCorrectValue()
    {
        // Arrange
        var game = new Game(2);
        game.InitNewGame(2);
        var tile = new TileGoToJail(game, "Go to Jail");

        // Act
        var result = tile.GetCardInformation();

        // Assert
        Assert.Equal(Language.gotojail, result);
    }
}