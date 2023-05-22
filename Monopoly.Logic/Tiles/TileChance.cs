using Monopoly.Logic.Properties;

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
        CurrentGame.AddInfo(string.Format(Language.moves, player.Name, Description));

        if (CurrentGame.ChanceCards.Peek() != null)
        {
            CurrentGame.AddInfo(player.Name + " " + Language.got + " " +
                                CurrentGame.ChanceCards.Peek().Description);
            CurrentGame.ChanceCards.Pop().Use(player);
        }
    }

    public override string GetCardInformation()
    {
        return Language.chance;
    }
}