using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeroBot.Plugins.RP.Migrations
{
    [Migration(04092019)]
    public class AddBadgeColumnMigration : Migration
    {
        public override void Down()
        {
        }

        public override void Up()
        {
            Alter.Table("RPUser")
                .AddColumn("Badges").AsString().WithDefaultValue("[]");
        }
    }
}
