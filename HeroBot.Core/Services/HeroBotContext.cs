using HeroBot.Common.Attributes;
using HeroBot.Common.Entities;
using HeroBot.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace HeroBot.Core.Services
{
    public class HeroBotContext : IDatabaseService
    {
        public Dictionary<String, NpgsqlConnection> Connections = new Dictionary<string, NpgsqlConnection>();
        private IConfigurationRoot _config;

        public HeroBotContext(IConfigurationRoot confg)
        {
            _config = confg;
            NeedPluginAttribute.databaseService = this;
        }

        public IDbConnection GetDbConnection()
        {
            StackTrace stackTrace = new StackTrace();           // get call stack
            StackFrame[] stackFrames = stackTrace.GetFrames();  // get method calls (frames)

            var assembly = stackFrames[1].GetMethod().DeclaringType.Assembly;
            return new Npgsql.NpgsqlConnection($"Server={_config.GetSection("postgres").GetSection("host").Value};Port={_config.GetSection("postgres").GetSection("port").Value};Database={assembly.GetName().Name};User Id={_config.GetSection("postgres").GetSection("auth").GetSection("name").Value};Password={_config.GetSection("postgres").GetSection("auth").GetSection("password").Value};SslMode=Require;Trust Server Certificate=true;Pooling=false;");
        }

        public IDbConnection GetDbConnection(string assm)
        {
            return new Npgsql.NpgsqlConnection($"Server={_config.GetSection("postgres").GetSection("host").Value};Port={_config.GetSection("postgres").GetSection("port").Value};Database={assm};User Id={_config.GetSection("postgres").GetSection("auth").GetSection("name").Value};Password={_config.GetSection("postgres").GetSection("auth").GetSection("password").Value};SslMode=Require;Trust Server Certificate=true;Pooling=false;");
        }
    }
}
