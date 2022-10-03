using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Dynamic;

namespace DynamicMimic
{
    public class Mimic<T> : DynamicObject
    {
        // Giving the fields user un-friendly names so
        // that it's unlikely to accidentally clash with
        // a name in the mimicked class.
        private readonly Spy<T> _spy__6283914;
        private T? _instance__3294054;

        public Mimic()
        {
            _spy__6283914 = new Spy<T>();
        }
        public Mimic(T instance)
        {
            _spy__6283914 = new Spy<T>();
            _instance__3294054 = instance;
        }
        public Mimic(params object[] args)
        {
            _spy__6283914 = new Spy<T>();
            _instance__3294054 = _spy__6283914.Construct(args);
        }

        public bool ConstructMimickedInstance(params object[] args)
            => _spy__6283914.TryConstruct(out _instance__3294054, args);

        public override bool TryInvokeMember(InvokeMemberBinder binder, object?[]? args, out object? result)
        {
            CheckMimickedInstanceIsNotNull();

            var member = _spy__6283914.Methods
                .Where(x => x.Name == binder.Name)
                .FirstOrDefault();

            if (member != null)
            {
                result = member.Invoke(_instance__3294054, args);
                return true;
            }

            // TODO could always return error here, or always return
            // true, but think this is the best option. Returning
            // true is misleading, and always throwing an error
            // or returning false isn't as good as checking the base
            // to see if it can find the member to invoke
            return base.TryInvokeMember(binder, args, out result);
        }

        public override bool TryGetMember(GetMemberBinder binder, out object? result)
        {
            CheckMimickedInstanceIsNotNull();

            // Try properties first
            var member = _spy__6283914.Properties
                .Where(x => x.Name == binder.Name)
                .FirstOrDefault();

            if (member != null)
            {
                result = member.GetValue(_instance__3294054);
                return true;
            }

            // Try fields if no property
            var field = _spy__6283914.Fields
                .Where(x => x.Name == binder.Name)
                .FirstOrDefault();
            
            if (field != null)
            {
                result = field.GetValue(_instance__3294054);
                return true;
            }

            return base.TryGetMember(binder, out result);
        }

        private void CheckMimickedInstanceIsNotNull()
        {
            if (_instance__3294054 == null)
            {
                throw new Exception("Cannot call a method on a null object, mimicked instance is null.");
            }
        }
    }
}
