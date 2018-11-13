// -----------------------------------------------------------------------
//  <copyright file="JwtUnitTest.cs" company="com.esoftor">
//      Copyright (c) 2014-2018 ESoftor. All rights reserved.
//  </copyright>
//  <last-editor>谭明超</last-editor>
//  <last-date>%time%</last-date>
// -----------------------------------------------------------------------
using Microsoft.Extensions.Configuration;
using ESoftor.Permission.Identity.JwtBearer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace UnitTestProject1.Jwt
{
    [TestClass]
    public class JwtUnitTest
    {
        [TestMethod]
        public void Method()
        {
            Claim[] claims =
            {
                new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Name, "zhangsan")
            };
            var token = JwtHelper.CreateToken(claims);
            Console.WriteLine(token);
        }
    }
}
