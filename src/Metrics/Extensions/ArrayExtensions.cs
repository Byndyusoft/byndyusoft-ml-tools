using System;
using System.Collections.Generic;

namespace Byndyusoft.ML.Tools.Metrics.Extensions
{
    public static class ArrayExtensions
    {
        public static IDictionary<TKey, HashSet<T>> ToDictionaryOfHashSets<T, TKey>(
            this T[] objects,
            params Func<T, TKey?>[] keyGetters)
            where TKey: notnull
        {
            var dictionaryOfHashSets = new Dictionary<TKey, HashSet<T>>();

            foreach (var @object in objects) 
                UpdateDictionaryOfHashSets(dictionaryOfHashSets, @object, keyGetters);

            return dictionaryOfHashSets;
        }

        private static void UpdateDictionaryOfHashSets<T, TKey>(
            Dictionary<TKey, HashSet<T>> dictionaryOfHashSets,
            T @object,
            params Func<T, TKey?>[] keyGetters)
            where TKey : notnull
        {
            foreach (var keyValueGetter in keyGetters)
            {
                var key = keyValueGetter(@object);
                if (key is null)
                    continue;

                if (dictionaryOfHashSets.TryGetValue(key, out var hashSet) == false)
                {
                    hashSet = new HashSet<T>();
                    dictionaryOfHashSets.Add(key, hashSet);
                }

                hashSet.Add(@object);
            }
        }
    }
}