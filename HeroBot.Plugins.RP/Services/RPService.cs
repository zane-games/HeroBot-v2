using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Discord;
using HeroBot.Common;
using HeroBot.Common.Interfaces;
using HeroBot.Plugins.RP.Entities;
using Newtonsoft.Json;

namespace HeroBot.Plugins.RP.Services
{
    public class RPService
    {

        public RPService(IDatabaseService databaseService, SimpleCacheImplementation cache)
        {
            _database = databaseService;
            _cache = cache;
        }

        private readonly IDatabaseService _database;
        private readonly SimpleCacheImplementation _cache;

        /**
         * We need to update this class to handle the caching
         */
        private static readonly string GetUserSql = "SELECT * FROM \"RPUser\" WHERE \"UserId\" = @Id";
        private static readonly string UpdateUserSql = "UPDATE \"RPUser\" SET \"Badges\" = @Badges, \"CityId\" = @CityId, \"Description\" = @Description, \"Website\" = @Website, \"Emoji\" = @Emoji, \"Personality\" = @Personality, \"Likes\" = @Likes,\"Job\" = @Job, \"Money\" = @Money WHERE \"UserId\" = @UserId";
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
                var val = await connection.QueryAsync<RPUser>(GetUserSql);
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


    }
}