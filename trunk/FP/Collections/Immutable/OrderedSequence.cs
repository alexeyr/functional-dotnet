using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

namespace FP.Collections.Immutable {
    /// <summary>
    /// A finger-tree-based ordered (by keys) sequence.
    /// </summary>
    /// <typeparam name="K">The type of the keys.</typeparam>
    /// <typeparam name="T">The type of the elements of the sequence.</typeparam>
    /// <remarks>Do not use the default constructor.</remarks>
    public struct OrderedSequence<K, T> : IEnumerable<T> {
        private readonly K _noKey;
        private readonly IComparer<K> _comparer;

        /// <summary>
        /// Gets the comparer used to compare keys.
        /// </summary>
        public IComparer<K> Comparer {
            get { return _comparer; }
        }

        private readonly Monoid<K> _monoid;
        private readonly FingerTree<Element, K> _ft;

        /// <summary>
        /// The sentinel value for keys. Never add an element with this value!
        /// </summary>
        public K NoKey {
            get { return _noKey; }
        }

        [DebuggerDisplay("Key = {Key}, Value = {Value}")]
        internal struct Element : IMeasured<K> {
            internal readonly K Key;
            internal readonly T Value;

            public Element(K key, T value) {
                Key = key;
                Value = value;
            }

            public K Measure {
                get { return Key; }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderedSequence{K,T}"/> class.
        /// </summary>
        /// <param name="comparer">The comparer used to check for equality to <paramref name="noKey"/>.</param>
        /// <param name="noKey">The sentinel value.</param>
        public OrderedSequence(IComparer<K> comparer, K noKey) {
            _comparer = comparer;
            _monoid = new Monoid<K>(noKey, (x, y) => comparer.Compare(y, noKey) == 0 ? x : y);
            _ft = FingerTree.Empty<Element, K>(_monoid);
            _noKey = noKey;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderedSequence{K,T}"/> class.
        /// </summary>
        /// <param name="noKey">The sentinel value.</param>
        public OrderedSequence(K noKey) : this(Comparer<K>.Default, noKey) { }

        internal OrderedSequence(IComparer<K> comparer, K noKey, FingerTree<Element, K> ft) {
            _comparer = comparer;
            _ft = ft;
            _monoid = ft.MeasureMonoid;
            _noKey = noKey;
        }

        private OrderedSequence<K, T> MakeOrderedSequence(FingerTree<Element, K> ft) {
            return new OrderedSequence<K, T>(_comparer, _noKey, ft);
        }

        /// <summary>
        /// Inserts the specified item with the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="item">The item.</param>
        /// <returns>The sequence with the inserted item.</returns>
        public OrderedSequence<K, T> Insert(K key, T item) {
            var comparer = _comparer;
            var split = _ft.Split(k => comparer.Compare(k, key) >= 0);
            return MakeOrderedSequence(split.First.Append(new Element(key, item)).Concat(split.Second));
        }

        /// <summary>
        /// Deletes a single item with the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>A pair. The first element is <c>true</c> if there were items with key equal to <paramref name="key"/>
        /// in the original sequence, and <c>false</c> otherwise. The second element is the result of deletion.</returns>
        /// <remarks>Key equality 
        /// If there are several elements with key equal to <paramref name="key"/>, the first of them shall be deleted.</remarks>
        public Pair<bool, OrderedSequence<K, T>> Delete(K key) {
            return DeleteHelper(key, false);
        }

        /// <summary>
        /// Deletes all items with the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>A pair. The first element is <c>true</c> if there were items with key equal to <paramref name="key"/>
        /// in the original sequence, and <c>false</c> otherwise. The second element is the result of deletion.</returns>
        public Pair<bool, OrderedSequence<K, T>> DeleteAll(K key) {
            return DeleteHelper(key, true);
        }

        private Pair<bool, OrderedSequence<K, T>> DeleteHelper(K key, bool deleteAll) {
            var comparer = _comparer;
            var split = _ft.Split(k => comparer.Compare(k, key) >= 0);
            var split2 = split.Second.Split(k => comparer.Compare(k, key) > 0);
            if (split2.First.IsEmpty)
                return Pair.New(false, this);
            var newFt = deleteAll ? split.First : split.First.Concat(split2.First.Tail);
            return Pair.New(true, MakeOrderedSequence(newFt.Concat(split2.Second)));
        }

        /// <summary>
        /// Merges the sequence with <paramref name="otherSequence"/>.
        /// </summary>
        /// <param name="otherSequence">The other sequence.</param>
        /// <returns></returns>
        /// <remarks>The other sequence <b>must</b> use the same comparer.</remarks>
        public OrderedSequence<K, T> Union(OrderedSequence<K, T> otherSequence) {
            return MakeOrderedSequence(MergeTrees(_ft, otherSequence._ft));
        }

        private FingerTree<Element, K> MergeTrees(FingerTree<Element, K> ft1, FingerTree<Element, K> ft2) {
            if (ft2.IsEmpty)
                return ft1;
            var comparer = _comparer;
            var ft2head = ft2.Head;
            var ft2tail = ft2.Tail;
            var ft1split = ft1.Split(k => comparer.Compare(k, ft2head.Key) > 0);
            var newRight = MergeTrees(ft2tail, ft1split.Second).Prepend(ft2head);
            return ft1split.First.Concat(newRight);
        }

        /// <summary>
        /// Merges the sequence with <paramref name="otherSequence"/>.
        /// </summary>
        /// <param name="otherSequence">The other sequence.</param>
        /// <returns></returns>
        /// <remarks>The other sequence <b>must</b> use the same comparer.</remarks>
        public OrderedSequence<K, T> Intersect(OrderedSequence<K, T> otherSequence) {
            return MakeOrderedSequence(IntersectTrees(_ft, otherSequence._ft));
        }

        private FingerTree<Element, K> IntersectTrees(FingerTree<Element, K> ft1, FingerTree<Element, K> ft2) {
            if (ft2.IsEmpty)
                return ft1;
            var comparer = _comparer;
            var ft2head = ft2.Head;
            var ft2tail = ft2.Tail;
            var ft1split = ft1.Split(k => comparer.Compare(k, ft2head.Key) > 0);
            var recursive = IntersectTrees(ft2tail, ft1split.Second);
            var ft1_LE_ft2head = ft1split.First;
            return ft1_LE_ft2head.IsEmpty ||
                   comparer.Compare(ft1_LE_ft2head.Last.Key, ft2head.Key) < 0 //Does ft1_LE_ft2head contain ft2head?
                       ? recursive
                       : recursive.Prepend(ft2head);
        }

        /// <summary>
        /// Returns the element with the maximal key without removing it.
        /// </summary>
        /// <remarks>If several elements have the maximal key, the element inserted last will be returned.</remarks>
        /// <exception cref="EmptySequenceException">There are no elements in the sequence.</exception>
        public Pair<K, T> PeekMax() {
            Element item = _ft.Last;
            return Pair.New(item.Key, item.Value);
        }

        /// <summary>
        /// Returns the element with the minimal key without removing it.
        /// </summary>
        /// <remarks>If several elements have the minimal key, the element inserted first will be returned.</remarks>
        /// <exception cref="EmptySequenceException">There are no elements in the sequence.</exception>
        public Pair<K, T> PeekMin() {
            Element item = _ft.Head;
            return Pair.New(item.Key, item.Value);
        }

        /// <summary>
        /// Removes the element with the maximal key.
        /// </summary>
        /// <remarks>If several elements have the maximal key, the element inserted last will be removed.</remarks>
        /// <exception cref="EmptySequenceException">There are no elements in the sequence.</exception>
        public OrderedSequence<K, T> RemoveMax() {
            return MakeOrderedSequence(_ft.Init);
        }

        /// <summary>
        /// Removes the element with the minimal key.
        /// </summary>
        /// <remarks>If several elements have the minimal key, the element inserted first will be removed.</remarks>
        /// <exception cref="EmptySequenceException">There are no elements in the sequence.</exception>
        public OrderedSequence<K, T> RemoveMin() {
            return MakeOrderedSequence(_ft.Tail);
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<T> GetEnumerator() {
            foreach (var element in _ft)
                yield return element.Value;
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }
    }

    /// <summary>
    /// Utility methods for creating <see cref="OrderedSequence{K,T}"/>.
    /// </summary>
    /// <seealso cref="OrderedSequence{K,T}"/>
    public static class OrderedSequence {
        /// <summary>
        /// Creates an empty <see cref="OrderedSequence{K,T}"/>.
        /// </summary>
        /// <param name="comparer">Used to compare keys.</param>
        /// <param name="noKey">The sentinel value. Do <b>not</b> add elements with this key to the sequence.</param>
        /// <remarks>If an element with priority equal to <paramref name="noKey"/> is ever inserted into the sequence,
        /// the behaviour of the resulting sequence is undefined.
        /// </remarks>
        public static OrderedSequence<K, T> Empty<K, T>(IComparer<K> comparer, K noKey) {
            return new OrderedSequence<K, T>(comparer, noKey);
        }

        /// <summary>
        /// Creates an empty <see cref="OrderedSequence{K,T}"/>.
        /// </summary>
        /// <param name="noKey">The sentinel value. Do <b>not</b> add elements with this key to the sequence.</param>
        /// <remarks>If an element with priority equal to <paramref name="noKey"/> is ever inserted into the sequence,
        /// the behaviour of the resulting sequence is undefined.</remarks>
        public static OrderedSequence<K, T> Empty<K, T>(K noKey) where K : IComparable<K> {
            return Empty<K, T>(Comparer<K>.Default, noKey);
        }

        /// <summary>
        /// Creates an empty <see cref="OrderedSequence{K,T}"/> where the keys are the same as items.
        /// </summary>
        /// <param name="comparer">Used to compare keys.</param>
        /// <param name="noKey">The sentinel value. Do <b>not</b> add elements with this key to the sequence.</param>
        /// <remarks>If an element with priority equal to <paramref name="noKey"/> is ever inserted into the sequence,
        /// the behaviour of the resulting sequence is undefined.
        /// </remarks>
        public static OrderedSequence<T, T> Empty<T>(IComparer<T> comparer, T noKey) {
            return new OrderedSequence<T, T>(comparer, noKey);
        }

        /// <summary>
        /// Creates an empty <see cref="OrderedSequence{K,T}"/> where the keys are the same as items.
        /// </summary>
        /// <param name="noKey">The sentinel value. Do <b>not</b> add elements with this key to the sequence.</param>
        /// <remarks>If an element with priority equal to <paramref name="noKey"/> is ever inserted into the sequence,
        /// the behaviour of the resulting sequence is undefined.</remarks>
        public static OrderedSequence<T, T> Empty<T>(T noKey) where T : IComparable<T> {
            return Empty<T, T>(Comparer<T>.Default, noKey);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderedSequence{K,T}"/> class.
        /// </summary>
        /// <remarks>If an element with priority equal to <see cref="double.NegativeInfinity"/> is ever inserted into the sequence,
        /// the behaviour of the resulting sequence is undefined.</remarks>
        public static OrderedSequence<double, T> Empty<T>() {
            return Empty<double, T>(double.NegativeInfinity);
        }

        /// <summary>
        /// Creates a <see cref="OrderedSequence{K,T}"/> with the elements from <paramref name="sequence"/>.
        /// </summary>
        /// <param name="comparer">Used to compare keys.</param>
        /// <param name="noKey">The sentinel value. Do <b>not</b> add elements with this key to the sequence.</param>
        /// <param name="sequence">The sequence of pairs (key, element) placed into the sequence initially.</param>
        /// <remarks>If an element with priority equal to <paramref name="noKey"/> is ever inserted into the sequence,
        /// the behaviour of the resulting sequence is undefined.
        /// 
        /// <paramref name="sequence"/> is not required to be ordered.</remarks>
        public static OrderedSequence<K, T> FromEnumerable<K, T>(IEnumerable<Pair<K, T>> sequence, IComparer<K> comparer, K noKey) {
            var empty = Empty<K, T>(comparer, noKey);
            Func<OrderedSequence<K, T>, Pair<K, T>, OrderedSequence<K, T>> insert = (seq, pair) => seq.Insert(pair.First, pair.Second);
            return sequence.ReduceL(insert)(empty);
        }

        /// <summary>
        /// Creates a <see cref="OrderedSequence{K,T}"/> with the elements from <paramref name="sequence"/>.
        /// </summary>
        /// <param name="noKey">The sentinel value. Do <b>not</b> add elements with this key to the sequence.</param>
        /// <param name="sequence">The sequence of pairs (key, element) placed into the sequence initially.</param>
        /// <remarks>If an element with priority equal to <paramref name="noKey"/> is ever inserted into the sequence,
        /// the behaviour of the resulting sequence is undefined.
        /// 
        /// <paramref name="sequence"/> is not required to be ordered.</remarks>
        public static OrderedSequence<K, T> FromEnumerable<K, T>(IEnumerable<Pair<K, T>> sequence, K noKey) where K : IComparable<K> {
            return FromEnumerable(sequence, Comparer<K>.Default, noKey);
        }

        /// <summary>
        /// Creates a <see cref="OrderedSequence{K,T}"/> with the elements from <paramref name="sequence"/> where the keys are the same as items.
        /// </summary>
        /// <param name="comparer">Used to compare keys.</param>
        /// <param name="noKey">The sentinel value. Do <b>not</b> add elements with this key to the sequence.</param>
        /// <param name="sequence">The sequence of pairs (key, element) placed into the sequence initially.</param>
        /// <remarks>If an element with priority equal to <paramref name="noKey"/> is ever inserted into the sequence,
        /// the behaviour of the resulting sequence is undefined.
        /// 
        /// <paramref name="sequence"/> is not required to be ordered.</remarks>
        public static OrderedSequence<T, T> FromEnumerable<T>(IEnumerable<T> sequence, IComparer<T> comparer, T noKey) {
            var empty = Empty<T, T>(comparer, noKey);
            Func<OrderedSequence<T, T>, T, OrderedSequence<T, T>> insert = (seq, t) => seq.Insert(t, t);
            return sequence.ReduceL(insert)(empty);
        }

        /// <summary>
        /// Creates a <see cref="OrderedSequence{K,T}"/> with the elements from <paramref name="sequence"/> where the keys are the same as items.
        /// </summary>
        /// <param name="noKey">The sentinel value. Do <b>not</b> add elements with this key to the sequence.</param>
        /// <param name="sequence">The sequence of pairs (key, element) placed into the sequence initially.</param>
        /// <remarks>If an element with priority equal to <paramref name="noKey"/> is ever inserted into the sequence,
        /// the behaviour of the resulting sequence is undefined.
        /// 
        /// <paramref name="sequence"/> is not required to be ordered.</remarks>
        public static OrderedSequence<T, T> FromEnumerable<T>(IEnumerable<T> sequence, T noKey) where T : IComparable<T> {
            return FromEnumerable(sequence, Comparer<T>.Default, noKey);
        }

        /// <summary>
        /// Creates a <see cref="OrderedSequence{K,T}"/> with the elements from <paramref name="sequence"/>.
        /// </summary>
        /// <param name="sequence">The sequence of pairs (key, element) inserted into the sequence.</param>
        /// <remarks>If an element with priority equal to <see cref="double.NegativeInfinity"/> is ever inserted into the sequence,
        /// the behaviour of the resulting sequence is undefined.
        /// 
        /// <paramref name="sequence"/> is not required to be ordered.</remarks>
        public static OrderedSequence<double, T> FromEnumerable<T>(IEnumerable<Pair<double, T>> sequence) {
            return FromEnumerable(sequence, double.NegativeInfinity);
        }

        /// <summary>
        /// Creates a <see cref="OrderedSequence{K,T}"/> with the elements from <paramref name="sequence"/>.
        /// </summary>
        /// <param name="comparer">Used to compare keys.</param>
        /// <param name="noKey">The sentinel value. Do <b>not</b> add elements with this key to the sequence.</param>
        /// <param name="sequence">The sequence of pairs (key, element) placed into the sequence initially.
        /// Must be ordered by keys.</param>
        /// <remarks>If an element with priority equal to <paramref name="noKey"/> is ever inserted into the sequence,
        /// the behaviour of the resulting sequence is undefined.</remarks>
        public static OrderedSequence<K, T> FromOrderedEnumerable<K, T>(IEnumerable<Pair<K, T>> sequence, IComparer<K> comparer, K noKey) {
            Func<FingerTree<OrderedSequence<K, T>.Element, K>, Pair<K, T>,
                    FingerTree<OrderedSequence<K, T>.Element, K>> append =
                        (tree, pair) => tree.Append(new OrderedSequence<K, T>.Element(pair.First, pair.Second));
            var monoid = new Monoid<K>(noKey, (x, y) => comparer.Compare(y, noKey) == 0 ? x : y);
            var empty = FingerTree.Empty<OrderedSequence<K, T>.Element, K>(monoid);
            return new OrderedSequence<K, T>(comparer, noKey, sequence.ReduceL(append)(empty));
        }

        /// <summary>
        /// Creates a <see cref="OrderedSequence{K,T}"/> with the elements from <paramref name="sequence"/>.
        /// </summary>
        /// <param name="noKey">The sentinel value. Do <b>not</b> add elements with this key to the sequence.</param>
        /// <param name="sequence">The sequence of pairs (key, element) placed into the sequence initially.
        /// Must be ordered by keys.</param>
        /// <remarks>If an element with priority equal to <paramref name="noKey"/> is ever inserted into the sequence,
        /// the behaviour of the resulting sequence is undefined.</remarks>
        public static OrderedSequence<K, T> FromOrderedEnumerable<K, T>(IEnumerable<Pair<K, T>> sequence, K noKey)
            where K : IComparable<K> {
            return FromOrderedEnumerable(sequence, Comparer<K>.Default, noKey);
        }

        /// <summary>
        /// Creates a <see cref="OrderedSequence{K,T}"/> with the elements from <paramref name="sequence"/>.
        /// </summary>
        /// <param name="comparer">Used to compare keys.</param>
        /// <param name="noKey">The sentinel value. Do <b>not</b> add elements with this key to the sequence.</param>
        /// <param name="sequence">The sequence of pairs (key, element) placed into the sequence initially.
        /// Must be ordered by keys.</param>
        /// <remarks>If an element with priority equal to <paramref name="noKey"/> is ever inserted into the sequence,
        /// the behaviour of the resulting sequence is undefined.</remarks>
        public static OrderedSequence<T, T> FromOrderedEnumerable<T>(IEnumerable<T> sequence, IComparer<T> comparer, T noKey) {
            Func<FingerTree<OrderedSequence<T, T>.Element, T>, T,
                FingerTree<OrderedSequence<T, T>.Element, T>> append =
                    (tree, t) => tree.Append(new OrderedSequence<T, T>.Element(t, t));
            var monoid = new Monoid<T>(noKey, (x, y) => comparer.Compare(y, noKey) == 0 ? x : y);
            var empty = FingerTree.Empty<OrderedSequence<T, T>.Element, T>(monoid);
            return new OrderedSequence<T, T>(comparer, noKey, sequence.ReduceL(append)(empty));
        }

        /// <summary>
        /// Creates a <see cref="OrderedSequence{K,T}"/> with the elements from <paramref name="sequence"/>.
        /// </summary>
        /// <param name="noKey">The sentinel value. Do <b>not</b> add elements with this key to the sequence.</param>
        /// <param name="sequence">The sequence of pairs (key, element) placed into the sequence initially.
        /// Must be ordered by keys.</param>
        /// <remarks>If an element with priority equal to <paramref name="noKey"/> is ever inserted into the sequence,
        /// the behaviour of the resulting sequence is undefined.</remarks>
        public static OrderedSequence<T, T> FromOrderedEnumerable<T>(IEnumerable<T> sequence, T noKey)
            where T : IComparable<T> {
            return FromOrderedEnumerable<T>(sequence, Comparer<T>.Default, noKey);
        }

        /// <summary>
        /// Creates a <see cref="OrderedSequence{K,T}"/> with the elements from <paramref name="sequence"/>.
        /// </summary>
        /// <param name="sequence">The sequence of pairs (key, element) placed into the sequence initially.
        /// Must be ordered by keys.</param>
        /// <remarks>If an element with priority equal to <see cref="double.NegativeInfinity"/> is ever inserted into the sequence,
        /// the behaviour of the resulting sequence is undefined.</remarks>
        public static OrderedSequence<double, T> FromOrderedEnumerable<T>(IEnumerable<Pair<double, T>> sequence) {
            return FromOrderedEnumerable(sequence, double.NegativeInfinity);
        }
    }
}