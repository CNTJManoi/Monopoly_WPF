using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monopoly.Database.Models;
using Monopoly.Logic;

namespace Monopoly.Database
{
    public class DatabaseController
    {
        private ApplicationContext _context;

        public DatabaseController()
        {
            _context = new ApplicationContext();
        }

        public async void AddPlayer(Player player)
        {
            if(!_context.Players.Where(x => x.Name == player.Name).ToList().Any())
            await _context.Players.AddAsync(player);
            _context.SaveChanges();
        }
        public async void AddSavedGame(string json)
        {
            await _context.SavedGames.AddAsync(new SaveGameModel(new Guid(), json));
            _context.SaveChanges();
        }
        public void RemoveSavedGame(SaveGameModel saveGame)
        {
            _context.SavedGames.Remove(saveGame);
            _context.SaveChanges();
        }
        public async void AddStatictics(Statictics statictics)
        {
            await _context.Statictics.AddAsync(statictics);
            _context.SaveChanges();
        }
    }
}
