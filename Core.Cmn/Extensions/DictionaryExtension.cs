﻿using System.Collections.Generic;

namespace Core.Cmn.Extensions
{
    public static class DictionaryExtension
    {
        public static void Merge(this IDictionary<string, object> instance, IDictionary<string, object> from, bool replaceExisting=true)
        {

            foreach (KeyValuePair<string, object> pair in from)
            {
                if (!replaceExisting && instance.ContainsKey(pair.Key))
                {
                    continue; 
                }

                instance[pair.Key] = pair.Value;
            }
        }
    }
}
