namespace HeroBot.Plugins.RP.Entities
{
    public class RPUser
    {
        public string Id { get; set; }
        public string CityId { get; set; } = null;
        public string Description { get; set; } = "*no description*";
        public string Website { get; set; } = null;
        public string Emoji { get; set; } = "😊";
        public string Personality { get; set; } = null;
        public long Likes { get; set; } = 0;
        public Jobs Job { get; set; } = Jobs.NO_JOB;
        public long Money { get; set; } = 10;
    }
}
