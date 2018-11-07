// -----------------------------------------------------------------------
//  <copyright file="AppAssemblyFinderUnitTest.cs" company="eSoft">
//      Copyright (c) 2014-2017 eSoft. All rights reserved.
//  </copyright>
//  <last-editor>谭明超</last-editor>
//  <last-date>$date$</last-date>
// -----------------------------------------------------------------------

using ESoftor.Framework;
using ESoftor.Reflection;
using ESoftor.Framework.Module;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace UnitTestProject1
{
    [TestClass]
    public class AppAssemblyFinderUnitTest
    {
        [TestMethod]
        public void Method1()
        {
            AppAssemblyFinder _finder = new AppAssemblyFinder();
            Type[] modules = _finder.FindTypes<ModuleBase>(
                type => type.IsDeriveClassFrom<ModuleBase>()
                );
            IEnumerable<ModuleBase> _modules = modules?.Select(m => (ModuleBase)Activator.CreateInstance(m)).OrderBy(m => m.ModuleLevel);

            Assert.IsTrue(_modules.Count() > 0);
        }
    }
}
