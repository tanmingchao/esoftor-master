// -----------------------------------------------------------------------
//  <copyright file="IdentityExtensions.cs" company="com.esoftor">
//      Copyright (c) 2014-2018 ESoftor. All rights reserved.
//  </copyright>
//  <last-editor>谭明超</last-editor>
//  <last-date>%time%</last-date>
// -----------------------------------------------------------------------

using ESoftor.Data;
using ESoftor.Extensions;
using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace ESoftor.Permission.Identity
{
    /// <summary>
    /// 
    /// </summary>
    public static class IdentityExtensions
    {
        /// <summary>
        /// 将<see cref="IdentityResult"/>转化为<see cref="OperationResult"/>
        /// </summary>
        public static OperationResult ToOperationResult(this IdentityResult identityResult)
        {
            return identityResult.Succeeded
                ? new OperationResult(OperationResultType.Success)
                : new OperationResult(OperationResultType.Error,
                    identityResult.Errors.Select(m => m.Description).ExpandAndToString());
        }

        /// <summary>
        /// 将<see cref="IdentityResult"/>转化为<see cref="OperationResult{TUser}"/>
        /// </summary>
        public static OperationResult<TUser> ToOperationResult<TUser>(this IdentityResult identityResult, TUser user)
        {
            return identityResult.Succeeded
                ? new OperationResult<TUser>(OperationResultType.Success, "Success", user)
                : new OperationResult<TUser>(OperationResultType.Error,
                    identityResult.Errors.Select(m => m.Description).ExpandAndToString());
        }

        /// <summary>
        /// 快速创建错误信息的IdentityResult
        /// </summary>
        public static IdentityResult Failed(this IdentityResult identityResult, params string[] errors)
        {
            var identityErrors = identityResult.Errors;
            identityErrors = identityErrors.Union(errors.Select(m => new IdentityError() { Description = m }));
            return IdentityResult.Failed(identityErrors.ToArray());
        }
    }
}
