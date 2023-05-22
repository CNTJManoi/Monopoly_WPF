using Monopoly.Logic.Properties;

namespace Monopoly.Logic.Tiles;

/// <summary>
///     Поле, которую игрок может купить
/// </summary>
public class TileCompany : TileBuyable
{
    public TileCompany(Game game, string description, int mortage, int price)
        : base(game, description, new[] { 0 }, mortage, price)
    {
    }

    public override void DoAction(Player player)
    {
        CurrentGame.AddInfo(string.Format(Language.moves, player.Name, Description));
        if (Owner != null && !Owner.Equals(player))
        {
            var moneyToPay = Owner.TotalCompanies >= 2 ? player.DiceEyes * 10 : player.DiceEyes * 4;
            player.PayMoneyTo(Owner, moneyToPay);
            CurrentGame.AddInfo(string.Format(Language.companypay, player.Name, moneyToPay,
                Owner.Name));
        }
    }

    public override string GetCardInformation()
    {
        var companyOwner = Owner == null ? Language.propertyowner : Owner.Name;
        return string.Format(Language.company, Description, Environment.NewLine, companyOwner, Price,
            Mortage);
    }
}