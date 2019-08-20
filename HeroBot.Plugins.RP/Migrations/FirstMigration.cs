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
            Create.Table("RPUsers")
                .WithColumn("Id").AsInt32().Identity().Unique()
                .WithColumn("Gold").AsInt32().WithDefaultValue(3)
                .WithColumn("Bolts").AsInt32().WithDefaultValue(20);
        }
    }
}
