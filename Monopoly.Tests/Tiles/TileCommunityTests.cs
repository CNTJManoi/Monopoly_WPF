using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monopoly.Logic;
using Monopoly.Logic.Cards;
using Monopoly.Logic.Tiles;
using Xunit;

namespace Monopoly.Tests.Tiles
{
    public class TileCommunityTests
    {
        [Fact]
        public void Should_AddPlayerAndCommunityCardDescriptionsToGameInfoQueue()
        {
            // Arrange
            var game = new Game(2);
            game.InitNewGame(2);
            Player player = game.Players[0];
            player.MoveTo(2);

            // Assert
            Assert.True(game.GameInfo.Where(x => x.Contains(player.Name + " " + Monopoly.Logic.Properties.Language.got)).Count() > 0);
            Assert.True(game.GameInfo.Where(x => x.Contains(string.Format(Monopoly.Logic.Properties.Language.moves, player.Name, player.CurrentTile.Description))).Count() > 0);
        }

        [Fact]
        public void Should_NotAddAnythingToGameInfoQueueIfCommunityCardIsNull()
        {
            // Arrange
            var game = new Game(2);
            game.InitNewGame(2);
            Player player = game.Players[0];
            player.MoveTo(2);

            // Assert
            Assert.True(game.GameInfo.Where(x => x.Contains(player.Name + " " + Monopoly.Logic.Properties.Language.got)).Count() > 0);
            Assert.True(game.GameInfo.Where(x => x.Contains(string.Format(Monopoly.Logic.Properties.Language.moves, player.Name, player.CurrentTile.Description))).Count() > 0);
        }
    }
}
