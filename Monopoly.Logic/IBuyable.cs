namespace Monopoly.Logic;

/// <summary>
///     Интерфейс который определяет можно ли купить поле
/// </summary>
public interface IBuyable
{
    Player Owner { get; set; }
    int[] Rent { get; set; }
    int Mortage { get; set; }
    int Price { get; set; }
    int TotalUpgrades { get; set; }
    bool OnMortage { get; set; }

    void Upgrade();
    void Downgrade();
}