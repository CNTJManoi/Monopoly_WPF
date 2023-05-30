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
    public TileLinkedList LoadDefaultBoard()
    {
        var board = new TileLinkedList();

        ICityFactory cityFactory = new ConcreteCityFactory();
        ITilePropertyFactory tilePropertyFactory = new ConcreteTilePropertyFactory();

        var purple = cityFactory.CreateCity();
        var gray = cityFactory.CreateCity();
        var pink = cityFactory.CreateCity();
        var orange = cityFactory.CreateCity();
        var red = cityFactory.CreateCity();
        var yellow = cityFactory.CreateCity();
        var green = cityFactory.CreateCity();
        var blue = cityFactory.CreateCity();

        var medAvenue = tilePropertyFactory.CreateTileProperty(CurrentGame,"Mediterranean Avenue", new[] { 2, 10, 30, 90, 160, 250 }, 30, 60, 50, purple);
        var balticAvenue = tilePropertyFactory.CreateTileProperty(CurrentGame, "Baltic Avenue", new[] { 4, 20, 60, 180, 320, 450 }, 30, 60, 50, purple);
        var orienAvenue = tilePropertyFactory.CreateTileProperty(CurrentGame, "Oriental Avenue", new[] { 6, 30, 90, 270, 400, 550 }, 50, 100, 50, gray);
        var vermAvenue = tilePropertyFactory.CreateTileProperty(CurrentGame, "Vermont Avenue", new[] { 6, 30, 90, 270, 400, 550 }, 50, 100, 50, gray);
        var connecAvenue = tilePropertyFactory.CreateTileProperty(CurrentGame, "Connecticut Avenue", new[] { 8, 40, 100, 300, 450, 600 }, 60, 120, 50, gray);
        var stCharlesPlace = tilePropertyFactory.CreateTileProperty(CurrentGame, "St. Charles Place", new[] { 10, 50, 150, 450, 625, 750 }, 70, 140, 100, pink);
        var statesAvenue = tilePropertyFactory.CreateTileProperty(CurrentGame, "States Avenue", new[] { 10, 50, 150, 450, 625, 750 }, 70, 140, 100, pink);
        var virginAvenue = tilePropertyFactory.CreateTileProperty(CurrentGame, "Virginia Avenue", new[] { 12, 60, 180, 500, 700, 900 }, 80, 160, 100, pink);
        var stJamesPlace = tilePropertyFactory.CreateTileProperty(CurrentGame, "St. James Place", new[] { 14, 70, 200, 550, 750, 950 }, 90, 180, 100, orange);
        var tennAvenue = tilePropertyFactory.CreateTileProperty(CurrentGame, "Tennessee Avenue", new[] { 14, 70, 200, 550, 750, 950 }, 90, 180, 100, orange);
        var nyAvenue = tilePropertyFactory.CreateTileProperty(CurrentGame, "New York Avenue", new[] { 16, 80, 220, 600, 800, 1000 }, 100, 200, 100, orange);
        var kentAvenue = tilePropertyFactory.CreateTileProperty(CurrentGame, "Kentucky Avenue", new[] { 18, 90, 250, 700, 875, 1050 }, 110, 220, 150, red);
        var indiAvenue = tilePropertyFactory.CreateTileProperty(CurrentGame, "Indiana Avenue", new[] { 18, 90, 250, 700, 875, 1050 }, 110, 220, 150, red);
        var illiAvenue = tilePropertyFactory.CreateTileProperty(CurrentGame, "Illinois Avenue", new[] { 20, 100, 300, 750, 925, 1100 }, 120, 240, 150, red);
        var atlAvenue = tilePropertyFactory.CreateTileProperty(CurrentGame, "Atlantic Avenue", new[] { 22, 110, 330, 800, 975, 1150 }, 130, 260, 150, yellow);
        var ventnAvenue = tilePropertyFactory.CreateTileProperty(CurrentGame, "Ventnor Avenue", new[] { 22, 110, 330, 800, 975, 1150 }, 130, 260, 150, yellow);
        var marvinGardens = tilePropertyFactory.CreateTileProperty(CurrentGame, "Marvin Gardens", new[] { 24, 120, 360, 850, 1025, 1200 }, 140, 280, 150, yellow);
        var pacifAvenue = tilePropertyFactory.CreateTileProperty(CurrentGame, "Pacific Avenue", new[] { 26, 130, 390, 900, 1100, 1275 }, 150, 300, 200, green);
        var northCaAvnue = tilePropertyFactory.CreateTileProperty(CurrentGame, "North Carolina Avenue", new[] { 26, 130, 390, 900, 1100, 1275 }, 150, 300, 200, green);
        var pennsyAvenue = tilePropertyFactory.CreateTileProperty(CurrentGame, "Pennsylvania Avenue", new[] { 28, 150, 450, 1000, 1200, 1400 }, 160, 320, 200, green);
        var parkPlace = tilePropertyFactory.CreateTileProperty(CurrentGame, "Park Place", new[] { 35, 175, 500, 1100, 1300, 1500 }, 175, 350, 200, blue);
        _boardwalk = tilePropertyFactory.CreateTileProperty(CurrentGame, "Boardwalk", new[] { 50, 200, 600, 1400, 1700, 2000 }, 200, 400, 200, blue);
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

        IGameFactory gameFactory = new ConcreteGameFactory();
        IBoardBuilder boardBuilder = gameFactory.CreateBoardBuilder();
        boardBuilder.SetBoard(board)
            .AddTileProperty(_boardwalk)
            .AddTile(new TileTaxes(CurrentGame, "Luxury Tax"))
            .AddTileProperty(parkPlace)
            .AddTile(new TileChance(CurrentGame, "Chance Card"))
            .AddTile(new TileRailRoad(CurrentGame, "Short Line", new[] { 25, 50, 100, 200 }, 100, 200))
            .AddTileProperty(pennsyAvenue)
            .AddTile(new TileCommunity(CurrentGame, "Community Chest"))
            .AddTileProperty(northCaAvnue)
            .AddTileProperty(pacifAvenue)
            .AddTile(_goToJail)
            .AddTileProperty(marvinGardens)
            .AddTile(new TileCompany(CurrentGame, "Water Works", 75, 150))
            .AddTileProperty(ventnAvenue)
            .AddTileProperty(atlAvenue)
            .AddTile(new TileRailRoad(CurrentGame, "B&O Railroad", new[] { 25, 50, 100, 200 }, 100, 200))
            .AddTileProperty(illiAvenue)
            .AddTileProperty(indiAvenue)
            .AddTile(new TileChance(CurrentGame, "Chance Card"))
            .AddTileProperty(kentAvenue)
            .AddTile(new TileFreeParking(CurrentGame, "Free Parking"))
            .AddTileProperty(nyAvenue)
            .AddTileProperty(tennAvenue)
            .AddTile(new TileCommunity(CurrentGame, "Community Chest"))
            .AddTileProperty(stJamesPlace)
            .AddTile(new TileRailRoad(CurrentGame, "Pennsylvania Railroad", new[] { 25, 50, 100, 200 }, 100, 200))
            .AddTileProperty(virginAvenue)
            .AddTileProperty(statesAvenue)
            .AddTile(new TileCompany(CurrentGame, "Electric Company", 75, 150))
            .AddTileProperty(stCharlesPlace)
            .AddTile(CurrentGame.JailVisit)
            .AddTileProperty(connecAvenue)
            .AddTileProperty(vermAvenue)
            .AddTile(new TileChance(CurrentGame, "Chance Card"))
            .AddTileProperty(orienAvenue)
            .AddTile(new TileRailRoad(CurrentGame, "Reading Railroad", new[] { 25, 50, 100, 200 }, 100, 200))
            .AddTile(new TileTaxes(CurrentGame, "Income Tax"))
            .AddTileProperty(balticAvenue)
            .AddTile(new TileCommunity(CurrentGame, "Community Card"))
            .AddTileProperty(medAvenue)
            .AddTile(CurrentGame.Start)
            .Build();

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