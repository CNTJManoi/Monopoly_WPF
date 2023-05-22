using System.Collections.ObjectModel;
using System.ComponentModel;
using Monopoly.Logic.Properties;
using Monopoly.Logic.Tiles;

namespace Monopoly.Logic;

/// <summary>
///     Класс, который хранит данные игры и управляет окружением самой игры.
/// </summary>
public class Game : INotifyPropertyChanged
{
    private int _freeParkingTreasure;


    private Game()
    {
        CurrentConfigFile = new Configuration(this);
        GameInfo = new QueueLimit<string>(70);
        PlayerDice = new Dice();
        PlayerTurn = 0;
    }

    /// <summary>
    ///     Конструктор для загрузки существующей игры
    /// </summary>
    public Game(string saveFile)
        : this()
    {
        InitSavedGame(saveFile);
    }

    /// <summary>
    ///     Конструктор для создания новой игры
    /// </summary>
    /// <param name="totalPlayers"></param>
    public Game(int totalPlayers, string name1 = "Player1",
        string name2 = "Player2", string name3 = "Player3", string name4 = "Player4")
        : this()
    {
        InitNewGame(totalPlayers, name1, name2, name3, name4);
    }

    public ObservableCollection<Player> Players { get; private set; }
    public Configuration CurrentConfigFile { get; }
    public Deck ChanceCards { get; private set; }
    public Deck CommunityCards { get; private set; }
    public Player CurrentPlayer { get; set; }
    public int PlayerTurn { get; private set; }
    public Dice PlayerDice { get; set; }

    public QueueLimit<string> GameInfo { get; }

    public TileLinkedList Board { get; private set; }
    public TileJailVisit JailVisit { get; set; }
    public TileJail Jail { get; set; }
    public TileGoToJail GoToJail { get; set; }
    public TileStart Start { get; set; }
    public TileProperty Boardwalk { get; set; }

    public int FreeParkingTreasure
    {
        get => _freeParkingTreasure;
        set
        {
            _freeParkingTreasure = value;
            RaisePropertyChanged("FreeParkingTreasure");
        }
    }

    public void AddInfo(string info)
    {
        GameInfo.Enqueue(info);
    }

    #region Initiation of Games

    /// <summary>
    ///     инициализировать новую игру
    /// </summary>
    /// <param name="maxplayers"></param>
    public void InitNewGame(int maxplayers, string name1 = "Player1",
        string name2 = "Player2", string name3 = "Player3", string name4 = "Player4")
    {
        Board = CurrentConfigFile.LoadDefauldBoard();
        LoadCards();

        Players = new ObservableCollection<Player>();
        Players.Add(new Player(this, name1, 1500, Start));
        if (maxplayers >= 2) Players.Add(new Player(this, name2, 1500, Start));
        if (maxplayers >= 3) Players.Add(new Player(this, name3, 1500, Start));
        if (maxplayers >= 4) Players.Add(new Player(this, name4, 1500, Start));

        CurrentPlayer = Players.First();
    }

    /// <summary>
    ///     инициализировать сохраненную игру
    /// </summary>
    /// <param name="savedGame"></param>
    public void InitSavedGame(string savedGame)
    {
        Board = CurrentConfigFile.LoadDefauldBoard();
        LoadCards();
        LoadPlayers(savedGame);
    }

    /// <summary>
    ///     Загрузка игроков из файла сохранения
    /// </summary>
    /// <param name="savedGame"></param>
    public void LoadPlayers(string savedGame)
    {
        Players = CurrentConfigFile.GetAllPlayers(savedGame);
        CurrentPlayer = Players.ElementAt(0);
    }

    /// <summary>
    ///     Загружает все карты из файла и заполняет ими 2 колоды
    /// </summary>
    private void LoadCards()
    {
        var cards = CurrentConfigFile.GetAllCards(@"Config\CardDescriptions").ToList();
        ChanceCards = new Deck(cards.GetRange(0, cards.Count / 2));
        cards.RemoveRange(0, cards.Count / 2);
        CommunityCards = new Deck(cards);
    }

    #endregion

    #region Game Mechanics

    public void SaveData(string filename)
    {
        CurrentConfigFile.SaveData(filename);
    }

    public void ThrowDiceAndMovePlayer()
    {
        if (!PlayerDice.HasBeenThrown)
        {
            CurrentPlayer.DiceEyes = PlayerDice.ThrowDice();
            PlayerDice.HasBeenThrown = true;
            AddInfo(string.Format(Language.throwdice, CurrentPlayer.Name, PlayerDice.FirstDice,
                PlayerDice.SecondDice));
            CurrentPlayer.MoveTo(CurrentPlayer.DiceEyes);
        }
    }

    public void ThrowDiceAndMovePlayer(int value)
    {
        CurrentPlayer.DiceEyes = value;
        PlayerDice.HasBeenThrown = true;
        AddInfo(string.Format(Language.cheatdice, CurrentPlayer.Name, value));

        CurrentPlayer.MoveTo(CurrentPlayer.DiceEyes);
    }

    public void NextTurn()
    {
        PlayerTurn++;
        if (PlayerTurn % Players.Count == 0) PlayerTurn = 0;
        CurrentPlayer = Players[PlayerTurn];
    }

    public void EndTurn()
    {
        if (!PlayerDice.IsDouble()) NextTurn();
        PlayerDice.HasBeenThrown = false;
    }

    #endregion

    #region View Bindings

    public ObservableCollection<ObservableCollection<bool>> ToBoardWithPlayersArray()
    {
        var array = new ObservableCollection<ObservableCollection<bool>>();
        for (var i = 0; i < Players.Count; i++)
        {
            var current = Board.Head;
            var sublist = new ObservableCollection<bool>();

            for (var j = 0; j < Board.Size; j++)
            {
                sublist.Add(current.Equals(Players.ElementAt(i).CurrentTile));
                current = current.NextTile;
            }

            sublist.Add(Players.ElementAt(i).IsInJail);
            array.Add(sublist);
        }

        for (var i = 0; i < 4 - Players.Count; i++)
        {
            var empty = new ObservableCollection<bool>();
            for (var j = 0; j < Board.Size + 1; j++) empty.Add(false);
            array.Add(empty);
        }

        return array;
    }

    public ObservableCollection<bool> ToBoardWithBuildingArray()
    {
        var buildingVisibility = new ObservableCollection<bool>();
        for (var x = 0; x < Board.Size; x++) buildingVisibility.Add(Board.GetAt(x).HasBuildings);
        return buildingVisibility;
    }

    /// <summary>
    ///     получает информацию, которая будет использоваться во всплывающих подсказках
    /// </summary>
    /// <returns></returns>
    public ObservableCollection<string> GetToolTipInfo()
    {
        var info = new ObservableCollection<string>();

        var current = Board.Head;
        for (var i = 0; i < Board.Size; i++)
        {
            info.Add(current.GetCardInformation());
            current = current.NextTile;
        }

        info.Add(Jail.GetCardInformation());
        return info;
    }

    public void RaisePropertyChanged(string prop)
    {
        if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(prop));
    }

    public event PropertyChangedEventHandler PropertyChanged;

    #endregion
}