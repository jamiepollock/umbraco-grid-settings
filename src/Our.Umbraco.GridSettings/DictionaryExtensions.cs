using System.Collections.Generic;

namespace Our.Umbraco.GridSettings
{
    internal static class DictionaryExtensions
    {
        public static void AddKeyValuePair<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, KeyValuePair<TKey, TValue> keyValuePair)
        {
            dictionary.Add(keyValuePair.Key, keyValuePair.Value);
        }
    }
}
