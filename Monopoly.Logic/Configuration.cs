using System.Collections.ObjectModel;
using Monopoly.Logic.Cards;
using Monopoly.Logic.Tiles;

namespace Monopoly.Logic;

/// <summary>
///     A class to load players, boards and cards.
/// </summary>
public class Configuration
{
    private TileGoToJail _goToJail;
    private TileProperty _boardwalk;
    public Configuration(Game game)
    {
        CurrentGame = game;
        var directoryInfo = Directory.GetCurrentDirectory();
        if (directoryInfo != null) FileName = directoryInfo + @"\Data\";
    }
    public string FileName { get; set; }
    private Game CurrentGame { get; }

    public List<Card> GetAllCards(string cardLocation)
    {
        var cards = new List<Card>();
        string line;
        var reader = new StreamReader(FileName + cardLocation + "-ru-RU.txt");
        while ((line = reader.ReadLine()) != null)
        {
            var lineInfo = line.Split('@');
            switch (lineInfo[0])
            {
                case "move":
                    if (lineInfo[2].Equals("Boardwalk"))
                        cards.Add(new CardMove(lineInfo[1], _boardwalk));
                    else if (lineInfo[2].Equals("Start"))
                        cards.Add(new CardMove(lineInfo[1], CurrentGame.Start));
                    else if (lineInfo[2].Equals("Jail"))
                        cards.Add(new CardMove(lineInfo[1], _goToJail));
                    else
                        cards.Add(new CardMove(lineInfo[1], int.Parse(lineInfo[2])));
                    break;

                case "money":
                    cards.Add(new CardMoney(lineInfo[1], int.Parse(lineInfo[2])));
                    break;
            }
        }

        return cards;
    }

    /// <summary>
    ///     Загрузить обычное поле
    /// </summary>
    /// <returns>Лист который содержит все поля</returns>
    public TileLinkedList LoadDefauldBoard()
    {
        var board = new TileLinkedList();

        var purple = new City();
        var gray = new City();
        var pink = new City();
        var orange = new City();
        var red = new City();
        var yellow = new City();
        var green = new City();
        var blue = new City();

        var medAvenue = new TileProperty(CurrentGame, "Mediterranean Avenue", new[] { 2, 10, 30, 90, 160, 250 }, 30, 60,
            50, purple);
        var balticAvenue = new TileProperty(CurrentGame, "Baltic Avenue", new[] { 4, 20, 60, 180, 320, 450 }, 30, 60,
            50, purple);
        var orienAvenue = new TileProperty(CurrentGame, "Oriental Avenue", new[] { 6, 30, 90, 270, 400, 550 }, 50, 100,
            50, gray);
        var vermAvenue = new TileProperty(CurrentGame, "Vermont Avenue", new[] { 6, 30, 90, 270, 400, 550 }, 50, 100,
            50, gray);
        var connecAvenue = new TileProperty(CurrentGame, "Connecticut Avenue", new[] { 8, 40, 100, 300, 450, 600 }, 60,
            120, 50, gray);
        var stCharlesPlace = new TileProperty(CurrentGame, "St. Charles Place", new[] { 10, 50, 150, 450, 625, 750 },
            70, 140, 100, pink);
        var statesAvenue = new TileProperty(CurrentGame, "States Avenue", new[] { 10, 50, 150, 450, 625, 750 }, 70, 140,
            100, pink);
        var virginAvenue = new TileProperty(CurrentGame, "Virginia Avenue", new[] { 12, 60, 180, 500, 700, 900 }, 80,
            160, 100, pink);
        var stJamesPlace = new TileProperty(CurrentGame, "St. James Place", new[] { 14, 70, 200, 550, 750, 950 }, 90,
            180, 100, orange);
        var tennAvenue = new TileProperty(CurrentGame, "Tennessee Avenue", new[] { 14, 70, 200, 550, 750, 950 }, 90,
            180, 100, orange);
        var nyAvenue = new TileProperty(CurrentGame, "New York Avenue", new[] { 16, 80, 220, 600, 800, 1000 }, 100, 200,
            100, orange);
        var kentAvenue = new TileProperty(CurrentGame, "Kentucky Avenue", new[] { 18, 90, 250, 700, 875, 1050 }, 110,
            220, 150, red);
        var indiAvenue = new TileProperty(CurrentGame, "Indiana Avenue", new[] { 18, 90, 250, 700, 875, 1050 }, 110,
            220, 150, red);
        var illiAvenue = new TileProperty(CurrentGame, "Illinois Avenue", new[] { 20, 100, 300, 750, 925, 1100 }, 120,
            240, 150, red);
        var atlAvenue = new TileProperty(CurrentGame, "Atlantic Avenue", new[] { 22, 110, 330, 800, 975, 1150 }, 130,
            260, 150, yellow);
        var ventnAvenue = new TileProperty(CurrentGame, "Ventnor Avenue", new[] { 22, 110, 330, 800, 975, 1150 }, 130,
            260, 150, yellow);
        var marvinGardens = new TileProperty(CurrentGame, "Marvin Gardens", new[] { 24, 120, 360, 850, 1025, 1200 },
            140, 280, 150, yellow);
        var pacifAvenue = new TileProperty(CurrentGame, "Pacific Avenue", new[] { 26, 130, 390, 900, 1100, 1275 }, 150,
            300, 200, green);
        var northCaAvnue = new TileProperty(CurrentGame, "North Carolina Avenue",
            new[] { 26, 130, 390, 900, 1100, 1275 }, 150, 300, 200, green);
        var pennsyAvenue = new TileProperty(CurrentGame, "Pennsylvania Avenue",
            new[] { 28, 150, 450, 1000, 1200, 1400 }, 160, 320, 200, green);
        var parkPlace = new TileProperty(CurrentGame, "Park Place", new[] { 35, 175, 500, 1100, 1300, 1500 }, 175, 350,
            200, blue);
        _boardwalk = new TileProperty(CurrentGame, "Boardwalk", new[] { 50, 200, 600, 1400, 1700, 2000 },
            200, 400, 200, blue);

        purple.Streets.Add(medAvenue);
        purple.Streets.Add(balticAvenue);
        gray.Streets.Add(orienAvenue);
        gray.Streets.Add(vermAvenue);
        gray.Streets.Add(connecAvenue);
        pink.Streets.Add(stCharlesPlace);
        pink.Streets.Add(statesAvenue);
        pink.Streets.Add(virginAvenue);
        orange.Streets.Add(stJamesPlace);
        orange.Streets.Add(tennAvenue);
        orange.Streets.Add(nyAvenue);
        red.Streets.Add(kentAvenue);
        red.Streets.Add(indiAvenue);
        red.Streets.Add(illiAvenue);
        yellow.Streets.Add(atlAvenue);
        yellow.Streets.Add(ventnAvenue);
        yellow.Streets.Add(marvinGardens);
        green.Streets.Add(pacifAvenue);
        green.Streets.Add(northCaAvnue);
        green.Streets.Add(pennsyAvenue);
        blue.Streets.Add(parkPlace);
        blue.Streets.Add(_boardwalk);

        CurrentGame.JailVisit = new TileJailVisit(CurrentGame, "Jail visit");
        CurrentGame.Jail = new TileJail(CurrentGame, "Jail");
        _goToJail = new TileGoToJail(CurrentGame, "Go to jail");

        CurrentGame.Start = new TileStart(CurrentGame, "Start");
        _boardwalk.NextTile = CurrentGame.Start;
        CurrentGame.Start.PreviousTile = _boardwalk;

        board.Add(_boardwalk);
        board.Add(new TileTaxes(CurrentGame, "Luxury Tax"));
        board.Add(parkPlace);
        board.Add(new TileChance(CurrentGame, "Chance Card"));
        board.Add(new TileRailRoad(CurrentGame, "Short Line", new[] { 25, 50, 100, 200 }, 100, 200));
        board.Add(pennsyAvenue);
        board.Add(new TileCommunity(CurrentGame, "Community Chest"));
        board.Add(northCaAvnue);
        board.Add(pacifAvenue);
        board.Add(_goToJail);
        board.Add(marvinGardens);
        board.Add(new TileCompany(CurrentGame, "Water Works", 75, 150));
        board.Add(ventnAvenue);
        board.Add(atlAvenue);
        board.Add(new TileRailRoad(CurrentGame, "B&O Railroad", new[] { 25, 50, 100, 200 }, 100, 200));
        board.Add(illiAvenue);
        board.Add(indiAvenue);
        board.Add(new TileChance(CurrentGame, "Chance Card"));
        board.Add(kentAvenue);
        board.Add(new TileFreeParking(CurrentGame, "Free Parking"));
        board.Add(nyAvenue);
        board.Add(tennAvenue);
        board.Add(new TileCommunity(CurrentGame, "Community Chest"));
        board.Add(stJamesPlace);
        board.Add(new TileRailRoad(CurrentGame, "Pennsylvania Railroad", new[] { 25, 50, 100, 200 }, 100, 200));
        board.Add(virginAvenue);
        board.Add(statesAvenue);
        board.Add(new TileCompany(CurrentGame, "Electric Company", 75, 150));
        board.Add(stCharlesPlace);
        board.Add(CurrentGame.JailVisit);
        board.Add(connecAvenue);
        board.Add(vermAvenue);
        board.Add(new TileChance(CurrentGame, "Chance Card"));
        board.Add(orienAvenue);
        board.Add(new TileRailRoad(CurrentGame, "Reading Railroad", new[] { 25, 50, 100, 200 }, 100, 200));
        board.Add(new TileTaxes(CurrentGame, "Income Tax"));
        board.Add(balticAvenue);
        board.Add(new TileCommunity(CurrentGame, "Community Card"));
        board.Add(medAvenue);
        board.Add(CurrentGame.Start);

        return board;
    }

    /// <summary>
    ///     загрузка игроков из файла сохранения
    /// </summary>
    /// <param name="savedGameLocation"></param>
    /// <returns></returns>
    public ObservableCollection<Player> GetAllPlayers(string savedGameLocation)
    {
        var players = new ObservableCollection<Player>();
        string line;
        var reader = new StreamReader(savedGameLocation);
        while ((line = reader.ReadLine()) != null)
        {
            var buildingList = new ObservableCollection<Tile>();
            var playerinfo = line.Split('@');

            var current = CurrentGame.Board.Head;
            Player player = null;

            for (var i = 0; i < CurrentGame.Board.Size; i++)
            {
                if (current.Description.Equals(playerinfo[2]))
                    player = new Player(CurrentGame, playerinfo[0], int.Parse(playerinfo[1]), current);

                foreach (var s in playerinfo[3].Split('-'))
                    if (current.Description.Equals(s.Split('!')[0]))
                    {
                        var tile = (TileBuyable)current;
                        tile.TotalUpgrades = int.Parse(s.Split('!')[1]);
                        tile.HasOwner = true;

                        if (tile.TotalUpgrades > 0) tile.HasBuildings = true;
                        buildingList.Add(tile);
                    }

                current = current.NextTile;
            }

            if (player != null)
            {
                player.Streets = buildingList;

                foreach (var tile in player.Streets.Cast<TileBuyable>().Where(tile => tile != null))
                    tile.Owner = player;

                players.Add(player);
            }
        }

        return players;
    }

    /// <summary>
    ///     сохранить игру в файл
    /// </summary>
    /// <param name="filename"></param>
    public void SaveData(string filename)
    {
        TextWriter writer = new StreamWriter(filename);
        foreach (var player in CurrentGame.Players)
        {
            var buildinglist = player.Streets.Cast<TileBuyable>().Aggregate(string.Empty,
                (current, tile) => current + tile.Description + "!" + tile.TotalUpgrades + "-");
            writer.WriteLine(player.Name + "@" + player.Money + "@" + player.CurrentTile.Description + "@" +
                             buildinglist);
        }

        writer.Close();
    }
}