// -----------------------------------------------------------------------
//  <copyright file="UserLogin.cs" company="com.esoftor">
//      Copyright (c) 2014-2018 ESoftor. All rights reserved.
//  </copyright>
//  <last-editor>谭明超</last-editor>
//  <last-date>%time%</last-date>
// -----------------------------------------------------------------------

using ESoftor.Framework.Entity;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace ESoftor.Core.Permission.Identity.Entity
{
    [Table(nameof(UserLogin))]
    public class UserLogin:EntityBase<Guid>
    {
        /// <summary>
        /// 获取或设置 登录的登录提供程序（例如facebook, google）。
        /// </summary>
        [DisplayName("登录的登录提供程序")]
        public string LoginProvider { get; set; }

        /// <summary>
        /// 获取或设置 此登录的提供者唯一标识符。
        /// </summary>
        [DisplayName("此登录的提供者唯一标识符")]
        public string ProviderKey { get; set; }

        /// <summary>
        /// 获取或设置 登录提供者在UI上显示的友好名称
        /// </summary>
        [DisplayName("显示的友好名称")]
        public string ProviderDisplayName { get; set; }

        /// <summary>
        /// 获取或设置 所属用户编号
        /// </summary>
        [DisplayName("用户编号")]
        public Guid UserId { get; set; }
    }
}
