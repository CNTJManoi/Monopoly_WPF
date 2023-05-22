using Monopoly.Logic.Properties;

namespace Monopoly.Logic.Tiles;

/// <summary>
///     Поле для использования коммунальной карты.
/// </summary>
public class TileCommunity : Tile
{
    public TileCommunity(Game game, string description)
        : base(game, description)
    {
    }

    public override void DoAction(Player player)
    {
        CurrentGame.AddInfo(string.Format(Language.moves, player.Name, Description));

        if (CurrentGame.CommunityCards.Peek() != null)
        {
            CurrentGame.AddInfo(player.Name + " " + Language.got + " " +
                                CurrentGame.CommunityCards.Peek().Description);
            CurrentGame.CommunityCards.Pop().Use(player);
        }
    }

    public override string GetCardInformation()
    {
        return Language.community;
    }
}