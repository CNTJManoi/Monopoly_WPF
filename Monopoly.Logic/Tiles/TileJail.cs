using System.Text.Json.Serialization;
using Monopoly.Logic.Properties;

namespace Monopoly.Logic.Tiles;

/// <summary>
///     Поле, которое держит игрока в тюрьме.
/// </summary>
public class TileJail : Tile
{
    public Game game { get; }
    public TileJail(Game game, string description)
        : base(game, description)
    {
        this.game = game;
        NextTile = this.game.JailVisit;
    }
    public override void DoAction(Player player)
    {
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