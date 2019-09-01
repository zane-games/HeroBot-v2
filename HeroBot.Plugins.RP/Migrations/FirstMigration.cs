using FluentMigrator;

namespace HeroBot.Plugins.RP.Migrations
{
    [Migration(02082019)]
    public class FirstMigration : Migration
    {
        public override void Down()
        {
            Delete.Table("RPUsers");
        }

        public override void Up()
        {
            Create.Table("City")
                .WithColumn("Id").AsString().Identity()
                .WithColumn("OwnerId").AsString().Unique()
                .WithColumn("Name").AsString().Unique()
                .WithColumn("Description").AsString()
                .WithColumn("Money").AsInt64();
            Create.Table("RPUser")
                .WithColumn("Id").AsString().Identity()
                .WithColumn("CityId").AsString().Nullable()
                .WithColumn("Description").AsString()
                .WithColumn("Website").AsString()
                .WithColumn("Emoji").AsFixedLengthString(2)
                .WithColumn("Personality").AsFixedLengthString(50)
                .WithColumn("Likes").AsInt64()
                .WithColumn("Job").AsInt32()
                .WithColumn("Money").AsInt64();
        }
    }
}
