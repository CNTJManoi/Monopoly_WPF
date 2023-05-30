using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monopoly.Logic.Tiles;

namespace Monopoly.Logic
{
    public class ConcreteTilePropertyFactory : ITilePropertyFactory
    {
        
        public TileProperty CreateTileProperty(Game CurrentGame, string name, int[] rents, int cost, int mortgageValue, int houseCost, City city)
        {
            return new TileProperty(CurrentGame, name, rents, cost, mortgageValue, houseCost, city);
        }
    }
}
