using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly.Database.Models
{
    public class SaveGameModel
    {
        public SaveGameModel(string jsonSaveGame)
        {
            JsonSaveGame = jsonSaveGame;
        }
        public int Id { get; set; }
        public string JsonSaveGame { get; }
    }
}
