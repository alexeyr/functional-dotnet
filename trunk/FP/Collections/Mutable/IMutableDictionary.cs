/* (C) Alexey Romanov 2008 */

using System.Collections.Generic;
using FP.Collections.Immutable;

namespace FP.Collections.Mutable {
    public interface IMutableDictionary<K, V> {
        bool Contains(K key);
        Maybe<V> this[K key] { get; set; }
        void Add(K key, V value);
        void Remove(K key);
        IEnumerable<K> Keys { get; }
        IEnumerable<V> Values { get; }
        IEnumerable<KeyValuePair<K, V>> Pairs { get; }
    }
}
