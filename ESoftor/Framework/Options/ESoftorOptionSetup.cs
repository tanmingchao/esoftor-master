// -----------------------------------------------------------------------
//  <copyright file="ESoftorOptionSetup.cs" company="com.esoftor">
//      Copyright (c) 2014-2018 ESoftor. All rights reserved.
//  </copyright>
//  <last-editor>谭明超</last-editor>
//  <last-date>%time%</last-date>
// -----------------------------------------------------------------------

using ESoftor.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Linq;

namespace ESoftor.Framework.Options
{
    /// <summary>
    ///     框架配置项配置类
    /// </summary>
    public class ESoftorOptionSetup : IConfigureOptions<ESoftorOption>
    {
        public ESoftorOptionSetup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private readonly IConfiguration _configuration;

        public void Configure(ESoftorOption options)
        {
            var optionRoot = _configuration.GetSection("ESoftor:DbContexts");
            var dbContexts = optionRoot.GetChildren();
            var defaultContext = dbContexts?.FirstOrDefault();//当前默认显示用单库模式

            var dbType = defaultContext.GetSection("DatabaseType");
            var connString = defaultContext.GetSection("ConnectString");
            var dbTypeName = defaultContext.GetSection("DbContextTypeName");

            Check.NotNullOrEmpty(dbType?.Value, nameof(dbType));
            Check.NotNullOrEmpty(connString?.Value, nameof(connString));
            Check.NotNullOrEmpty(dbTypeName?.Value, nameof(dbTypeName));

            options.ESoftorDbOption = new ESoftorDbOption()
            {
                ConnectString = connString.Value,
                DatabaseType = (DatabaseType)Convert.ToInt32(dbType.Value),
                DbContextTypeName = dbType.Value
            };
        }
    }
}
