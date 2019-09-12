using Discord;
using Discord.Addons.Interactive;
using Discord.Commands;
using HeroBot.Common.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroBot.Plugins.HeroBot.Modules
{
    public class InteractivityModule : InteractiveBase
    {
        [NeedPlugin()]
        [Command("serverList")]
        public async Task ServerList()
        {
            var pages = Context.Client.Guilds
                .ToList()
                .ChunkBy(5)
                .Select(x =>
                {
                    return new PaginatedMessage.Page()
                    {
                        Fields = x.Select(x =>
                        {
                            return new EmbedFieldBuilder()
                            {
                                IsInline = false,
                                Name = x.Name,
                                Value = $"{x.MemberCount} members."
                            };
                        }).ToList(),

                    };
                });
            await PagedReplyAsync(new PaginatedMessage()
            {
                Pages = pages,
            }, new ReactionList
            {
                Forward = true,
                Backward = true,
                Jump = true,
                Trash = true
            });
        }
    }
}
