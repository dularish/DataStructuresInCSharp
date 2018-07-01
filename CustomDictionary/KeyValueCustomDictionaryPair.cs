using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CustomDictionary
{
    public class KeyValueCustomDictionaryPair<T1, T2>
    {
        public T1 Key;
        public T2 Value;

        public KeyValueCustomDictionaryPair(T1 key, T2 value)
        {
            Key = key;
            Value = value;
        }

        public override bool Equals (object obj)
        {

            if (this == null || obj == null || GetType() != obj.GetType()) 
            {
                return false;
            }

            if (this.Key.Equals(((KeyValueCustomDictionaryPair<T1, T2>)obj).Key))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    
        // override object.GetHashCode
        public override int GetHashCode()
        {
            //Using the default .NET GetHashCode here
            return Key.GetHashCode();
        }
    }
}
