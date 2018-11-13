// -----------------------------------------------------------------------
//  <copyright file="UserRole.cs" company="com.esoftor">
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
    [Table(nameof(UserRole))]
    public class UserRole : EntityBase<Guid>
    {
        /// <summary>
        /// 获取或设置 用户编号
        /// </summary>
        [DisplayName("用户编号")]
        public Guid UserId { get; set; }

        /// <summary>
        /// 获取或设置 角色编号
        /// </summary>
        [DisplayName("角色编号")]
        public Guid RoleId { get; set; }

        /// <summary>
        /// 获取或设置 创建时间
        /// </summary>
        [DisplayName("创建时间")]
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// 获取或设置 是否锁定当前信息
        /// </summary>
        [DisplayName("是否锁定")]
        public bool IsLocked { get; set; }
    }
}
