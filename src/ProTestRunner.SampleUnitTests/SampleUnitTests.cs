using System;
using NUnit.Framework;

namespace ProTestRunner.SampleUnitTests
{
    [TestFixture]
    public class SampleUnitTests
    {
        [Test]
        public void MathsWork()
        {
            Console.WriteLine("Run thingy to insert into ham sandwich.");
            Assert.AreEqual(1+1, 2);
        }
    }
}
