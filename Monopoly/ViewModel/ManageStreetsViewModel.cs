﻿using System.Windows;
using System.Windows.Input;
using Monopoly.Logic;
using Monopoly.Logic.Tiles;

namespace Monopoly.ViewModel {
    public class ManageStreetsViewModel {
        public Game Game { get; set; }
        public TileProperty SelectedTile { get; set; }

        public ManageStreetsViewModel(Game game) {
            Game = game;
        }

        #region Commands

        private ICommand _upgradeCommand;
        public ICommand UpgradeCommand {
            get {
                return _upgradeCommand ??
                       (_upgradeCommand =
                        new RelayCommand(p => DoUpgradeCommand()));
            }
        }

        private ICommand _downgradeCommand;
        public ICommand DowngradeCommand {
            get {
                return _downgradeCommand ??
                       (_downgradeCommand =
                        new RelayCommand(p => DoDowngradeCommand()));
            }
        }

        #endregion

        #region Command Methods

        private void DoUpgradeCommand() {
            if (SelectedTile != null && (SelectedTile != null && SelectedTile.City.OwnsAllProperties(Game.CurrentPlayer) && SelectedTile.CanBeUpgraded() || SelectedTile.OnMortage)) {
                SelectedTile.Upgrade();
            } else if (SelectedTile == null) {
                MessageBox.Show("Выберите обновляемый элемент в списке.");
            } else if (SelectedTile != null && !SelectedTile.City.OwnsAllProperties(Game.CurrentPlayer)) {
                MessageBox.Show("Прежде чем начать строительство домов, необходимо иметь все дома на улице.");
            } else if (!SelectedTile.CanBeUpgraded()) {
                MessageBox.Show("Сначала вы должны иметь на всех улицах одинаковое количество домов, прежде чем сможете модернизировать эту улицу!");
            }
        }

        private void DoDowngradeCommand() {
            if (SelectedTile != null && SelectedTile.CanBeDowngraded()) {
                SelectedTile.Downgrade();
            } else if (SelectedTile == null) {
                MessageBox.Show("Выберите улицу, которую вы хотите понизить, пожалуйста");
            } else if (SelectedTile.OnMortage) {
                MessageBox.Show("Это здание нельзя понижать еще больше!");
            } else if (!SelectedTile.CanBeDowngraded()) {
                MessageBox.Show("Сначала все улицы должны иметь одинаковое количество домов, прежде чем вы сможете понизить уровень этой улицы!");
            }

        }

        #endregion
    }
}
