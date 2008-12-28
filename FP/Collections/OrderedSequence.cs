/*
* OrderedSequence.cs is part of functional-dotnet project
* 
* Copyright (c) 2008 Alexey Romanov
* All rights reserved.
*
* This source file is available under The New BSD License.
* See license.txt file for more information.
* 
* THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND 
* CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, 
* INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF 
* MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED.
*/

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using FP.Core;
using FP.HaskellNames;
using FP.Validation;

namespace FP.Collections {
    /// <summary>
    /// A finger-tree-based ordered (by keys) sequence.
    /// </summary>
    /// <typeparam name="K">The type of the keys.</typeparam>
    /// <typeparam name="T">The type of the elements of the sequence.</typeparam>
    /// <remarks>Do not use the default constructor.</remarks>
    public struct OrderedSequence<K, T> : IEnumerable<T>, IEquatable<OrderedSequence<K, T>>, IRandomAccessSequence<Tuple<K, T>, OrderedSequence<K, T>> {
        private readonly IComparer<K> _comparer;
        private readonly FingerTree<Element, Tuple<K, int>> _ft;

        /// <summary>
        /// The sentinel value for keys. Never add an element with this value!
        /// </summary>
        public K NoKey {
            get { return Monoid.Zero.Item1; }
        }

        /// <summary>
        /// Gets the comparer used to compare keys.
        /// </summary>
        public IComparer<K> Comparer {
            get { return _comparer; }
        }

        private Monoid<Tuple<K, int>> Monoid {
            get { return _ft.MeasureMonoid; }
        }

        [DebuggerDisplay("Key = {Key}, Value = {Value}")]
        internal struct Element : IMeasured<Tuple<K, int>> {
            internal readonly K Key;
            internal readonly T Value;

            public Element(K key, T value) {
                Key = key;
                Value = value;
            }

            public Tuple<K, int> Measure {
                get { return Pair.New(Key, 1); }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderedSequence{K,T}"/> class.
        /// </summary>
        /// <param name="comparer">The comparer used to check for equality to <paramref name="noKey"/>.</param>
        /// <param name="noKey">The sentinel value.</param>
        public OrderedSequence(IComparer<K> comparer, K noKey) {
            _comparer = comparer;
            _ft =
                FingerTree.Empty<Element, Tuple<K, int>>(OrderedSequence.MakeMonoid(noKey, comparer));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderedSequence{K,T}"/> class.
        /// </summary>
        /// <param name="noKey">The sentinel value.</param>
        public OrderedSequence(K noKey) : this(Comparer<K>.Default, noKey) {}

        internal OrderedSequence(IComparer<K> comparer, FingerTree<Element, Tuple<K, int>> ft) {
            _comparer = comparer;
            _ft = ft;
        }

        private OrderedSequence<K, T> MakeOrderedSequence(FingerTree<Element, Tuple<K, int>> ft) {
            return new OrderedSequence<K, T>(_comparer, ft);
        }

        /// <summary>
        /// Inserts the specified item with the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="item">The item.</param>
        /// <returns>The sequence with the inserted item.</returns>
        public OrderedSequence<K, T> Insert(K key, T item) {
            return MakeOrderedSequence(InsertIntoTree(_ft, key, item, _comparer));
        }

        /// <summary>
        /// Inserts all items in <paramref name="sequence"/> into this sequence.
        /// </summary>
        /// <param name="sequence">The sequence of (key, item) pairs.</param>
        /// <returns>The sequence with the inserted items.</returns>
        public OrderedSequence<K, T> InsertRange(IEnumerable<Tuple<K, T>> sequence) {
            var comparer = _comparer;
            var ft =
                sequence.FoldLeft(
                    (tree, pair) => InsertIntoTree(tree, pair.Item1, pair.Item2, comparer), _ft);
            return MakeOrderedSequence(ft);
        }

        private static FingerTree<Element, Tuple<K, int>> InsertIntoTree(
            FingerTree<Element, Tuple<K, int>> tree, K key, T item, IComparer<K> comparer) {
            var split = tree.Split(pair => comparer.Compare(pair.Item1, key) >= 0);
            return (split.Item1 | new Element(key, item)) + split.Item2;
        }

        /// <summary>
        /// Deletes a single item with the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>A pair. The first element is <c>true</c> if there were items with key equal to <paramref name="key"/>
        /// in the original sequence, and <c>false</c> otherwise. The second element is the result of deletion.</returns>
        /// <remarks>Key equality 
        /// If there are several elements with key equal to <paramref name="key"/>, the first of them shall be deleted.</remarks>
        public Tuple<bool, OrderedSequence<K, T>> Delete(K key) {
            return DeleteHelper(key, false);
        }

        /// <summary>
        /// Deletes all items with the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>A pair. The first element is <c>true</c> if there were items with key equal to <paramref name="key"/>
        /// in the original sequence, and <c>false</c> otherwise. The second element is the result of deletion.</returns>
        public Tuple<bool, OrderedSequence<K, T>> DeleteAll(K key) {
            return DeleteHelper(key, true);
        }

        private Tuple<bool, OrderedSequence<K, T>> DeleteHelper(K key, bool deleteAll) {
            var comparer = _comparer;
            var split = _ft.Split(pair => comparer.Compare(pair.Item1, key) >= 0);
            var split2 = split.Item2.Split(pair => comparer.Compare(pair.Item1, key) > 0);
            if (split2.Item1.IsEmpty)
                return Pair.New(false, this);
            var newFt = deleteAll ? split.Item1 : split.Item1 + split2.Item1.Tail;
            return Pair.New(true, MakeOrderedSequence(newFt + split2.Item2));
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

        // ReSharper disable SuggestBaseTypeForParameter
        private FingerTree<Element, Tuple<K, int>> MergeTrees(
            FingerTree<Element, Tuple<K, int>> ft1,
            FingerTree<Element, Tuple<K, int>> ft2) {
            // ReSharper restore SuggestBaseTypeForParameter
            if (ft2.IsEmpty)
                return ft1;
            var comparer = _comparer;
            var ft2head = ft2.Head;
            var ft2tail = ft2.Tail;
            var ft1split = ft1.Split(pair => comparer.Compare(pair.Item1, ft2head.Key) > 0);
            var newRight = ft2head | MergeTrees(ft2tail, ft1split.Item2);
            return ft1split.Item1 + newRight;
        }

        /// <summary>
        /// Intersects the sequence with <paramref name="otherSequence"/>.
        /// </summary>
        /// <param name="otherSequence">The other sequence.</param>
        /// <returns></returns>
        /// <remarks>The other sequence <b>must</b> use the same comparer.</remarks>
        public OrderedSequence<K, T> Intersect(OrderedSequence<K, T> otherSequence) {
            return MakeOrderedSequence(IntersectTrees(_ft, otherSequence._ft));
        }

        private FingerTree<Element, Tuple<K, int>> IntersectTrees(
            FingerTree<Element, Tuple<K, int>> ft1,
            FingerTree<Element, Tuple<K, int>> ft2) {
            if (ft2.IsEmpty)
                return ft2;
            var comparer = _comparer;
            var ft2head = ft2.Head;
            var ft2tail = ft2.Tail;
            var ft1split = ft1.Split(pair => comparer.Compare(pair.Item1, ft2head.Key) > 0);
            var recursive = IntersectTrees(ft2tail, ft1split.Item2);
            var ft1_LE_ft2head = ft1split.Item1;
            return ft1_LE_ft2head.IsEmpty ||
                   comparer.Compare(ft1_LE_ft2head.Last.Key, ft2head.Key) < 0
                   //Does ft1_LE_ft2head contain ft2head?
                       ? recursive
                       : ft2head | recursive;
        }

        /// <summary>
        /// Returns the element with the maximal key without removing it.
        /// </summary>
        /// <remarks>If several elements have the maximal key, the element inserted last will be returned.</remarks>
        /// <exception cref="EmptySequenceException">There are no elements in the sequence.</exception>
        public Tuple<K, T> PeekMax() {
            Element item = _ft.Last;
            return Pair.New(item.Key, item.Value);
        }

        /// <summary>
        /// Returns the element with the minimal key without removing it.
        /// </summary>
        /// <remarks>If several elements have the minimal key, the element inserted first will be returned.</remarks>
        /// <exception cref="EmptySequenceException">There are no elements in the sequence.</exception>
        public Tuple<K, T> PeekMin() {
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
        public IEnumerator<Tuple<K, T>> GetEnumerator() {
            foreach (var element in _ft)
                yield return Pair.New(element.Key, element.Value);
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator() {
            foreach (var element in _ft)
                yield return element.Value;
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        /// <summary>
        /// Merges <paramref name="tree1"/> and <paramref name="tree2"/>.
        /// </summary>
        public static OrderedSequence<K, T> operator +(
            OrderedSequence<K, T> tree1, OrderedSequence<K, T> tree2) {
            return tree1.Union(tree2);
        }

        /// <summary>
        /// Gets the number of elements in the sequence.
        /// </summary>
        public int Count {
            get { return _ft.Measure.Item2; }
        }

        public OrderedSequence<K, T> Subsequence(int startIndex, int count) {
            Requires.That
                .IsIndexInRange(this, startIndex, "startIndex")
                .IsIndexInRange(this, startIndex + count, "startIndex + count")
                .Check();

            var split1 = _ft.Split(pair => pair.Item2 > startIndex);
            var split2 = split1.Item2.Split(pair => pair.Item2 >= count);
            return new OrderedSequence<K,T>(_comparer, split2.Item1);
        }

        /// <summary>
        /// Gets the <see cref="Tuple{T1,T2}"/> at the specified index.
        /// </summary>
        /// <remarks><c>this[0]</c> is the element with the smallest key, 
        /// <c>this[Count - 1]</c> is the element with the greatest key.</remarks>
        /// <exception cref="ArgumentOutOfRangeException"><c>index</c> is out of range.
        /// </exception>
        public Tuple<K, T> this[int index] {
            get {
                Requires.That.IsIndexInRange(this, index, "index").Check();
                Element result = _ft.SplitTree(pair => pair.Item2 > index, Monoid.Zero).Middle;
                return Pair.New(result.Measure.Item1, result.Value);
            }
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same
        /// type.
        /// </summary>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other" /> parameter;
        /// otherwise, false.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        /// <remarks>It is possible to have two unequal sequences with the same elements
        /// and comparers.</remarks>
        public bool Equals(OrderedSequence<K, T> other) {
            return Equals(other._comparer, _comparer) &&
                   Equals(other._ft, _ft);
        }

        /// <summary>
        /// Indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <returns>
        /// true if <paramref name="obj" /> and this instance are the same type and represent the same value; otherwise, false.
        /// </returns>
        /// <param name="obj">Another object to compare to. </param><filterpriority>2</filterpriority>
        public override bool Equals(object obj) {
            if (obj.GetType() != typeof (OrderedSequence<K, T>)) return false;
            return Equals((OrderedSequence<K, T>) obj);
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>
        /// A 32-bit signed integer that is the hash code for this instance.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override int GetHashCode() {
            return _ft.GetHashCode();
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="left">The left sequence.</param>
        /// <param name="right">The right sequence.</param>
        /// <returns>Whether the sequences are equal.</returns>
        public static bool operator ==(OrderedSequence<K, T> left, OrderedSequence<K, T> right) {
            return left.Equals(right);
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left sequence.</param>
        /// <param name="right">The right sequence.</param>
        /// <returns>Whether the sequences are not equal.</returns>
        public static bool operator !=(OrderedSequence<K, T> left, OrderedSequence<K, T> right) {
            return !left.Equals(right);
        }

        /// <summary>
        /// Appends <paramref name="item"/> to <paramref name="tree"/>.
        /// </summary>
        public static OrderedSequence<K, T> operator |(OrderedSequence<K, T> tree, Tuple<K, T> item) {
            return tree.Insert(item.Item1, item.Item2);
        }

        public bool IsEmpty {
            get { return Count == 0; }
        }
    }

    /// <summary>
    /// Utility methods for creating <see cref="OrderedSequence{K,T}"/>.
    /// </summary>
    /// <seealso cref="OrderedSequence{K,T}"/>
    public static class OrderedSequence {
        internal static Monoid<Tuple<T, int>> MakeMonoid<T>(T noKey, IComparer<T> comparer) {
            return
                new Monoid<T>(noKey, (x, y) => comparer.Compare(y, noKey) == 0 ? x : y).Product(
                    Monoids.Size);
        }

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
        public static OrderedSequence<K, T> FromEnumerable<K, T>(IEnumerable<Tuple<K, T>> sequence,
                                                                 IComparer<K> comparer, K noKey) {
            var empty = Empty<K, T>(comparer, noKey);
            return empty.InsertRange(sequence);
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
        public static OrderedSequence<K, T> FromEnumerable<K, T>(IEnumerable<Tuple<K, T>> sequence,
                                                                 K noKey) where K : IComparable<K> {
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
        public static OrderedSequence<T, T> FromEnumerable<T>(IEnumerable<T> sequence,
                                                              IComparer<T> comparer, T noKey) {
            var empty = Empty<T, T>(comparer, noKey);
            return empty.InsertRange(sequence.Map(x => Pair.New(x, x)));
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
        public static OrderedSequence<T, T> FromEnumerable<T>(IEnumerable<T> sequence, T noKey)
            where T : IComparable<T> {
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
        public static OrderedSequence<double, T> FromEnumerable<T>(
            IEnumerable<Tuple<double, T>> sequence) {
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
        public static OrderedSequence<K, T> FromOrderedEnumerable<K, T>(
            IEnumerable<Tuple<K, T>> sequence, IComparer<K> comparer, K noKey) {
            Func<FingerTree<OrderedSequence<K, T>.Element, Tuple<K, int>>, Tuple<K, T>,
                FingerTree<OrderedSequence<K, T>.Element, Tuple<K, int>>> append =
                    (tree, pair) => tree | new OrderedSequence<K, T>.Element(pair.Item1, pair.Item2);
            var empty =
                FingerTree.Empty<OrderedSequence<K, T>.Element, Tuple<K, int>>(MakeMonoid(noKey,
                                                                                          comparer));
            return new OrderedSequence<K, T>(comparer, sequence.FoldLeft(append, empty));
        }

        /// <summary>
        /// Creates a <see cref="OrderedSequence{K,T}"/> with the elements from <paramref name="sequence"/>.
        /// </summary>
        /// <param name="noKey">The sentinel value. Do <b>not</b> add elements with this key to the sequence.</param>
        /// <param name="sequence">The sequence of pairs (key, element) placed into the sequence initially.
        /// Must be ordered by keys.</param>
        /// <remarks>If an element with priority equal to <paramref name="noKey"/> is ever inserted into the sequence,
        /// the behaviour of the resulting sequence is undefined.</remarks>
        public static OrderedSequence<K, T> FromOrderedEnumerable<K, T>(
            IEnumerable<Tuple<K, T>> sequence, K noKey)
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
        public static OrderedSequence<T, T> FromOrderedEnumerable<T>(IEnumerable<T> sequence,
                                                                     IComparer<T> comparer, T noKey) {
            Func<FingerTree<OrderedSequence<T, T>.Element, Tuple<T, int>>, T,
                FingerTree<OrderedSequence<T, T>.Element, Tuple<T, int>>> append =
                    (tree, t) => tree | new OrderedSequence<T, T>.Element(t, t);
            var empty =
                FingerTree.Empty<OrderedSequence<T, T>.Element, Tuple<T, int>>(MakeMonoid(noKey,
                                                                                          comparer));
            return new OrderedSequence<T, T>(comparer, sequence.FoldLeft(append, empty));
        }

        /// <summary>
        /// Creates a <see cref="OrderedSequence{K,T}"/> with the elements from <paramref name="sequence"/>.
        /// </summary>
        /// <param name="noKey">The sentinel value. Do <b>not</b> add elements with this key to the sequence.</param>
        /// <param name="sequence">The sequence of pairs (key, element) placed into the sequence initially.
        /// Must be ordered by keys.</param>
        /// <remarks>If an element with priority equal to <paramref name="noKey"/> is ever inserted into the sequence,
        /// the behaviour of the resulting sequence is undefined.</remarks>
        public static OrderedSequence<T, T> FromOrderedEnumerable<T>(IEnumerable<T> sequence,
                                                                     T noKey)
            where T : IComparable<T> {
            return FromOrderedEnumerable(sequence, Comparer<T>.Default, noKey);
        }

        /// <summary>
        /// Creates a <see cref="OrderedSequence{K,T}"/> with the elements from <paramref name="sequence"/>.
        /// </summary>
        /// <param name="sequence">The sequence of pairs (key, element) placed into the sequence initially.
        /// Must be ordered by keys.</param>
        /// <remarks>If an element with priority equal to <see cref="double.NegativeInfinity"/> is ever inserted into the sequence,
        /// the behaviour of the resulting sequence is undefined.</remarks>
        public static OrderedSequence<double, T> FromOrderedEnumerable<T>(
            IEnumerable<Tuple<double, T>> sequence) {
            return FromOrderedEnumerable(sequence, double.NegativeInfinity);
        }
    }
}