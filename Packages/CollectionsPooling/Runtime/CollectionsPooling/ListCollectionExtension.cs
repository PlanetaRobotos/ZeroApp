using System.Collections.Generic;
using UnityEngine.Pool;

namespace CollectionsPooling
{
    public static class ListCollectionExtension
    {
        public static List<TItem> GetList<TItem>(this ICollectionsPoolService service) => 
            service
                .GetPool<ListPool<TItem>>()
                .GetCollection();

        public static PooledObject<List<TItem>> GetList<TItem>(this ICollectionsPoolService service, out List<TItem> collection) => 
            service
                .GetPool<ListPool<TItem>>()
                .GetCollection(out collection);

        public static void Release<TItem>(this ICollectionsPoolService service, List<TItem> collection) => 
            service.
                GetPool<ListPool<TItem>>()
                .ReleaseCollection(collection);
    }
}