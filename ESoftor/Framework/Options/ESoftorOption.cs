// -----------------------------------------------------------------------
//  <copyright file="ESoftorOptions.cs" company="com.esoftor">
//      Copyright (c) 2014-2018 ESoftor. All rights reserved.
//  </copyright>
//  <last-editor>谭明超</last-editor>
//  <last-date>%time%</last-date>
// -----------------------------------------------------------------------

namespace ESoftor.Framework.Options
{
    /// <summary>
    ///     框架配置选项
    /// </summary>
    public class ESoftorOption
    {
        /// <summary>
        ///     上下文对象配置
        /// </summary>
        public ESoftorDbOption ESoftorDbOption { get; set; }
        /// <summary>
        ///     Jwtoken对象配置
        /// </summary>
        public ESoftorJwtOption ESoftorJwtOption { get; set; }
    }
}
