// -----------------------------------------------------------------------
//  <copyright file="AjaxResultExtensions.cs" company="com.esoftor">
//      Copyright (c) 2014-2018 ESoftor. All rights reserved.
//  </copyright>
//  <last-editor>谭明超</last-editor>
//  <last-date>%time%</last-date>
// -----------------------------------------------------------------------

using ESoftor.Data;
using ESoftor.Extensions;

namespace ESoftor.AspNetCore.UI
{
    /// <summary>
    /// <see cref="AjaxResult"/>扩展方法
    /// </summary>
    public static class AjaxResultExtensions
    {
        /// <summary>
        /// 将业务操作结果转ajax操作结果
        /// </summary>
        public static AjaxResult ToAjaxResult<T>(this OperationResult<T> result)
        {
            string content = result.Message ?? result.ResultType.ToDescription();
            AjaxResultType type = result.ResultType.ToAjaxResultType();
            return new AjaxResult(content, type, result.Data);
        }

        /// <summary>
        /// 将业务操作结果转ajax操作结果
        /// </summary>
        public static AjaxResult ToAjaxResult(this OperationResult result)
        {
            string content = result.Message ?? result.ResultType.ToDescription();
            AjaxResultType type = result.ResultType.ToAjaxResultType();
            return new AjaxResult(content, type);
        }

        /// <summary>
        /// 把业务结果类型<see cref="OperationResultType"/>转换为Ajax结果类型<see cref="AjaxResultType"/>
        /// </summary>
        public static AjaxResultType ToAjaxResultType(this OperationResultType resultType)
        {
            switch (resultType)
            {
                case OperationResultType.Success:
                    return AjaxResultType.Success;
                case OperationResultType.NoChanged:
                    return AjaxResultType.Info;
                default:
                    return AjaxResultType.Error;
            }
        }

        /// <summary>
        /// 判断业务结果类型是否是Error结果
        /// </summary>
        public static bool IsError(this OperationResultType resultType)
        {
            return resultType == OperationResultType.QueryNull
                || resultType == OperationResultType.ValidError
                || resultType == OperationResultType.Error;
        }
    }
}
