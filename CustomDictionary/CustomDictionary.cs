using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomDictionary
{
    public class CustomDictionary<TKey,TValue>
    {
        private CustomDictionaryArrayEntry<TKey, TValue>[] CustomDictionaryArray;

        private int _CustomDictionaryArrayCapacity;

        public int CustomDictionaryArrayCapacity
        {
            get
            {
                return _CustomDictionaryArrayCapacity;
            }
            set
            {
                _CustomDictionaryArrayCapacity = value;
                aHashing = new Random().Next(1000);
                bHashing = new Random().Next(1000);
                primeNumber = GetPrimeNumberGreaterThan(_CustomDictionaryArrayCapacity);
                NumberOfEmptyArrayPositions = _CustomDictionaryArrayCapacity;
                ArrayFillRatio = 0;
                KeysCount = 0;
            }
        }

        public double ArrayFillRatio;

        public int KeysCount;

        private double thresholdFillRatio = 0.75;

        public int NumberOfEmptyArrayPositions;

        private int aHashing;
        private int bHashing;

        private int primeNumber;

        public CustomDictionary()
	    {
            CustomDictionaryArrayCapacity = 4;
            ArrayFillRatio = 0;
            KeysCount = 0;

            CustomDictionaryArray = new CustomDictionaryArrayEntry<TKey, TValue>[CustomDictionaryArrayCapacity];
            
	    }

        public TValue this[TKey key]
        {
            get
            {
                int indexForKey = GetIndexForKey(key);

                if (CustomDictionaryArray[indexForKey] == null || !CustomDictionaryArray[indexForKey].Contains(key))
                {
                    throw new Exception("Key does not exists");
                }
                else
                {
                    TValue val = CustomDictionaryArray[indexForKey].GetValueFor(key);
                    return val;
                }
            }
            set
            {
                int indexForKey = GetIndexForKey(key);

                if (CustomDictionaryArray[indexForKey] == null || !CustomDictionaryArray[indexForKey].Contains(key))
                {
                    throw new Exception("Key does not exists");
                }
                else
                {
                    CustomDictionaryArray[indexForKey].SetValueFor(key, value);
                }
            }
        }

        private int GetPrimeNumberGreaterThan(int CustomDictionaryArrayCapacity)
        {
            int numberToTest = CustomDictionaryArrayCapacity;
            while (true)
            {
                bool IsNotPrime = false;
                for (int i = 2; i <= numberToTest/2; i++)
                {
                    if (numberToTest % i == 0)
                    {
                        numberToTest++;
                        IsNotPrime = true;
                        break;
                    }
                }

                if (!IsNotPrime)
                {
                    return numberToTest;
                }
            }
        }

        public void Add(TKey key, TValue value)
        {
            if (ArrayFillRatio > thresholdFillRatio)
            {
                CustomDictionaryArrayEntry<TKey, TValue>[] CopyOfExistingArray = new CustomDictionaryArrayEntry<TKey, TValue>[CustomDictionaryArrayCapacity];
                CustomDictionaryArray.CopyTo(CopyOfExistingArray, 0);
                CustomDictionaryArrayCapacity *= 2;
                CustomDictionaryArray = new CustomDictionaryArrayEntry<TKey, TValue>[CustomDictionaryArrayCapacity];
                
                NumberOfEmptyArrayPositions = CustomDictionaryArrayCapacity;
                ArrayFillRatio = 0;
                KeysCount = 0;

                for (int i = 0; i < CopyOfExistingArray.Length; i++)
                {
                    if (CopyOfExistingArray[i] == null)
                    {
                        continue;
                    }
                    List<Tuple<TKey, TValue>> keyValuePairs = CopyOfExistingArray[i].GetKeyValuePairs();
                    for (int j = 0; j < keyValuePairs.Count; j++)
                    {
                        Add(keyValuePairs[j].Item1, keyValuePairs[j].Item2);
                    }
                }
            }

            int indexForKey = GetIndexForKey(key);

            if (CustomDictionaryArray[indexForKey] != null && CustomDictionaryArray[indexForKey].Contains(key))
            {
                throw new Exception("Key already exists");
            }
            else
            {
                if (CustomDictionaryArray[indexForKey] == null)
                {
                    NumberOfEmptyArrayPositions--;
                    CustomDictionaryArray[indexForKey] = new CustomDictionaryArrayEntry<TKey, TValue>();
                }
                CustomDictionaryArray[indexForKey].Add(key, value);
                KeysCount++;
                //ArrayFillRatio = (float) (CustomDictionaryArrayCapacity - NumberOfEmptyArrayPositions) / CustomDictionaryArrayCapacity;
                ArrayFillRatio = (float)(KeysCount) / CustomDictionaryArrayCapacity;
            }
        }

        public void Remove(TKey key)
        {
            int indexForKey = GetIndexForKey(key);

            if (CustomDictionaryArray[indexForKey] == null || !CustomDictionaryArray[indexForKey].Contains(key))
            {
                throw new Exception("Key does not exists");
            }
            else
            {
                CustomDictionaryArray[indexForKey].Delete(key);
                if (CustomDictionaryArray[indexForKey] == null)
                {
                    NumberOfEmptyArrayPositions++;
                }
                KeysCount--;
                //ArrayFillRatio = (float)(CustomDictionaryArrayCapacity - NumberOfEmptyArrayPositions) / CustomDictionaryArrayCapacity;
                ArrayFillRatio = (float)(KeysCount) / CustomDictionaryArrayCapacity;
            }
        }

        private int GetIndexForKey(TKey key)
        {
            int hashCode = GetCustomHashCode(key);

            return hashCode % CustomDictionaryArrayCapacity;
        }

        private int GetCustomHashCode(TKey key)
        {
            if (typeof(TKey) == typeof(int))
            {
                return ((aHashing * Convert.ToInt32(key)) + bHashing) % primeNumber;
            }
            else if (typeof(TKey) == typeof(string))
            {
                int HashCode = 0;
                string keyString = key.ToString();
                for (int i = 0; i < keyString.Length; i++)
                {
                    HashCode = ((aHashing * HashCode) + keyString[i]) % primeNumber;
                }

                return HashCode;
            }
            else
            {
                throw new Exception("Key type not supported");
            }
        }
    }
}
