using System;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Discord.WebSocket;
using HeroBot.Common.Interfaces;

namespace HeroBot.Plugins.RP.Services
{
    public class RPService
    {

        public RPService(IDatabaseService databaseService) {
            _database = databaseService;
        }
        private readonly static string CreateUser = "INSERT INTO \"RPUsers\" (\"Id\") VALUES (@id)";
        private readonly static string GetUser = "SELECT * FROM \"RPUsers\" WHERE \"Id\"=@id";
        private readonly static string UpdateUer = "UPDATE \"RPUsers\" SET \"Bolts\"=@bolts,\"Gold\"=@gold WHERE \"Id\"=@id";
        private readonly IDatabaseService _database;

        internal async Task<bool> Start(SocketUser user)
        {
            await _database.GetDbConnection().ExecuteAsync(CreateUser, new { id = (long)user.Id });
            return true;
        }

        internal static Ressources ToRessourceEnum(string s) {
            switch (s) {
                case "gold":
                    return Ressources.GOLD;
                case "bolt":
                    return Ressources.BOLT;
                default:
                    throw new InvalidOperationException();
            }
        }

        internal bool GetAccount(SocketUser user, out RPUser userAccount)
        {
            var re = _database.GetDbConnection().Query<RPUser>(GetUser, new { id = (long)user.Id }).FirstOrDefault();
            if (re != null) {
                userAccount = re;
                return true;
            }
            userAccount = null;
            return false;
        }

        internal void SetAccount(RPUser userAccount)
        {
            _database.GetDbConnection().Execute(UpdateUer, new { bolts = userAccount.Bolts, gold = userAccount.Gold, id = userAccount.Id });
        }
    }
    public enum Ressources {
        BOLT = 0,
        GOLD = 1
    }
}
