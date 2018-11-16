// -----------------------------------------------------------------------
//  <copyright file="IIdentityContract.cs" company="com.esoftor">
//      Copyright (c) 2014-2018 ESoftor. All rights reserved.
//  </copyright>
//  <last-editor>谭明超</last-editor>
//  <last-date>%time%</last-date>
// -----------------------------------------------------------------------

using ESoftor.Core.Permission.Identity.Dto;
using ESoftor.Core.Permission.Identity.Entity;
using ESoftor.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace ESoftor.Core.Permission.Identity
{
    public interface IIdentityContract
    {

        #region 身份认证

        /// <summary>
        /// 注册账号
        /// </summary>
        /// <param name="dto">注册信息</param>
        /// <returns>业务操作结果</returns>
        Task<OperationResult<User>> Register(RegisterDto dto);

        /// <summary>
        /// 使用账号登录
        /// </summary>
        /// <param name="dto">登录信息</param>
        /// <returns>业务操作结果</returns>
        Task<OperationResult<string>> Login(LoginDto dto);

        /// <summary>
        /// Jwt登录
        /// </summary>
        /// <param name="dto">登录信息</param>
        /// <returns>业务操作结果</returns>
        Task<OperationResult<string>> JwtLogin(LoginDto dto);

        /// <summary>
        /// 账号退出
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <returns>业务操作结果</returns>
        Task<OperationResult> Logout(int userId);

        #endregion

        #region 授权
        Task<AuthenticationProperties> OAuth2(string provider, string redirectUrl);
        Task<bool> OAuth2Callback(string returnUrl = null, string remoteError = null);
        #endregion

    }
}
