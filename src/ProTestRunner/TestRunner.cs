using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Core;

namespace ProTestRunner
{
    /// <summary>
    /// Allows you to run NUnit tests contained in an assembly. The tests discovery is done by a <see cref="NUnit.Core.RemoteTestRunner"/> or your own Runner.
    /// </summary>
    public sealed class TestRunner : ITestRunner, IDisposable
    {
        private NUnit.Core.TestRunner _testRunner;
        private readonly Dictionary<string, string> _testResults = new Dictionary<string, string>();

        /// <summary>
        /// Creates an instance with a RemoteTestRunner as the test runner.
        /// </summary>
        public TestRunner()
        {
            _testRunner = new RemoteTestRunner();
        }

        /// <summary>
        /// Creates an instance with the provided <see cref="NUnit.Core.TestRunner"/> test runner.
        /// </summary>
        public TestRunner(NUnit.Core.TestRunner testRunner)
        {
            _testRunner = testRunner;
        }

        /// <summary>
        /// Runs all the NUnit tests contained in the assembly with the possibility of outputting every line or just results as signaled by the flag isVerbose.
        /// </summary>
        /// <remarks>
        /// Default implementation for every line output by a test is to print to <see cref="Console"/>, on completed run the runner is disposed.
        /// </remarks>
        /// <param name="pathToTestContainerAssembly">The absolute path to the DLL that contains the tests.</param>
        /// <param name="isVerbose">If it should output every line or just results.</param>
        /// <returns>A dictionary with the name of the test as key and its result as value</returns>
        public IDictionary<string, string> RunAssemblyTests(string pathToTestContainerAssembly, bool isVerbose)
        {
            return RunAssemblyTests(pathToTestContainerAssembly, isVerbose, TestFilter.Empty, LoggingThreshold.Warn, null, null, null);
        }

        /// <summary>
        /// Runs the unit tests contained in the assembly that passes the filter and outputs messages according to the given threshold.
        /// </summary>
        /// <param name="pathToTestContainerAssembly">The absolute path to the DLL that contains the tests.</param>
        /// <param name="isVerbose">If it should output every line or just results.</param>
        /// <param name="testFilter">The filter to decide which tests to run.</param>
        /// <param name="loggingThreshold">The threshold above which messages will be output.</param>
        /// <returns>A dictionary with the name of the test as key and its result as value</returns>
        public IDictionary<string, string> RunAssemblyTests(
            string pathToTestContainerAssembly,
            bool isVerbose,
            ITestFilter testFilter,
            LoggingThreshold loggingThreshold)
        {
            return RunAssemblyTests(pathToTestContainerAssembly, isVerbose, testFilter, loggingThreshold, null, null);
        }

        /// <summary>
        /// Runs all the NUnit tests contained in the assembly with the possibility of determining what happens on tests completed and line output.
        /// </summary>
        /// <param name="pathToTestContainerAssembly">The absolute path to the DLL that contains the tests.</param>
        /// <param name="isVerbose">If it should output every line or just results.</param>
        /// <param name="onLineOutput">What happens when a line is output is <paramref name="isVerbose"/> is true.</param>
        /// <param name="onCompletedRun">What happens when the run has finished.</param>
        /// <returns>A dictionary with the name of the test as key and its result as value</returns>
        public IDictionary<string, string> RunAssemblyTests(
            string pathToTestContainerAssembly,
            bool isVerbose,
            EventHandler onLineOutput,
            EventHandler onCompletedRun)
        {
            return RunAssemblyTests(pathToTestContainerAssembly, isVerbose, TestFilter.Empty, LoggingThreshold.Warn, onLineOutput: onLineOutput, onCompletedRun: onCompletedRun, eventListener: null);
        }

        /// <summary>
        /// Runs the unit tests contained in the assembly that passes the filter and outputs messages according to the given threshold.
        /// </summary>
        /// <param name="pathToTestContainerAssembly">The absolute path to the DLL that contains the tests.</param>
        /// <param name="isVerbose">If it should output every line or just results.</param>
        /// <param name="testFilter">The filter to decide which tests to run.</param>
        /// <param name="loggingThreshold">The threshold above which messages will be output.</param>
        /// <param name="onLineOutput">What happens when a line is output is <paramref name="isVerbose"/> is true.</param>
        /// <param name="onCompletedRun">What happens when the run has finished.</param>
        /// <returns>A dictionary with the name of the test as key and its result as value</returns>
        public IDictionary<string, string> RunAssemblyTests(
            string pathToTestContainerAssembly,
            bool isVerbose,
            ITestFilter testFilter,
            LoggingThreshold loggingThreshold,
            EventHandler onLineOutput,
            EventHandler onCompletedRun)
        {
            return RunAssemblyTests(pathToTestContainerAssembly, isVerbose, testFilter, loggingThreshold, onLineOutput, onCompletedRun, null);
        }

        /// <summary>
        /// Runs all the NUnit tests contained in the assembly with the possibility of defining your own <see cref="ITestRunnerEventListener"/>
        /// using an empty TestFilter and LoggingThreshold of Warn.
        /// </summary>
        /// <param name="pathToTestContainerAssembly">The absolute path to the DLL that contains the tests.</param>
        /// <param name="isVerbose">If it should output every line or just results.</param>
        /// <param name="eventListener">An <see cref="ITestRunnerEventListener"/> that contains the output and onLineOutput and onCompletedRun events.</param>
        /// <returns>A dictionary with the name of the test as key and its result as value</returns>
        /// <remarks>Logging Threshold is Warn.</remarks>
        public IDictionary<string, string> RunAssemblyTests(
            string pathToTestContainerAssembly,
            bool isVerbose,
            ITestRunnerEventListener eventListener)
        {
            return RunAssemblyTests(pathToTestContainerAssembly, isVerbose, TestFilter.Empty, LoggingThreshold.Warn, onLineOutput: null, onCompletedRun: null, eventListener: eventListener);
        }

        /// <summary>
        /// Runs all the NUnit tests contained in the assembly allowing you to define an <see cref="ITestFilter"/>, <see cref="LoggingThreshold"/>,
        /// what happends when the run is completed, a line is outputted and an <see cref="ITestRunnerEventListener"/>
        /// </summary>
        /// <param name="pathToTestContainerAssembly">The absolute path to the DLL that contains the tests.</param>
        /// <param name="isVerbose">If it should output every line or just results.</param>
        /// <param name="testFilter">The filter to decide which tests to run.</param>
        /// <param name="loggingThreshold">The threshold above which messages will be output by the NUnit TestRunner, does not affect the returned dictionary.</param>
        /// <param name="eventListener">An <see cref="ITestRunnerEventListener"/> that contains the output and onLineOutput and onCompletedRun events.</param>
        /// <returns>A dictionary with the name of the test as key and its result as value</returns>
        public IDictionary<string, string> RunAssemblyTests(
            string pathToTestContainerAssembly,
            bool isVerbose,
            ITestFilter testFilter,
            LoggingThreshold loggingThreshold,
            ITestRunnerEventListener eventListener = null)
        {
            return RunAssemblyTests(pathToTestContainerAssembly, isVerbose, testFilter, loggingThreshold, onLineOutput: null, onCompletedRun: null, eventListener: eventListener);
        }

        private IDictionary<string, string> RunAssemblyTests(
            string pathToTestContainerAssembly, 
            bool isVerbose, 
            ITestFilter testFilter, 
            LoggingThreshold loggingThreshold, 
            EventHandler onLineOutput = null, 
            EventHandler onCompletedRun = null, 
            ITestRunnerEventListener eventListener = null)
        {
            var consoleOut = Console.Out;

            if (eventListener == null)
            {
                eventListener = new TestRunnerEventListener();

                RegisterCompletedRunEvent(eventListener, onCompletedRun);

                if (isVerbose)
                {
                    RegisterLineOutputEvent(eventListener, onLineOutput, consoleOut);
                }
            }

            var testPackage = new TestPackage(pathToTestContainerAssembly, new List<string> { pathToTestContainerAssembly });
            _testRunner.Load(testPackage);

            TestResult results = _testRunner.Run(eventListener, testFilter, false, loggingThreshold);
            
            BuildTestResults(results, consoleOut);

            return _testResults;
        }

        private void RegisterCompletedRunEvent(ITestRunnerEventListener testRunnerEventListener, EventHandler onCompletedRun)
        {
            EventHandler defaultOnCompletedRun = (sender, args) => _testRunner.CancelRun();

            testRunnerEventListener.OnCompletedRun += onCompletedRun ?? defaultOnCompletedRun;
        }

        private void RegisterLineOutputEvent(ITestRunnerEventListener testRunnerEventListener, EventHandler onLineOutput, TextWriter consoleOut)
        {
            EventHandler defaultOnLineOutput = (sender, args) =>
                {
                    Console.SetOut(consoleOut);
                    Console.WriteLine(testRunnerEventListener.Output);
                };

            testRunnerEventListener.OnLineOutput += onLineOutput ?? defaultOnLineOutput;
        }

        private void BuildTestResults(TestResult result, TextWriter consoleOut)
        {
            Console.SetOut(consoleOut);
            if (result.HasResults)
            {
                foreach (var childResult in result.Results)
                {
                    BuildTestResults((TestResult)childResult, consoleOut);
                }
                return;
            }

            _testResults.Add(result.FullName, result.ResultState.ToString());
        }

        public void Dispose()
        {
            _testRunner.Dispose();
        }
    }
}