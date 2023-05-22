using System.Linq;
using Monopoly.Logic;
using Monopoly.Logic.Properties;
using Xunit;

namespace Monopoly.Tests.Tiles;

public class TileCommunityTests
{
    [Fact]
    public void Should_AddPlayerAndCommunityCardDescriptionsToGameInfoQueue()
    {
        // Arrange
        var game = new Game(2);
        game.InitNewGame(2);
        var player = game.Players[0];
        player.MoveTo(2);

        // Assert
        Assert.True(game.GameInfo.Where(x => x.Contains(player.Name + " " + Language.got)).Count() > 0);
        Assert.True(game.GameInfo
            .Where(x => x.Contains(string.Format(Language.moves, player.Name, player.CurrentTile.Description)))
            .Count() > 0);
    }

    [Fact]
    public void Should_NotAddAnythingToGameInfoQueueIfCommunityCardIsNull()
    {
        // Arrange
        var game = new Game(2);
        game.InitNewGame(2);
        var player = game.Players[0];
        player.MoveTo(2);

        // Assert
        Assert.True(game.GameInfo.Where(x => x.Contains(player.Name + " " + Language.got)).Count() > 0);
        Assert.True(game.GameInfo
            .Where(x => x.Contains(string.Format(Language.moves, player.Name, player.CurrentTile.Description)))
            .Count() > 0);
    }
}