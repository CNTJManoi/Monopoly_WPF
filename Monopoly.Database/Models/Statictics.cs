using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Database.Models
{
    public class Statictics
    {
        public Statictics(Guid id, TimeSpan timeOfGame, int sumPlayersScore, int countMoves)
        {
            Id = id;
            TimeOfGame = timeOfGame;
            SumPlayersScore = sumPlayersScore;
            CountMoves = countMoves;
        }

        public Guid Id { get; }
        public TimeSpan TimeOfGame { get; }
        public int SumPlayersScore { get; }
        public int CountMoves { get; }
    }
}
