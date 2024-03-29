﻿using System;
using Monopoly.Logic;
using Monopoly.Logic.Properties;
using Monopoly.Logic.Tiles;
using Xunit;

namespace Monopoly.Tests.Tiles;

public class TileRailRoadTests
{
    [Fact]
    public void DoAction_WhenOwnerIsNull_ShouldNotChargePlayer()
    {
        // Arrange
        var game = new Game(2);
        game.InitNewGame(2);
        ;
        var player = game.CurrentPlayer;
        var tile = new TileRailRoad(game, "Reading Railroad", new[] { 25, 50, 100, 200 }, 100, 200);
        // Act
        tile.DoAction(player);

        // Assert
        Assert.Equal(1500, player.Money);
    }

    [Fact]
    public void DoAction_WhenPlayerIsOwner_ShouldNotChargePlayer()
    {
        // Arrange
        var game = new Game(2);
        game.InitNewGame(2);
        ;
        var player = game.CurrentPlayer;
        var tile = new TileRailRoad(game, "Reading Railroad", new[] { 25, 50, 100, 200 }, 100, 200);
        tile.Owner = player;

        // Act
        tile.DoAction(player);

        // Assert
        Assert.Equal(1500, player.Money);
    }

    [Fact]
    public void GetCardInformation_ShouldReturnCorrectString()
    {
        // Arrange
        var game = new Game(2);
        game.InitNewGame(2);
        var tile = new TileRailRoad(game, "Reading Railroad", new[] { 25, 50, 100, 200 }, 100, 200);

        // Act
        var result = tile.GetCardInformation();

        // Assert
        Assert.Equal(
            string.Format(Language.railroad, tile.Description, Environment.NewLine, Language.propertyowner, tile.Price,
                tile.Rent[0], tile.Rent[0] * 2, tile.Rent[0] * 4, tile.Rent[0] * 8, tile.Mortage),
            result);
    }
}