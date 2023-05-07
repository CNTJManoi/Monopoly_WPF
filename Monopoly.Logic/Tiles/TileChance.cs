namespace Monopoly.Logic.Tiles;

/// <summary>
///     Поле шанс карты
/// </summary>
public class TileChance : Tile
{
    public TileChance(Game game, string description)
        : base(game, description)
    {
    }

    public override void DoAction(Player player)
    {
        CurrentGame.GameInfo.Enqueue(string.Format(Properties.Language.moves, player.Name, Description));

        if (CurrentGame.ChanceCards.Peek() != null)
        {
            CurrentGame.GameInfo.Enqueue(player.Name + " " + Properties.Language.got + " " +
                                         CurrentGame.ChanceCards.Peek().Description);
            CurrentGame.ChanceCards.Pop().Use(player);
        }
    }

    public override string GetCardInformation()
    {
        return Properties.Language.chance;
    }
}