namespace CollectionsPooling
{
    public interface ICollectionPool
    {
        int CountAll { get; }
        int CountActive { get; }
        int CountInactive { get; }
        void Clear();
    }
}