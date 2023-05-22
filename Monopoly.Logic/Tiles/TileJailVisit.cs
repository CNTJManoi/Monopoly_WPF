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
        CurrentGame.AddInfo(string.Format(Properties.Language.moves, player.Name, Description));
    }

    public override string GetCardInformation()
    {
        return Properties.Language.jailvisit;
    }
}