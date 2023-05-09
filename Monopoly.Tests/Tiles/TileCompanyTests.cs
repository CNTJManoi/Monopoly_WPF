using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monopoly.Logic;
using Monopoly.Logic.Tiles;
using Xunit;

namespace Monopoly.Tests.Tiles
{
    public class TileCompanyTests
    {
        [Fact]
        public void DoAction_AddsDescriptionToGameInfo()
        {
            // Arrange
            var game = new Game(2);
            game.InitNewGame(2);
            var company = new TileCompany(game, "Electric Company", 100, 150);

            // Act
            company.DoAction(game.CurrentPlayer);

            // Assert
            Assert.True(game.GameInfo.Where(x => x.Contains(string.Format(Logic.Properties.Language.moves, game.CurrentPlayer.Name, company.Description))).Count() > 0);
        }

        [Fact]
        public void DoAction_DoesNotChargeRent_IfCompanyNotOwned()
        {
            // Arrange
            var game = new Game(2);
            game.InitNewGame(2);
            game.CurrentPlayer.Money = 50;
            var company = new TileCompany(game, "Electric Company", 100, 150);
            // Act
            company.DoAction(game.CurrentPlayer);

            // Assert
            Assert.Equal(50, game.CurrentPlayer.Money);
        }

        [Fact]
        public void DoAction_ChargesRent_IfCompanyOwned()
        {
            // Arrange
            var game = new Game(2);
            game.InitNewGame(2);
            var company = new TileCompany(game, "Electric Company", 100, 150);
            company.Owner = game.Players[0];
            game.Players[1].DiceEyes = 6;
            game.Players[1].Money = 300;

            // Act
            company.DoAction(game.Players[1]);

            // Assert
            Assert.Equal(276, game.Players[1].Money);
        }

        [Fact]
        public void GetCardInformation_FormatsCorrectly_IfCompanyOwned()
        {
            // Arrange
            var game = new Game(2);
            game.InitNewGame(2);
            var company = new TileCompany(game, "Electric Company", 100, 150);
            company.Owner = game.Players[0];

            // Act
            var cardInfo = company.GetCardInformation();

            // Assert
            var expectedCardInfo = string.Format(Logic.Properties.Language.company, "Electric Company", Environment.NewLine, game.Players[0].Name, 150,
                100);
            Assert.Equal(expectedCardInfo, cardInfo);
        }

        [Fact]
        public void GetCardInformation_FormatsCorrectly_IfCompanyNotOwned()
        {
            // Arrange
            var game = new Game(2);
            game.InitNewGame(2);
            var company = new TileCompany(game, "Electric Company", 100, 150);

            // Act
            var cardInfo = company.GetCardInformation();

            // Assert
            var expectedCardInfo = string.Format(Logic.Properties.Language.company, "Electric Company", Environment.NewLine, Logic.Properties.Language.propertyowner, 150,
                100);
            Assert.Equal(expectedCardInfo, cardInfo);
        }
    }
}
