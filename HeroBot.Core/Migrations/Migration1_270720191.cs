using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeroBot.Core.Migrations
{
    [Migration(270720191)]
    public class Migration1270720191 : Migration
    {
        public override void Down()
        {
            Delete.Table("GuildPlugin");
        }

        public override void Up()
        {
            Create.Table("GuildPlugin")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey()
                .WithColumn("plugin").AsString()
                .WithColumn("guild").AsInt64()
                .WithColumn("Config").AsString().Nullable();
        }
    }
}
