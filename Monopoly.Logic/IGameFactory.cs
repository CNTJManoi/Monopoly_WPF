using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Logic
{
    public interface IGameFactory
    {
        IBoardBuilder CreateBoardBuilder();
        ICityFactory CreateCityFactory();
        ITilePropertyFactory CreateTilePropertyFactory();
    }   
}
