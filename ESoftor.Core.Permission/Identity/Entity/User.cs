﻿// -----------------------------------------------------------------------
//  <copyright file="User.cs" company="com.esoftor">
//      Copyright (c) 2014-2018 ESoftor. All rights reserved.
//  </copyright>
//  <last-editor>谭明超</last-editor>
//  <last-date>%time%</last-date>
// -----------------------------------------------------------------------

using ESoftor.Framework.Entity;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ESoftor.Core.Permission.Identity.Entity
{
    [Table(nameof(User))]
    public class User : EntityBase<Guid>
    {
        public User()
        {
            CreatedTime = DateTime.Now;
        }

        /// <summary>
        /// 获取或设置 用户名
        /// </summary>
        [Required, DisplayName("用户名")]
        public string UserName { get; set; }

        /// <summary>
        /// 获取或设置 用户昵称
        /// </summary>
        [DisplayName("用户昵称")]
        public string NickName { get; set; }

        /// <summary>
        /// 获取或设置 电子邮箱
        /// </summary>
        [Required, DisplayName("电子邮箱"), DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        /// <summary>
        /// 获取或设置 表示用户是否已确认其电子邮件地址的标志
        /// </summary>
        [DisplayName("电子邮箱确认")]
        public bool EmailConfirmed { get; set; }

        /// <summary>
        /// 获取或设置 密码哈希值
        /// </summary>
        [DisplayName("密码哈希值")]
        public string PasswordHash { get; set; }

        /// <summary>
        /// 获取或设置 用户头像
        /// </summary>
        [DisplayName("用户头像")]
        public string HeadImg { get; set; }

        /// <summary>
        /// 获取或设置 每当用户凭据发生变化（密码更改、登录删除）时必须更改的随机值。
        /// </summary>
        [DisplayName("安全标识")]
        public string SecurityStamp { get; set; }

        /// <summary>
        /// 获取或设置 一个随机值，必须在用户持续存储时更改。
        /// </summary>
        [DisplayName("版本标识")]
        public string ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// 获取或设置 手机号码
        /// </summary>
        [DisplayName("手机号码")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 获取或设置 手机号码是否已确认
        /// </summary>
        [DisplayName("手机号码确定")]
        public bool PhoneNumberConfirmed { get; set; }

        /// <summary>
        /// 获取或设置 当任何用户锁定结束时，UTC的日期和时间。
        /// </summary>
        [DisplayName("锁定时间")]
        public DateTimeOffset? LockoutEnd { get; set; }

        /// <summary>
        /// 获取或设置 当前用户失败的登录尝试次数。
        /// </summary>
        [DisplayName("登录失败次数")]
        public int AccessFailedCount { get; set; }

        /// <summary>
        /// 获取或设置 是否系统用户
        /// </summary>
        [DisplayName("是否系统用户")]
        public bool IsSystem { get; set; }

        /// <summary>
        /// 获取或设置 是否锁定当前信息
        /// </summary>
        [DisplayName("是否锁定")]
        public bool IsLocked { get; set; }

        /// <summary>
        /// 获取或设置 创建时间
        /// </summary>
        [DisplayName("创建时间")]
        public DateTime CreatedTime { get; set; }
    }
}