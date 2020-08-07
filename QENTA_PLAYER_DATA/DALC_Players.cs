using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using QENTA_PLAYER_ENTITIES;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace QENTA_PLAYER_DATA
{
    public class DALC_Players
    {
        private string URL_Service_Player = "https://www.balldontlie.io/api/v1/players";
        private List<E_players> Players = new List<E_players>();

        public DALC_Players()
        {
            
        }
        
        private List<E_players> ObtenerJugadores()
        {
            string stringJson = new WebClient().DownloadString(URL_Service_Player);

            dynamic jsonObj = JsonConvert.DeserializeObject(stringJson);

            Players = new List<E_players>();

            foreach (var obj in jsonObj.data)
            {
                E_players player = new E_players()
                {
                    Id = obj.id,
                    First_name = obj.first_name,
                    Last_nam = obj.last_name,
                    Position = obj.position,
                    Height_feet = obj.height_feet,
                    Height_inches = obj.height_inches,
                    Weight_pounds = obj.weight_pounds,
                    Team = new E_Team()
                    {
                        Id = obj.id,
                        Abbreviation = obj.abbreviation,
                        City = obj.city,
                        Conference = obj.conference,
                        Division = obj.division,
                        Full_name = obj.full_name,
                        Name = obj.name,
                    }
                };

                Players.Add(player);
            }

            string path = @"Players.json";
            System.IO.File.WriteAllText(path, JsonConvert.SerializeObject(Players));

            return Players;
        }

        public List<E_players> GetPlayers()
        {
            List<E_players> _players = ReadJsonFile();
            return _players;
        }

        public E_players GetPlayer(int idPlayer)
        {
            List<E_players> _players = ReadJsonFile();

            E_players player = _players.FirstOrDefault(p => p.Id == idPlayer);

            if (player == null)
            {
                return null;
            }

            return player;
        }

        public E_players UpdatePlayer(E_players newPlayer)
        {
            if(newPlayer != null)
            {
                List<E_players> _players = ReadJsonFile();
                E_players currentPlayer = _players.FirstOrDefault(p => p.Id == newPlayer.Id);

                if (currentPlayer != null)
                {
                    Players.Remove(currentPlayer);

                    currentPlayer.First_name = newPlayer.First_name;
                    currentPlayer.Last_nam = newPlayer.Last_nam;
                    currentPlayer.Position = newPlayer.Position;
                    currentPlayer.Height_feet = newPlayer.Height_feet;
                    currentPlayer.Height_inches = newPlayer.Height_inches;
                    currentPlayer.Weight_pounds = newPlayer.Weight_pounds;

                    Players.Add(newPlayer);

                    string path = @"Players.json";
                    System.IO.File.Delete(path);
                    System.IO.File.WriteAllText(path, JsonConvert.SerializeObject(Players));

                    return newPlayer;
                }
            }

            return null;
        }

        private List<E_players> ReadJsonFile()
        {
            string path = @"Players.json";
            using (StreamReader jsonStream = File.OpenText(path))
            {
                var json = jsonStream.ReadToEnd();
                List<E_players> players = JsonConvert.DeserializeObject<List<E_players>>(json);
                return players;
            }
        }
    }
}
