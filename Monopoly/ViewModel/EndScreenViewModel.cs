using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Monopoly.Database;
using Monopoly.Database.Models;
using Monopoly.Logic;
using Monopoly.Logic.Tiles;
using Player = Monopoly.Database.Models.Player;

namespace Monopoly.ViewModel;

public class EndScreenViewModel : BasicViewModel
{
    private DatabaseController dataBase;
    private Game game;
    public EndScreenViewModel(Game game, DatabaseController dataBase)
    {
        var result = game.CalculateScores();
        Players = result.Players;
        PlayerMoneys = result.PlayerMoneys;
        Winner = result.Winner;
        WinningPlayer = Players[Winner];
        this.dataBase = dataBase;
        this.game = game;
        SendDatabaseInfo();
    }

    private async void SendDatabaseInfo()
    {
        List<Database.Models.Player> Players = new List<Player>();
        int scoreOne = game.Players[0].Money;
        int scoreTwo = game.Players[1].Money;
        int scoreThree = 0;
        int scoreFour = 0;
        if (game.Players.Count > 2)
            scoreThree = game.Players[2].Money;
        if (game.Players.Count > 3)
            scoreFour = game.Players[3].Money;
        for (int i = 0; i < game.Players.Count; i++)
        {
            Players.Add(await dataBase.ReturnPlayer(game.Players[i].Name));
        }
        await dataBase.AddStatictics(new Statictics(
            Players,
            DateTime.Now - game.BeginGame,
            PlayerMoneys.Sum(),
            game.CountMoves,
            scoreOne,
            scoreTwo,
            scoreThree,
            scoreFour));
    }
    public string[] Players { get; }
    public int[] PlayerMoneys { get; }
    public int Winner { get; }
    public string WinningPlayer { get; }


}