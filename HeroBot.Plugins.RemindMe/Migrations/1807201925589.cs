using FluentMigrator;

namespace HeroBot.Plugins.RemindMe.Migrations
{
    [Migration(1807201925589)]
#pragma warning disable IDE1006
    public class _1807201925589 : Migration
#pragma warning enable IDE1006
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
