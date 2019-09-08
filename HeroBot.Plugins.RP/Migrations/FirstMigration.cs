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
                .WithColumn("OwnerId").AsString().PrimaryKey()
                .WithColumn("Name").AsString().Unique()
                .WithColumn("Description").AsString()
                .WithColumn("Money").AsInt64();
            Create.Table("RPUser")
                .WithColumn("UserId").AsString().PrimaryKey()
                .WithColumn("CityId").AsString().Nullable()
                .WithColumn("Description").AsString()
                .WithColumn("Website").AsString().Nullable()
                .WithColumn("Emoji").AsFixedLengthString(2)
                .WithColumn("Personality").AsFixedLengthString(50)
                .WithColumn("Likes").AsInt64()
                .WithColumn("Job").AsInt32()
                .WithColumn("Money").AsInt64();
        }
    }
}
