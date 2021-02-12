using HeroBot.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;
using System.Diagnostics;

namespace HeroBot.Core.Services
{
    public class HeroBotContext : IDatabaseService
    {
        private readonly IConfigurationRoot _config;

        public HeroBotContext(IConfigurationRoot confg)
        {
            _config = confg;
        }

        public IDbConnection GetDbConnection()
        {
            StackTrace stackTrace = new StackTrace();           // get call stack
            StackFrame[] stackFrames = stackTrace.GetFrames();  // get method calls (frames)

            var assembly = stackFrames[1].GetMethod().DeclaringType.Assembly;
            return new NpgsqlConnection($"Server={_config.GetSection("postgres").GetSection("host").Value};Port={_config.GetSection("postgres").GetSection("port").Value};Database={assembly.GetName().Name};User Id={_config.GetSection("postgres").GetSection("auth").GetSection("name").Value};Password={_config.GetSection("postgres").GetSection("auth").GetSection("password").Value};SslMode=Prefer;Trust Server Certificate=true;Pooling=true;");
        }

        public IDbConnection GetDbConnection(string v)
        {
            return new NpgsqlConnection($"Server={_config.GetSection("postgres").GetSection("host").Value};Port={_config.GetSection("postgres").GetSection("port").Value};Database={v};User Id={_config.GetSection("postgres").GetSection("auth").GetSection("name").Value};Password={_config.GetSection("postgres").GetSection("auth").GetSection("password").Value};SslMode=Prefer;Trust Server Certificate=true;Pooling=true;");
        }
    }
}
