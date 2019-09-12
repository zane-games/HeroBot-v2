using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace HeroBot.Plugins.RP.Entities
{
    public class RPUser
    {
        public string UserId { get; set; }
        public string CityId { get; set; } = string.Empty;
        public string Description { get; set; } = "*no description*";
        public string Website { get; set; } = string.Empty;
        public string Emoji { get; set; } = "😊";
        public string Personality { get; set; } = string.Empty;
        public long Likes { get; set; } = 0;
        public Jobs Job { get; set; } = Jobs.NO_JOB;
        public long Money { get; set; } = 10;
        public DateTime? Birthday { get; set; } = null;
    }
}