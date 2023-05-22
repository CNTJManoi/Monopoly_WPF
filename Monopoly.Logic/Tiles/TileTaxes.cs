using Monopoly.Logic.Properties;

namespace Monopoly.Logic.Tiles;

/// <summary>
///     Поле уплаты налогов
/// </summary>
public class TileTaxes : Tile
{
    public TileTaxes(Game game, string description)
        : base(game, description)
    {
    }

    public override void DoAction(Player player)
    {
        CurrentGame.AddInfo(string.Format(Language.moves, player.Name, Description));

        if (player.Money > 1000)
        {
            CurrentGame.AddInfo(string.Format(Language.taxespay200, player.Name));
            CurrentGame.FreeParkingTreasure += 200;
            player.Money -= 200;
        }
        else
        {
            CurrentGame.AddInfo(string.Format(Language.taxespay10, player.Name, player.Money / 10));
            CurrentGame.FreeParkingTreasure += player.Money / 10;
            player.Money -= player.Money / 10;
        }
    }

    public override string GetCardInformation()
    {
        return string.Format(Language.taxes, Environment.NewLine);
    }
}