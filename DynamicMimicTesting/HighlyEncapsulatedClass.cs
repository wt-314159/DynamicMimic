using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicMimicTesting
{
    internal class HighlyEncapsulatedClass
    {
        private readonly string secretDescription = "This is a highly encapsulated class." +
            "Most members are private and can't be accessed externally";
        private readonly int hiddenFigure = 42;
        // HACK You would obviously NEVER store a password
        // like this in real code, this is just for testing
        private string password = "Super Secret Passowrd 123";
        private const char code = '$';

        public double SetByConstructor { get; }
        public string ChangedByConstructor { get; private set; } = "Default value";
        public int SetByConstructorArgument { get; }
        public int Counter { get; private set; }
        public static int StaticNum { get; set; } = 4;

        private HighlyEncapsulatedClass(int argument)
        {
            SetByConstructor = 53;
            ChangedByConstructor = "Constructor Value";
            SetByConstructorArgument = argument;
        }

        public HighlyEncapsulatedClass()
        {
            // Wipe everything!
            hiddenFigure = 0;
            password = "";
            ChangedByConstructor = "";
        }

        private string GetSecret()
        {
            if (password == "Super Secret Passowrd 123")
            {
                return $"Super secret information: 6 x 7 = {hiddenFigure}";
            }
            else
            {
                return "Wrong password, you're not allowed to know the secret!";
            }
        }

        private void DoSomeSecretWork(int a, string message)
        {
            Counter += a;
            Console.WriteLine($"Incremented Counter by {a}, new value:{Counter}");
            Console.WriteLine($"Message from inside secret class: {message}");
        }

        private static int DoSomeSecretStaticStuff(int a)
        {
            Console.WriteLine($"Doing some secret static calculations");
            int result = a + StaticNum;
            Console.WriteLine($"{a} + {StaticNum} = {result}");
            return result;
        }
    }
}
