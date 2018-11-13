// -----------------------------------------------------------------------
//  <copyright file="BastEntity.cs" company="com.esoftor">
//      Copyright (c) 2014-2018 ESoftor. All rights reserved.
//  </copyright>
//  <last-editor>谭明超</last-editor>
//  <last-date>%time%</last-date>
// -----------------------------------------------------------------------

using ESoftor.Data;
using ESoftor.Extensions;
using ESoftor.Framework.Infrastructure;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ESoftor.Framework.Entity
{
    public class EntityBase<TKey> : IEntity<TKey>
    {
        /// <summary>
        /// 初始化一个<see cref="EntityBase{TKey}"/>类型的新实例
        /// </summary>
        protected EntityBase()
        {
            if (typeof(TKey) == typeof(Guid))
            {
                ID = CombGuid.NewGuid().CastTo<TKey>();
            }
        }

        [Key]
        public TKey ID { get; set; }

        /// <summary>
        ///     说明
        /// </summary>
        [StringLength(128), Description("说明")]
        public string Remark { get; set; }

    }
}
