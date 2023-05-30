using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monopoly.Logic.Tiles;

namespace Monopoly.Logic
{
    // Конкретная реализация строителя доски
    public class ConcreteBoardBuilder : IBoardBuilder
    {
        private TileLinkedList board;

        public IBoardBuilder SetBoard(TileLinkedList board)
        {
            this.board = board;
            return this;
        }

        public IBoardBuilder AddTileProperty(TileProperty tileProperty)
        {
            board.Add(tileProperty);
            return this;
        }

        public IBoardBuilder AddTile(Tile tile)
        {
            board.Add(tile);
            return this;
        }
        public TileLinkedList Build()
        {
            return board;
        }
    }
}
