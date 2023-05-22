namespace Monopoly.Logic.Cards;

/// <summary>
///     Карта, которая дает или берет деньги у игрока
/// </summary>
public class CardMoney : Card
{
    public CardMoney(string description, int moneyForPlayer)
        : base(description)
    {
        MoneyForPlayer = moneyForPlayer;
    }

    public int MoneyForPlayer { get; }

    public override void Use(Player player)
    {
        player.Money += MoneyForPlayer;
        //отрицательные деньги идут на бесплатную парковку
        if (MoneyForPlayer <= 0) player.CurrentGame.FreeParkingTreasure += MoneyForPlayer * -1;
    }
}