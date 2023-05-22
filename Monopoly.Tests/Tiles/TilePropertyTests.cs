using Monopoly.Logic;
using Monopoly.Logic.Tiles;
using Xunit;

namespace Monopoly.Tests.Tiles;

public class TilePropertyTests
{
    [Fact]
    public void DoAction_PlayerPaysRentToOwner()
    {
        // Arrange
        var game = new Game(2);
        game.InitNewGame(2);
        var owner = game.Players[0];
        var player = game.Players[1];
        var city = new City();
        var rent = new[] { 10, 20, 30, 40, 50, 60 };
        var tile = new TileProperty(game, "Tile", rent, 100, 200, 300, city);
        tile.Owner = owner;
        var expectedOwnerMoney = owner.Money + rent[0];
        var expectedPlayerMoney = player.Money - rent[0];
        // Act
        tile.DoAction(player);

        // Assert
        Assert.Equal(expectedOwnerMoney, owner.Money);
        Assert.Equal(expectedPlayerMoney, player.Money);
    }

    [Fact]
    public void DoAction_PlayerPaysDoubleRentToOwnerIfOwnerOwnsAllPropertiesInCityAndTileHasNoUpgrades()
    {
        // Arrange
        var game = new Game(2);
        game.InitNewGame(2);
        var owner = game.Players[0];
        var player = game.Players[1];
        var city = new City();
        var rent = new[] { 10, 20, 30, 40, 50, 60 };
        var tile = new TileProperty(game, "Tile", rent, 100, 200, 300, city);
        tile.Owner = owner;
        city.Streets.Add(tile);
        var expectedOwnerMoney = owner.Money + rent[0];
        var expectedPlayerMoney = player.Money - rent[0];

        // Act
        tile.DoAction(player);

        // Assert
        Assert.Equal(expectedOwnerMoney, owner.Money);
        Assert.Equal(expectedPlayerMoney, player.Money);
    }

    [Fact]
    public void DoAction_PlayerPaysRentToOwnerIfOwnerOwnsAllPropertiesInCityAndTileHasUpgrades()
    {
        // Arrange
        var game = new Game(2);
        game.InitNewGame(2);
        var owner = game.Players[0];
        var player = game.Players[1];
        var city = new City();
        var rent = new[] { 10, 20, 30, 40, 50, 60 };
        var tile = new TileProperty(game, "Tile", rent, 100, 200, 300, city);
        tile.Owner = owner;
        city.Streets.Add(tile);
        tile.TotalUpgrades = 1;
        var expectedOwnerMoney = owner.Money + rent[1];
        var expectedPlayerMoney = player.Money - rent[1];

        // Act
        tile.DoAction(player);

        // Assert
        Assert.Equal(expectedOwnerMoney, owner.Money);
        Assert.Equal(expectedPlayerMoney, player.Money);
    }

    [Fact]
    public void DoAction_PlayerDoesNotPayRentIfTileIsOwnedByPlayer()
    {
        // Arrange
        var game = new Game(2);
        game.InitNewGame(2);
        var owner = game.Players[0];
        var player = game.Players[1];
        var city = new City();
        var rent = new[] { 10, 20, 30, 40, 50, 60 };
        var tile = new TileProperty(game, "Tile", rent, 100, 200, 300, city);
        tile.Owner = player;
        var expectedOwnerMoney = owner.Money;
        var expectedPlayerMoney = player.Money;

        // Act
        tile.DoAction(player);

        // Assert
        Assert.Equal(expectedOwnerMoney, owner.Money);
        Assert.Equal(expectedPlayerMoney, player.Money);
    }

    [Fact]
    public void CanBeUpgraded_ReturnsTrueIfTileIsNotOnMortgageAndThereAreStreetsWithHigherOrEqualUpgradesInCity()
    {
        // Arrange
        var game = new Game(2);
        game.InitNewGame(2);
        var city = new City();
        var tile1 = new TileProperty(game, "Tile1", new[] { 10, 20, 30, 40, 50, 60 }, 100, 200, 300, city);
        var tile2 = new TileProperty(game, "Tile2", new[] { 10, 20, 30, 40, 50, 60 }, 100, 200, 300, city);
        var tile3 = new TileProperty(game, "Tile3", new[] { 10, 20, 30, 40, 50, 60 }, 100, 200, 300, city);
        city.Streets.Add(tile1);
        city.Streets.Add(tile2);
        city.Streets.Add(tile3);
        tile1.TotalUpgrades = 1;
        tile2.TotalUpgrades = 2;
        tile3.TotalUpgrades = 3;

        // Act
        var result = tile1.CanBeUpgraded();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void CanBeUpgraded_ReturnsFalseIfTileIsOnMortgage()
    {
        // Arrange
        var game = new Game(2);
        game.InitNewGame(2);
        var city = new City();
        var tile = new TileProperty(game, "Tile", new[] { 10, 20, 30, 40, 50, 60 }, 100, 200, 300, city);
        tile.OnMortage = true;

        // Act
        var result = tile.CanBeUpgraded();

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void CanBeUpgraded_ReturnsFalseIfThereAreNoStreetsWithHigherOrEqualUpgradesInCity()
    {
        // Arrange
        var game = new Game(2);
        game.InitNewGame(2);
        var city = new City();
        var tile1 = new TileProperty(game, "Tile1", new[] { 10, 20, 30, 40, 50, 60 }, 100, 200, 300, city);
        var tile2 = new TileProperty(game, "Tile2", new[] { 10, 20, 30, 40, 50, 60 }, 100, 200, 300, city);
        var tile3 = new TileProperty(game, "Tile3", new[] { 10, 20, 30, 40, 50, 60 }, 100, 200, 300, city);
        city.Streets.Add(tile1);
        city.Streets.Add(tile2);
        city.Streets.Add(tile3);
        tile1.TotalUpgrades = 5;
        tile2.TotalUpgrades = 5;
        tile3.TotalUpgrades = 5;

        // Act
        var result = tile1.CanBeUpgraded();

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void CanBeUpgraded_ReturnsFalseIfTileHasMaximumUpgrades()
    {
        // Arrange
        var game = new Game(2);
        game.InitNewGame(2);
        var city = new City();
        var tile = new TileProperty(game, "Tile", new[] { 10, 20, 30, 40, 50, 60 }, 100, 200, 300, city);
        tile.TotalUpgrades = 5;

        // Act
        var result = tile.CanBeUpgraded();

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void CanBeDowngraded_ReturnsTrueIfTileIsNotOnMortgageAndThereAreStreetsWithLowerOrEqualUpgradesInCity()
    {
        // Arrange
        var game = new Game(2);
        game.InitNewGame(2);
        var city = new City();
        var tile1 = new TileProperty(game, "Tile1", new[] { 10, 20, 30, 40, 50, 60 }, 100, 200, 300, city);
        var tile2 = new TileProperty(game, "Tile2", new[] { 10, 20, 30, 40, 50, 60 }, 100, 200, 300, city);
        var tile3 = new TileProperty(game, "Tile3", new[] { 10, 20, 30, 40, 50, 60 }, 100, 200, 300, city);
        city.Streets.Add(tile1);
        city.Streets.Add(tile2);
        city.Streets.Add(tile3);
        tile1.TotalUpgrades = 3;
        tile2.TotalUpgrades = 2;
        tile3.TotalUpgrades = 1;

        // Act
        var result = tile1.CanBeDowngraded();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void CanBeDowngraded_ReturnsFalseIfTileIsOnMortgage()
    {
        // Arrange
        var game = new Game(2);
        game.InitNewGame(2);
        var city = new City();
        var tile = new TileProperty(game, "Tile", new[] { 10, 20, 30, 40, 50, 60 }, 100, 200, 300, city);
        tile.OnMortage = true;

        // Act
        var result = tile.CanBeDowngraded();

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void CanBeDowngraded_ReturnsTrueIfThereAreNoStreetsWithLowerOrEqualUpgradesInCity()
    {
        // Arrange
        var game = new Game(2);
        game.InitNewGame(2);
        var city = new City();
        var tile1 = new TileProperty(game, "Tile1", new[] { 10, 20, 30, 40, 50, 60 }, 100, 200, 300, city);
        var tile2 = new TileProperty(game, "Tile2", new[] { 10, 20, 30, 40, 50, 60 }, 100, 200, 300, city);
        var tile3 = new TileProperty(game, "Tile3", new[] { 10, 20, 30, 40, 50, 60 }, 100, 200, 300, city);
        city.Streets.Add(tile1);
        city.Streets.Add(tile2);
        city.Streets.Add(tile3);
        tile1.TotalUpgrades = 1;
        tile2.TotalUpgrades = 2;
        tile3.TotalUpgrades = 3;

        // Act
        var result = tile3.CanBeDowngraded();

        // Assert
        Assert.True(result);
    }
}