using System;
using System.Collections;
using Microsoft.SPOT;

namespace Techeasy.MicroFramework.Net.Http
{
    public class NameValueCollection
    {
        private readonly ArrayList _nameValuesPairs;

        public NameValueCollection()
        {
            _nameValuesPairs = new ArrayList();
        }

        public int Count { get { return _nameValuesPairs.Count; } }

        public void Clear()
        {
            _nameValuesPairs.Clear();
        }

        private NameValuesPair Get(String name)
        {
            foreach (var nameValuesPairObj in _nameValuesPairs)
            {
                var nameValuesPair = nameValuesPairObj as NameValuesPair;
                if (nameValuesPair.Name.Equals(name))
                    return nameValuesPair;
            }
            return null;
        }

        private NameValuesPair Get(Int32 index)
        {
            var nameValuesPairObj = _nameValuesPairs[index];
            if (nameValuesPairObj == null)
                return null;

            return nameValuesPairObj as NameValuesPair;
        }

        public void Add(String name, String value)
        {
            NameValuesPair nameValuesPair = Get(name);
            if (nameValuesPair == null)
            {
                nameValuesPair = new NameValuesPair();
                nameValuesPair.Name = name;
                _nameValuesPairs.Add(nameValuesPair);
            }
            nameValuesPair.AddValue(value);
        }

        public void Remove(String name)
        {
            NameValuesPair nameValuesPair = Get(name);
            _nameValuesPairs.Remove(nameValuesPair);
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
            get { return _nameValuesPairs.ToArray(typeof (NameValuesPair)) as NameValuesPair[]; }
        }
    }
}
