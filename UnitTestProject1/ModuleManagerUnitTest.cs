// -----------------------------------------------------------------------
//  <copyright file="ModuleManagerUnitTest.cs" company="com.esoftor">
//      Copyright (c) 2014-2018 ESoftor. All rights reserved.
//  </copyright>
//  <last-editor>谭明超</last-editor>
//  <last-date>%time%</last-date>
// -----------------------------------------------------------------------

using ESoftor.Framework.Module;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTestProject1
{
    [TestClass]
    public class ModuleManagerUnitTest
    {
        [TestMethod]
        public void TestMethod()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddESoftor();
            IServiceProvider provider = services.BuildServiceProvider();


        }
    }
}
