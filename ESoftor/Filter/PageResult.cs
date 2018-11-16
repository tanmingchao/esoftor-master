// -----------------------------------------------------------------------
//  <copyright file="PageResult.cs" company="eSoft">
//      Copyright (c) 2014-2017 eSoft. All rights reserved.
//  </copyright>
//  <last-editor>谭明超</last-editor>
//  <last-date>$date$</last-date>
// -----------------------------------------------------------------------

using System;

namespace ESoftor.Filter
{
    /// <summary>
    /// 数据分页信息
    /// </summary>
    public class PageResult<T>
    {
        /// <summary>
        /// 初始化一个<see cref="PageResult{T}"/>类型的新实例
        /// </summary>
        public PageResult()
            : this(new T[0], 0)
        { }

        /// <summary>
        /// 初始化一个<see cref="PageResult{T}"/>类型的新实例
        /// </summary>
        public PageResult(T[] data, int total)
        {
            Data = data;
            Total = total;
        }

        /// <summary>
        /// 获取或设置 分页数据
        /// </summary>
        public T[] Data { get; set; }

        /// <summary>
        /// 获取或设置 总记录数
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// 转换为分页数据
        /// </summary>
        public PageData<T> ToPageData()
        {
            return new PageData<T>(Data, Total);
        }

        /// <summary>
        /// 转换为指定类型的页结果
        /// </summary>
        /// <typeparam name="TResult">页结果数据类型</typeparam>
        /// <param name="func">数据转移委托</param>
        /// <returns>页结果</returns>
        public PageResult<TResult> ToPageResult<TResult>(Func<T[], TResult[]> func)
        {
            return new PageResult<TResult>(func(Data), Total);
        }
    }
}
