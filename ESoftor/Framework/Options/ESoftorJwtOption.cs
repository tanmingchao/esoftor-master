// -----------------------------------------------------------------------
//  <copyright file="JwtOptions.cs" company="com.esoftor">
//      Copyright (c) 2014-2018 ESoftor. All rights reserved.
//  </copyright>
//  <last-editor>谭明超</last-editor>
//  <last-date>%time%</last-date>
// -----------------------------------------------------------------------

namespace ESoftor.Framework.Options
{
    /// <summary>
    ///     jwt配置选项
    /// </summary>
    public class ESoftorJwtOption
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public string Secret { get; set; }
    }
}
