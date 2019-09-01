using System;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Discord;
using Discord.WebSocket;
using HeroBot.Common.Interfaces;
using Dapper.FastCrud;
using HeroBot.Plugins.RP.Entities;

namespace HeroBot.Plugins.RP.Services
{
    public class RPService
    {

        public RPService(IDatabaseService databaseService) {
            _database = databaseService;
        }

        private readonly IDatabaseService _database;

        internal async Task<bool> CreateUser(IUser user)
        {
            var entity = new RPUser() { Id = user.Id.ToString() };
            using var connection = _database.GetDbConnection();
            await connection.InsertAsync(entity);
            return true;
        }

        internal Task<RPUser> GetRPUser(IUser user) {
            using var connection = _database.GetDbConnection();
            return connection.GetAsync<RPUser>(new RPUser() { Id = user.Id.ToString() });
        }

        internal void UpdateUser(IUser user, Func<RPUser, RPUser> func)
        {
            throw new NotImplementedException();
        }
    }
    public enum Ressources {
        BOLT = 0,
        GOLD = 1
    }
}
