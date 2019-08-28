using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTable
{
    class HashTable : IHashTable
    {
        object[] _hashTable;
        int _capacity;
        int _count;

        public HashTable()
        {
            _hashTable = new object[HashHelpers.GetInitialPrime()];
            _count = 0;
            _capacity = _hashTable.Length;
        }

        public object this[object key]
        {
            get
            {
                int index = GetHash(key) % _hashTable.Length;
                if (_hashTable[index] == null)
                    throw new ArgumentException("Key does not exist");
                return _hashTable[index];
            }
            set
            {
                if (value == null)
                {
                    Delete(key);
                    return;
                }
                int index = GetHash(key) % _hashTable.Length;
                while (_hashTable[index] != null)
                {
                    index = (index + 1) % _hashTable.Length;
                }
                _hashTable[index] = value;
                _count++;

                if (_count > _capacity)
                    RecreateArray();
            }
        }

        public void Add(object key, object value)
        {
            int index = GetHash(key) % _hashTable.Length;
            if (_hashTable[index] != null)
                throw new ArgumentException("Key is already exists");
            _count++;

            if (_count > _capacity)
                RecreateArray();
        }

        public bool Contains(object key)
        {
            int index = GetHash(key) % _hashTable.Length;
            if (_hashTable[index] != null)
                return true;
            return false;
        }

        public bool TryGet(object key, out object value)
        {
            int index = GetHash(key) % _hashTable.Length;   
            if (_hashTable[index] != null)
            {
                value = _hashTable[index];
                return true;
            }
            value = null;
            return false;
        }

        public int GetHash(object key)
        {
            return key.GetHashCode();
        }

        void RecreateArray()
        {
            object[] sourceArray = new object[_hashTable.Length];
            sourceArray = _hashTable;

            _hashTable = new object[HashHelpers.ExpandPrime(_capacity)];
            Array.Copy(sourceArray, _hashTable, sourceArray.Length);
        }

        void Delete(object key)
        {
            int index = GetHash(key) % _hashTable.Length;

            if (_hashTable[index] != null)
                _hashTable[index] = null;
        }
    }
}
