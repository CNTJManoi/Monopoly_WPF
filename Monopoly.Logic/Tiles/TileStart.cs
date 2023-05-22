using Monopoly.Logic.Properties;

namespace Monopoly.Logic.Tiles;

/// <summary>
///     Поле старта
/// </summary>
public class TileStart : Tile
{
    public TileStart(Game game, string description)
        : base(game, description)
    {
    }

    public override void DoAction(Player player)
    {
        CurrentGame.AddInfo(string.Format(Language.moves, player.Name, Description));
        CurrentGame.AddInfo(string.Format(Language.startmoney, player.Name));
        player.Money += 400;
    }

    public override string GetCardInformation()
    {
        return string.Format(Language.start, Description, Environment.NewLine);
    }
}