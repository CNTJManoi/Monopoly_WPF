using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monopoly.Logic.Tiles;

namespace Monopoly.Logic
{
    public class ConcreteCityFactory : ICityFactory
    {
        public City CreateCity()
        {
            return new City();
        }
    }
}
