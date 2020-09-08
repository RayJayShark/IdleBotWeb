using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdleBotWeb.Models
{
    public class Item
    {
        public uint Id { get; set; }
        public string Name { get; set; }
        public uint Value { get; set; }
        public uint Cost { get; set; }
    }
}
