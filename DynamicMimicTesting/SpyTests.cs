using DynamicMimic;

namespace DynamicMimicTesting
{
    [TestClass]
    public class SpyTests
    {
        [TestMethod]
        public void TestConstructor()
        {
            var spy = new Spy<HighlyEncapsulatedClass>();
            Assert.AreEqual(4, spy.Properties.Count());
            Assert.AreEqual(7, spy.Fields.Count());
            Assert.AreEqual(14, spy.Methods.Count());
            Assert.AreEqual(2, spy.Constructors.Count());
        }

        [TestMethod]
        public void TestInvokingConstructors()
        {
            int arg = 4;
            var spy = new Spy<HighlyEncapsulatedClass>();
            HighlyEncapsulatedClass? test1 = spy.Construct();
            HighlyEncapsulatedClass? test2 = spy.Construct(arg);

            Assert.IsNotNull(test1);
            Assert.AreEqual("", test1.ChangedByConstructor);

            Assert.IsNotNull(test2);
            Assert.AreEqual(arg, test2.SetByConstructorArgument);
            Assert.AreNotEqual("", test2.ChangedByConstructor);
        }
    }
}