// -----------------------------------------------------------------------
//  <copyright file="Role.cs" company="com.esoftor">
//      Copyright (c) 2014-2018 ESoftor. All rights reserved.
//  </copyright>
//  <last-editor>谭明超</last-editor>
//  <last-date>%time%</last-date>
// -----------------------------------------------------------------------

using ESoftor.Framework.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ESoftor.Core.Permission.Identity.Entity
{
    [Table(nameof(Role))]
    public class Role : EntityBase<Guid>
    {
        /// <summary>
        /// 获取或设置 角色名称
        /// </summary>
        [Required, DisplayName("角色名称")]
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置 一个随机值，每当某个角色被保存到存储区时，该值将发生变化。
        /// </summary>
        [DisplayName("版本标识")]
        public string ConcurrencyStamp { get; set; } = Guid.NewGuid().ToString();

        /// <summary>
        /// 获取或设置 是否管理员角色
        /// </summary>
        [DisplayName("是否管理员角色")]
        public bool IsAdmin { get; set; }

        /// <summary>
        /// 获取或设置 是否默认角色，用户注册后拥有此角色
        /// </summary>
        [DisplayName("是否默认角色")]
        public bool IsDefault { get; set; }

        /// <summary>
        /// 获取或设置 是否锁定
        /// </summary>
        [DisplayName("是否锁定")]
        public bool IsLocked { get; set; }

        /// <summary>
        /// 获取设置 信息创建时间
        /// </summary>
        [DisplayName("创建时间")]
        public DateTime CreatedTime { get; set; }
    }
}
