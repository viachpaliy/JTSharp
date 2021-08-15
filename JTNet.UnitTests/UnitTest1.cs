using NUnit.Framework;
using JTNet.Types;

namespace JTNet.UnitTests
{
    [TestFixture]	
    public class GUIDTests
    {
        

        [Test]
        public void Test_END_OF_ELEMENTS()
        {
            string e = "{FFFFFFFF-FFFF-FFFF-FF-FF-FF-FF-FF-FF-FF-FF}";
            Assert.AreEqual(e, GUID.END_OF_ELEMENTS.ToString());
        }
    }
}
