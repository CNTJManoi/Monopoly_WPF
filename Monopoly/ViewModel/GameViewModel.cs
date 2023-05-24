using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Windows.Input;
using Microsoft.Win32;
using Monopoly.Data;
using Monopoly.Logic;
using Monopoly.Logic.Tiles;

namespace Monopoly.ViewModel;

/// <summary>
///     A viewmodel for databinding with a view.
/// </summary>
public class GameViewModel : BasicViewModel
{
    private readonly MainWindowViewModel _mainViewModel;
    private FileUploader _fileUploader;
    public GameViewModel(MainWindowViewModel _mainModel, Game game)
    {
        _fileUploader = new FileUploader();
        GameIsFinished = false;
        MyGame = game;
        BuildingPositions = MyGame.ToBoardWithBuildingArray();
        DiceAmount = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 };
        PlayerList = new string[MyGame.Players.Count];
        for (var x = 0; x < MyGame.Players.Count; x++) 
            PlayerList[x] = MyGame.Players[x].Name;
        UpdatePlayersTimer = new Timer(Timer_Tick, null, 0, 1000);
        Cheat = true;
        _mainViewModel = _mainModel;
    }

    public Game MyGame { get; set; }
    public Timer UpdatePlayersTimer { get; set; }
    public bool GameIsFinished { get; set; }

    private void Timer_Tick(object sender)
    {
        BuildingPositions = MyGame.ToBoardWithBuildingArray();
        PlayerPostions = MyGame.ToBoardWithPlayersArray();
        ToolTipForTiles = GetToolTipInfo();
    }

    /// <summary>
    ///     получает информацию, которая будет использоваться во всплывающих подсказках
    /// </summary>
    /// <returns></returns>
    private ObservableCollection<string> GetToolTipInfo()
    {
        var info = new ObservableCollection<string>();

        var current = MyGame.Board.Head;
        for (var i = 0; i < MyGame.Board.Size; i++)
        {
            info.Add(current.GetCardInformation());
            current = current.NextTile;
        }

        info.Add(MyGame.Jail.GetCardInformation());
        return info;
    }
    public void OnWindowClosing(object sender, CancelEventArgs e)
    {
        if (!GameIsFinished)
        {
            var directoryInfo = Directory.GetParent(Directory.GetCurrentDirectory()).Parent;
            var filename = string.Empty;
            if (directoryInfo != null)
            {
                var dlg = new SaveFileDialog
                {
                    Filter = "Monopoly Files (*.poly)|*.poly",
                    InitialDirectory = directoryInfo.FullName + @"\Data\Saves\"
                };
                dlg.ShowDialog();
                filename = dlg.FileName;
            }

            if (!string.IsNullOrEmpty(filename) && filename.Contains(".poly")) _fileUploader.SaveData(MyGame.Configuration, filename);
        }
    }

    #region Commands

    private ICommand _throwDiceCommand;

    public ICommand ThrowDiceCommand
    {
        get
        {
            return _throwDiceCommand ??
                   (_throwDiceCommand =
                       new RelayCommand(p => MyGame.ThrowDiceAndMovePlayer(),
                           p => !MyGame.PlayerDice.HasBeenThrown)); // todo: нарушение закона Деметры. лучше добавиьт в Game метод IsMovePossible
        }
    }

    private ICommand _changePlayerCheatCommand;

    public ICommand ChangePlayerCheatCommand
    {
        get
        {
            return _changePlayerCheatCommand ??
                   (_changePlayerCheatCommand =
                       new RelayCommand(p => ChangePlayer(SelectedPlayer)));
        }
    }

    private ICommand _throwCheatDiceCommand;

    public ICommand ThrowCheatDiceCommand
    {
        get
        {
            return _throwCheatDiceCommand ??
                   (_throwCheatDiceCommand =
                       new RelayCommand(p => MyGame.ThrowDiceAndMovePlayer(CheatSelected)));
        }
    }

    private ICommand _endTurnCommand;

    public ICommand EndTurnCommand
    {
        get
        {
            return _endTurnCommand ??
                   (_endTurnCommand =
                       new RelayCommand(p => EndTurn(), p => CanEndTurn()));
        }
    }

    private ICommand _manageCommand;

    public ICommand ManageCommand
    {
        get
        {
            return _manageCommand ??
                   (_manageCommand =
                       new RelayCommand(p => ShowManageWindow()));
        }
    }

    private ICommand _buyCommand;

    public ICommand BuyCommand
    {
        get
        {
            return _buyCommand ??
                   (_buyCommand =
                       new RelayCommand(p => MyGame.CurrentPlayer.BuyBuilding(), p => CanBuy()));
        }
    }

    private ICommand _bankruptCommand;

    public ICommand BankruptCommand
    {
        get
        {
            return _bankruptCommand ??
                   (_bankruptCommand =
                       new RelayCommand(p => EndGame()));
        }
    }

    #endregion

    #region Command methods

    private void EndGame()
    {
        GameIsFinished = true;
        var EndViewModel = new EndScreenViewModel(MyGame);
        _mainViewModel.GoToViewModel(EndViewModel);
    }

    private void EndTurn()
    {
        MyGame.EndTurn();
    }


    private void ShowManageWindow()
    {
        var manager = new ManageStreetsViewModel(MyGame, _mainViewModel, this);
        _mainViewModel.GoToViewModel(manager);
    }
    private bool CanBuy()
    {
        if (!MyGame.CurrentPlayer.CurrentTile.HasOwner)
        {
            var buyableTile = (TileBuyable)MyGame.CurrentPlayer.CurrentTile;
            return buyableTile != null && MyGame.CurrentPlayer.Money > buyableTile.Price;
        }

        return false;
    }
    private bool CanEndTurn()
    {
        return MyGame.CanEndTurn();
    }

    private void ChangePlayer(int index)
    {
        MyGame.ChangePlayer(index);
    }

    #endregion

    #region bind properties

    private ObservableCollection<string> _toolTipForTiles;

    public ObservableCollection<string> ToolTipForTiles
    {
        get => _toolTipForTiles;
        set
        {
            if (_toolTipForTiles != value)
            {
                _toolTipForTiles = value;
                RaisePropertyChanged("ToolTipForTiles");
            }
        }
    }


    private ObservableCollection<ObservableCollection<bool>> _playerPostions;

    public ObservableCollection<ObservableCollection<bool>> PlayerPostions
    {
        get => _playerPostions;
        set
        {
            if (_playerPostions != value)
            {
                _playerPostions = value;
                RaisePropertyChanged("PlayerPostions");
            }
        }
    }

    private ObservableCollection<bool> _buildingPositions;

    public ObservableCollection<bool> BuildingPositions
    {
        get => _buildingPositions;
        set
        {
            if (_buildingPositions != value)
            {
                _buildingPositions = value;
                RaisePropertyChanged("BuildingPositions");
            }
        }
    }

    private int[] _diceAmount;

    public int[] DiceAmount
    {
        get => _diceAmount;
        set
        {
            if (_diceAmount != value)
            {
                _diceAmount = value;
                RaisePropertyChanged("DiceAmount");
            }
        }
    }

    private int _selectedPlayer;

    public int SelectedPlayer
    {
        get => _selectedPlayer;
        set
        {
            if (_selectedPlayer != value)
            {
                _selectedPlayer = value;
                RaisePropertyChanged("DiceAmount");
            }
        }
    }

    private string[] _playerList;

    public string[] PlayerList
    {
        get => _playerList;
        set
        {
            if (_playerList != value)
            {
                _playerList = value;
                RaisePropertyChanged("PlayerList");
            }
        }
    }

    private int _cheatSelected;

    public int CheatSelected
    {
        get => _cheatSelected;
        set
        {
            if (_cheatSelected != value)
            {
                _cheatSelected = value;
                RaisePropertyChanged("CheatSelected");
            }
        }
    }

    private bool _cheat;

    public bool Cheat
    {
        get => _cheat;
        set
        {
            if (_cheat != value)
            {
                _cheat = value;
                RaisePropertyChanged("Cheat");
            }
        }
    }

    #endregion
}