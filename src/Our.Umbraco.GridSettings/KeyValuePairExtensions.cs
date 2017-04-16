using System.Collections.Generic;

namespace Our.Umbraco.GridSettings
{
    internal static class KeyValuePairExtensions
    {
        public static bool IsValid(this KeyValuePair<string, string> entry)
        {
            var hasKey = string.IsNullOrWhiteSpace(entry.Key) == false;
            var hasValue = string.IsNullOrWhiteSpace(entry.Value) == false;

            return hasKey && hasValue;
        }
    }
}
