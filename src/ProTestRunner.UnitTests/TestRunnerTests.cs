using System.Collections.Generic;
using System.IO;
using System.Text;
using NUnit.Framework;
using System;

namespace ProTestRunner.UnitTests
{
    [TestFixture]
    public class TestRunnerTests
    {
        private const string TEST_ASSEMBLY_NAME = "ProTestRunner.SampleUnitTests.dll";

        private ITestRunner _testRunner;

        [SetUp]
        public void SetUp()
        {
            _testRunner = new TestRunner();
        }

        [Test]
        public void RunAssemblyTests_DefaultConfig_OutputsLinesAndResults()
        {
            IDictionary<string, string> results = _testRunner.RunAssemblyTests(FindTestAssembly(), true);
            foreach (var result in results)
            {
                Console.WriteLine(result.Key +" "+ result.Value);
            }
        }

        [Test]
        public void RunAssemblyTests_VerboseIsActivatedWithCustomEvents_DoesCustomActions()
        {
            var sb = new StringBuilder();

            EventHandler onCompletedRun = (obj, arg) => sb.AppendLine("Tests finished!");

            EventHandler onLineOutput = (obj, arg) => sb.AppendLine(obj.ToString());

            IDictionary<string, string> results = _testRunner.RunAssemblyTests(FindTestAssembly(), true, onLineOutput, onCompletedRun);

            Console.WriteLine(sb.ToString());
        }

        [Test]
        public void RunAssemblyTests_CustomEventListenerProvided_UsesCustomEventListener()
        {
            var sampleEventListener = new SampleEventListener();
            IDictionary<string, string> results = _testRunner.RunAssemblyTests(FindTestAssembly(), true, sampleEventListener);
            Console.WriteLine(sampleEventListener.Output.ToString());
        }

        private static string FindTestAssembly()
        {
            string consoleAppAssemblyPath = System.Reflection.Assembly.GetExecutingAssembly().CodeBase;
            consoleAppAssemblyPath = Path.GetDirectoryName(consoleAppAssemblyPath).Replace("file:\\", "");

            consoleAppAssemblyPath = Path.Combine(consoleAppAssemblyPath, TEST_ASSEMBLY_NAME);

            return consoleAppAssemblyPath;
        }
    }
}
