using System;
using System.Collections.Generic;
using NUnit.Core;

namespace ProTestRunner
{
    public interface ITestRunner
    {

        /// <summary>
        /// Runs all the NUnit tests contained in the assembly with the possibility of outputing every line or just results as signaled by the flag isVerbose.
        /// </summary>
        /// <remarks>
        /// Default implementation for every line output by a test is to print to <see cref="Console"/>, on completed run the runner is disposed.
        /// </remarks>
        /// <param name="pathToTestContainerAssembly">The absolute path to the DLL that contains the tests.</param>
        /// <param name="isVerbose">If it should output every line or just results.</param>
        /// <returns>A dictionary with the name of the test as key and its result as value</returns>
        IDictionary<string, string> RunAssemblyTests(string pathToTestContainerAssembly, bool isVerbose);

        /// <summary>
        /// Runs the unit tests contained in the assembly that passes the filter and outputs messages according to the given threshold.
        /// </summary>
        /// <param name="pathToTestContainerAssembly">The absolute path to the DLL that contains the tests.</param>
        /// <param name="isVerbose">If it should output every line or just results.</param>
        /// <param name="testFilter">The filter to decide which tests to run.</param>
        /// <param name="loggingThreshold">The threshold above which messages will be output by the NUnit TestRunner, does not affect the returned dictionary.</param>
        /// <returns>A dictionary with the name of the test as key and its result as value</returns>
        IDictionary<string, string> RunAssemblyTests(
            string pathToTestContainerAssembly, 
            bool isVerbose,
            ITestFilter testFilter,
            LoggingThreshold loggingThreshold);
        
        /// <summary>
        /// Runs all the NUnit tests contained in the assembly with the possibility of determining what happens on tests completed and line output.
        /// </summary>
        /// <param name="pathToTestContainerAssembly">The absolute path to the DLL that contains the tests.</param>
        /// <param name="isVerbose">If it should output every line or just results.</param>
        /// <param name="onLineOutput">What happens when a line is output is <paramref name="isVerbose"/> is true.</param>
        /// <param name="onCompletedRun">What happens when the run has finished.</param>
        /// <returns>A dictionary with the name of the test as key and its result as value</returns>
        IDictionary<string, string> RunAssemblyTests(
            string pathToTestContainerAssembly, 
            bool isVerbose, 
            EventHandler onLineOutput, 
            EventHandler onCompletedRun);

        /// <summary>
        /// Runs the unit tests contained in the assembly that passes the filter and outputs messages according to the given threshold.
        /// </summary>
        /// <param name="pathToTestContainerAssembly">The absolute path to the DLL that contains the tests.</param>
        /// <param name="isVerbose">If it should output every line or just results.</param>
        /// <param name="testFilter">The filter to decide which tests to run.</param>
        /// <param name="loggingThreshold">The threshold above which messages will be output by the NUnit TestRunner, does not affect the returned dictionary.</param>
        /// <param name="onLineOutput">What happens when a line is output is <paramref name="isVerbose"/> is true.</param>
        /// <param name="onCompletedRun">What happens when the run has finished.</param>
        /// <returns>A dictionary with the name of the test as key and its result as value</returns>
        IDictionary<string, string> RunAssemblyTests(
            string pathToTestContainerAssembly,
            bool isVerbose,
            ITestFilter testFilter,
            LoggingThreshold loggingThreshold,
            EventHandler onLineOutput,
            EventHandler onCompletedRun); 
            
        /// <summary>
        /// Runs all the NUnit tests contained in the assembly with the possibility of defining your own <see cref="ITestRunnerEventListener"/>
        /// using an empty TestFilter and LoggingThreshold of Warn.
        /// </summary>
        /// <param name="pathToTestContainerAssembly">The absolute path to the DLL that contains the tests.</param>
        /// <param name="isVerbose">If it should output every line or just results.</param>
        /// <param name="eventListener">An <see cref="ITestRunnerEventListener"/> that contains the output and onLineOutput and onCompletedRun events.</param>
        /// <returns>A dictionary with the name of the test as key and its result as value</returns>
        IDictionary<string, string> RunAssemblyTests(
            string pathToTestContainerAssembly,
            bool isVerbose,
            ITestRunnerEventListener eventListener);

        /// <summary>
        /// Runs all the NUnit tests contained in the assembly allowing you to define an <see cref="ITestFilter"/>, <see cref="LoggingThreshold"/>,
        /// , what happends when the run is completed, a line is outputter and an <see cref="ITestRunnerEventListener"/>
        /// </summary>
        /// <param name="pathToTestContainerAssembly">The absolute path to the DLL that contains the tests.</param>
        /// <param name="isVerbose">If it should output every line or just results.</param>
        /// <param name="testFilter">The filter to decide which tests to run.</param>
        /// <param name="loggingThreshold">The threshold above which messages will be output by the NUnit TestRunner, does not affect the returned dictionary.</param>
        /// <param name="eventListener">An <see cref="ITestRunnerEventListener"/> that contains the output and onLineOutput and onCompletedRun events.</param>
        /// <returns>A dictionary with the name of the test as key and its result as value</returns>
        IDictionary<string, string> RunAssemblyTests(
            string pathToTestContainerAssembly,
            bool isVerbose,
            ITestFilter testFilter,
            LoggingThreshold loggingThreshold,
            ITestRunnerEventListener eventListener = null);
    }
}