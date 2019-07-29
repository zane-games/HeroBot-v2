using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeroBot.Core.Migrations
{
    [Migration(270720191)]
    public class Migration1_270720191 : Migration
    {
        public override void Down()
        {
            Delete.Table("Plugins");
        }

        public override void Up()
        {
            Create.Table("Plugins")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey()
                .WithColumn("Name").AsString().Unique();
            Create.Table("Guilds")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey();
            Create.Table("GuildPlugin")
                .WithColumn("Id").AsInt32().Identity().PrimaryKey()
                .WithColumn("Guild").AsInt32().ForeignKey("Guilds", "Id")
                .WithColumn("Plugin").AsInt32().ForeignKey("Plugins", "Id");
        }
    }
}
