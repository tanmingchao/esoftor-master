// -----------------------------------------------------------------------
//  <copyright file="WeiXinAuthenticationExtensions.cs" company="eSoft">
//      Copyright (c) 2014-2017 eSoft. All rights reserved.
//  </copyright>
//  <last-editor>谭明超</last-editor>
//  <last-date>$date$</last-date>
// -----------------------------------------------------------------------

using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ESoftor.Permission.OAuth2.WeChat
{
    public static class WeiXinAuthenticationExtensions
    {
        /// <summary> 
        /// </summary>
        public static AuthenticationBuilder AddWeixinAuthentication(this AuthenticationBuilder builder)
        {
            return builder.AddWeixinAuthentication(WeixinAuthenticationDefaults.AuthenticationScheme, WeixinAuthenticationDefaults.DisplayName, options => { });
        }

        /// <summary> 
        /// </summary>
        public static AuthenticationBuilder AddWeixinAuthentication(this AuthenticationBuilder builder, Action<WeixinAuthenticationOptions> configureOptions)
        {
            return builder.AddWeixinAuthentication(WeixinAuthenticationDefaults.AuthenticationScheme, WeixinAuthenticationDefaults.DisplayName, configureOptions);
        }

        /// <summary> 
        /// </summary>
        public static AuthenticationBuilder AddWeixinAuthentication(this AuthenticationBuilder builder, string authenticationScheme, Action<WeixinAuthenticationOptions> configureOptions)
        {
            return builder.AddWeixinAuthentication(authenticationScheme, WeixinAuthenticationDefaults.DisplayName, configureOptions);
        }

        /// <summary> 
        /// </summary>
        public static AuthenticationBuilder AddWeixinAuthentication(this AuthenticationBuilder builder, string authenticationScheme, string displayName, Action<WeixinAuthenticationOptions> configureOptions)
        {
            return builder.AddOAuth<WeixinAuthenticationOptions, WeixinAuthenticationHandler>(authenticationScheme, displayName, configureOptions);
        }
    }
}
