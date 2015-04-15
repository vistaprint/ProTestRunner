using System;
using System.Text;
using NUnit.Core;

namespace ProTestRunner.UnitTests
{
    internal class SampleEventListener : ITestRunnerEventListener
    {
        public event EventHandler OnCompletedRun;
        public event EventHandler OnLineOutput;
        public StringBuilder Output { get; set; }

        public SampleEventListener()
        {
            Output = new StringBuilder();
            OnCompletedRun += (sender, args) => Output.AppendLine(sender.ToString());
            OnLineOutput += (sender, args) => Output.AppendLine(sender.ToString());
        }

        public void RunStarted(string name, int testCount)
        {
            Output.AppendLine("Run started!");
        }

        public void RunFinished(TestResult result)
        {
            Output.AppendLine("Run finished!");
        }

        public void RunFinished(Exception exception)
        {
           OnCompletedRun("Run finished with an exception!", new EventArgs());
        }

        public void TestStarted(TestName testName)
        {
            Output.AppendLine("Test "+testName.Name+" started!");
        }

        public void TestFinished(TestResult result)
        {
            Output.AppendLine("Run finished!");
        }

        public void SuiteStarted(TestName testName)
        {
            Output.AppendLine("Suite started!");
        }

        public void SuiteFinished(TestResult result)
        {
            Output.AppendLine("Suite Finished!");
        }

        public void UnhandledException(Exception exception)
        {
            Output.AppendLine("Something wrong happened :(");
        }

        public void TestOutput(TestOutput testOutput)
        {
            OnLineOutput(testOutput.Text, new EventArgs());
        }
    }
}
