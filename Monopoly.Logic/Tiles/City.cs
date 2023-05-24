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
        bool AllProp = true;
        foreach (var street in Streets)
        {
            if (street.HasOwner) continue;
            else AllProp = false;
        }

        if (AllProp && Streets.Count == 2)
        {
            return Streets.ElementAt(0).Owner.Equals(player) && Streets.ElementAt(1).Owner.Equals(player);
        }
        if(AllProp && Streets.Count == 3)
        {
            return Streets.ElementAt(0).Owner.Equals(player) && Streets.ElementAt(1).Owner.Equals(player) &&
                   Streets.ElementAt(2).Owner.Equals(player);
        }

        return false;
    }
}