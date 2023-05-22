using System.Linq;
using Monopoly.Logic;
using Monopoly.Logic.Properties;
using Monopoly.Logic.Tiles;
using Xunit;

namespace Monopoly.Tests.Tiles;

public class TileFreeParkingTests
{
    [Fact]
    public void DoAction_AddsDescriptionToGameInfo()
    {
        // Arrange
        var game = new Game(2);
        game.InitNewGame(2);
        var tile = new TileFreeParking(game, "Free Parking");

        // Act
        tile.DoAction(game.CurrentPlayer);

        // Assert
        Assert.True(game.GameInfo.Where(x =>
            x.Contains(string.Format(Language.moves, game.CurrentPlayer.Name, "Free Parking"))).Count() > 0);
    }

    [Fact]
    public void DoAction_AddsTreasureToPlayer_IfTreasureIsPositive()
    {
        // Arrange
        var game = new Game(2);
        game.InitNewGame(2);
        var tile = new TileFreeParking(game, "Free Parking");
        game.FreeParkingTreasure = 100;
        game.CurrentPlayer.Money = 0;

        // Act
        tile.DoAction(game.CurrentPlayer);

        // Assert
        Assert.Equal(100, game.CurrentPlayer.Money);
    }

    [Fact]
    public void DoAction_AddsTreasureToGameInfo_IfTreasureIsPositive()
    {
        // Arrange
        var game = new Game(2);
        game.InitNewGame(2);
        var tile = new TileFreeParking(game, "Free Parking");
        game.FreeParkingTreasure = 100;
        game.CurrentPlayer.Money = 0;

        // Act
        tile.DoAction(game.CurrentPlayer);

        // Assert
        Assert.True(game.GameInfo.Where(x =>
            x.Contains(string.Format(Language.freeparkingpay, game.CurrentPlayer.Name,
                100))).Count() > 0);
    }

    [Fact]
    public void DoAction_ClearsTreasure_IfTreasureIsPositive()
    {
        // Arrange
        var game = new Game(2);
        game.InitNewGame(2);
        var tile = new TileFreeParking(game, "Free Parking");
        game.FreeParkingTreasure = 100;
        game.CurrentPlayer.Money = 0;

        // Act
        tile.DoAction(game.CurrentPlayer);

        // Assert
        Assert.Equal(0, game.FreeParkingTreasure);
    }

    [Fact]
    public void DoAction_DoesNotAddTreasureToPlayer_IfTreasureIsZero()
    {
        // Arrange
        var game = new Game(2);
        game.InitNewGame(2);
        var tile = new TileFreeParking(game, "Free Parking");
        game.FreeParkingTreasure = 0;
        game.CurrentPlayer.Money = 0;

        // Act
        tile.DoAction(game.CurrentPlayer);

        // Assert
        Assert.Equal(0, game.CurrentPlayer.Money);
    }

    [Fact]
    public void DoAction_DoesNotAddTreasureToGameInfo_IfTreasureIsZero()
    {
        // Arrange
        var game = new Game(2);
        game.InitNewGame(2);
        var tile = new TileFreeParking(game, "Free Parking");

        // Act
        tile.DoAction(game.CurrentPlayer);

        // Assert
        Assert.True(game.GameInfo.Count == 1);
    }

    [Fact]
    public void GetCardInformation_ReturnsCorrectString()
    {
        // Arrange
        var game = new Game(2);
        game.InitNewGame(2);
        var tile = new TileFreeParking(game, "Free Parking");

        // Act
        var cardInfo = tile.GetCardInformation();

        // Assert
        Assert.Equal(Language.freeparking, cardInfo);
    }
}