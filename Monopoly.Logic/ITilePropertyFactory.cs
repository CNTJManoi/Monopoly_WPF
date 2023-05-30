using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monopoly.Logic.Tiles;

namespace Monopoly.Logic
{
    public interface ITilePropertyFactory
    {
        TileProperty CreateTileProperty(Game CurrentGame, string name, int[] rents, int cost, int mortgageValue, int houseCost, City city);
    }
}
