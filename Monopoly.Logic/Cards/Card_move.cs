using Monopoly.Logic.Tiles;

namespace Monopoly.Logic.Cards;

public class CardMove : Card
{
    public CardMove(string description, int movePlaces)
        : base(description)
    {
        MovePlace = null;
        MovePlaces = movePlaces;
    }

    public CardMove(string description, Tile moveTo)
        : base(description)
    {
        MovePlaces = -1;
        MovePlace = moveTo;
    }

    public Tile MovePlace { get; set; }
    public int MovePlaces { get; set; }

    public override void Use(Player player)
    {
        if (MovePlace != null)
            //переместиться непосредственно на указанное поле
            player.MoveTo(MovePlace);
        else if (MovePlaces != -1)
            //перемещаться вперед или назад на доске
            player.MoveTo(MovePlaces);
        else
            //не может двигаться из-за недостоверной информации, указанной в конструкторе
            throw new InvalidOperationException();
    }
}