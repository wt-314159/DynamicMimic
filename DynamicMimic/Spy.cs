using System.Reflection;
using static System.Reflection.BindingFlags;

namespace DynamicMimic
{
    public class Spy<T>
    {
        public PropertyInfo[] Properties { get; private set; }
        public FieldInfo[] Fields { get; private set; }
        public MethodInfo[] Methods { get; set; }
        public ConstructorInfo[] Constructors { get; set; }

        public Spy()
        {
            var type = typeof(T);
            var allFlags = NonPublic | Public | Instance;

            Properties = type.GetProperties(allFlags);
            Fields = type.GetFields(allFlags);
            Methods = type.GetMethods(allFlags);
            Constructors = type.GetConstructors(allFlags);
        }

        public bool TryConstruct(out T? obj, params object[] args)
        {
            try
            {
                obj = Construct(args);
                return true;
            }
            catch(TargetInvocationException)
            {
                obj = default;
                return false;
            }
        }

        public T? Construct(params object[] args)
        {
            foreach (var constructor in Constructors)
            {
                try
                {
                    T obj = (T)constructor.Invoke(args);
                    if (obj != null && obj.Equals(default) == false)
                    {
                        return obj;
                    }
                }
                catch
                {
                    continue;
                }
            }
            throw new TargetInvocationException("No suitable constructor could be found.", null);
        }
    }
}