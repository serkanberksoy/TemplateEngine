using System.Collections.Generic;
using NUnit.Framework;
using TemplateEngine.BL;

namespace TemplateEngine.Tests
{
    [TestFixture]
    public class ReplaceTests
    {
        private IDictionary<string, string> _replacementDictionary = new Dictionary<string, string>
                                                                         {
                                                                             {"Test", "123"},
                                                                             {"Name", "Serkan"},
                                                                             {"Another", "another"},
                                                                             {"Surname", "Berksoy"},
                                                                             {"Value", "val"},
                                                                             {"Ýý", "Ýý"},
                                                                             {"Ii", "Ii"}
                                                                         };

        private IStringTemplateEngine _stringTemplateEngine;

        [TestFixtureSetUp]
        public void Init()
        {
            _stringTemplateEngine = new StringTemplateEngine();
        }

        [Test]
        public void ReplaceTokenWithValueTest()
        {
            string expected = "Hello Serkan Berksoy";
            string template = "Hello ${Name}";

            IDictionary<string, string> values = new Dictionary<string, string> { { "Name", "Serkan Berksoy" } };
            
            string replacement = _stringTemplateEngine.Replace(template, values);

            Assert.AreEqual(expected, replacement);
        }


        [Test]
        public void ReplaceWithRedundantListTest()
        {
            string expected = "Hello Serkan Berksoy";
            string template = "Hello ${Name} ${Surname}";

            string replacement = _stringTemplateEngine.Replace(template, _replacementDictionary);

            Assert.AreEqual(expected, replacement);
        }

        [Test]
        public void ReplaceEmptyStringWhenTokenDoesNotExistTest()
        {
            string expected = "Hello Serkan  123";
            string template = "Hello ${Name} ${Surname2} ${Test}";

            string replacement = _stringTemplateEngine.Replace(template, _replacementDictionary);

            Assert.AreEqual(expected, replacement);
        }
        
        [Test]
        public void ReplaceWithEmptyDictionaryTest()
        {
            string expected = "Hello  ";
            string template = "Hello ${Name} ${Surname}";

            string replacement = _stringTemplateEngine.Replace(template, new Dictionary<string, string>());

            Assert.AreEqual(expected, replacement);
        }

        [Test]
        public void ReplaceWithNullDictionaryTest()
        {
            string expected = "Hello  ";
            string template = "Hello ${Name} ${Surname}";

            string replacement = _stringTemplateEngine.Replace(template, null);

            Assert.AreEqual(expected, replacement);
        }

        [Test]
        public void ReplaceWithCaseInsensivityTest()
        {
            string expected = "Hello Serkan Serkan";
            string template = "Hello ${Name} ${name}";

            string replacement = _stringTemplateEngine.Replace(template, _replacementDictionary);

            Assert.AreEqual(expected, replacement);
        }

        [Test]
        public void TurkishCharactersTest()
        {
            string expected = "Hello Ii Ýý";
            string template = "Hello ${Ii} ${Ýý}";

            string replacement = _stringTemplateEngine.Replace(template, _replacementDictionary);

            Assert.AreEqual(expected, replacement);
        }
        
        [Test]
        public void NullTokenValueTest()
        {
            string expected = "Hello  Berksoy";
            string template = "Hello ${Name} ${Surname}";

            string replacement = _stringTemplateEngine.Replace(template, new Dictionary<string, string>
                                                                             {
                                                                                 {"Name", null},
                                                                                 {"Surname", "Berksoy"}
                                                                             });

            Assert.AreEqual(expected, replacement);
        }


        /*
         * OK - Bir string içinde bir token arayacak ve bulacak "Merhaba ${Serkan}"
         * OK - O token'ý bulduðunda içindeki variable name'i düzgün alacak
         * OK - Replace token with value test
         * OK - Birden fazla token varsa hepsini alýp bir list içine koyacak
         * OK - Replace baþýnda, sonunda ortasýnda test
         * OK - Dýþarýdan gelen liste ile karþýlaþtýracak ve string içinde gerekli olanlarý replace edecek
         * OK - Eðer dýþarýdan gelen liste az ise empty string ile replace edecek
         * OK - Eðer dýþarýdan gelen liste fazla ise problem çýkmayacak
         * OK - Dýþarýdan gelen liste empty ise patlamayacak
         * OK - Dýþarýdan gelen liste null ise patlamayacak
         * OK - Replace with case insensitive token test edilecek
         * OK - Replace two tokens with same name insensitively
         * OK - Invariant Culture Test edilecek (Türkçe Ýý)
         * OK - String null ise patlamayacak
         * OK - string empty ise patlamayacak
         * OK - Variable name boþ ise hata atacak
         * 
         * Token düzgün kapanmamýþsa hata atacak Bu zor býrakýyoruz               
         * Deðiþik limit token name testleri yapýlacak (týrnak vs.)
         
         * 
         * 1. Meaningfull test fn name
         * 2. FN ends with Test
         * 3. Arrange - Act - Assert
         * 4. Write the MINIMUM code that passes the test
         * 5. Red - Green - Refactor 
         * 6. DO NOT UNDERESTIMATE REFACTOR, DO IT PROPERLY
         * 
         */
    }
}