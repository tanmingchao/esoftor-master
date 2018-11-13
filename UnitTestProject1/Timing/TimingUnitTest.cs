// -----------------------------------------------------------------------
//  <copyright file="TimingUnitTest.cs" company="com.esoftor">
//      Copyright (c) 2014-2018 ESoftor. All rights reserved.
//  </copyright>
//  <last-editor>谭明超</last-editor>
//  <last-date>%time%</last-date>
// -----------------------------------------------------------------------

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using ESoftor.Timing;

namespace UnitTestProject1.Timing
{
    [TestClass]
    public class TimingUnitTest
    {
        [TestMethod]
        public void Method()
        {
            var timeStamp = DateTime.UtcNow.ToJsGetTime();
            var timeStamp2 = (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000;
            Assert.AreEqual(timeStamp, timeStamp2);
        }
    }
}
