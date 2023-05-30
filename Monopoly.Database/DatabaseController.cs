using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Monopoly.Database.Models;
using Monopoly.Logic;

namespace Monopoly.Database
{
    public class DatabaseController
    {
        private ApplicationContext _context;
        private static DatabaseController instance;

        private DatabaseController()
        {
            _context = new ApplicationContext();
        }   

        public static DatabaseController getInstance()
        {
            if (instance == null)
                instance = new DatabaseController();
            return instance;
        }

        public async Task AddPlayer(string name)
        {
            var r = _context.Players.ToList();
            if (!_context.Players.Where(x => x.Name == name).ToList().Any())
            await _context.Players.AddAsync(new Models.Player( name));
            _context.SaveChanges();
        }

        public async Task<bool> ExistPlayer(string name)
        {
            return await _context.Players.AnyAsync(x => x.Name == name);
        }
        public async Task<Models.Player> ReturnPlayer(string name)
        {
            if (await ExistPlayer(name))
            {
                return await _context.Players.SingleAsync(x => x.Name == name);
            }
            return null;
        }
        public async Task AddSavedGame(string json)
        {
            await _context.SavedGames.AddAsync(new SaveGameModel(json));
            _context.SaveChanges();
        }
        public void RemoveSavedGame(SaveGameModel saveGame)
        {
            _context.SavedGames.Remove(saveGame);
            _context.SaveChanges();
        }

        public async Task<SaveGameModel> ReturnSavedGame(int id)
        {
            return await _context.SavedGames.SingleAsync(x => x.Id == id);
        }
        public async Task AddStatictics(Statictics statictics)
        {
            await _context.Statictics.AddAsync(statictics);
            _context.SaveChanges();
        }
    }
}
