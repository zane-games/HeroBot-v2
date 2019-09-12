using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Discord;
using Discord.WebSocket;
using HeroBot.Common;
using HeroBot.Common.Helpers;
using HeroBot.Common.Interfaces;
using HeroBot.Plugins.RP.Entities;
using Newtonsoft.Json;

namespace HeroBot.Plugins.RP.Services
{
    public class RPService
    {

        public RPService(IDatabaseService databaseService, SimpleCacheImplementation cache,DiscordShardedClient discordShardedClient)
        {
            _discord = discordShardedClient;
            _database = databaseService;
            StartLoop();
            _cache = cache;
        }

        private readonly IDatabaseService _database;
        private readonly SimpleCacheImplementation _cache;
        private readonly DiscordShardedClient _discord;

        /**
         * We need to update this class to handle the caching
         */
        private static readonly string GetLeaderBoardSql = "select * from \"RPUser\" order by \"Money\" desc limit 10";
        private static readonly string GetUserBirthDayToday = "SELECT * FROM \"RPUser\" WHERE experimental_strftime(\"Birthday\",'%m-%d') = experimental_strftime(now(),'%d-%m');";
        private static readonly string GetUserSql = "SELECT * FROM \"RPUser\" WHERE \"UserId\" = @Id";
        private static readonly string UpdateUserSql = "UPDATE \"RPUser\" SET  \"Birthday\" = @Birthday,\"CityId\" = @CityId, \"Description\" = @Description, \"Website\" = @Website, \"Emoji\" = @Emoji, \"Personality\" = @Personality, \"Likes\" = @Likes,\"Job\" = @Job, \"Money\" = @Money WHERE \"UserId\" = @UserId";
        private static readonly string CreateUserSql = "INSERT INTO \"RPUser\" (\"UserId\",\"CityId\",\"Description\",\"Website\",\"Emoji\",\"Personality\",\"Likes\",\"Job\",\"Money\") VALUES (@UserId,@CityId,@Description,@Website,@Emoji,@Personality,@Likes,@Job,@Money)";
        internal async Task<bool> CreateUser(IUser user)
        {
            var entity = new RPUser() { UserId = user.Id.ToString() };
            using var connection = _database.GetDbConnection();
            await connection.ExecuteAsync(CreateUserSql, entity);

            return true;
        }

        internal async Task<RPUser> GetRPUser(IUser user)
        {
            var cacheResult = await _cache.GetValueAsync($"rp-user-{user.Id}");
            if (cacheResult.HasValue)
                return JsonConvert.DeserializeObject<RPUser>(cacheResult);
            else
            {
                using var connection = _database.GetDbConnection();
                var val = await connection.QueryAsync<RPUser>(GetUserSql, new { Id = user.Id.ToString()});
                if (val.Any())
                {
                    await _cache.CacheValueAsync($"rp-user-{user.Id}", JsonConvert.SerializeObject(val.First()));
                    return val.First();
                }
                else
                    return null;
            }
        }
        internal async Task UpdateUser(RPUser rPUser)
        {
            using var connection = _database.GetDbConnection();
            await _cache.InvalidateValueAsync($"rp-user-{rPUser.UserId}");
            await connection.ExecuteAsync(UpdateUserSql, rPUser);
        }

        internal async Task<IEnumerable<RPUser>> GetLeaderBoard() {
            using var connection = _database.GetDbConnection();
            return await connection.QueryAsync<RPUser>(GetLeaderBoardSql);
        }


        /// <summary>
        /// Determine the timeSpan Delay must wait before running the code
        /// </summary>
        /// <param name="date"></param>
        /// <param name="scheduler"></param>
        private async Task StartLoop()
        {
            while (true)
            {
                //Time when method needs to be called
                var DailyTime = "12:30:00";
                var timeParts = DailyTime.Split(new char[1] { ':' });

                var dateNow = DateTime.Now;
                var date = new DateTime(dateNow.Year, dateNow.Month, dateNow.Day,
                           int.Parse(timeParts[0]), int.Parse(timeParts[1]), int.Parse(timeParts[2]));
                TimeSpan ts;
                if (date > dateNow)
                    ts = date - dateNow;
                else
                {
                    date = date.AddDays(1);
                    ts = date - dateNow;
                }
                Console.WriteLine($"Waiting {ts.ToHumanReadable()}");
                //waits certan time and run the code
                await Task.Delay(ts).ContinueWith(async (x) => await Birthday());
            }
        }

        internal async Task Birthday()
        {
            using var connection = _database.GetDbConnection();
            var birthday = await connection.QueryAsync<dynamic>(GetUserBirthDayToday);
            foreach (var acc in birthday)
            {
                try
                {
                    var user = _discord.GetUser(ulong.Parse((string)acc.UserId));
                    if (user != null)
                    {
                        await user.SendMessageAsync($"Happy birthday `{user.Username}` ! 🍰 🎉");
                    }
                }
                catch (Exception e) { /* Ignore birthday errors */Console.WriteLine(e); }
            }
        }
    }
}