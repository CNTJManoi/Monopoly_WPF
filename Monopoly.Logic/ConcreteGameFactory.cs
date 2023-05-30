using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Logic
{
    // Конкретная реализация фабрики игры
    public class ConcreteGameFactory : IGameFactory
    {
        public IBoardBuilder CreateBoardBuilder()
        {
            return new ConcreteBoardBuilder();
        }

        public ICityFactory CreateCityFactory()
        {
            return new ConcreteCityFactory();
        }

        public ITilePropertyFactory CreateTilePropertyFactory()
        {
            return new ConcreteTilePropertyFactory();
        }
    }
}
