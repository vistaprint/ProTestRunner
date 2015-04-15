using System;
using System.Text;
using NUnit.Core;

namespace ProTestRunner
{
    public interface ITestRunnerEventListener : EventListener
    {
        /// <summary>
        /// Defines what happens when all the tests in the assembly where run.
        /// </summary>
        event EventHandler OnCompletedRun;

        /// <summary>
        /// Defines what happens when a test outputs a line.
        /// </summary>
        event EventHandler OnLineOutput;

        /// <summary>
        /// The general output form the tests. You can either use this at the end, or <see cref="OnLineOutput"/> to get the output as it's generated.
        /// </summary>
        StringBuilder Output { get; set; }
    }
}