using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using TemplateEngine.BL;

namespace TemplateEngine.Tests
{
    [TestFixture]
    public class FindTokenTests
    {
        private StringTemplateEngine _stringTemplateEngine;
    
        [TestFixtureSetUp]
        public void Init()
        {
            _stringTemplateEngine = new StringTemplateEngine();
        }

        [Test]
        public void FindTokenInStringTest()
        {
            IEnumerable<string> tokenList = _stringTemplateEngine.FindTokenList("Hello ${Name}");
            Assert.AreEqual(1, tokenList.Count());
        }

        [Test]
        public void FindTokenNameInStringTest()
        {
            IEnumerable<string> tokenList = _stringTemplateEngine.FindTokenList("Hello ${Name}");
            Assert.AreEqual("name", tokenList.First());
        }


        [Test]
        public void FindMultipleTokenNameInStringTest()
        {
            IEnumerable<string> tokenList = _stringTemplateEngine.FindTokenList("Hello ${Name} asd ${Surname} asd");

            Assert.AreEqual(2, tokenList.Count());
            CollectionAssert.Contains(tokenList, "name");
            CollectionAssert.Contains(tokenList, "surname");
        }
    }
}
