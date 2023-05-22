using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;
using Microsoft.Win32;
using System.Windows.Input;
using Monopoly.View;
using System.Windows;

namespace Monopoly.ViewModel {
    /// <summary>
    /// Модель представления для связывания базы данных с представлением.
    /// </summary>
    public class MenuViewModel : BasicViewModel
    {

        private MainWindowViewModel _mainViewModel;
        public MenuViewModel(MainWindowViewModel model) {
            MaxPlayers = new List<int> { 2, 3, 4 };
            Languages = new string[] { "Russian" };
            _mainViewModel = model;
        }

        # region Commands

        private ICommand _openFilecommand;
        public ICommand OpenFileCommand {
            get {
                return _openFilecommand ??
                       (_openFilecommand =
                        new RelayCommand(p => OpenFile()));
            }
        }


        private ICommand _startNewGameCommand;
        public ICommand StartNewGameCommand {
            get {
                return _startNewGameCommand ??
                       (_startNewGameCommand =
                        new RelayCommand(p => StartGame(), p => CanStartGame()));
            }
        }

        # endregion

        #region Command methods

        public void StartGame()
        {
            string nickname1 = "";
            if (!string.IsNullOrEmpty(NicknameOnePlayer)) nickname1 = NicknameOnePlayer;
            else nickname1 = "Player1";
            string nickname2 = "";
            if (!string.IsNullOrEmpty(NicknameOnePlayer)) nickname2 = NicknameTwoPlayer;
            else nickname1 = "Player2";
            string nickname3 = "";
            if (!string.IsNullOrEmpty(NicknameOnePlayer)) nickname3 = NicknameThreePlayer;
            else nickname1 = "Player3";
            string nickname4 = "";
            if (!string.IsNullOrEmpty(NicknameOnePlayer)) nickname4 = NicknameFourPlayer;
            else nickname1 = "Player4";
            GameViewModel gmv = new GameViewModel(TotalPlayers, _mainViewModel, nickname1, nickname2, nickname3, nickname4);
            _mainViewModel.GoToViewModel(gmv);
        }

        public bool CanStartGame() {
            return TotalPlayers >= 2;
        }

        public void OpenFile() {
            var directoryInfo = Directory.GetParent(Directory.GetCurrentDirectory()).Parent;
            string filename = string.Empty;

            if (directoryInfo != null) {
                OpenFileDialog dlg = new OpenFileDialog {
                    Filter = "Monopoly Files (*.poly)|*.poly",
                    InitialDirectory = directoryInfo.FullName + @"\Data\Saves\",
                };

                dlg.ShowDialog();
                filename = dlg.FileName;
            }

            if (!string.IsNullOrEmpty(filename) && filename.Contains(".poly")) {
                GameViewModel gmv = new GameViewModel(filename, _mainViewModel);
                _mainViewModel.GoToViewModel(gmv);
            }
        }
        #endregion

        #region Bind properties

        private List<int> _maxPlayers;
        public List<int> MaxPlayers {
            get { return _maxPlayers; }
            set {
                if (_maxPlayers != value) {
                    _maxPlayers = value;
                    RaisePropertyChanged("MaxPlayers");
                }
            }
        }

        private string[] _languages;
        public string[] Languages {
            get { return _languages; }
            set {
                if (_languages != value) {
                    _languages = value;
                    RaisePropertyChanged("Languages");
                }
            }
        }

        private int _totalPlayers;
        public int TotalPlayers {
            get { return _totalPlayers; }
            set {
                if (_totalPlayers != value) {
                    _totalPlayers = value;
                    Clear();
                    RaisePropertyChanged("TotalPlayers");
                }
            }
        }

        private string _nicknameOnePlayer;
        public string NicknameOnePlayer
        {
            get { return _nicknameOnePlayer; }
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
            get { return _nicknameTwoPlayer; }
            set
            {
                if (_nicknameTwoPlayer != value)
                {
                    _nicknameTwoPlayer = value;
                    RaisePropertyChanged("NicknameTwoPlayer");
                }
            }
        }
        private string _nicknameThreePlayer;
        public string NicknameThreePlayer
        {
            get { return _nicknameThreePlayer; }
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
            get { return _nicknameFourPlayer; }
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
}
