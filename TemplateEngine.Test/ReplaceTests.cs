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
                                                                             {"��", "��"},
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
            string expected = "Hello Ii ��";
            string template = "Hello ${Ii} ${��}";

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
         * OK - Bir string i�inde bir token arayacak ve bulacak "Merhaba ${Serkan}"
         * OK - O token'� buldu�unda i�indeki variable name'i d�zg�n alacak
         * OK - Replace token with value test
         * OK - Birden fazla token varsa hepsini al�p bir list i�ine koyacak
         * OK - Replace ba��nda, sonunda ortas�nda test
         * OK - D��ar�dan gelen liste ile kar��la�t�racak ve string i�inde gerekli olanlar� replace edecek
         * OK - E�er d��ar�dan gelen liste az ise empty string ile replace edecek
         * OK - E�er d��ar�dan gelen liste fazla ise problem ��kmayacak
         * OK - D��ar�dan gelen liste empty ise patlamayacak
         * OK - D��ar�dan gelen liste null ise patlamayacak
         * OK - Replace with case insensitive token test edilecek
         * OK - Replace two tokens with same name insensitively
         * OK - Invariant Culture Test edilecek (T�rk�e ��)
         * OK - String null ise patlamayacak
         * OK - string empty ise patlamayacak
         * OK - Variable name bo� ise hata atacak
         * 
         * Token d�zg�n kapanmam��sa hata atacak Bu zor b�rak�yoruz               
         * De�i�ik limit token name testleri yap�lacak (t�rnak vs.)
         
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