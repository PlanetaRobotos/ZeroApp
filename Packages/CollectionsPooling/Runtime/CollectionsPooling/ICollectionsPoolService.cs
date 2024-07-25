namespace CollectionsPooling
{
    public interface ICollectionsPoolService
    {
        TPool GetPool<TPool>() where TPool : class, ICollectionPool, new();
        void Clear();
    }
}