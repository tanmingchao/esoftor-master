// -----------------------------------------------------------------------
//  <copyright file="UserToken.cs" company="com.esoftor">
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
    [Table(nameof(UserToken))]
    public class UserToken : EntityBase<Guid>
    {
        /// <summary>
        /// 获取或设置 用户编号
        /// </summary>
        [DisplayName("用户编号")]
        public Guid UserId { get; set; }

        /// <summary>
        /// 获取或设置 当前用户标识的登录提供者
        /// </summary>
        [DisplayName("登录提供者")]
        public string LoginProvider { get; set; }

        /// <summary>
        /// 获取或设置 令牌名称
        /// </summary>
        [DisplayName("令牌名称")]
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置 令牌值
        /// </summary>
        [DisplayName("令牌值")]
        public string Value { get; set; }
    }
}
