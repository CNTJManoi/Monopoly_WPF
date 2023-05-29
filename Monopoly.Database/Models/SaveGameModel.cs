using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Database.Models
{
    public class SaveGameModel
    {
        public SaveGameModel(Guid id, string jsonSaveGame)
        {
            Id = id;
            JsonSaveGame = jsonSaveGame;
        }

        public Guid Id { get; }
        public string JsonSaveGame { get; }
    }
}
