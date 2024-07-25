using System.Collections.Generic;
using UnityEngine.Pool;

namespace CollectionsPooling
{
    public static class DictionaryCollectionExtension
    {
        public static Dictionary<TKey, TValue> GetDictionary<TKey, TValue>(this ICollectionsPoolService service) => 
            service
                .GetPool<DictionaryPool<TKey, TValue>>()
                .GetCollection();

        public static PooledObject<Dictionary<TKey, TValue>> GetDictionary<TKey, TValue>(this ICollectionsPoolService service, out Dictionary<TKey, TValue> collection) => 
            service
                .GetPool<DictionaryPool<TKey, TValue>>()
                .GetCollection(out collection);

        public static void Release<TKey, TValue>(this ICollectionsPoolService service, Dictionary<TKey, TValue> collection) => 
            service
                .GetPool<DictionaryPool<TKey, TValue>>()
                .ReleaseCollection(collection);
    }
}