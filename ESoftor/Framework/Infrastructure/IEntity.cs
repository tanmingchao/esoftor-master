// -----------------------------------------------------------------------
//  <copyright file="IEntity.cs" company="eSoft">
//      Copyright (c) 2014-2017 eSoft. All rights reserved.
//  </copyright>
//  <last-editor>谭明超</last-editor>
//  <last-date>$date$</last-date>
// -----------------------------------------------------------------------

namespace ESoftor.Framework.Infrastructure
{
    /// <summary>
    ///     实体接口对象
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public interface IEntity<out TKey>
    {
        TKey ID { get; }
    }
}
