using System.Collections.Generic;

namespace CollectionsPooling
{
    internal sealed class DictionaryPool<TKey, TValue> : CollectionPool<Dictionary<TKey, TValue>, KeyValuePair<TKey, TValue>>
    {
        public override string ToString() => 
            $"[Dictionary<{typeof(TKey).Name}, {typeof(TValue).Name}>] active: [{CountActive}], inactive: [{CountInactive}], all: [{CountAll}]";
    }
}