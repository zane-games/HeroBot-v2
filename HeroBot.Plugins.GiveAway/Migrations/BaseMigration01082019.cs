using FluentMigrator;

namespace HeroBot.Plugins.GiveAway.Migrations
{
    [Migration(01082019)]
    public class BaseMigration01082019 : Migration
    {
        public override void Down()
        {
            Delete.Table("GiveAways");
        }

        public override void Up()
        {
            Create.Table("GiveAways")
                .WithColumn("Id").AsInt16().Identity().PrimaryKey()
                .WithColumn("winners").AsInt64()
                .WithColumn("message").AsInt64()
                .WithColumn("channel").AsInt64()
                .WithColumn("price").AsString();
        }
    }
}
