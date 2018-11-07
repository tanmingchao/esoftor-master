using ESoftor.Framework;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace UnitTestProject1
{
    [TestClass]
    public class AppSettnigManagerUnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false)
                .AddJsonFile("appsettings.Development.json", true);
            IConfiguration configuration = builder.Build();

            var conString1 = configuration["ESoftor:DbContexts:Default:ConnectString"];
            string conString2 = AppSettingManager.Get("ESoftor:DbContexts:Default:ConnectString");

            Assert.AreEqual(conString1, conString2);
        }
    }
}
