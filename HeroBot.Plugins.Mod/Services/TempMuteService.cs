using Discord.WebSocket;
using HeroBot.Common.Attributes;
using HeroBot.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HeroBot.Plugins.Mod.Services
{
    [Service]
    public class TempMuteService
    {
        private readonly IRedisService _redis;
        private readonly DiscordShardedClient _discord;

        public TempMuteService(IRedisService redisService, Random random, IConfigurationRoot o, DiscordShardedClient discordSocketClient)
        {
            _redis = redisService;
            _discord = discordSocketClient;
            _redis.GetSubscriber().SubscribeAsync($"__keyevent@{o.GetSection("redis")["database"]}__:expired", OnKeyRemove).Wait();
        }

        private void OnKeyRemove(RedisChannel arg1, RedisValue arg2)
        {
            Console.WriteLine(arg2.ToString());
            var name = arg2.ToString().Split(':');
            Console.WriteLine(JsonConvert.SerializeObject(name));
            if (name.Length == 4 && name[0] == "tempmute" && name[1] == "remove")
            {
                    var guildId = name[3];
                    var userId = name[2];
                    var guild = _discord.GetGuild(ulong.Parse(guildId));
                    if (guild != null) {
                        var user = guild.GetUser(ulong.Parse(userId));
                        user.GetOrCreateDMChannelAsync().ContinueWith((dm) => {
                            try
                            {
                                dm.Result.TriggerTypingAsync().Wait();
                                dm.Result.SendMessageAsync($"Hey {user.Mention} ! You have been **unmuted** in `{guild.Name}`");
                            }
                            catch (Exception)
                            {
                                /* We ignore the expressions in the redis handler because, finally, we need to unmute the user */
                            }
                            finally
                            {
                                foreach (SocketCategoryChannel socketTextChannel in guild.CategoryChannels)
                                {
                                    if (socketTextChannel.GetPermissionOverwrite(user) != null)
                                        socketTextChannel.RemovePermissionOverwriteAsync(user).Wait();
                                }
                                foreach (SocketTextChannel socketTextChannel1 in guild.TextChannels)
                                {
                                    if (socketTextChannel1.GetPermissionOverwrite(user) != null)
                                        socketTextChannel1.RemovePermissionOverwriteAsync(user).Wait();
                                }
                            }
                        });
                    }
                
            }
        }

        public async Task<bool> CreatetempMute(TempMute reminder)
        {
            if (!await _redis.GetDatabase().KeyExistsAsync($"tempmute:remove:{reminder.userId}:{reminder.guildId}"))
            {
                await _redis.GetDatabase().StringSetAsync($"tempmute:remove:{reminder.userId}:{reminder.guildId}", String.Empty, reminder.TimeSpan);
                return true;
            }
            else return false;
        }
    }
    public class TempMute {
        public TimeSpan TimeSpan { get; set; }
        public string Reason { get; set; }
        public ulong guildId { get; set; }
        public ulong userId { get; set; }
    }
}
