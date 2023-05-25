using System.Collections.ObjectModel;
using System.ComponentModel;
using Monopoly.Logic.Cards;
using Monopoly.Logic.Models;
using Monopoly.Logic.Properties;
using Monopoly.Logic.Tiles;

namespace Monopoly.Logic;

/// <summary>
///     Класс, который хранит данные игры и управляет окружением самой игры.
/// </summary>
public class Game : PropertyNotificator
{
    private int _freeParkingTreasure;
    private Configuration _currentConfigFile;

    private Game()
    {
        _currentConfigFile = new Configuration(this);
        GameInfo = new QueueLimit<string>(70);
        PlayerDice = new Dice();
        PlayerTurn = 0;

    }

    /// <summary>
    ///     Конструктор для загрузки существующей игры
    /// </summary>
    public Game(string saveFile, List<Card> cards)
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
    public Game(string saveFile)
        : this()
    {
        InitSavedGame(saveFile);
    }

    public ObservableCollection<Player> Players { get; private set; }
    
    public Deck ChanceCards { get; private set; }
    public Deck CommunityCards { get; private set; }
    public Player CurrentPlayer { get; private set; }
    public int PlayerTurn { get; private set; }

    public Dice PlayerDice { get; }
    public QueueLimit<string> GameInfo { get; }

    public TileLinkedList Board { get; private set; }
    public TileJailVisit JailVisit { get; set; }
    public TileJail Jail { get; set; }
    public TileStart Start { get; set; }
    

    public int FreeParkingTreasure
    {
        get => _freeParkingTreasure;
        set
        {
            _freeParkingTreasure = value;
            RaisePropertyChanged("FreeParkingTreasure");
        }
    }
    public Configuration Configuration
    {
        get
        {
            return _currentConfigFile;

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
        Board = _currentConfigFile.LoadDefauldBoard();

        Players = new ObservableCollection<Player>();
        Players.Add(new Player(this, name1, 1500, Start));
        if (maxplayers >= 2) Players.Add(new Player(this, name2, 1500, Start));
        if (maxplayers >= 3) Players.Add(new Player(this, name3, 1500, Start));
        if (maxplayers >= 4) Players.Add(new Player(this, name4, 1500, Start));

        CurrentPlayer = Players.First();
    }

    public void GetCards(List<Card> cards)
    {
        LoadCards(cards);
    }
    /// <summary>
    ///     инициализировать сохраненную игру
    /// </summary>
    /// <param name="savedGame"></param>
    public void InitSavedGame(string savedGame)
    {
        // todo: дублирование с 99 и 100 строками. Возможно следовало бы добавить метод добавить игрока,
        // и создавать игроков вне данного класса. ТОгда инициализация игры была бы одинаковой для обоих случаев
        Board = _currentConfigFile.LoadDefauldBoard();
        LoadPlayers(_currentConfigFile.GetAllPlayers(savedGame));
    }

    /// <summary>
    ///     Загрузка игроков из файла сохранения
    /// </summary>
    /// <param name="savedGame"></param>
    public void LoadPlayers(ObservableCollection<Player> loadPlayers)
    {
        Players = loadPlayers;
        CurrentPlayer = Players.ElementAt(0);
    }

    /// <summary>
    ///     Загружает все карты из файла и заполняет ими 2 колоды
    /// </summary>
    private void LoadCards(List<Card> cards)
    {
        
        ChanceCards = new Deck(cards.GetRange(0, cards.Count / 2));
        cards.RemoveRange(0, cards.Count / 2);
        CommunityCards = new Deck(cards);
    }

    #endregion

    #region Game Mechanics

    public void ThrowDiceAndMovePlayer()
    {
        if (!PlayerDice.HasBeenThrown)
        {
            CurrentPlayer.DiceEyes = PlayerDice.ThrowDice();
            
            AddInfo(string.Format(Language.throwdice, CurrentPlayer.Name, PlayerDice.FirstDice,
                PlayerDice.SecondDice));
            CurrentPlayer.MoveTo(CurrentPlayer.DiceEyes);
        }
    }

    // todo: тут и дублирование и метод, вроде бы нужный для тестирования
    // в этом случае более подходящим было бы создание наследника с переопределением предыдущего метода
    public void ThrowDiceAndMovePlayer(int value)
    {
        CurrentPlayer.DiceEyes = value;
        PlayerDice.HasBeenThrown = true;
        AddInfo(string.Format(Language.cheatdice, CurrentPlayer.Name, value));

        CurrentPlayer.MoveTo(CurrentPlayer.DiceEyes);
    }
    private void NextTurn()
    {
        PlayerTurn++;
        if (PlayerTurn % Players.Count == 0) PlayerTurn = 0;
        CurrentPlayer = Players[PlayerTurn];
    }

    public void EndTurn()
    {
        if (!PlayerDice.IsDouble()) 
            NextTurn();
        PlayerDice.EndTurn();
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

    public void ChangePlayer(int index)
    {
        CurrentPlayer = Players[index];
    }
    private StaticticsModel CalculateWinner(StaticticsModel sm)
    {
        sm.Winner = 0;

        if (sm.PlayerMoneys[1] > sm.PlayerMoneys[sm.Winner]) sm.Winner = 1;
        if (Players.Count > 2)
            if (sm.PlayerMoneys[2] > sm.PlayerMoneys[sm.Winner])
                sm.Winner = 2;
        if (Players.Count > 3)
            if (sm.PlayerMoneys[3] > sm.PlayerMoneys[sm.Winner])
                sm.Winner = 3;
        return sm;
    }
    public StaticticsModel CalculateScores()
    {
        StaticticsModel sm = new StaticticsModel(this);
        for (var index = 0; index < Players.Count; index++)
        {
            foreach (var tile in Players[index].Streets)
            {
                var tileProp = tile as TileProperty;
                var tileRailroad = tile as TileRailRoad;
                var tileComp = tile as TileCompany;

                if (tileProp != null)
                {
                    if (tileProp.TotalUpgrades > 0)
                    {
                        for (var downgrade = 0; downgrade < tileProp.TotalUpgrades; downgrade++) tileProp.Downgrade();
                        tileProp.Downgrade();
                    }
                }
                else if (tileRailroad != null)
                {
                    tileRailroad.Downgrade();
                }
                else if (tileComp != null)
                {
                    tileComp.Downgrade();
                }
            }

            sm.PlayerMoneys[index] = Players[index].Money;
        }
        return CalculateWinner(sm);
    }

    public bool CanEndTurn()
    {
        if (PlayerDice.HasBeenThrown && !PlayerDice.IsDouble() &&
            CurrentPlayer.Money >= 0) return true;
        if (PlayerDice.IsDouble())
        {
            PlayerDice.HasBeenThrown = false;
            return false;
        }

        return false;
    }

    public string DoUpgrade(TileProperty SelectedTile)
    {
        if (SelectedTile != null &&
            ((SelectedTile != null && SelectedTile.City.OwnsAllProperties(CurrentPlayer) &&
              SelectedTile.CanBeUpgraded()) || SelectedTile.OnMortage))
        {
            SelectedTile.Upgrade();
            return "Успешно!";
        }
        if (SelectedTile == null)
            return "Выберите обновляемый элемент в списке.";
        if (SelectedTile != null && !SelectedTile.City.OwnsAllProperties(CurrentPlayer))
            return "Прежде чем начать строительство домов, необходимо иметь все дома на улице.";
        if (!SelectedTile.CanBeUpgraded())
            return
                "Сначала вы должны иметь на всех улицах одинаковое количество домов, прежде чем сможете модернизировать эту улицу!";
        return "Успешно!";
    }

    public string DoDowngrade(TileProperty SelectedTile)
    {
        if (SelectedTile != null && SelectedTile.CanBeDowngraded())
        {
            SelectedTile.Downgrade();
            return "Успешно";
        }
        
        else if (SelectedTile == null)
            return "Выберите улицу, которую вы хотите понизить, пожалуйста";
        else if (SelectedTile.OnMortage)
            return "Это здание нельзя понижать еще больше!";
        else if (!SelectedTile.CanBeDowngraded())
            return
                "Сначала все улицы должны иметь одинаковое количество домов, прежде чем вы сможете понизить уровень этой улицы!";
        return "Успешно";
    }
    #endregion
}