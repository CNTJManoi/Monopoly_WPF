using Monopoly.Logic.Properties;

namespace Monopoly.Logic.Tiles;

/// <summary>
///     Поле для посещения тюрьмы
/// </summary>
public class TileJailVisit : Tile
{
    public TileJailVisit(Game game, string description)
        : base(game, description)
    {
    }

    public override void DoAction(Player player)
    {
        CurrentGame.AddInfo(string.Format(Language.moves, player.Name, Description));
    }

    public override string GetCardInformation()
    {
        return Language.jailvisit;
    }
}