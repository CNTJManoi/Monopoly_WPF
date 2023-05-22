using System.Collections.ObjectModel;

namespace Monopoly.Logic.Tiles;

/// <summary>
///     Класс, в котором хранятся свойства, принадлежащие городу
/// </summary>
public class City
{
    public City()
    {
        Streets = new ObservableCollection<TileProperty>();
    }

    public ObservableCollection<TileProperty> Streets { get; }

    /// <summary>
    ///     Проверяет, владеет ли игрок всеми объектами недвижимости, принадлежащими этому городу
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    public bool OwnsAllProperties(Player player)
    {
        if (Streets.Count == 2)
        {
            if (Streets.ElementAt(0).HasOwner && Streets.ElementAt(1).HasOwner)
                return Streets.ElementAt(0).Owner.Equals(player) && Streets.ElementAt(1).Owner.Equals(player);
        }
        else if (Streets.Count == 3 && Streets.ElementAt(0).HasOwner && Streets.ElementAt(1).HasOwner &&
                 Streets.ElementAt(2).HasOwner)
        {
            return Streets.ElementAt(0).Owner.Equals(player) && Streets.ElementAt(1).Owner.Equals(player) &&
                   Streets.ElementAt(2).Owner.Equals(player);
        }

        return false;
    }
}