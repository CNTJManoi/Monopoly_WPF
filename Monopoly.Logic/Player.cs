using System.Collections.ObjectModel;
using System.ComponentModel;
using Monopoly.Logic.Tiles;

namespace Monopoly.Logic;

/// <summary>
///     Класс, содержащий данные и методы для игрока.
/// </summary>
public class Player : INotifyPropertyChanged
{
    private int _money;

    private string _playerInfo;

    public Player(Game game, string name, int money, Tile currentTile)
    {
        Name = name;
        Money = money;
        CurrentTile = currentTile;
        TotalRailRoads = 0;
        CurrentGame = game;
        DiceEyes = -1;
        IsInJail = false;
        Streets = new ObservableCollection<Tile>();
    }

    public string Name { get; set; }
    public Tile CurrentTile { get; set; }
    public Game CurrentGame { get; set; }

    public int DiceEyes { get; set; }
    public int TotalCompanies { get; set; }
    public bool IsInJail { get; set; }
    public int JailCounter { get; set; }
    public int TotalRailRoads { get; set; }
    public ObservableCollection<Tile> Streets { get; set; }

    public string PlayerInfo
    {
        get => _playerInfo;
        set
        {
            _playerInfo = value;
            RaisePropertyChanged("PlayerInfo");
        }
    }

    public int Money
    {
        get => _money;
        set
        {
            _money = value;
            PlayerInfo = Name + " $" + Money;
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    public void PayMoneyTo(Player otherplayer, int moneyToPay)
    {
        if (Money - moneyToPay < 0)
        {
            throw new ArgumentException("Недостаточно денег для оплаты аренды!");
        }
        Money -= moneyToPay;
        otherplayer.Money += moneyToPay;
    }

    /// <summary>
    ///     перемещает игрока вперед или назад по доске
    /// </summary>
    /// <param name="positions"></param>
    public void MoveTo(int positions)
    {
        if (!IsInJail)
        {
            if (positions < 0)
                for (var i = positions; i < 0; i++)
                    CurrentTile = CurrentTile.PreviousTile;
            else
                for (var i = 0; i < positions; i++)
                {
                    CurrentTile = CurrentTile.NextTile;
                    if (CurrentTile.Equals(CurrentGame.Start)) Money += 200;
                }
        }

        CurrentTile.DoAction(this);
    }

    /// <summary>
    ///     устанавливает позицию игрока равной указанному полю
    /// </summary>
    /// <param name="tile"></param>
    public void MoveTo(Tile tile)
    {
        CurrentTile = tile;
        CurrentTile.DoAction(this);
    }


    public void BuyBuilding()
    {
        var street = (TileBuyable)CurrentTile;
        if (street.HasOwner) return;
        if (Money > street.Price)
        {
            Money -= street.Price;
            street.HasOwner = true;
            street.Owner = this;
            Streets.Add(street);
            CurrentGame.GameInfo.Enqueue(string.Format("{0} купил {1}", Name, street.Description));
            ;
            if (street is TileCompany)
                TotalCompanies++;
            else if (street is TileRailRoad) TotalRailRoads++;
        }
    }

    public void RaisePropertyChanged(string prop)
    {
        if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(prop));
    }
}