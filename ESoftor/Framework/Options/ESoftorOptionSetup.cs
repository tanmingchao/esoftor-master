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
            var defaultContext = _configuration.GetSection("ESoftor:DbContexts:Default");

            var dbType = defaultContext.GetSection("DatabaseType");
            var connString = defaultContext.GetSection("ConnectString");
            var dbTypeName = defaultContext.GetSection("DbContextTypeName");

            Console.WriteLine(_configuration.GetSection("ESoftor:DbContexts:Default:DatabaseType")?.Key ?? "dbType对象值为空了");

            Check.NotNull(dbType?.Value, nameof(dbType));
            Check.NotNullOrEmpty(connString?.Value, nameof(connString));
            Check.NotNullOrEmpty(dbTypeName?.Value, nameof(dbTypeName));

            options.ESoftorDbOption = new ESoftorDbOption()
            {
                ConnectString = connString.Value,
                DatabaseType = (DatabaseType)Convert.ToInt32(dbType.Value),
                DbContextTypeName = dbTypeName.Value
            };

            //jwt配置
            var jwtOptions = _configuration.GetSection("ESoftor:Jwt");

            var audience = jwtOptions.GetSection("Audience");
            var issuer = jwtOptions.GetSection("Issuer");
            var secret = jwtOptions.GetSection("Secret");

            Check.NotNullOrEmpty(audience?.Value, nameof(audience));
            Check.NotNullOrEmpty(issuer?.Value, nameof(issuer));
            Check.NotNullOrEmpty(secret?.Value, nameof(secret));

            options.ESoftorJwtOption = new ESoftorJwtOption()
            {
                Audience = audience.Value,
                Issuer = issuer.Value,
                Secret = secret.Value
            };
        }
    }
}
