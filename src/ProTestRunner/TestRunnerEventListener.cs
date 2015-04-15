using System;
using System.Globalization;
using System.Text;
using NUnit.Core;

namespace ProTestRunner
{
    public sealed class TestRunnerEventListener : ITestRunnerEventListener
    {
        public event EventHandler OnCompletedRun;
        public event EventHandler OnLineOutput;
        public StringBuilder Output { get; set; }

        private int _totalTestsPassed;
        private int _totalTestsErrored;

        public TestRunnerEventListener()
        {
            Output = new StringBuilder();
        }

        public void RunStarted(string assemblyName, int testCount)
        {
            Output.AppendFormat(TimeStamp, "Running ", testCount , " tests in " , assemblyName, "\n");
            _totalTestsPassed = 0;
            _totalTestsErrored = 0;
        }

        public void RunFinished(Exception exception)
        {
            Output.AppendLine(TimeStamp + "Run errored: " + exception);
            if (OnCompletedRun != null)
            {
                OnCompletedRun(exception, new EventArgs());
            }
        }

        public void RunFinished(TestResult result)
        {
            Output.AppendLine(TimeStamp + "Run completed in " + result.Time + " seconds");
            Output.AppendLine(TimeStamp + _totalTestsPassed + " tests passed, " + _totalTestsErrored + " tests failed.");
            if (OnCompletedRun != null)
            {
                OnCompletedRun(result, new EventArgs());
            }
        }

        public void TestStarted(TestName testName)
        {
            Output.AppendLine(TimeStamp + testName.FullName);
        }

        public void TestOutput(TestOutput testOutput)
        {
            string textOutput = TimeStamp + testOutput.Text;
            Output.AppendLine(textOutput);
            if (OnLineOutput != null)
            {
                OnLineOutput(textOutput, new EventArgs());
            }
        }

        public void TestFinished(TestResult result)
        {
            if (result.IsSuccess)
            {
                Output.AppendLine(TimeStamp + "Test Passed!");
                _totalTestsPassed++;
            }
            else
            {
                Output.AppendLine(TimeStamp + "Test FAILED!");
                _totalTestsErrored++;
            }
        }

        public void UnhandledException(Exception exception)
        {
            Output.AppendLine(TimeStamp + "Unhandled Exception: " + exception +"\n\t StackTrace: "+exception.StackTrace);
        }

        /// <summary>
        /// Does nothing.
        /// </summary>
        public void SuiteStarted(TestName testName)
        {
        }

        /// <summary>
        /// Does nothing.
        /// </summary>
        public void SuiteFinished(TestResult result)
        {
        }

        private string TimeStamp
        {
            get
            {
                return "[" + DateTime.Now.ToString(CultureInfo.InvariantCulture) + "] ";
            }
        }
    }
}