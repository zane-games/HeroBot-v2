using Discord.Commands;
using HeroBot.Common.Attributes;
using HeroBot.Common.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HeroBot.Core.Services
{
    public class CooldownService : ICooldownService
    {
        private readonly IRedisService _redis;

        public CooldownService(IRedisService redisService) {
            CooldownAttribute._cooldown = this;
            _redis = redisService;
        }

        public async Task<TimeSpan?> IsCommandCooldowned(ulong userid, string commandName)
        {
            return (await _redis.GetDatabase().StringGetWithExpiryAsync($"{userid}-c-{commandName}")).Expiry;
        }

        public async Task<TimeSpan?> IsModuleCooldowned(ulong userid, string moduleName)
        {
                return (await _redis.GetDatabase().StringGetWithExpiryAsync($"{userid}-m-{moduleName}")).Expiry;
        }

        internal async Task OnCommand(CommandInfo commandInfo,SocketCommandContext commandContext) {
            if (commandInfo.Preconditions.Any(x => x is CooldownAttribute)) {
                var seconds = (commandInfo.Preconditions.Where(x => x is CooldownAttribute).OrderByDescending(x => (x as CooldownAttribute).cooldown.Seconds).First() as CooldownAttribute).cooldown;
                await _redis.GetDatabase().StringSetAsync($"{commandContext.User.Id}-c-{commandInfo.Name}", String.Empty, seconds);
            }
            if (commandInfo.Module.Preconditions.Any(x => x is CooldownAttribute)) {
                var seconds = (commandInfo.Module.Preconditions.Where(x => x is CooldownAttribute).OrderByDescending(x => (x as CooldownAttribute).cooldown.Seconds).First() as CooldownAttribute).cooldown;
                await _redis.GetDatabase().StringSetAsync($"{commandContext.User.Id}-m-{commandInfo.Module.Name}", String.Empty,seconds);
            }
        }
    }
}
