// -----------------------------------------------------------------------
//  <copyright file="JwtHandler.cs" company="com.esoftor">
//      Copyright (c) 2014-2018 ESoftor. All rights reserved.
//  </copyright>
//  <last-editor>谭明超</last-editor>
//  <last-date>%time%</last-date>
// -----------------------------------------------------------------------

using ESoftor.Extensions;
using ESoftor.Framework;
using ESoftor.Framework.Options;
using ESoftor.Timing;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ESoftor.Permission.Identity.JwtBearer
{
    public class JwtHelper
    {
        /// <summary>
        ///     创建token对象
        /// </summary>
        /// <param name="claims">身份信息集合</param>
        /// <param name="expiresMinutes">过期时间(分钟)</param>
        /// <returns>创建的token对象</returns>
        public static string CreateToken(Claim[] claims, int expiresMinutes = 20)
        {
            var jwtOptions = AppSettingManager.Get<ESoftorJwtOption>("ESoftor:Jwt") ??
                new ESoftorJwtOption()
                {
                    Audience = AppSettingManager.Get("ESoftor:Jwt:Audience"),
                    Issuer = AppSettingManager.Get("ESoftor:Jwt:Issuer"),
                    Secret = AppSettingManager.Get("ESoftor:Jwt:Secret")
                };

            SecurityKey key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtOptions.Secret));
            SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            DateTime now = DateTime.UtcNow;

            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Audience = jwtOptions.Audience,
                Issuer = jwtOptions.Issuer,
                SigningCredentials = credentials,
                NotBefore = now,
                IssuedAt = now,
                Expires = now.Add(TimeSpan.FromMinutes(expiresMinutes))
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(descriptor);
            return tokenHandler.WriteToken(token);
        }

        /// <summary>
        ///     验证身份 验证签名的有效性,(TODO:待优化,需要重构)
        /// </summary>
        /// <param name="encodeJwt">转码之后的jwt信息</param>
        /// <param name="validatePayLoad"></param>
        /// <returns></returns>
        public static bool ValidateToken(string encodeJwt, Func<Dictionary<string, object>, bool> validatePayLoad)
        {
            var jwtOptions = AppSettingManager.Get<ESoftorJwtOption>("ESoftor:Jwt");

            var success = true;
            var jwtArr = encodeJwt.Split('.');
            var header = Base64UrlEncoder.Decode(jwtArr[0]).FromJsonString<Dictionary<string, object>>();
            var payLoad = Base64UrlEncoder.Decode(jwtArr[1]).FromJsonString<Dictionary<string, object>>();

            var hs256 = new HMACSHA256(Encoding.ASCII.GetBytes(jwtOptions.Secret));
            //首先验证签名是否正确（必须的）
            success = success && string.Equals(jwtArr[2], Base64UrlEncoder.Encode(hs256.ComputeHash(Encoding.UTF8.GetBytes(string.Concat(jwtArr[0], ".", jwtArr[1])))));
            if (!success)
                return success;//签名不正确直接返回

            //其次验证是否在有效期内（也应该必须）
            var now = Convert.ToInt64(DateTime.UtcNow.ToJsGetTime());
            success = success && (now >= long.Parse(payLoad["nbf"].ToString()) && now < long.Parse(payLoad["exp"].ToString()));

            //再其次 进行自定义的验证
            success = success && validatePayLoad(payLoad);

            return success;
        }

    }
}
