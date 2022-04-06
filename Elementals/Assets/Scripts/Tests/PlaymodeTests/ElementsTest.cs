using Elements;
using NUnit.Framework;

namespace Tests.PlaymodeTests
{
    
    public class ElementsTest
    {
        [Test]
        public void TestElementConfigExists()
        {
            Assert.IsNotNull(ElementConfig.Instance);
        }
    }
}