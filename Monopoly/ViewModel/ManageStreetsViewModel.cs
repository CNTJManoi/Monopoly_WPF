using System.Windows;
using System.Windows.Input;
using Monopoly.Logic;
using Monopoly.Logic.Tiles;

namespace Monopoly.ViewModel;

public class ManageStreetsViewModel : BasicViewModel
{
    private readonly GameViewModel _gameView;
    private readonly MainWindowViewModel _mainWindowViewModel;

    public ManageStreetsViewModel(Game game, MainWindowViewModel mainWindow, GameViewModel gameView)
    {
        Game = game;
        _mainWindowViewModel = mainWindow;
        _gameView = gameView;
    }
    
    private Game Game { get; }
    public TileProperty SelectedTile { get; set; }

    #region Commands

    private ICommand _upgradeCommand;

    public ICommand UpgradeCommand
    {
        get
        {
            return _upgradeCommand ??
                   (_upgradeCommand =
                       new RelayCommand(p => DoUpgradeCommand()));
        }
    }

    private ICommand _downgradeCommand;

    public ICommand DowngradeCommand
    {
        get
        {
            return _downgradeCommand ??
                   (_downgradeCommand =
                       new RelayCommand(p => DoDowngradeCommand()));
        }
    }

    private ICommand _exitCommand;

    public ICommand ExitCommand
    {
        get
        {
            return _exitCommand ??
                   (_exitCommand =
                       new RelayCommand(p => Exit()));
        }
    }

    #endregion

    #region Command Methods

    private void DoUpgradeCommand()
    {
        MessageBox.Show(Game.DoUpgrade(SelectedTile));
    }

    private void DoDowngradeCommand()
    {
        MessageBox.Show(Game.DoDowngrade(SelectedTile));
    }

    private void Exit()
    {
        _mainWindowViewModel.GoToViewModel(_gameView);
    }

    #endregion
}