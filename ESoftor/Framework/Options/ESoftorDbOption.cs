// -----------------------------------------------------------------------
//  <copyright file="ESoftorDbOption.cs" company="com.esoftor">
//      Copyright (c) 2014-2018 ESoftor. All rights reserved.
//  </copyright>
//  <last-editor>谭明超</last-editor>
//  <last-date>%time%</last-date>
// -----------------------------------------------------------------------

using System;

namespace ESoftor.Framework.Options
{
    /// <summary>
    ///     上下文配置选项
    /// </summary>
    public class ESoftorDbOption
    {
        /// <summary>
        ///     连接字符串
        /// </summary>
        public string ConnectString { get; set; }
        /// <summary>
        ///     数据库类型
        /// </summary>
        public DatabaseType DatabaseType { get; set; }
        /// <summary>
        ///     上下文类型
        /// </summary>
        public Type DbContextType => string.IsNullOrWhiteSpace(DbContextTypeName) ? null : Type.GetType(DbContextTypeName);
        /// <summary>
        ///     上下文类型名称
        /// </summary>
        public String DbContextTypeName { get; set; }
    }
}
