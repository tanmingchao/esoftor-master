// -----------------------------------------------------------------------
//  <copyright file="UserDetail.cs" company="com.esoftor">
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
    [Table(nameof(UserDetail))]
    public class UserDetail : EntityBase<Guid>
    {
        /// <summary>
        /// 获取或设置 注册IP
        /// </summary>
        [DisplayName("注册IP")]
        public string RegisterIp { get; set; }

        /// <summary>
        /// 获取或设置 用户编号
        /// </summary>
        [DisplayName("用户编号")]
        public virtual int UserId { get; set; }


    }
}
