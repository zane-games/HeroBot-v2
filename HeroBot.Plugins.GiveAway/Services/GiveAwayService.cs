using Dapper;
using Discord;
using Discord.WebSocket;
using HeroBot.Common.Attributes;
using HeroBot.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Timers;

namespace HeroBot.Plugins.GiveAway.Services
{
    [Service]
    public class GiveAwayService
    {
        private readonly IDatabaseService _sqlDatabase;
        private readonly Random _random;
        private readonly IRedisService _redisDatabase;
        private readonly ISubscriber _sub;
        private readonly DiscordShardedClient _discord;
        private readonly Timer _timer;
        private readonly static string GetGiveawayById = "SELECT * FROM \"GiveAways\" WHERE \"Id\"=@id";
        private readonly static string Creategiveaway = "INSERT INTO \"GiveAways\" (\"winners\",\"price\",\"message\",\"channel\") VALUES (@winners,@price,@message,@channel) RETURNING \"Id\"";
        public GiveAwayService(IDatabaseService databaseService,Random random, IRedisService redisService, IConfigurationRoot o, DiscordShardedClient discordShardedClient)
        {
            // We get the database from the provider
            _sqlDatabase = databaseService;
            _random = random;
            _redisDatabase = redisService;
            _sub = _redisDatabase.GetSubscriber();
            _discord = discordShardedClient;
            _timer = new Timer(120000);
            _timer.Elapsed += Update;
            _sub.Subscribe($"__keyevent@{o.GetSection("redis")["database"]}__:expired", OnRedisEvent);
        }

        private void Update(object sender, ElapsedEventArgs e)
        {
            
        }

        private void OnRedisEvent(RedisChannel arg1, RedisValue arg2)
        {
            try
            {
                var name = arg2.ToString().Split(':');
                if (name.Length == 2 && name[0] == "giveaway")
                {
                    var id = long.Parse(name[1]);
                    // Get the giveaway from the database
                    var connection = _sqlDatabase.GetDbConnection();
                    // Query the database
                    connection.QueryAsync(GetGiveawayById, new { id }).ContinueWith(async (task) =>
                    {
                        try
                        {
                            // Get the giveaway from the query result
                            var result = task.Result;
                            // If the giveaway exists in the database
                            if (result.Any())
                            {
                                // We get the first giveaway
                                var giveaway = result.First();
                                // Try to get the channel from the discord client
                                SocketTextChannel channel = (SocketTextChannel)_discord.GetChannel((ulong)giveaway.channel);
                                // If the channel exists
                                if (channel != null)
                                {
                                    // Get the giveaway's message
                                    IUserMessage message = (IUserMessage)await channel.GetMessageAsync((ulong)giveaway.message);
                                    if (message != null)
                                    {
                                        var emote = new Emoji("🎉");
                                        var reactions = await message.GetReactionUsersAsync(emote, 9999).FlattenAsync();
                                        var win = new List<IUser>();
                                        var count = reactions.Count();
                                        var wC = giveaway.winners;
                                        if (wC > count)
                                            wC = count;
                                        while (wC != 0)
                                        {
                                            var winner = reactions.ElementAt(_random.Next(count));
                                            while (win.Any(x => x.Id == winner.Id))
                                                winner = reactions.ElementAt(_random.Next(count));
                                            win.Add(winner);
                                            wC--;
                                        } 
                                        await channel.SendMessageAsync(string.Join(",", win.Select(x => x.Mention)) + " won `" + giveaway.price + "`");
                                        await message.ModifyAsync(x => x.Embed = message.Embeds.First().ToEmbedBuilder().WithDescription($"Finished ! {string.Join(",", win.Select(x => x.Mention))}").Build());
                                    }
                                }
                            }
                        }
                        catch (Exception) { }
                    });

                }
            }
            catch (Exception) {  }
        }

        public async Task CreateGiveaway(IGuildChannel channel,IMessage message,TimeSpan time,string price,int winners)
        {
            var connection = _sqlDatabase.GetDbConnection();
            var id = (long)await connection.ExecuteScalarAsync(Creategiveaway, new { winners, price, message = (long)message.Id, channel = (long)channel.Id });
            await _redisDatabase.GetDatabase().StringSetAsync($"giveaway:{id}", String.Empty,time);
        }
    }
}