﻿using System.Collections.Generic;

namespace Galifee.Core.SetupContextStorages
{
    public class PropertyStore
    {
        private Dictionary<string, object> _properties = new Dictionary<string, object>();

        public object GetProperty(string key)
        {
            if (_properties.ContainsKey(key))
            {
                return _properties[key];
            }

            return null;
        }

        public object this[string key]
        {
            get
            {
                return GetProperty(key);
            }
            set
            {
                SetProperty(key, value);
            }
        }

        public void SetProperty(string key, object value)
        {
            if (_properties.ContainsKey(key))
            {
                _properties[key] = value;
            }
            else
            {
                _properties.Add(key, value);
            }
        }
    }
}