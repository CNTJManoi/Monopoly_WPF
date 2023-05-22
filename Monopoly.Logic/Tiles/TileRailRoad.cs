using Monopoly.Logic.Properties;

namespace Monopoly.Logic.Tiles;

public class TileRailRoad : TileBuyable
{
    public TileRailRoad(Game game, string description, int[] rent, int mortage, int price)
        : base(game, description, rent, mortage, price)
    {
    }

    public override void DoAction(Player player)
    {
        CurrentGame.AddInfo(string.Format(Language.moves, player.Name, Description));

        if (Owner != null && !player.Equals(Owner))
        {
            var toPay = Rent[Owner.TotalRailRoads - 1];
            player.PayMoneyTo(Owner, toPay);
            CurrentGame.AddInfo(string.Format(Language.companypay, player.Name, toPay, Owner.Name));
        }
    }

    public override string GetCardInformation()
    {
        var railRoadOwner = Owner == null ? Language.propertyowner : Owner.Name;
        return string.Format(Language.railroad, Description, Environment.NewLine, railRoadOwner, Price,
            Rent[0], Rent[0] * 2, Rent[0] * 4, Rent[0] * 8, Mortage);
    }
}