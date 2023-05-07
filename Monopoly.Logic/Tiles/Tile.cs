namespace Monopoly.Logic.Tiles;

/// <summary>
///     Базовый класс для определения полей для доски монополии.
/// </summary>
public abstract class Tile
{
    protected Tile(Game game, string description)
    {
        CurrentGame = game;
        Description = description;
        HasOwner = true;
        HasBuildings = false;
    }

    public Game CurrentGame { get; set; }

    public Tile NextTile { get; set; }
    public Tile PreviousTile { get; set; }

    public string Description { get; set; }
    public bool HasOwner { get; set; }
    public bool HasBuildings { get; set; }

    /// <summary>
    ///     Определяет, что произойдет, когда игрок окажется на поле.
    /// </summary>
    /// <param name="player"></param>
    public abstract void DoAction(Player player);

    public abstract string GetCardInformation();
}