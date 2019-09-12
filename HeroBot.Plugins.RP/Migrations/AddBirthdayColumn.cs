using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeroBot.Plugins.RP.Migrations
{
    [Migration(11092019)]
    public class AddBirthdayColumn : Migration
    {
        public override void Down()
        {
        }

        public override void Up()
        {
            Alter.Table("RPUser")
                .AddColumn("Birthday").AsDate().Nullable();
        }
    }
}
