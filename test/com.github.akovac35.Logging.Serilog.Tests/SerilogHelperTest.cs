// License:
// Apache License Version 2.0, January 2004

// Authors:
//   Aleksander Kovač

using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using Serilog;
using Serilog.Core;
using System.IO;
using System.Threading.Tasks;

namespace com.github.akovac35.Logging.Serilog.Tests
{
    [TestFixture]
    public class SerilogHelperTest
    {
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
        }

        [SetUp]
        public void SetUp()
        {
        }

        private string serilogConfig = "serilog.json";

        private string validJson = "serilog_valid.json";

        private string invalidJson = "serilog_invalid.json";

        [Test]
        public async System.Threading.Tasks.Task UpdateLogger_ForValidJson_CreatesLogger()
        {
            Log.Logger = new SilentLogger();
            Assert.IsFalse(Log.Logger is Logger);

            CopyFile(serilogConfig, validJson);
            IConfigurationBuilder configurationBuilder;
            SerilogHelper.CreateLogger(configure =>
            {
                configurationBuilder = configure.AddJsonFile(JsonFilePath(serilogConfig), optional: false, reloadOnChange: true);
            });
            Assert.IsTrue(Log.Logger is Logger);

            Log.Logger = new SilentLogger();
            Assert.IsFalse(Log.Logger is Logger);

            UpdateFile(serilogConfig);
            await WaitForReloadEvent();
            Assert.IsTrue(Log.Logger is Logger);
        }

        [Test]
        public async System.Threading.Tasks.Task UpdateLogger_ForInvalidJson_IsAbleToReload()
        {
            Log.Logger = new SilentLogger();
            Assert.IsFalse(Log.Logger is Logger);

            CopyFile(serilogConfig, validJson);
            IConfigurationBuilder configurationBuilder;
            SerilogHelper.CreateLogger(configure =>
            {
                configurationBuilder = configure.AddJsonFile(JsonFilePath(serilogConfig), optional: false, reloadOnChange: true);
            });
            Assert.IsTrue(Log.Logger is Logger);

            Log.Logger = new SilentLogger();
            Assert.IsFalse(Log.Logger is Logger);

            // Verify reload does not occur for invalid json
            CopyFile(serilogConfig, invalidJson);
            await WaitForReloadEvent();
            Assert.IsFalse(Log.Logger is Logger);

            // Verify reload token is still active
            CopyFile(serilogConfig, validJson);
            await WaitForReloadEvent();
            Assert.IsTrue(Log.Logger is Logger);
        }

        private void UpdateFile(string fileName, string contents = null)
        {
            string json = File.ReadAllText(JsonFilePath(fileName));
            File.WriteAllText(JsonFilePath(fileName), contents == null ? $"{json} " : contents);
        }

        private void CopyFile(string destination, string source)
        {
            string json = File.ReadAllText(JsonFilePath(source));
            File.WriteAllText(JsonFilePath(destination), json);
        }

        private string JsonFilePath(string fileName)
        {
            return Path.Combine(TestContext.CurrentContext.TestDirectory, fileName);
        }

        private async Task WaitForReloadEvent(int durationSeconds = 5)
        {
            int i = durationSeconds;
            while (i > 0)
            {
                i--;
                if (Log.Logger is Logger) break;
                await Task.Delay(1000);
            }
        }
    }
}
