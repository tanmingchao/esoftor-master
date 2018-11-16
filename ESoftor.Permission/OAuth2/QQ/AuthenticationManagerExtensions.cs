// -----------------------------------------------------------------------
//  <copyright file="AuthenticationManagerExtensions.cs" company="eSoft">
//      Copyright (c) 2014-2017 eSoft. All rights reserved.
//  </copyright>
//  <last-editor>谭明超</last-editor>
//  <last-date>$date$</last-date>
// -----------------------------------------------------------------------

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ESoftor.Permission.OAuth2.QQ
{
    public static class HttpContextExtensions
    {
        /// <summary> 
        ///  Get the external login information from qq provider.
        /// </summary> 
        public static async Task<Dictionary<string, string>> GetExternalQQLoginInfoAsync(this HttpContext httpContext, string expectedXsrf = null)
        {
            var auth = await httpContext.AuthenticateAsync(QQAuthenticationDefaults.AuthenticationScheme);

            var items = auth?.Properties?.Items;
            if (auth?.Principal == null || items == null || !items.ContainsKey("LoginProvider"))
            {
                return null;
            }

            if (expectedXsrf != null)
            {
                if (!items.ContainsKey("XsrfId"))
                {
                    return null;
                }
                var userId = items["XsrfId"] as string;
                if (userId != expectedXsrf)
                {
                    return null;
                }
            }

            var userInfo = auth.Principal.FindFirst("urn:qq:user_info");
            if (userInfo == null)
            {
                return null;
            }

            if (!string.IsNullOrEmpty(userInfo.Value))
            {
                var jObject = JObject.Parse(userInfo.Value);

                Dictionary<string, string> dict = new Dictionary<string, string>();

                foreach (var item in jObject)
                {
                    dict[item.Key] = item.Value?.ToString();
                }

                return dict;
            }

            return null;

        }
    }
}
