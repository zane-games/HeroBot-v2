using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeroBot.Plugins.RP.Migrations
{
    [Migration(10092019)]
    public class RemoveBadgeColumn : Migration
    {
        public override void Down()
        {
            
        }

        public override void Up()
        {
            /**
             * Deleting the badge columtn, because the shop is comming ^^ 
            **/
            Create.Table("BadgeBuy")
                .WithColumn("Id").AsString().PrimaryKey()
                .WithColumn("BadgeId").AsString()
                .WithColumn("UserId").AsString();
            Create.Table("Badges")
                .WithColumn("Id").AsString().PrimaryKey().ReferencedBy("BadgeBuy", "BadgeId")
                .WithColumn("Emoji").AsString().Unique();
        }
    }
}
