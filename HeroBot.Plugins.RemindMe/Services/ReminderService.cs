using Dapper;
using Discord.WebSocket;
using HeroBot.Common.Attributes;
using HeroBot.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Npgsql;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroBot.Plugins.RemindMe.Services
{
    [Service]
    public class ReminderService
    {
        private IRedisService _redis;
        private DiscordShardedClient _discord;
        private IDatabaseService _database;

        private readonly string GetReminderById = "SELECT * FROM \"Reminders\" WHERE \"Id\"=@id";
        private readonly string GetReminderPerUser = "SELECT * FROM \"Reminders\" WHERE \"userId\"=@id";
        private readonly string DeleteReminderbyId = "DELETE FROM \"Reminders\" WHERE \"Id\"=@id";
        private readonly string CountRemindersPerUser = "SELECT COUNT(*) FROM \"Reminders\" WHERE \"userId\"=@id";
        private readonly string InsertReminder = "INSERT INTO \"Reminders\" (\"userId\",\"reason\") VALUES (@userId,@reason) RETURNING \"Id\"";
        public ReminderService(IRedisService redisService, IConfigurationRoot o, IDatabaseService databaseService, DiscordShardedClient discordSocketClient)
        {
            _redis = redisService;
            _discord = discordSocketClient;
            _database = databaseService;
            _redis.GetSubscriber().SubscribeAsync($"__keyevent@{o.GetSection("redis")["database"]}__:expired", OnKeyRemove).Wait();
        }

        private void OnKeyRemove(RedisChannel arg1, RedisValue arg2)
        {
            try
            {
                Console.WriteLine(arg2.ToString());
                var name = arg2.ToString().Split(':');
                Console.WriteLine(name.Length);
                if (name.Length == 3)
                {
                    if (name[0] == "reminder" && name[1] == "remove")
                    {
                        var reminderId = name[2];
                        using (var conn = (NpgsqlConnection)_database.GetDbConnection())
                        {
                            conn.Open();
                            conn.QueryAsync(GetReminderById, new { id = reminderId }).ContinueWith((x) =>
                              {
                                  var cont = x.Result.FirstOrDefault();
                                  if (cont != null)
                                  {
                                      var userId = (ulong)cont.userId;
                                      var reason = (string)cont.reason;
                                      _discord.GetUser(userId).GetOrCreateDMChannelAsync().ContinueWith((x) =>
                                      {
                                          var dm = x.Result;
                                          dm.SendMessageAsync($"Hay :watch: ! It's time to {reason}").ContinueWith((v) =>
                                          {
                                              conn.Execute(new CommandDefinition(DeleteReminderbyId, new { id = reminderId }));
                                          });
                                      });
                                  }
                              });
                        }
                    }
                }
            }
            catch (Exception e) { Console.WriteLine(e); }
        }

        internal async Task<IEnumerable<dynamic>> GetReminders(ulong id)
        {
            using (var conn = (NpgsqlConnection)_database.GetDbConnection())
            {
                var result = conn.Query(GetReminderPerUser, new { id = (long)id});
                return result.Select(async (x) =>
                {
                    var v = (await _redis.GetDatabase().StringGetWithExpiryAsync($"reminder:remove:{x.Id}")).Expiry;

                    return new
                    {
                        x,
                        r = v
                    };
                }).Select((x) => { x.Wait(); return x.Result; }).Select(x =>
                {
                    if (!x.r.HasValue)
                    {
                        return new
                        {
                            x.x,
                            r = x.r,
                            anulated = true
                        };
                    }
                    return new
                    {
                        x.x,
                        r = x.r,
                        anulated = false
                    };
                });
            }
        }

        public async Task CreateReminder(Reminder reminder)
        {

            using (var conn = (NpgsqlConnection)_database.GetDbConnection())
            {
                conn.Open();
                var cont = conn.Query(CountRemindersPerUser, new { id = (long)reminder.userId }).First().count;
                if (cont < 11)
                {
                    var id = conn.Query(InsertReminder, new { userId = (long)reminder.userId, reason = reminder.remind}).First().Id;
                    await _redis.GetDatabase().StringSetAsync($"reminder:remove:{id}", String.Empty, reminder.TimeSpan);
                }
                else
                {
                    throw new Exception("User has too many reminders !");
                }
            }
        }
    }
    public class Reminder
    {
        [JsonIgnore]
        public ulong userId;
        public string remind;
        [JsonIgnore]
        public TimeSpan TimeSpan;
    }
}