namespace Monopoly.Logic.Cards;

/// <summary>
///     Базовый класс для карт
/// </summary>
public abstract class Card
{
    protected Card(string description)
    {
        Description = description;
    }

    public string Description { get; }

    /// <summary>
    ///     Абстрактный метод, который можно использовать для определния что должны делать карты
    /// </summary>
    /// <param name="player"></param>
    public abstract void Use(Player player);
}