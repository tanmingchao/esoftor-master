// -----------------------------------------------------------------------
//  <copyright file="AppSettingManager.cs" company="ESoft">
//      Copyright (c) 2014-2018 ESoft. All rights reserved.
//  </copyright>
//  <last-editor>谭明超</last-editor>
//  <last-date>2018/11/7 15:20:28</last-date>
// -----------------------------------------------------------------------
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.IO;

namespace ESoftor.Framework
{
    public static class AppSettingManager
    {
        private static IConfiguration _configuration;
        static AppSettingManager()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false)
                .AddJsonFile("appsettings.Development.json", true);
            _configuration = builder.Build();
        }

        public static string Get(string key)
        {
            return _configuration[key];
        }

        public static T Get<T>(string key)
        {
            string json = Get(key);
            return JsonConvert.DeserializeObject<T>(json);
        }

    }
}
