﻿using System;
using Monopoly.Logic;
using Monopoly.Logic.Tiles;
using Xunit;

namespace Monopoly.Tests;

public class PlayerTests
{
    [Fact]
    public void PayMoneyTo_DeductsMoneyFromThisPlayer()
    {
        // Arrange
        var g = new Game(2);
        g.InitNewGame(2);
        var ts = new TileStart(g, "Поле старта");
        var player1 = new Player(g, "Player  1", 100, ts);
        var player2 = new Player(g, "Player  2", 100, ts);

        // Act
        player1.PayMoneyTo(player2, 50);

        // Assert
        Assert.Equal(50, player1.Money);
    }

    [Fact]
    public void PayMoneyTo_AddsMoneyToOtherPlayer()
    {
        // Arrange
        var g = new Game(2);
        g.InitNewGame(2);
        var ts = new TileStart(g, "Поле старта");
        var player1 = new Player(g, "Player  1", 100, ts);
        var player2 = new Player(g, "Player  2", 100, ts);

        // Act
        player1.PayMoneyTo(player2, 50);

        // Assert
        Assert.Equal(150, player2.Money);
    }

    [Fact]
    public void MoveTo_MoveForward_PositivePositions()
    {
        // Arrange
        var g = new Game(2);
        g.InitNewGame(2);
        var player = new Player(g, "Player  1", 50, g.Board.GetAt(0));
        player.CurrentTile = g.Board.GetAt(0);
        player.IsInJail = false;

        // Act
        player.MoveTo(3);

        // Assert
        Assert.Equal(g.Board.GetAt(3), player.CurrentTile);
    }

    [Fact]
    public void MoveTo_MoveBackward_NegativePositions()
    {
        // Arrange
        var g = new Game(2);
        g.InitNewGame(2);
        var player = new Player(g, "Player  1", 50, g.Board.GetAt(3));

        // Act
        player.MoveTo(-3);

        // Assert
        Assert.Equal(g.Start, player.CurrentTile);
    }

    [Fact]
    public void MoveTo_GoThroughStart_Add200Money()
    {
        // Arrange
        var g = new Game(2);
        g.InitNewGame(2);
        var player = new Player(g, "Player  1", 1500, g.Board.GetAt(35));

        // Act
        player.MoveTo(15);

        // Assert
        Assert.Equal(1700, player.Money);
    }

    [Fact]
    public void MoveTo_GoToJail_NotMove()
    {
        // Arrange
        var g = new Game(2);
        g.InitNewGame(2);
        var player = new Player(g, "Player  1", 50, g.Board.GetAt(29));

        // Act
        player.MoveTo(1);

        // Assert
        Assert.True(player.IsInJail);
        Assert.Equal(g.Jail, player.CurrentTile);
    }

    [Fact]
    public void BuyBuilding_EnoughMoney_BuyStreet()
    {
        // Arrange
        var g = new Game(2);
        g.InitNewGame(2);
        var player = new Player(g, "Player  1", 860, g.Board.GetAt(1));
        var street = (TileBuyable)g.Board.GetAt(1);
        street.HasOwner = false;

        // Act
        player.BuyBuilding();

        // Assert
        Assert.True(street.HasOwner);
        Assert.Equal(player, street.Owner);
        Assert.Equal(800, player.Money);
        Assert.Contains($"{player.Name} купил {street.Description}", g.GameInfo);
        Assert.Contains(street, player.Streets);
    }

    [Fact]
    public void BuyBuilding_NotEnoughMoney_DoNotBuyStreet()
    {
        // Arrange
        var g = new Game(2);
        g.InitNewGame(2);
        var player = new Player(g, "Player  1", 50, g.Board.GetAt(1));
        var street = (TileBuyable)g.Board.GetAt(1);
        street.HasOwner = false;

        // Act
        player.BuyBuilding();

        // Assert
        Assert.False(street.HasOwner);
        Assert.Null(street.Owner);
        Assert.Equal(50, player.Money);
        Assert.DoesNotContain($"{player.Name} купил {street.Description}", g.GameInfo);
        Assert.DoesNotContain(street, player.Streets);
    }

    [Fact]
    public void BuyBuilding_BuyCompany_TotalCompanies()
    {
        // Arrange
        var g = new Game(2);
        g.InitNewGame(2);
        var player = new Player(g, "Player  1", 200, g.Board.GetAt(12));
        var company = (TileCompany)g.Board.GetAt(12);
        company.HasOwner = false;

        // Act
        player.BuyBuilding();

        // Assert
        Assert.Equal(1, player.TotalCompanies);
        Assert.Equal(50, player.Money);
    }

    [Fact]
    public void BuyBuilding_BuyRailRoad_TotalRailRoads()
    {
        // Arrange
        var g = new Game(2);
        g.InitNewGame(2);
        g.Configuration.LoadDefaultBoard();
        var player = new Player(g, "Player  1", 1500, g.Board.GetAt(5));
        var railRoad = (TileRailRoad)g.Board.GetAt(5);
        railRoad.HasOwner = false;

        // Act
        player.BuyBuilding();

        // Assert
        Assert.Equal(1, player.TotalRailRoads);
        Assert.Equal(1300, player.Money);
    }

    [Fact]
    public void BuyBuilding_AlreadyOwned_DoNotBuyStreet()
    {
        // Arrange
        var g = new Game(2);
        g.InitNewGame(2);
        g.Configuration.LoadDefaultBoard();
        var player1 = new Player(g, "Player  1", 1500, g.Board.GetAt(1));
        var player2 = new Player(g, "Player  2", 1500, g.Board.GetAt(1));
        var street = (TileBuyable)g.Board.GetAt(1);
        street.HasOwner = true;
        street.Owner = player1;

        // Act
        player2.BuyBuilding();

        // Assert
        Assert.True(street.HasOwner);
        Assert.Equal(player1, street.Owner);
        Assert.Equal(1500, player2.Money);
        Assert.DoesNotContain($"{player2.Name} купил {street.Description}", g.GameInfo);
        Assert.DoesNotContain(street, player2.Streets);
    }
}