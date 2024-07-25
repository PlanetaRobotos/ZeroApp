using System.Collections.Generic;

namespace CollectionsPooling
{
    public sealed class ListPool<TItem> : CollectionPool<List<TItem>, TItem>
    {
        public override string ToString() => 
            $"[List<{typeof(TItem).Name}>] active: [{CountActive}], inactive: [{CountInactive}], all: [{CountAll}]";
    }
}