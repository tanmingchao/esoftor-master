// -----------------------------------------------------------------------
//  <copyright file="QQAuthenticationOptions.cs" company="eSoft">
//      Copyright (c) 2014-2017 eSoft. All rights reserved.
//  </copyright>
//  <last-editor>谭明超</last-editor>
//  <last-date>$date$</last-date>
// -----------------------------------------------------------------------

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace ESoftor.Permission.OAuth2.QQ
{
    /// <summary>
    ///  
    /// </summary>
    public class QQAuthenticationOptions : OAuthOptions
    {
        public string OpenIdEndpoint { get; set; }

        public QQAuthenticationOptions()
        {

            ClaimsIssuer = QQAuthenticationDefaults.Issuer;
            CallbackPath = new PathString(QQAuthenticationDefaults.CallbackPath);

            AuthorizationEndpoint = QQAuthenticationDefaults.AuthorizationEndpoint;
            TokenEndpoint = QQAuthenticationDefaults.TokenEndpoint;
            UserInformationEndpoint = QQAuthenticationDefaults.UserInformationEndpoint;
            OpenIdEndpoint = QQAuthenticationDefaults.UserOpenIdEndpoint;

            ClaimActionCollectionMapExtensions.MapJsonKey(ClaimActions, ClaimTypes.NameIdentifier, "id");
            ClaimActionCollectionMapExtensions.MapJsonKey(ClaimActions, ClaimTypes.Name, "displayName");

        }
    }
}
