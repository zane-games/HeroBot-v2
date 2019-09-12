using HeroBot.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;

namespace HeroBotv2.Services
{
    public class RedisService : IRedisService
    {
        private readonly LoggingService _loggingService;
        private readonly IConfigurationRoot _config;
        private ISubscriber _subscriber;
        private IDatabaseAsync _database;

        public RedisService(LoggingService loggingService, IConfigurationRoot configurationRoot)
        {
            _loggingService = loggingService;
            _config = configurationRoot;
            BeginConnection();
        }
        private void BeginConnection()
        {
            var config = new ConfigurationOptions() {
                ClientName = "HeroBot_v2_runtime",
                Password = _config.GetSection("redis").GetSection("auth").GetSection("password").Value,
                DefaultDatabase = int.Parse(_config.GetSection("redis").GetSection("database").Value)
            };
            config.EndPoints.Add($"{_config.GetSection("redis").GetSection("host").Value}:{_config.GetSection("redis").GetSection("port").Value}");
            var redis = ConnectionMultiplexer.Connect(config);
            redis.ConnectionFailed += (sender, evcent) =>
            {
                _loggingService.Log(Discord.LogSeverity.Error, "Can't connect to Redis host");
            };
            redis.ConnectionRestored += (sender, evcent) =>
            {
                _loggingService.Log(Discord.LogSeverity.Warning, "Reconnected to the redis cluster");
            };
            _subscriber = redis.GetSubscriber();
            _loggingService.Log(Discord.LogSeverity.Info, "Connected to redis pub/sub !");
            _database = redis.GetDatabase(int.Parse(_config.GetSection("redis")["database"]), new object { });
            _loggingService.Log(Discord.LogSeverity.Info, "Connected to redis database !");
        }

        public ISubscriber GetSubscriber()
        {
            return _subscriber;
        }

        public IDatabaseAsync GetDatabase()
        {
            return _database;
        }
    }
}