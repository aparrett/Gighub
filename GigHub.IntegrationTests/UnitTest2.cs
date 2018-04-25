using NUnit.Framework;

namespace GigHub.IntegrationTests
{
    [TestFixture]
    public class UnitTest2
    {
        [Test]
        public void TestMethod1()
        {
            var test = 1;
            var result = 1;

            Assert.That(result, Is.EqualTo(test));
        }
    }
}
