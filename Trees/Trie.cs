using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trees
{
    public class Trie
    {
        const int ALPHABET_SIZE = 26;

        public TrieNode Root;

        public class TrieNode
        {
            public TrieNode[] children = new TrieNode[ALPHABET_SIZE];

            public bool IsEndOfWord;

            public TrieNode()
            {
                children = new TrieNode[ALPHABET_SIZE];
                IsEndOfWord = false;
            }
        }

        public void Insert(string key)
        {
            if (Root == null)
            {
                Root = new TrieNode();
            }
            TrieNode trav = Root;

            for (int i = 0; i < key.Length; i++)
            {
                int index = key[i] - 'a';
                if (trav.children[index] == null)
                {
                    trav.children[index] = new TrieNode();
                }

                trav = trav.children[index];
            }

            trav.IsEndOfWord = true;
        }

        public bool Search(string key)
        {
            if (Root == null)
            {
                return false;
            }
            else
            {
                TrieNode trav = Root;

                for (int i = 0; i < key.Length; i++)
                {
                    int index = key[i] - 'a';
                    if (trav.children[index] == null)
                    {
                        return false;
                    }
                    trav = trav.children[index];
                }

                if (trav != null && trav.IsEndOfWord)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
