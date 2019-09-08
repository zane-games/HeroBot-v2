using FluentMigrator;

namespace HeroBot.Plugins.RemindMe.Migrations
{
    [Migration(1807201925589)]
    public class _1807201925589 : Migration
    {
        public override void Down()
        {
            Delete.Table("Reminders");
        }

        public override void Up()
        {
            Create.Table("Reminders")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("userId").AsInt64()
                .WithColumn("reason").AsString();
        }
    }
}
