using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Discord;
using HeroBot.Common.Interfaces;
using HeroBot.Plugins.RP.Entities;

namespace HeroBot.Plugins.RP.Services
{
    public class RPService
    {

        public RPService(IDatabaseService databaseService) {
            _database = databaseService;
        }

        private readonly IDatabaseService _database;
        private static readonly string GetUsetSql = "SELECT * FROM \"RPUser\" WHERE \"UserId\" = @Id";
        private static readonly string GetCityName = "SELECT \"Name\" FROM \"City\" WHERE \"Id\" = @Id";
        private static readonly string UpdateUserSql = "UPDATE \"RPUser\" SET \"Badges\" = @Badges, \"CityId\" = @CityId, \"Description\" = @Description, \"Website\" = @Website, \"Emoji\" = @Emoji, \"Personality\" = @Personality, \"Likes\" = @Likes,\"Job\" = @Job, \"Money\" = @Money WHERE \"UserId\" = @UserId";
        private static readonly string CreateUserSql = "INSERT INTO \"RPUser\" (\"UserId\",\"CityId\",\"Description\",\"Website\",\"Emoji\",\"Personality\",\"Likes\",\"Job\",\"Money\") VALUES (@UserId,@CityId,@Description,@Website,@Emoji,@Personality,@Likes,@Job,@Money)";
        internal async Task<bool> CreateUser(IUser user)
        {
            var entity = new RPUser() { UserId = user.Id.ToString() };
            using var connection = _database.GetDbConnection();
            await connection.ExecuteAsync(CreateUserSql, entity);
            return true;
        }
        
        internal async Task<RPUser> GetRPUser(IUser user) {
            using var connection = _database.GetDbConnection();
            var res = (await connection.QueryAsync<RPUser>(GetUsetSql, new { Id = user.Id.ToString() }));
            if (res.Any())
                return res.First();
            return null;
        }

        internal async Task UpdateUser(RPUser rPUser)
        {
            using var connection = _database.GetDbConnection();
            await connection.ExecuteAsync(UpdateUserSql, rPUser);
        }


    }
}
