﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monopoly.Logic;
using Monopoly.Logic.Tiles;
using Xunit;

namespace Monopoly.Tests.Tiles
{
    public class CityTests
    {

        [Fact]
        public void OwnsAllProperties_ShouldReturnFalse_WhenCityHasOnlyOneStreet()
        {
            // Arrange
            var game = new Game(2);
            game.InitNewGame(2);
            TileProperty t1 = (TileProperty)game.Board.GetAt(1);
            TileProperty t2 = (TileProperty)game.Board.GetAt(3);
            Player player = game.Players[0];

            // Act
            var result = t1.City.OwnsAllProperties(player);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void OwnsAllProperties_ShouldReturnFalse_WhenPlayerDoesNotOwnAllStreets()
        {
            // Arrange
            var game = new Game(2);
            game.InitNewGame(2);
            TileProperty t1 = (TileProperty)game.Board.GetAt(1);
            TileProperty t2 = (TileProperty)game.Board.GetAt(3);
            Player player1 = game.Players[0];
            Player player2 = game.Players[1];
            t1.Owner = player1;
            t2.Owner = player2;
            t1.HasOwner = true;
            t2.HasOwner = true;

            // Act
            var result = t1.City.OwnsAllProperties(player1);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void OwnsAllProperties_ShouldReturnTrue_WhenPlayerOwnsAllStreets()
        {
            // Arrange
            var game = new Game(2);
            game.InitNewGame(2);
            TileProperty t1 = (TileProperty)game.Board.GetAt(1);
            TileProperty t2 = (TileProperty)game.Board.GetAt(3);
            Player player1 = game.Players[0];
            t1.Owner = player1;
            t2.Owner = player1;
            t1.HasOwner = true;
            t2.HasOwner = true;

            // Act
            var result = t1.City.OwnsAllProperties(player1);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void OwnsAllProperties_ShouldReturnFalse_WhenCityHasThreeStreetsButPlayerDoesNotOwnAll()
        {
            // Arrange
            var game = new Game(2);
            game.InitNewGame(2);
            TileProperty t1 = (TileProperty)game.Board.GetAt(6);
            TileProperty t2 = (TileProperty)game.Board.GetAt(8);
            TileProperty t3 = (TileProperty)game.Board.GetAt(9);
            Player player1 = game.Players[0];
            Player player2 = game.Players[1];
            t1.Owner = player1;
            t2.Owner = player1;
            t3.Owner = player2;
            t1.HasOwner = true;
            t2.HasOwner = true;
            t3.HasOwner = true;

            // Act
            var result = t1.City.OwnsAllProperties(player1);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void OwnsAllProperties_ShouldReturnTrue_WhenCityHasThreeStreetsAndPlayerOwnsAll()
        {
            // Arrange
            var game = new Game(2);
            game.InitNewGame(2);
            TileProperty t1 = (TileProperty)game.Board.GetAt(6);
            TileProperty t2 = (TileProperty)game.Board.GetAt(8);
            TileProperty t3 = (TileProperty)game.Board.GetAt(9);
            Player player1 = game.Players[0];
            t1.Owner = player1;
            t2.Owner = player1;
            t3.Owner = player1;
            t1.HasOwner = true;
            t2.HasOwner = true;
            t3.HasOwner = true;

            // Act
            var result = t1.City.OwnsAllProperties(player1);

            // Assert
            Assert.True(result);
        }
    }
}