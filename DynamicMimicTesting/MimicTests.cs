using DynamicMimic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicMimicTesting
{
    [TestClass]
    public class MimicTests
    {
        [TestMethod]
        public void TestConstructor()
        {
            var privateClass = new HighlyEncapsulatedClass();
            var mimic = new Mimic<HighlyEncapsulatedClass>(privateClass);
            Assert.IsNotNull(mimic);

            var mimic2 = new Mimic<HighlyEncapsulatedClass>(4);
            Assert.IsNotNull(mimic2);
        }

        [TestMethod]
        public void TestInvokeMethod()
        {
            dynamic mimic = new Mimic<HighlyEncapsulatedClass>(5);
            string secret = mimic.GetSecret();
            Assert.IsTrue(secret?.Contains("Super secret information"));
        }

        [TestMethod]
        public void TestGetMember()
        {
            dynamic mimic = new Mimic<HighlyEncapsulatedClass>(5);
            string password = mimic.password;
            Assert.AreEqual("Super Secret Passowrd 123", password);
        }
    }
}
