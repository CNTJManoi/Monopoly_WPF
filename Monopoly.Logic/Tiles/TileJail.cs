using Monopoly.Logic.Properties;

namespace Monopoly.Logic.Tiles;

/// <summary>
///     Поле, которое держит игрока в тюрьме.
/// </summary>
public class TileJail : Tile
{
    public TileJail(Game game, string description)
        : base(game, description)
    {
        NextTile = game.JailVisit;
    }

    public override void DoAction(Player player)
    {
        // todo:  возможно стоит рассмотреть перенос логики в класс Player.
        // тогда не придётся следить за переменной JailCounter
        // либо наоборот вынести проверку того, сколько ходов какой игрок находится в тюрьме в данный класс
        if (player.JailCounter != 0)
            CurrentGame.AddInfo(string.Format(Language.jailperson, player.Name));
        else
            CurrentGame.AddInfo(string.Format(Language.moves, player.Name, Description));
        player.CheckOutJail();
    }

    public override string GetCardInformation()
    {
        return Language.jail;
    }
}