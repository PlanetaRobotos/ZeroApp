using System.Collections.Generic;
using UnityEngine.Pool;

namespace CollectionsPooling
{
    public class CollectionPool<TCollection, TItem> : ICollectionPool
        where TCollection : class, ICollection<TItem>, new()
    {
        private readonly ObjectPool<TCollection> _pool;

        protected CollectionPool()
        {
            _pool = new ObjectPool<TCollection>(createFunc: () => new TCollection(), actionOnRelease: l => l.Clear());
        }

        public int CountActive => _pool.CountActive;
        public int CountInactive => _pool.CountInactive;

        public int CountAll => _pool.CountAll;

        public TCollection GetCollection() => 
            _pool.Get();

        public PooledObject<TCollection> GetCollection(out TCollection value) => 
            _pool.Get(out value);

        public void ReleaseCollection(TCollection toRelease) => 
            _pool.Release(toRelease);

        public void Clear() => 
            _pool.Clear();
    }
}