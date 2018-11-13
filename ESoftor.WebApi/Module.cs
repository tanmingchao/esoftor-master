// -----------------------------------------------------------------------
//  <copyright file="Module.cs" company="eSoft">
//      Copyright (c) 2014-2017 eSoft. All rights reserved.
//  </copyright>
//  <last-editor>谭明超</last-editor>
//  <last-date>$date$</last-date>
// -----------------------------------------------------------------------

using ESoftor.Framework.Module;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ESoftor.WebApi
{
    public class Module : ModuleBase
    {
        public override ModuleLevel ModuleLevel => ModuleLevel.Other;
        public override IServiceCollection AddModule(IServiceCollection services)
        {
            //services.ConfigureSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("esoftor web api", new Swashbuckle.AspNetCore.Swagger.Info { Title = "esoftor web api", Version = "v1" });
            //    //c.IncludeXmlComments(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ESoftor.WebApi.xml"));//这里的 xml名称是上面属性 中XML文档文件的名称
            //    Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "ESoftor.WebApi.xml").ToList().ForEach(file =>
            //    {
            //        c.IncludeXmlComments(file);
            //    });
            //});
            return services;
        }

        public override void UseModule(IServiceProvider provider)
        {
            
        }
    }
}
