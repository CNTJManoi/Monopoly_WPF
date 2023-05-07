namespace Monopoly.Logic.Tiles;

/// <summary>
///     Поле, которая получает все налоговые деньги
/// </summary>
public class TileFreeParking : Tile
{
    public TileFreeParking(Game game, string description)
        : base(game, description)
    {
    }

    public override void DoAction(Player player)
    {
        CurrentGame.GameInfo.Enqueue(string.Format(Properties.Language.moves, player.Name, Description));
        if (CurrentGame.FreeParkingTreasure != 0)
            CurrentGame.GameInfo.Enqueue(string.Format(Properties.Language.freeparkingpay, player.Name,
                CurrentGame.FreeParkingTreasure));

        player.Money += CurrentGame.FreeParkingTreasure;
        CurrentGame.FreeParkingTreasure = 0;
    }

    public override string GetCardInformation()
    {
        return Properties.Language.freeparking;
    }
}