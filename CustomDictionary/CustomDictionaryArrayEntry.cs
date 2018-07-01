using ArrayAlgorithms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CustomDictionary
{
    class CustomDictionaryArrayEntry<TKey,TValue>
    {
        internal CustomLinkedList<KeyValueCustomDictionaryPair<TKey, TValue>> ArrayEntryList;

        public CustomDictionaryArrayEntry()
        {
            ArrayEntryList = new CustomLinkedList<KeyValueCustomDictionaryPair<TKey, TValue>>();
        }

        internal bool Contains(TKey key)
        {
            CustomLinkedList<KeyValueCustomDictionaryPair<TKey, TValue>>.Node currentNode = ArrayEntryList.Root;
            while (currentNode != null)
            {
                if (currentNode.Data.Key.Equals(key))
                {
                    return true;
                }
                currentNode = currentNode.Next;
            }

            return false;
        }

        internal void Add(TKey key, TValue value)
        {
            ArrayEntryList.Insert(new KeyValueCustomDictionaryPair<TKey, TValue>(key, value));
        }

        internal List<Tuple<TKey, TValue>> GetKeyValuePairs()
        {
            List<Tuple<TKey, TValue>> listToReturn = new List<Tuple<TKey, TValue>>();

            //the implementation is chaining
            CustomLinkedList<KeyValueCustomDictionaryPair<TKey, TValue>>.Node currentNode = ArrayEntryList.Root;
            while (currentNode != null)
            {
                listToReturn.Add(new Tuple<TKey, TValue>(currentNode.Data.Key, currentNode.Data.Value));
                currentNode = currentNode.Next;
            }

            return listToReturn;
        }

        internal void Delete(TKey key)
        {
            ArrayEntryList.Remove(new KeyValueCustomDictionaryPair<TKey, TValue>(key, default(TValue)));
        }

        internal TValue GetValueFor(TKey key)
        {
            CustomLinkedList<KeyValueCustomDictionaryPair<TKey, TValue>>.Node currentNode = ArrayEntryList.Root;
            while (currentNode != null)
            {
                if (currentNode.Data.Key.Equals(key))
                {
                    return currentNode.Data.Value;
                }
                currentNode = currentNode.Next;
            }

            throw new Exception("Key does not exists");
        }

        internal void SetValueFor(TKey key, TValue value)
        {
            CustomLinkedList<KeyValueCustomDictionaryPair<TKey, TValue>>.Node currentNode = ArrayEntryList.Root;
            while (currentNode != null)
            {
                if (currentNode.Data.Key.Equals(key))
                {
                    currentNode.Data.Value = value;
                    return;
                }
                currentNode = currentNode.Next;
            }

            throw new Exception("Key does not exists");
        }
    }
}
