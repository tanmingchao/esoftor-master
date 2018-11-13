// -----------------------------------------------------------------------
//  <copyright file="Module.cs" company="eSoft">
//      Copyright (c) 2014-2017 eSoft. All rights reserved.
//  </copyright>
//  <last-editor>谭明超</last-editor>
//  <last-date>$date$</last-date>
// -----------------------------------------------------------------------

using System;
using System.Security.Principal;
using System.Text;
using ESoftor.Core.Permission.Identity;
using ESoftor.Core.Permission.Identity.Entity;
using ESoftor.Framework.Module;
using ESoftor.Framework;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace ESoftor.Core.Permission
{
    public class Module : ModuleBase
    {
        public override ModuleLevel ModuleLevel => ModuleLevel.Business;
        public override IServiceCollection AddModule(IServiceCollection services)
        {
            services.AddScoped<IUserStore<User>, UserStore>();
            services.AddScoped<IRoleStore<Role>, RoleStore>();
            //services.AddScoped<SignInManager<User>>();
            //services.AddScoped<UserManager<User>>();
            //services.AddScoped<RoleManager<Role>>();
            
            services.AddScoped<IIdentityContract, IdentityService>();

            //注入当前用户，替换Thread.CurrentPrincipal的作用
            services.AddTransient<IPrincipal>(provider =>
            {
                IHttpContextAccessor accessor = provider.GetService<IHttpContextAccessor>();
                return accessor?.HttpContext?.User;
            });

            Action<IdentityOptions> identityOptionsAction = options =>
            {
                //登录
                options.SignIn.RequireConfirmedEmail = true;
                //密码
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                //用户
                options.User.RequireUniqueEmail = true;
                //锁定
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
            };
            IdentityBuilder builder = services.AddIdentity<User, Role>(identityOptionsAction);
            builder.AddDefaultTokenProviders();

            //添加Authentication服务
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(jwt =>
                {
                    string secret = AppSettingManager.Get("ESoftor:Jwt:Secret");
                    jwt.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidIssuer = AppSettingManager.Get("ESoftor:Jwt:Issuer"),
                        ValidAudience = AppSettingManager.Get("ESoftor:Jwt:Audience"),
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret))
                    };
                    jwt.SecurityTokenValidators.Clear();
                    //jwt.SecurityTokenValidators.Add(new OnlineUserJwtSecurityTokenHandler());//在线用户
                });
            return services;
        }

        public override void UseModule(IServiceProvider provider)
        {

        }
    }
}
