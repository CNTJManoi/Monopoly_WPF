using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monopoly.Logic;
using Monopoly.Logic.Tiles;
using Xunit;

namespace Monopoly.Tests.Tiles
{
    public class TileJailVisitTests
    {
        [Fact]
        public void GetCardInformation_ReturnsCorrectText()
        {
            // Arrange
            var game = new Game(2);
            game.InitNewGame(2);
            var tile = new TileJailVisit(game, "Just Visiting");

            // Act
            var result = tile.GetCardInformation();

            // Assert
            Assert.Equal(Logic.Properties.Language.jailvisit, result);
        }
        [Fact]
        public void DoAction_WhenPlayerOnTileJailVisit_AddsCorrectMessageToGameInfoQueue()
        {
            // Arrange
            var game = new Game(2);
            game.InitNewGame(2);
            var tile = new TileJailVisit(game, "Just Visiting");

            // Act
            tile.DoAction(game.CurrentPlayer);

            // Assert
            Assert.Contains(string.Format(Logic.Properties.Language.moves, game.CurrentPlayer.Name, tile.Description),
                game.GameInfo);
        }
    }
}
