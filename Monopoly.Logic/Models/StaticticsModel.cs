using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Logic.Models
{
    public class StaticticsModel
    {
        public StaticticsModel(Game game)
        {
            Players = new string[game.Players.Count];
            for (var x = 0; x < game.Players.Count; x++)
                Players[x] = game.Players[x].Name;
            PlayerMoneys = new int[game.Players.Count];
        }

        public string[] Players { get; set; }
        public int[] PlayerMoneys { get; set; }
        public int Winner { get; set; }
    }
}
