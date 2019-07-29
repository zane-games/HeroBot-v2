using HeroBot.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace HeroBotv2.Services
{
    public class RedisService : IRedisService
    {
        private LoggingService _loggingService;
        private IConfigurationRoot _config;
        private ConnectionMultiplexer redis;
        private ISubscriber _subscriber;
        private IDatabaseAsync _database;

        public RedisService(LoggingService loggingService, IConfigurationRoot configurationRoot) {
            _loggingService = loggingService;
            _config = configurationRoot;
            BeginConnection();
        }
        private void BeginConnection()
        {
            var config = new ConfigurationOptions();
            config.ClientName = "HeroBot_v2_runtime";
            config.Password = _config.GetSection("redis").GetSection("auth").GetSection("password").Value;
            config.DefaultDatabase = int.Parse(_config.GetSection("redis").GetSection("database").Value);
            config.EndPoints.Add($"{_config.GetSection("redis").GetSection("host").Value}:{_config.GetSection("redis").GetSection("port").Value}");
            redis = ConnectionMultiplexer.Connect(config);
            redis.ConnectionFailed += (sender,evcent) => {
                _loggingService.Log(Discord.LogSeverity.Error, "Can't connect to Redis host");
            };
            redis.ConnectionRestored += (sender, evcent) => {
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
