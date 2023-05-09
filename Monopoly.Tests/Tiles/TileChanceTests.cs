using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monopoly.Logic;
using Monopoly.Logic.Cards;
using Monopoly.Logic.Properties;
using Monopoly.Logic.Tiles;
using Xunit;

namespace Monopoly.Tests.Tiles
{
    public class TileChanceTests
    {
        [Fact]
        public void Should_AddPlayerAndChanceCardDescriptionsToGameInfoQueue()
        {
            // Arrange
            var game = new Game(2);
            game.InitNewGame(2);
            Player player = game.Players[0];
            player.MoveTo(7);
            var countInfo = game.GameInfo.Count;

            // Assert
            Assert.True(game.GameInfo
                .Count(x => x.Contains(player.Name + " " + Language.got)) == 1);
            Assert.True(game.GameInfo.Count(x => x.Contains(string.Format(Monopoly.Logic.Properties.Language.moves,
                player.Name, player.CurrentTile.Description))) == 1);
        }

        [Fact]
        public void Should_UseChanceCardAndRemoveFromStackIfChanceCardIsNotNull()
        {
            // Arrange
            var game = new Game(2);
            game.InitNewGame(2);
            Player player = game.Players[0];
            var countChanceCards = game.ChanceCards.Cards.Length;
            player.MoveTo(7);
            var countInfo = game.GameInfo.Count;

            // Assert
            Assert.True(game.GameInfo
                .Count(x => x.Contains(player.Name + " " + Language.got)) == 1);
            Assert.True(game.GameInfo.Count(x => x.Contains(string.Format(Monopoly.Logic.Properties.Language.moves,
                player.Name, player.CurrentTile.Description))) == 1);
        }
    }
}
