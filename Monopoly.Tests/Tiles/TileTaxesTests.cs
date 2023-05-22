using System;
using Monopoly.Logic;
using Monopoly.Logic.Properties;
using Monopoly.Logic.Tiles;
using Xunit;

namespace Monopoly.Tests.Tiles;

public class TileTaxesTests
{
    [Fact]
    public void DoAction_PlayerHasMoreThan1000Money_TaxesPay200()
    {
        // Arrange
        var game = new Game(2);
        game.InitNewGame(2);
        var player = game.CurrentPlayer;
        player.Money = 1500;
        var tile = new TileTaxes(game, "Taxes");
        // Act
        tile.DoAction(player);

        // Assert
        Assert.Equal(1300, player.Money);
        Assert.Equal(200, game.FreeParkingTreasure);
    }

    [Fact]
    public void DoAction_PlayerHasLessThan1000Money_TaxesPay10Percent()
    {
        // Arrange
        var game = new Game(2);
        game.InitNewGame(2);
        var player = game.CurrentPlayer;
        player.Money = 500;
        var tile = new TileTaxes(game, "Taxes");

        // Act
        tile.DoAction(player);

        // Assert
        Assert.Equal(450, player.Money);
        Assert.Equal(50, game.FreeParkingTreasure);
    }

    [Fact]
    public void GetCardInformation_ReturnsTaxesCardInformation()
    {
        // Arrange
        var game = new Game(2);
        game.InitNewGame(2);
        var tile = new TileTaxes(game, "Taxes");

        // Act
        var result = tile.GetCardInformation();

        // Assert
        Assert.Equal(string.Format(Language.taxes, Environment.NewLine), result);
    }
}