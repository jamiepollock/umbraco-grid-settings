using System.Collections.Generic;

namespace Our.Umbraco.GridSettings
{
    internal static class KeyValuePairExtensions
    {
        public static bool IsValid(this KeyValuePair<string, string> entry)
        {
            return (string.IsNullOrWhiteSpace(entry.Key) &&
                    string.IsNullOrWhiteSpace(entry.Value))
                    == false;
        }
    }
}
