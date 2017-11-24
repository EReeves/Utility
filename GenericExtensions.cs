using System.Collections.Generic;

namespace Game.Shared.Utility
{
    public static class GenericExtensions
    {
        /// <summary>
        /// Quick way to try get a value from a dictionary.
        /// </summary>
        /// <typeparam name="TK"></typeparam>
        /// <typeparam name="TV"></typeparam>
        /// <param name="dict"></param>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static TV GetValue<TK, TV>(this IDictionary<TK, TV> dict, TK key, TV defaultValue = default(TV))
        {
            TV value;
            return dict.TryGetValue(key, out value) ? value : defaultValue;
        }
    }
}