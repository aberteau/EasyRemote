using System;
using System.Collections;

namespace Techeasy.MicroFramework.Library
{
    public class NameValueCollection
    {
        private readonly Hashtable _hashtable;

        public NameValueCollection()
        {
            _hashtable = new Hashtable();
        }

        public int Count { get { return _hashtable.Count; } }

        public void Clear()
        {
            _hashtable.Clear();
        }

        private NameValuesPair Get(String name)
        {
            if (!Contains(name))
                return null;

            return _hashtable[name] as NameValuesPair;
        }

        private bool Contains(string name)
        {
            return _hashtable.Contains(name);
        }

        private NameValuesPair Get(Int32 index)
        {
            return _hashtable[index] as NameValuesPair;
        }

        public void Add(String name, String value)
        {
            NameValuesPair nameValuesPair = Get(name);
            if (nameValuesPair == null)
            {
                nameValuesPair = new NameValuesPair();
                nameValuesPair.Name = name;
                _hashtable.Add(name, nameValuesPair);
            }
            nameValuesPair.AddValue(value);
        }

        public void Remove(String name)
        {
            _hashtable.Remove(name);
        }

        public String[] GetValues(String name)
        {
            NameValuesPair nameValuesPair = Get(name);
            return GetValues(nameValuesPair);
        }

        private static string[] GetValues(NameValuesPair nameValuesPair)
        {
            return nameValuesPair == null ? null : nameValuesPair.Values;
        }

        public String[] GetValues(Int32 index)
        {
            NameValuesPair nameValuesPair = Get(index);
            return GetValues(nameValuesPair);
        }

        public String[] this[String name]
        {
            get { return GetValues(name); }
        }

        public String[] this[Int32 index]
        {
            get { return GetValues(index); }
        }

        public String GetKey(Int32 index)
        {
            NameValuesPair nameValuesPair = Get(index);
            return nameValuesPair.Name;
        }

        public NameValuesPair[] Pairs
        {
            get
            {
                NameValuesPair[] pairs = new NameValuesPair[_hashtable.Count];
                _hashtable.Values.CopyTo(pairs, 0);
                return pairs;
            }
        }

        public Hashtable ToHashtable()
        {
            Hashtable hashtable = new Hashtable();
            foreach (var nameValuesPair in Pairs)
                hashtable.Add(nameValuesPair.Name, nameValuesPair.Values);

            return hashtable;
        }
    }
}
