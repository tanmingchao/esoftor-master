// -----------------------------------------------------------------------
//  <copyright file="OperationResult.cs" company="com.esoftor">
//      Copyright (c) 2014-2018 ESoftor. All rights reserved.
//  </copyright>
//  <last-editor>谭明超</last-editor>
//  <last-date>%time%</last-date>
// -----------------------------------------------------------------------

using ESoftor.Extensions;
using System;
using System.Linq;

namespace ESoftor.Data
{
    /// <summary>
    /// 业务操作结果信息类，对操作结果进行封装
    /// </summary>
    public class OperationResult : OperationResult<object>
    {
        static OperationResult()
        {
            Success = new OperationResult(OperationResultType.Success);
            NoChanged = new OperationResult(OperationResultType.NoChanged);
        }

        /// <summary>
        /// 初始化一个<see cref="OperationResult"/>类型的新实例
        /// </summary>
        public OperationResult()
            : this(OperationResultType.NoChanged)
        { }

        /// <summary>
        /// 初始化一个<see cref="OperationResult"/>类型的新实例
        /// </summary>
        public OperationResult(OperationResultType resultType)
            : this(resultType, null, null)
        { }

        /// <summary>
        /// 初始化一个<see cref="OperationResult"/>类型的新实例
        /// </summary>
        public OperationResult(OperationResultType resultType, string message)
            : this(resultType, message, null)
        { }

        /// <summary>
        /// 初始化一个<see cref="OperationResult"/>类型的新实例
        /// </summary>
        public OperationResult(OperationResultType resultType, string message, object data)
            : base(resultType, message, data)
        { }

        /// <summary>
        /// 获取 成功的操作结果
        /// </summary>
        public static OperationResult Success { get; private set; }

        /// <summary>
        /// 获取 未变更的操作结果
        /// </summary>
        public new static OperationResult NoChanged { get; private set; }

        /// <summary>
        /// 将<see cref="OperationResult{TData}"/>转换为<see cref="OperationResult"/>
        /// </summary>
        /// <returns></returns>
        public OperationResult<T> ToOperationResult<T>()
        {
            T data = default(T);
            if (Data is T variable)
            {
                data = variable;
            }
            return new OperationResult<T>(ResultType, Message, data);
        }
    }


    /// <summary>
    /// 泛型版本的业务操作结果信息类，对操作结果进行封装
    /// </summary>
    /// <typeparam name="TData">返回数据的类型</typeparam>
    public class OperationResult<TData>
    {
        static OperationResult()
        {
            NoChanged = new OperationResult<TData>(OperationResultType.NoChanged);
        }

        /// <summary>
        /// 初始化一个<see cref="OperationResult"/>类型的新实例
        /// </summary>
        public OperationResult()
            : this(OperationResultType.NoChanged)
        { }

        /// <summary>
        /// 初始化一个<see cref="OperationResult{TData}"/>类型的新实例
        /// </summary>
        public OperationResult(OperationResultType resultType)
            : this(resultType, null, default(TData))
        { }

        /// <summary>
        /// 初始化一个<see cref="OperationResult{TData}"/>类型的新实例
        /// </summary>
        public OperationResult(OperationResultType resultType, string message)
            : this(resultType, message, default(TData))
        { }

        /// <summary>
        /// 初始化一个<see cref="OperationResult{TData}"/>类型的新实例
        /// </summary>
        public OperationResult(OperationResultType resultType, string message, TData data)
        {
            this.ResultType = resultType;
            this.Message = string.IsNullOrWhiteSpace(message) ? ResultType.ToDescription() : message;
            this.Data = data;
        }

        /// <summary>
        /// 获取或设置 返回消息
        /// </summary>
        public string Message { get; set; }
        public OperationResultType ResultType { get; set; }
        public TData Data { get; set; }

        /// <summary>
        /// 获取 未变更的操作结果
        /// </summary>
        public static OperationResult<TData> NoChanged { get; private set; }

        /// <summary>
        /// 获取 是否成功
        /// </summary>
        public bool Successed
        {
            get { return ResultType == OperationResultType.Success; }
        }

        /// <summary>
        /// 获取 是否失败
        /// </summary>
        public bool Errored
        {
            get
            {
                bool contains = new[] {
                    OperationResultType.ValidError,
                    OperationResultType.QueryNull,
                    OperationResultType.Error
                }.Contains(ResultType);
                return contains;
            }
        }

        /// <summary>
        /// 将<see cref="OperationResult{TData}"/>转换为<see cref="OperationResult"/>
        /// </summary>
        /// <returns></returns>
        public OperationResult ToOperationResult()
        {
            return new OperationResult(ResultType, Message, Data);
        }
    }
}
