using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord.WebSocket;

namespace HeroBot.Plugins.RP.Services
{
    public class RPService
    {

        private readonly List<RPUser> rPUsers = new List<RPUser>();

        internal async Task<bool> Start(SocketUser user)
        {
            rPUsers.Add(new RPUser()
            {
                Bolt = 10,
                Gold = 2,
                UserId = user.Id,
                Id = rPUsers.Count+1
            });
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
            if (rPUsers.Any(x => x.UserId == user.Id))
            {
                userAccount = rPUsers.First(x => x.UserId == user.Id);
            }
            else
            {
                userAccount = null;
                return false;
            }
            return true;
        }

        internal void SetAccount(SocketUser user, RPUser userAccount)
        {
            var userd = rPUsers.FirstOrDefault(x => x.Id == userAccount.Id);
            rPUsers.Remove(userd);
            rPUsers.Add(userAccount);
        }
    }
    public enum Ressources {
        BOLT = 0,
        GOLD = 1
    }
}
