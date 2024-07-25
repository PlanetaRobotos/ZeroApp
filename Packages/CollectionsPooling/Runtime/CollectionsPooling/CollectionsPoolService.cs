using System;
using System.Collections.Generic;

namespace CollectionsPooling
{
    public sealed class CollectionsPoolService : ICollectionsPoolService
    {
        private readonly Dictionary<Type, ICollectionPool> _pools;

        public CollectionsPoolService()
        {
            _pools = new Dictionary<Type, ICollectionPool>();
        }

        public TPool GetPool<TPool>() where TPool : class, ICollectionPool, new()
        {
            Type itemType = typeof(TPool);
            
            if (!_pools.ContainsKey(itemType))
            {
                _pools.Add(itemType, new TPool());
            }

            return (TPool)_pools[itemType];
        }

        public void Clear()
        {
            foreach (ICollectionPool collectionPool in _pools.Values)
            {
                collectionPool.Clear();
            }
            
            _pools.Clear();
        }

        public override string ToString()
        {
            var result = $"[{GetType().Name}] contains [{_pools.Count}] pools.";

            foreach (ICollectionPool pool in _pools.Values)
            {
                result += $"\n{pool}";
            }

            return result;
        }
    }
}