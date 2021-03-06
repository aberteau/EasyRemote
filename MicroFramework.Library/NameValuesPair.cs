using System;
using System.Collections;

namespace Techeasy.MicroFramework.Library
{
    public class NameValuesPair
    {
        public String Name { get; set; }

        public String[] Values
        {
            get { return _innerValues.ToArray(typeof(String)) as String[]; }
        }

        public void AddValue(String value)
        {
            _innerValues.Add(value);
        }

        public NameValuesPair()
        {
            _innerValues = new ArrayList();
        }

        private readonly ArrayList _innerValues;
    }
}
