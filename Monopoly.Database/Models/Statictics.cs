using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Database.Models
{
    public class Statictics
    {
        private Statictics(TimeSpan timeOfGame, int sumPlayersScore, 
            int countMoves, int scoreOnePlayer, 
            int scoreTwoPlayer, int scoreThreePlayer, int scoreFourPlayer)
        {
            TimeOfGame = timeOfGame;
            SumPlayersScore = sumPlayersScore;
            CountMoves = countMoves;
            
            ScoreOnePlayer = scoreOnePlayer;
            ScoreTwoPlayer = scoreTwoPlayer;
            ScoreThreePlayer = scoreThreePlayer;
            ScoreFourPlayer = scoreFourPlayer;
        }

        public Statictics(List<Models.Player> players, TimeSpan timeOfGame, int sumPlayersScore,
            int countMoves, int scoreOnePlayer,
            int scoreTwoPlayer, int scoreThreePlayer, int scoreFourPlayer) : this(timeOfGame, sumPlayersScore, countMoves, scoreOnePlayer, 
            scoreTwoPlayer, scoreThreePlayer, scoreFourPlayer)
        {
            Players = players;
        }
        public int Id { get; set; }
        public TimeSpan TimeOfGame { get; }
        public int SumPlayersScore { get; }
        public int CountMoves { get; }
        public List<Models.Player> Players { get; }
        public int ScoreOnePlayer { get; }
        public int ScoreTwoPlayer { get; }
        public int ScoreThreePlayer { get; }
        public int ScoreFourPlayer { get; }
    }
}
