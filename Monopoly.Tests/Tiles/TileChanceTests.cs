using System.Linq;
using Monopoly.Logic;
using Monopoly.Logic.Properties;
using Xunit;

namespace Monopoly.Tests.Tiles;

public class TileChanceTests
{
    [Fact]
    public void Should_AddPlayerAndChanceCardDescriptionsToGameInfoQueue()
    {
        // Arrange
        var game = new Game(2);
        game.InitNewGame(2);
        var player = game.Players[0];
        Configuration _configFile = new Configuration(game);
        var cards = _configFile.GetAllCards(@"Config\CardDescriptions").ToList();
        game.GetCards(cards);
        player.MoveTo(7);
        var countInfo = game.GameInfo.Count;

        // Assert
        Assert.True(game.GameInfo
            .Count(x => x.Contains(player.Name + " " + Language.got)) == 1);
        Assert.True(game.GameInfo.Count(x => x.Contains(string.Format(Language.moves,
            player.Name, player.CurrentTile.Description))) == 1);
    }

    [Fact]
    public void Should_UseChanceCardAndRemoveFromStackIfChanceCardIsNotNull()
    {
        // Arrange
        var game = new Game(2);
        game.InitNewGame(2);
        Configuration _configFile = new Configuration(game);
        var cards = _configFile.GetAllCards(@"Config\CardDescriptions").ToList();
        game.GetCards(cards);
        var player = game.Players[0];

        var countChanceCards = game.ChanceCards.Cards.Length;
        player.MoveTo(7);
        var countInfo = game.GameInfo.Count;

        // Assert
        Assert.True(game.GameInfo
            .Count(x => x.Contains(player.Name + " " + Language.got)) == 1);
        Assert.True(game.GameInfo.Count(x => x.Contains(string.Format(Language.moves,
            player.Name, player.CurrentTile.Description))) == 1);
    }
}