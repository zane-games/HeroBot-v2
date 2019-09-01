namespace HeroBot.Plugins.RP.Entities
{
    public class RPUser
    {
        public string Id { get; set; }
        public string CityId { get; set; }
        public string Description { get; set; }
        public string Website { get; set; }
        public string Emoji { get; set; }
        public string Personality { get; set; }
        public long Likes { get; set; }
        public Jobs Job { get; set; }
        public long Money { get; set; }
    }
}
