// -----------------------------------------------------------------------
//  <copyright file="QQAuthenticationExtensions.cs" company="eSoft">
//      Copyright (c) 2014-2017 eSoft. All rights reserved.
//  </copyright>
//  <last-editor>谭明超</last-editor>
//  <last-date>$date$</last-date>
// -----------------------------------------------------------------------

using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ESoftor.Permission.OAuth2.QQ
{
    /// <summary>
    /// 
    /// </summary>
    public static class QQAuthenticationExtensions
    {
        /// <summary> 
        /// </summary>
        public static AuthenticationBuilder AddQQAuthentication(this AuthenticationBuilder builder)
        {
            return builder.AddQQAuthentication(QQAuthenticationDefaults.AuthenticationScheme, QQAuthenticationDefaults.DisplayName, options => { });
        }

        /// <summary> 
        /// </summary>
        public static AuthenticationBuilder AddQQAuthentication(this AuthenticationBuilder builder, Action<QQAuthenticationOptions> configureOptions)
        {
            return builder.AddQQAuthentication(QQAuthenticationDefaults.AuthenticationScheme, QQAuthenticationDefaults.DisplayName, configureOptions);
        }

        /// <summary> 
        /// </summary>
        public static AuthenticationBuilder AddQQAuthentication(this AuthenticationBuilder builder, string authenticationScheme, Action<QQAuthenticationOptions> configureOptions)
        {
            return builder.AddQQAuthentication(authenticationScheme, QQAuthenticationDefaults.DisplayName, configureOptions);
        }

        /// <summary> 
        /// </summary>
        public static AuthenticationBuilder AddQQAuthentication(this AuthenticationBuilder builder, string authenticationScheme, string displayName, Action<QQAuthenticationOptions> configureOptions)
        {
            return builder.AddOAuth<QQAuthenticationOptions, QQAuthenticationHandler>(authenticationScheme, displayName, configureOptions);
        }
    }
}
