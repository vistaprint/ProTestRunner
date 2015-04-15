using NUnit.Core;

namespace ProTestRunner.UnitTests
{
    internal class SampleTestFilter : ITestFilter
    {
        public bool Pass(ITest test)
        {
            return true;
        }

        public bool Match(ITest test)
        {
            return true;
        }

        public bool IsEmpty { get; private set; }
    }
}
