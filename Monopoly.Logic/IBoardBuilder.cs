using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monopoly.Logic.Tiles;

namespace Monopoly.Logic
{
    // Интерфейс строителя доски
    public interface IBoardBuilder
    {
        IBoardBuilder SetBoard(TileLinkedList board);
        IBoardBuilder AddTileProperty(TileProperty tileProperty);
        IBoardBuilder AddTile(Tile tile);
        TileLinkedList Build();
    }
}
