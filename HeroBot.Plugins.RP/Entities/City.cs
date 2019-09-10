using System;
using System.Collections.Generic;
using System.Text;

namespace HeroBot.Plugins.RP.Entities
{
    class City
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string OwnerId {get;set;}
        public string Description { get; set; } = "*no description*";
        public long Money { get; set; } = 10;
    }
}
