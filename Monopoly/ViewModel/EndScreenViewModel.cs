using Monopoly.Logic;
using Monopoly.Logic.Tiles;

namespace Monopoly.ViewModel;

public class EndScreenViewModel : BasicViewModel
{
    public EndScreenViewModel(Game game)
    {
        var result = game.CalculateScores();
        Players = result.Players;
        PlayerMoneys = result.PlayerMoneys;
        Winner = result.Winner;
        WinningPlayer = Players[Winner];
    }
    public string[] Players { get; }
    public int[] PlayerMoneys { get; }
    public int Winner { get; }
    public string WinningPlayer { get; }

    
}