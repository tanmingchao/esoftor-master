// -----------------------------------------------------------------------
//  <copyright file="AjaxResult.cs" company="com.esoftor">
//      Copyright (c) 2014-2018 ESoftor. All rights reserved.
//  </copyright>
//  <last-editor>谭明超</last-editor>
//  <last-date>%time%</last-date>
// -----------------------------------------------------------------------

namespace ESoftor.AspNetCore.UI
{
    /// <summary>
    /// 表示Ajax操作结果 
    /// </summary>
    public class AjaxResult
    {
        /// <summary>
        /// 初始化一个<see cref="AjaxResult"/>类型的新实例
        /// </summary>
        public AjaxResult()
            : this(null)
        { }

        /// <summary>
        /// 初始化一个<see cref="AjaxResult"/>类型的新实例
        /// </summary>
        public AjaxResult(string content, AjaxResultType type = AjaxResultType.Success, object data = null)
            : this(content, data, type)
        { }

        /// <summary>
        /// 初始化一个<see cref="AjaxResult"/>类型的新实例
        /// </summary>
        public AjaxResult(string content, object data, AjaxResultType type = AjaxResultType.Success)
        {
            Type = type;
            Content = content;
            Data = data;
        }

        /// <summary>
        /// 获取或设置 Ajax操作结果类型
        /// </summary>
        public AjaxResultType Type { get; set; }

        /// <summary>
        /// 获取或设置 消息内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 获取或设置 返回数据
        /// </summary>
        public object Data { get; set; }

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Successed()
        {
            return Type == AjaxResultType.Success;
        }

        /// <summary>
        /// 是否错误
        /// </summary>
        public bool Error()
        {
            return Type == AjaxResultType.Error;
        }

        /// <summary>
        /// 成功的AjaxResult
        /// </summary>
        public static AjaxResult Success(object data = null)
        {
            return new AjaxResult("操作执行成功", AjaxResultType.Success, data);
        }
    }
}
