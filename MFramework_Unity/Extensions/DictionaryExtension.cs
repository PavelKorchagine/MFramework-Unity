using System;
using System.Collections.Generic;
using System.Linq;

namespace MFramework_Unity.Tools
{
    /// <summary>
    /// DictionaryExtension
    /// </summary>
    public static partial class DictionaryExtension
    {
        /// <summary>
        /// Merge
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dictionary"></param>
        /// <param name="dictionaries"></param>
        /// <returns></returns>
        public static Dictionary<TKey, TValue> Merge<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, params Dictionary<TKey, TValue>[] dictionaries)
        {
            return dictionaries.Aggregate(dictionary,
                (current, self) => current.Union(self).ToDictionary(kv => kv.Key, kv => kv.Value));
        }

        /// <summary>
        /// ForEach
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="self"></param>
        /// <param name="action"></param>
        public static void ForEach<K, V>(this Dictionary<K, V> self, Action<K, V> action)
        {
            var dictE = self.GetEnumerator();
            while (dictE.MoveNext())
            {
                var current = dictE.Current;
                action(current.Key, current.Value);
            }

            dictE.Dispose();
        }

        /// <summary>
        /// AddRange
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <param name="self"></param>
        /// <param name="addInDict"></param>
        /// <param name="isOverride"></param>
        public static void AddRange<K, V>(this Dictionary<K, V> self, Dictionary<K, V> addInDict, bool isOverride = false)
        {
            var dictE = addInDict.GetEnumerator();

            while (dictE.MoveNext())
            {
                var current = dictE.Current;
                if (self.ContainsKey(current.Key))
                {
                    if (isOverride)
                        self[current.Key] = current.Value;
                    continue;
                }

                self.Add(current.Key, current.Value);
            }

            dictE.Dispose();
        }

        /// <summary>
        ///  尝试根据key得到value，得到了的话直接返回value，没有得到直接返回null
        /// </summary>
        /// <typeparam name="Tkey"></typeparam>
        /// <typeparam name="Tvalue"></typeparam>
        /// <param name="dict"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static Tvalue TryGet<Tkey, Tvalue>(this Dictionary<Tkey, Tvalue> dict, Tkey key)
        {
            Tvalue value;
            dict.TryGetValue(key, out value);
            return value;
        }

    }
}