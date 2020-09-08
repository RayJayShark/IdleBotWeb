using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdleBotWeb.Models
{
    public class Player
    {
        public ulong Id { get; set; }
        public string Avatar { get; set; }
        public string Name { get; set; }
        public string Faction { get; set; }
        public string Class { get; set; }
        public uint CurrentHp { get; set; }
        public uint Money { get; set; }
        public uint Level { get; set; }
        public uint Experience { get; set; }
        public ushort HealthStat { get; set; }
        public ushort StrengthStat { get; set; }
        public ushort DefenseStat { get; set; }
        public Dictionary<uint, uint> Inventory { get; set; }   // <itemId, quantity>
    }
}
