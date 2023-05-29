using System;
using System.Linq;
using Monopoly.Database;
using Monopoly.Database.Models;
using Monopoly.Logic;
using Monopoly.Logic.Tiles;

namespace Monopoly.ViewModel;

public class EndScreenViewModel : BasicViewModel
{
    public EndScreenViewModel(Game game, DatabaseController dataBase)
    {
        var result = game.CalculateScores();
        Players = result.Players;
        PlayerMoneys = result.PlayerMoneys;
        Winner = result.Winner;
        WinningPlayer = Players[Winner];
        
        dataBase.AddStatictics(new Statictics(new Guid(), game.BeginGame - DateTime.Now, PlayerMoneys.Sum(), game.CountMoves));
    }
    public string[] Players { get; }
    public int[] PlayerMoneys { get; }
    public int Winner { get; }
    public string WinningPlayer { get; }

    
}