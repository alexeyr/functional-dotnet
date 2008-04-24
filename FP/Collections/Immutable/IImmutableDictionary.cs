/* (C) Alexey Romanov 2008 */

using System.Collections.Generic;

namespace FP.Collections.Immutable {
    public interface IImmutableDictionary<K, V> {
        bool Contains(K key);
        Maybe<V> this[K key] { get; }
        IImmutableDictionary<K, V> Add(K key, V value);
        IImmutableDictionary<K, V> Remove(K key);
        IEnumerable<K> Keys { get; }
        IEnumerable<V> Values { get; }
        IEnumerable<KeyValuePair<K, V>> Pairs { get; }
    }
}