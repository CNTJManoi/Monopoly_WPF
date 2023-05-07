namespace Monopoly.Logic.Tiles;

/// <summary>
///     Лист который содержит все поля
/// </summary>
public class TileLinkedList
{
    public TileLinkedList()
    {
        Size = 0;
    }

    public Tile Head { get; set; }
    public int Size { get; set; }

    public void Add(Tile data)
    {
        Size++;
        if (Head == null)
        {
            Head = data;
        }
        else
        {
            var temp = Head;
            Head = data;
            if (Head.NextTile == null) Head.NextTile = temp;
            if (temp.PreviousTile == null) temp.PreviousTile = Head;
        }
    }

    public Tile GetAt(int x)
    {
        var current = Head;
        for (var i = 0; i < x; i++) current = current.NextTile;
        return current;
    }
}