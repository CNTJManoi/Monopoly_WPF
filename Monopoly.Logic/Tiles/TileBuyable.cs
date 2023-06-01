using Monopoly.Logic.Properties;

namespace Monopoly.Logic.Tiles;

/// <summary>
///     Поле которое может купить игрок
/// </summary>
public abstract class TileBuyable : Tile
{
    protected TileBuyable(Game game, string description, int[] rent, int mortage, int price)
        : base(game, description)
    {
        Rent = rent;
        Mortage = mortage;
        Price = price;
        TotalUpgrades = 0;
        Owner = null;
        HasOwner = false;
        OnMortage = false;
    }

    public int UpgradeCost { get; set; }

    public Player Owner { get; set; }
    public int[] Rent { get; set; }
    public int Mortage { get; set; }
    public int Price { get; set; }
    public int TotalUpgrades { get; set; }
    public bool OnMortage { get; set; }

    /// <summary>
    ///     модернизирует объект IBuyable
    /// </summary>
    public void Upgrade()
    {
        if (TotalUpgrades == 0)
        {
            if (OnMortage)
            {
                if (Owner.Money > Mortage)
                {
                    Owner.Money -= Mortage;
                    OnMortage = false;
                }
            }
            else
            {
                if (Owner.Money > UpgradeCost)
                {
                    TotalUpgrades++;
                    Owner.Money -= UpgradeCost;
                    HasBuildings = true;
                }
            }
        }
        else if (TotalUpgrades < 5)
        {
            if (Owner.Money > UpgradeCost)
            {
                CurrentGame.AddInfo(string.Format(Language.upgrade, Owner.Name, Description));
                TotalUpgrades++;
                Owner.Money -= UpgradeCost;
            }
        }
    }

    /// <summary>
    ///     понижает уровень объекта IBuyable
    /// </summary>
    public void Downgrade()
    {
        if (TotalUpgrades > 1)
        {
            CurrentGame.AddInfo(string.Format(Language.downgrade, Owner.Name, Description));
            TotalUpgrades--;
            Owner.Money += UpgradeCost;
        }
        else if (TotalUpgrades == 1)
        {
            TotalUpgrades--;
            HasBuildings = false;
            Owner.Money += UpgradeCost;
        }
        else
        {
            CurrentGame.AddInfo(string.Format(Language.mortaged, Owner.Name, Description));
            OnMortage = true;
            Owner.Money += Mortage;
        }
    }
}