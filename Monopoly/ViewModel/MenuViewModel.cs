using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Input;
using Microsoft.Win32;
using Monopoly.Data;
using Monopoly.Database;
using Monopoly.Logic;

namespace Monopoly.ViewModel;

/// <summary>
///     Модель представления для связывания базы данных с представлением.
/// </summary>
public class MenuViewModel : BasicViewModel
{
    private readonly MainWindowViewModel _mainViewModel;
    private Configuration _configFile;
    private Game _game;

    public MenuViewModel(MainWindowViewModel model)
    {
        MaxPlayers = new List<int> { 2, 3, 4 };
        Languages = new[] { "Russian" };
        _mainViewModel = model;
    }

    # region Commands

    private ICommand _openFilecommand;

    public ICommand OpenFileCommand
    {
        get
        {
            return _openFilecommand ??
                   (_openFilecommand =
                       new RelayCommand(p => OpenFile()));
        }
    }

    private ICommand _loadSaveGame;

    public ICommand LoadSaveGame
    {
        get
        {
            return _loadSaveGame ??
                   (_loadSaveGame =
                       new RelayCommand(p => LoadDatabaseGame()));
        }
    }

    private ICommand _startNewGameCommand;

    public ICommand StartNewGameCommand
    {
        get
        {
            return _startNewGameCommand ??
                   (_startNewGameCommand =
                       new RelayCommand(p => StartGame(), p => CanStartGame()));
        }
    }

    # endregion

    #region Command methods

    private async void LoadDatabaseGame()
    {
        int id = 0;
        try
        {
            id = int.Parse(IdGame);
        }
        catch (FormatException e)
        {
            Console.WriteLine("Неверный ID");
            return;
        }
        catch (ArgumentNullException e)
        {
            Console.WriteLine("Введите ID");
            return;
        }
        var jsonGame = await DatabaseController.getInstance().ReturnSavedGame(id);
        _game = Monopoly.Json.JsonConverter.DeserializeObject(jsonGame.JsonSaveGame);
        var gmv = new GameViewModel(_mainViewModel, _game, DatabaseController.getInstance());
        _mainViewModel.GoToViewModel(gmv);
    }

    public void StartGame()
    {
        var nickname1 = "";
        if (!string.IsNullOrEmpty(NicknameOnePlayer)) nickname1 = NicknameOnePlayer;
        else nickname1 = "Player1";
        var nickname2 = "";
        if (!string.IsNullOrEmpty(NicknameOnePlayer)) nickname2 = NicknameTwoPlayer;
        else nickname2 = "Player2";
        var nickname3 = "";
        if (!string.IsNullOrEmpty(NicknameOnePlayer)) nickname3 = NicknameThreePlayer;
        else nickname3 = "Player3";
        var nickname4 = "";
        if (!string.IsNullOrEmpty(NicknameOnePlayer)) nickname4 = NicknameFourPlayer;
        else nickname4 = "Player4";
        DatabaseLoad(nickname1, nickname2, nickname3, nickname4, TotalPlayers);
        _game = new Game(TotalPlayers, nickname1, nickname2, nickname3, nickname4);
        _configFile = new Configuration(_game);
        var cards = _configFile.GetAllCards(@"Config\CardDescriptions").ToList();
        _game.GetCards(cards);
        var gmv = new GameViewModel(_mainViewModel, _game, DatabaseController.getInstance());
        _mainViewModel.GoToViewModel(gmv);
    }

    private async void DatabaseLoad(string name1, string name2, string name3, string name4, int totalPlayers)
    {
        if (!await DatabaseController.getInstance().ExistPlayer(name1))
        {
            await DatabaseController.getInstance().AddPlayer(name1);
        }
        if (!await DatabaseController.getInstance().ExistPlayer(name2) && totalPlayers > 1)
        {
            await DatabaseController.getInstance().AddPlayer(name2);
        }
        if (!await DatabaseController.getInstance().ExistPlayer(name3) && totalPlayers > 2)
        {
            await DatabaseController.getInstance().AddPlayer(name3);
        }
        if (!await DatabaseController.getInstance().ExistPlayer(name4) && totalPlayers > 3)
        {
            await DatabaseController.getInstance().AddPlayer(name4);
        }
    }
    public bool CanStartGame()
    {
        return TotalPlayers >= 2;
    }
    public void OpenFile()
    {
        FileUploader fu = new FileUploader();
        var path = fu.GetFilePath();
        if (path != "")
        {
            _game = new Game(path);
            _configFile = new Configuration(_game);
            var cards = _configFile.GetAllCards(@"Config\CardDescriptions").ToList();
            _game.GetCards(cards);
            var gmv = new GameViewModel(_mainViewModel, _game, DatabaseController.getInstance());
            _mainViewModel.GoToViewModel(gmv);
        }
    }

    #endregion

    #region Bind properties

    private List<int> _maxPlayers;

    public List<int> MaxPlayers
    {
        get => _maxPlayers;
        set
        {
            if (_maxPlayers != value)
            {
                _maxPlayers = value;
                RaisePropertyChanged("MaxPlayers");
            }
        }
    }

    private string[] _languages;

    public string[] Languages
    {
        get => _languages;
        set
        {
            if (_languages != value)
            {
                _languages = value;
                RaisePropertyChanged("Languages");
            }
        }
    }

    private int _totalPlayers;

    public int TotalPlayers
    {
        get => _totalPlayers;
        set
        {
            if (_totalPlayers != value)
            {
                _totalPlayers = value;
                Clear();
                RaisePropertyChanged("TotalPlayers");
            }
        }
    }

    private string _nicknameOnePlayer;

    public string NicknameOnePlayer
    {
        get => _nicknameOnePlayer;
        set
        {
            if (_nicknameOnePlayer != value)
            {
                _nicknameOnePlayer = value;
                RaisePropertyChanged("NicknameOnePlayer");
            }
        }
    }

    private string _nicknameTwoPlayer;

    public string NicknameTwoPlayer
    {
        get => _nicknameTwoPlayer;
        set
        {
            if (_nicknameTwoPlayer != value)
            {
                _nicknameTwoPlayer = value;
                RaisePropertyChanged("NicknameTwoPlayer");
            }
        }
    }
    private string _idGame;

    public string IdGame
    {
        get => _idGame;
        set
        {
            if (_idGame != value)
            {
                _idGame = value;
                RaisePropertyChanged("NicknameTwoPlayer");
            }
        }
    }

    private string _nicknameThreePlayer;

    public string NicknameThreePlayer
    {
        get => _nicknameThreePlayer;
        set
        {
            if (_nicknameThreePlayer != value)
            {
                _nicknameThreePlayer = value;
                RaisePropertyChanged("NicknameThreePlayer");
            }
        }
    }

    private string _nicknameFourPlayer;

    public string NicknameFourPlayer
    {
        get => _nicknameFourPlayer;
        set
        {
            if (_nicknameFourPlayer != value)
            {
                _nicknameFourPlayer = value;
                RaisePropertyChanged("NicknameFourPlayer");
            }
        }
    }

    private void Clear()
    {
        NicknameOnePlayer = "";
        NicknameTwoPlayer = "";
        NicknameThreePlayer = "";
        NicknameFourPlayer = "";
    }

    #endregion
}