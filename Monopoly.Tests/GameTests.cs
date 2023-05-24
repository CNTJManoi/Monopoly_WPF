using System.Collections.ObjectModel;
using System.Linq;
using Monopoly.Logic;
using Monopoly.Logic.Properties;
using Monopoly.Logic.Tiles;
using Xunit;

namespace Monopoly.Tests;

public class GameTests
{
    [Fact]
    public void InitNewGame_InitializesBoard()
    {
        // Arrange
        var game = new Game(2);

        // Act
        game.InitNewGame(2);

        // Assert
        Assert.NotNull(game.Board);
        Assert.IsType<TileLinkedList>(game.Board);
    }

    [Fact]
    public void InitNewGame_LoadsCards()
    {
        // Arrange
        var game = new Game(2);

        // Act
        game.InitNewGame(2);

        // Assert
        Assert.NotEmpty(game.ChanceCards.Cards);
        Assert.NotEmpty(game.CommunityCards.Cards);
    }

    [Fact]
    public void InitNewGame_InitializesPlayers()
    {
        // Arrange
        var game = new Game(2);

        // Act
        game.InitNewGame(2);

        // Assert
        Assert.NotNull(game.Players);
        Assert.IsType<ObservableCollection<Player>>(game.Players);
    }

    [Fact]
    public void InitNewGame_AddsCorrectNumberOfPlayers()
    {
        // Arrange
        var game = new Game(3);

        // Act
        game.InitNewGame(3);

        // Assert
        Assert.Equal(3, game.Players.Count);
    }

    [Fact]
    public void InitNewGame_SetsCurrentPlayer()
    {
        // Arrange
        var game = new Game(2);

        // Act
        game.InitNewGame(2);

        // Assert
        Assert.NotNull(game.CurrentPlayer);
        Assert.IsType<Player>(game.CurrentPlayer);
        Assert.Equal(game.CurrentPlayer, game.Players.First());
    }

    [Fact]
    public void ThrowDiceAndMovePlayer_DiceThrownSuccessfully()
    {
        // Arrange
        var g = new Game(2);
        g.InitNewGame(2);
        var game = new Game(2);
        game.InitNewGame(2);
        var player = game.CurrentPlayer;


        // Act
        game.ThrowDiceAndMovePlayer();

        // Assert
        Assert.True(game.PlayerDice.HasBeenThrown);
        Assert.InRange(game.PlayerDice.FirstDice, 1, 6);
        Assert.InRange(game.PlayerDice.SecondDice, 1, 6);
        Assert.Equal(game.PlayerDice.FirstDice + game.PlayerDice.SecondDice, player.DiceEyes);
    }

    [Fact]
    public void ThrowDiceAndMovePlayer_GameInfoQueueUpdated()
    {
        // Arrange
        var g = new Game(2);
        g.InitNewGame(2);
        var game = new Game(2);
        game.InitNewGame(2);
        var player = game.CurrentPlayer;

        // Act
        game.ThrowDiceAndMovePlayer();

        // Assert
        Assert.Contains(
            string.Format(Language.throwdice, player.Name, game.PlayerDice.FirstDice, game.PlayerDice.SecondDice),
            game.GameInfo);
    }

    [Fact]
    public void ThrowDiceAndMovePlayer_PlayerMovedToCorrectPosition()
    {
        // Arrange
        var game = new Game(2);
        game.InitNewGame(2);
        game.Players[0].MoveTo(3);

        // Act
        game.ThrowDiceAndMovePlayer();

        // Assert
        Assert.Equal(game.Board.GetAt(game.CurrentPlayer.DiceEyes + 3), game.Players[0].CurrentTile);
    }

    [Fact]
    public void ThrowDiceAndMovePlayer_MaximumDiceValue()
    {
        // Arrange
        var game = new Game(2);
        game.InitNewGame(2);

        // Ac
        game.ThrowDiceAndMovePlayer(12);

        // Assert
        Assert.Equal(12, game.CurrentPlayer.DiceEyes);
        Assert.Equal(game.Board.GetAt(game.CurrentPlayer.DiceEyes), game.Players[0].CurrentTile);
    }

    [Fact]
    public void ThrowDiceAndMovePlayer_DiceAlreadyThrown()
    {
        // Arrange
        var game = new Game(2);
        game.InitNewGame(2);

        // Act
        game.PlayerDice.HasBeenThrown = true;
        game.ThrowDiceAndMovePlayer();

        // Assert
        Assert.Equal(game.Start, game.CurrentPlayer.CurrentTile);
        Assert.Equal(null,
            game.GameInfo.FirstOrDefault(x => x == string.Format(Language.throwdice, game.CurrentPlayer.Name,
                game.PlayerDice.FirstDice, game.PlayerDice.SecondDice)));
    }

    [Fact]
    public void EndTurn_NextPlayerTurnIfNotDouble()
    {
        // Arrange
        var g = new Game(2);
        g.InitNewGame(2);
        var game = new Game(2);
        game.InitNewGame(2);
        do
        {
            game.ThrowDiceAndMovePlayer();
        } while (game.PlayerDice.FirstDice == game.PlayerDice.SecondDice);
        // Act
        game.EndTurn();

        // Assert
        Assert.Equal(1, game.PlayerTurn);
        Assert.False(game.PlayerDice.HasBeenThrown);
    }

    [Fact]
    public void EndTurn_NoNextPlayerTurnIfDouble()
    {
        // Arrange
        var game = new Game(2);
        game.InitNewGame(2);

        // Act
        game.EndTurn();

        // Assert
        Assert.Equal(0, game.PlayerTurn);
        Assert.False(game.PlayerDice.HasBeenThrown);
    }
}