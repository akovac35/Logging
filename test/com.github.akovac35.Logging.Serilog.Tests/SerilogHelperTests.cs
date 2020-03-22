using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using Serilog;
using Serilog.Core;
using System.IO;
using System.Threading.Tasks;

namespace com.github.akovac35.Logging.Serilog.Tests
{
    [TestFixture]
    public class SerilogHelperTests
    {
        [OneTimeSetUp]
        public void Setup()
        {
        }

        [Test, Order(1)]
        public async System.Threading.Tasks.Task UpdateLoggerTestForValidJsonAsync()
        {
            string validJson = "serilog_valid.json";
            // Not yet configured
            Assert.IsFalse(Log.Logger is Logger);

            IConfigurationBuilder configurationBuilder;
            SerilogHelper.CreateLogger(configure =>
            {
                configurationBuilder = configure.AddJsonFile(JsonFilePath(validJson), optional: false, reloadOnChange: true);
            });
            Assert.IsTrue(Log.Logger is Logger);

            Log.Logger = new SilentLogger();
            Assert.IsFalse(Log.Logger is Logger);

            UpdateFile(validJson);
            await WaitForReloadEvent();
            Assert.IsTrue(Log.Logger is Logger);
        }

        [Test, Order(2)]
        public async System.Threading.Tasks.Task UpdateLoggerTestForInvalidJsonAsync()
        {
            string validJson = "serilog_valid.json";
            string invalidJson = "serilog_invalid.json";

            // Reset configuration
            IConfigurationBuilder configurationBuilder;
            SerilogHelper.CreateLogger(configure =>
            {
                configurationBuilder = configure.AddJsonFile(JsonFilePath(validJson), optional: false, reloadOnChange: true);
            });
            Assert.IsTrue(Log.Logger is Logger);

            Log.Logger = new SilentLogger();
            Assert.IsFalse(Log.Logger is Logger);

            // Verify reload does not occur for invalid json
            string originalJson = File.ReadAllText(JsonFilePath(validJson));
            ReplaceFile(validJson, invalidJson);
            await WaitForReloadEvent();
            Assert.IsFalse(Log.Logger is Logger);

            // Verify reload token is still active
            UpdateFile(validJson, originalJson);
            await WaitForReloadEvent();
            Assert.IsTrue(Log.Logger is Logger);
        }

        public void UpdateFile(string fileName, string contents = null)
        {
            string json = File.ReadAllText(JsonFilePath(fileName));
            File.WriteAllText(JsonFilePath(fileName), contents == null ? $"{json} " : contents);
        }

        public void ReplaceFile(string destination, string source)
        {
            string json = File.ReadAllText(JsonFilePath(source));
            File.WriteAllText(JsonFilePath(destination), json);
        }

        public string JsonFilePath(string fileName)
        {
            return Path.Combine(TestContext.CurrentContext.TestDirectory, fileName);
        }

        public async Task WaitForReloadEvent(int durationSeconds = 5)
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
