﻿namespace Monopoly.Logic.Tiles;

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
        CurrentGame.GameInfo.Enqueue(string.Format(Properties.Language.moves, player.Name, Description));
        if (Owner != null && !Owner.Equals(player))
        {
            var moneyToPay = Owner.TotalCompanies >= 2 ? player.DiceEyes * 10 : player.DiceEyes * 4;
            player.PayMoneyTo(Owner, moneyToPay);
            CurrentGame.GameInfo.Enqueue(string.Format(Properties.Language.companypay, player.Name, moneyToPay,
                Owner.Name));
        }
    }

    public override string GetCardInformation()
    {
        var companyOwner = Owner == null ? Properties.Language.propertyowner : Owner.Name;
        return string.Format(Properties.Language.company, Description, Environment.NewLine, companyOwner, Price,
            Mortage);
    }
}