using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Dapper;
using IdleBotWeb.Models;
using Microsoft.AspNetCore.Connections.Features;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using Exception = System.Exception;

namespace IdleBotWeb.Services
{
    public class DatabaseService
    {
        private string ConnectionString { get; set; }

        public DatabaseService(IConfiguration config)
        {
            ConnectionString =
                $"server={config["Server"]};" +
                $"user={config["User"]};" +
                $"password={config["Password"]};" +
                $"database={config["Name"]};" +
                $"port={config["Port"]}";
        }

        public string AccessDatabase()
        {
            using var connection = new MySqlConnection(ConnectionString);
            try
            {
                connection.Open();
                return "Connected to database!";
            }
            catch (Exception ex)
            {
                return "Failed to connect to database :(";
            }
            finally
            {
                connection.Close();
            }
        }

        public IEnumerable<Player> GetAllPlayers()
        {
            using var connection = new MySqlConnection(ConnectionString);
            try
            {
                connection.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to connect to database");
                Console.WriteLine(ex.ToString());
                return null;
            }

            var players =
                connection.Query<Player>(
                    "SELECT id, avatar, name, faction, class, currentHp, money, level, experience, healthStat, strengthStat, defenseStat FROM player");
            connection.Close();
            return players;
        }

        public IEnumerable<Player> GetAllPlayersWithInventory()
        {
            using var connection = new MySqlConnection(ConnectionString);
            try
            {
                connection.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to connect to database");
                Console.WriteLine(ex.ToString());
                return null;
            }

            var players =
                connection.Query<Player>(
                    "SELECT id, avatar, name, faction, class, currentHp, money, level, experience, healthStat, strengthStat, defenseStat FROM player");
            var inventories = connection.Query<(ulong, uint, uint)>("SELECT playerId, itemId, quantity FROM inventory");

            foreach (var (playerId, itemId, quantity) in inventories)
            {
                players.First(p => p.Id == playerId).Inventory.Add(itemId, quantity);
            }

            connection.Close();
            return players;
        }

        public Player GetPlayer(ulong playerId)
        {
            using var connection = new MySqlConnection(ConnectionString);
            try
            {
                connection.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to connect to database");
                Console.WriteLine(ex.ToString());
                return null;
            }

            var player = connection.QueryFirst<Player>(
                $"SELECT id, avatar, name, faction, class, currentHp, money, level, experience, healthStat, strengthStat, defenseStat FROM player WHERE id = {playerId}");
            connection.Close();
            return player;
        }

        public Player GetPlayerWithInventory(ulong playerId)
        {
            using var connection = new MySqlConnection(ConnectionString);
            try
            {
                connection.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to connect to database");
                Console.WriteLine(ex.ToString());
                return null;
            }

            var player = connection.QueryFirst<Player>(
                $"SELECT id, avatar, name, faction, class, currentHp, money, level, experience, healthStat, strengthStat, defenseStat FROM player WHERE id = {playerId}");

            var inventory = connection.Query<(uint, uint)>($"SELECT itemId, quantity FROM inventory WHERE playerId = {playerId}");
            foreach (var (id, quantity) in inventory)
            {
                player.Inventory.Add(id, quantity);
            }

            connection.Close();
            return player;
        }

        public bool GivePlayerItem(ulong playerId, uint itemId)
        {
            using var connection = new MySqlConnection(ConnectionString);
            try {
                connection.Open();
            }
            catch (Exception ex) {
                Console.WriteLine("Failed to connect to database");
                Console.WriteLine(ex.ToString());
                return false;
            }

            try
            {
                var itemCount = connection.QueryFirst<uint>(
                    $"SELECT quantity FROM inventory WHERE playerId = {playerId} AND itemId = {itemId}");


                var affectedRows = connection.Execute($"UPDATE inventory SET quantity = {itemCount + 1} WHERE playerId = {playerId} AND itemId = {itemId}");
                return affectedRows > 0;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return false;
            }
        }

        public bool TakePlayerMoney(ulong playerId, uint moneyToTake)
        {
            using var connection = new MySqlConnection(ConnectionString);
            try {
                connection.Open();
            }
            catch (Exception ex) {
                Console.WriteLine("Failed to connect to database");
                Console.WriteLine(ex.ToString());
                return false;
            }

            try
            {
                var currentMoney = connection.QueryFirst<uint>($"SELECT money FROM player WHERE id = {playerId}");
                if (currentMoney < moneyToTake)
                    return false;

                var affectedRows =
                    connection.Execute(
                        $"UPDATE player SET money = {currentMoney - moneyToTake} WHERE id = {playerId}");
                return affectedRows > 0;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return false;
            }
        }

        public void UpdateAvatar(ulong playerId, string avatarUrl)
        {
            using var connection = new MySqlConnection(ConnectionString);
            try
            {
                connection.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to connect to database");
                Console.WriteLine(ex.ToString());
            }

            connection.Execute($"UPDATE player SET avatar = '{avatarUrl}' WHERE id = '{playerId}'");
            connection.Close();
        }

        public List<Item> GetItems()
        {
            using var connection = new MySqlConnection(ConnectionString);
            try
            {
                connection.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to connect to database");
                Console.WriteLine(ex.ToString());
            }

            var items = connection.Query<Item>("SELECT * FROM item");
            return items.ToList();
        }
    }
}
