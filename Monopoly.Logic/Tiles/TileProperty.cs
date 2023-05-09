namespace Monopoly.Logic.Tiles;

/// <summary>
///     Обычное поле которое можно купить
/// </summary>
public class TileProperty : TileBuyable
{
    public TileProperty(Game game, string description, int[] rent, int mortage, int price, int upgradeCost, City city)
        : base(game, description, rent, mortage, price)
    {
        City = city;
        UpgradeCost = upgradeCost;
    }

    public City City { get; set; }

    public override void DoAction(Player player)
    {
        CurrentGame.GameInfo.Enqueue(string.Format(Properties.Language.moves, player.Name, Description));

        if (Owner != null && !player.Equals(Owner))
        {
            if (City.OwnsAllProperties(Owner))
            {
                if (TotalUpgrades == 0 && !OnMortage)
                {
                    player.PayMoneyTo(Owner, Rent[TotalUpgrades] * 2);
                    CurrentGame.GameInfo.Enqueue(string.Format(Properties.Language.companypay, player.Name,
                        Rent[TotalUpgrades] * 2, Owner.Name));
                }
                else
                {
                    player.PayMoneyTo(Owner, Rent[TotalUpgrades]);
                    CurrentGame.GameInfo.Enqueue(string.Format(Properties.Language.companypay, player.Name,
                        Rent[TotalUpgrades], Owner.Name));
                }
            }
            else
            {
                player.PayMoneyTo(Owner, Rent[TotalUpgrades]);
                CurrentGame.GameInfo.Enqueue(string.Format(Properties.Language.companypay, player.Name, Rent[TotalUpgrades],
                    Owner.Name));
            }
        }
    }

    public bool CanBeUpgraded()
    {
        if (OnMortage) return false;
        var first = false;
        var second = false;
        var third = false;

        for (var x = 0; x < City.Streets.Count; x++)
            switch (x)
            {
                case 0:
                    if (!Equals(City.Streets[x]))
                        if (City.Streets[x].TotalUpgrades >= TotalUpgrades)
                            first = true;
                    break;
                case 1:
                    if (!Equals(City.Streets[x]))
                        if (City.Streets[x].TotalUpgrades >= TotalUpgrades)
                            second = true;
                    break;
                case 2:
                    if (!Equals(City.Streets[x]))
                        if (City.Streets[x].TotalUpgrades > TotalUpgrades)
                            third = true;
                    break;
            }

        return (first || second || third) && TotalUpgrades < 5;
    }

    public bool CanBeDowngraded()
    {
        var first = false;
        var second = false;
        var third = false;

        for (var x = 0; x < City.Streets.Count; x++)
            switch (x)
            {
                case 0:
                    if (City.Streets[x].TotalUpgrades <= TotalUpgrades) first = true;
                    break;
                case 1:
                    if (City.Streets[x].TotalUpgrades <= TotalUpgrades) second = true;
                    break;
                case 2:
                    if (City.Streets[x].TotalUpgrades <= TotalUpgrades) third = true;
                    break;
            }

        return (first || second || third) && !OnMortage;
    }

    public override string GetCardInformation()
    {
        string propertyOwner;
        var houses = string.Empty;
        if (Owner == null)
        {
            propertyOwner = Properties.Language.propertyowner;
        }
        else
        {
            propertyOwner = Owner.Name;
            houses = string.Format(Properties.Language.propertyhouses, TotalUpgrades, Environment.NewLine);
        }

        return string.Format(Properties.Language.property, Description, propertyOwner, houses, Price, Rent[0],
            UpgradeCost, Rent[1], Rent[2], Rent[3], Rent[4], Rent[5], Mortage, Environment.NewLine);
    }
}