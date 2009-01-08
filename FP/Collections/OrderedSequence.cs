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
using System.Linq;
using FP.Core;
using FP.Validation;

namespace FP.Collections {
    /// <summary>
    /// A finger-tree-based ordered sequence.
    /// </summary>
    /// <typeparam name="T">The type of the elements of the sequence.</typeparam>
    /// <remarks>Do not use the default constructor.</remarks>
    public struct OrderedSequence<T> : IEnumerable<T>, IEquatable<OrderedSequence<T>> where T : IComparable<T> {
        private static readonly OrderedSequence<T> _emptyInstance =
            new OrderedSequence<T>(FingerTreeOrdered<Element, T>.EmptyInstance);

        /// <summary>
        /// Gets the empty ordered sequence.
        /// </summary>
        /// <value>The empty instance.</value>
        public static OrderedSequence<T> EmptyInstance { get { return _emptyInstance; } }

        private readonly FingerTreeOrdered<Element, T> _ft;

        internal OrderedSequence(FingerTreeOrdered<Element, T> ft) {
            _ft = ft;
        }

        [DebuggerDisplay("{Value}")]
        [DebuggerStepThrough]
        internal struct Element : IMeasured<T> {
            internal readonly T Value;

            public Element(T value) {
                Value = value;
            }

            public T Measure {
                get { return Value; }
            }
        }

        /// <summary>
        /// Gets the maximal element of the sequence.
        /// </summary>
        /// <remarks>If several elements are equal and maximal, ties may be broken arbitrarily.</remarks>
        /// <exception cref="EmptyEnumerableException">There are no elements in the sequence.</exception>
        public T Max {
            get { return _ft.Last.Value; }
        }

        /// <summary>
        /// Gets the minimal element of the sequence.
        /// </summary>
        /// <remarks>If several elements are equal and minimal, ties may be broken arbitrarily.</remarks>
        /// <exception cref="EmptyEnumerableException">There are no elements in the sequence.</exception>
        public T Min {
            get { return _ft.Head.Value; }
        }

        /// <summary>
        /// Removes the maximal element of the sequence.
        /// </summary>
        /// <remarks>If several elements are equal and maximal, ties may be broken arbitrarily.</remarks>
        /// <exception cref="EmptyEnumerableException">There are no elements in the sequence.</exception>
        public OrderedSequence<T> RemoveMax() {
            return new OrderedSequence<T>(_ft.Init);
        }

        /// <summary>
        /// Removes the minimal element of the sequence.
        /// </summary>
        /// <remarks>If several elements are equal and minimal, ties may be broken arbitrarily.</remarks>
        /// <exception cref="EmptyEnumerableException">There are no elements in the sequence.</exception>
        public OrderedSequence<T> RemoveMin() {
            return new OrderedSequence<T>(_ft.Tail);
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to
        /// iterate through the collection.
        /// </returns>
        public IEnumerator<T> GetEnumerator() {
            foreach (var element in _ft)
                yield return element.Value;
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same
        /// type.
        /// </summary>
        /// <returns>
        /// <c>true</c> if the current object is equal to the <paramref name="other" /> parameter;
        /// otherwise, <c>false</c>.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public bool Equals(OrderedSequence<T> other) {
            return Equals(other._ft, _ft);
        }

        /// <summary>
        /// Indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <returns>
        /// true if <paramref name="obj" /> and this instance are the same type and represent the same value; otherwise, false.
        /// </returns>
        /// <param name="obj">Another object to compare to. </param><filterpriority>2</filterpriority>
        public override bool Equals(object obj) {
            if (obj.GetType() != typeof(OrderedSequence<T>)) return false;
            return Equals((OrderedSequence<T>) obj);
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
        /// Merges <paramref name="seq1"/> and <paramref name="seq2"/>.
        /// </summary>
        public static OrderedSequence<T> operator +(OrderedSequence<T> seq1, OrderedSequence<T> seq2) {
            return seq1.Union(seq2);
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="left">The left sequence.</param>
        /// <param name="right">The right sequence.</param>
        /// <returns>Whether the sequences are equal.</returns>
        public static bool operator ==(OrderedSequence<T> left, OrderedSequence<T> right) {
            return left.Equals(right);
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left sequence.</param>
        /// <param name="right">The right sequence.</param>
        /// <returns>Whether the sequences are not equal.</returns>
        public static bool operator !=(OrderedSequence<T> left, OrderedSequence<T> right) {
            return !left.Equals(right);
        }

        /// <summary>
        /// Appends <paramref name="item"/> to <paramref name="seq"/>.
        /// </summary>
        public static OrderedSequence<T> operator |(OrderedSequence<T> seq, T item) {
            return seq.Insert(item);
        }

        public bool IsEmpty {
            get { return _ft.IsEmpty; }
        }

        /// <summary>
        ///  Returns an iterator which yields all elements of the sequence in the reverse order.
        /// </summary>
        /// <remarks>
        /// This should always be equivalent to, but faster than, 
        /// <code>
        ///  AsEnumerable().Reverse();
        /// </code>
        /// </remarks>
        public IEnumerable<T> ReverseIterator() {
            return _ft.ReverseIterator().Select(el => el.Value);
        }

        public Optional<T> this[T key] {
            get { return _ft[key].Map(el => el.Value); }
        }

        public bool Contains(T key) {
            return _ft[key].HasValue;
        }

        /// <summary>
        /// Extracts all elements with the given key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns><see cref="Optional{T}.None"/> if there are no elements equal to <paramref cref="key"/>;
        /// The first field of the result contains all elements less than or equal to
        /// <see cref="key"/> except one, the second is equal to it, and the third one greater elements.</returns>
        public Optional<Tuple<OrderedSequence<T>, T, OrderedSequence<T>>> ExtractOne(T key) {
            var maybeTriple = _ft.ExtractOne(key);
            if (!maybeTriple.HasValue)
                return Optional<Tuple<OrderedSequence<T>, T, OrderedSequence<T>>>.None;
            var triple = maybeTriple.Value;
            return Tuple.New(
                new OrderedSequence<T>(triple.Item1),
                triple.Item2.Value,
                new OrderedSequence<T>(triple.Item3));
        }

        /// <summary>
        /// Extracts all elements with the given key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>Three trees. The first one contains all elements less than
        /// <paramref cref="key"/>, the second one equal elements, and the third one greater
        /// elements.</returns>
        public Tuple<OrderedSequence<T>, OrderedSequence<T>, OrderedSequence<T>> ExtractAll(T key) {
            var triple = _ft.ExtractAll(key);
            return Tuple.New(
                new OrderedSequence<T>(triple.Item1),
                new OrderedSequence<T>(triple.Item2),
                new OrderedSequence<T>(triple.Item3));
        }

        /// <summary>
        /// Inserts the specified item into the sequence.
        /// </summary>
        /// <param name="item">The item.</param>
        public OrderedSequence<T> Insert(T item) {
            return new OrderedSequence<T>(_ft.Insert(new Element(item)));
        }

        /// <summary>
        /// Inserts all specified items into the sequence.
        /// </summary>
        /// <param name="items">The items.</param>
        public OrderedSequence<T> Insert(params T[] items) {
            return InsertRange(items.AsEnumerable());
        }

        /// <summary>
        /// Inserts all specified items into the sequence.
        /// </summary>
        /// <param name="items">The items.</param>
        public OrderedSequence<T> InsertRange(IEnumerable<T> items) {
            return new OrderedSequence<T>(_ft.InsertRange(items.Select(t => new Element(t))));
        }

        /// <summary>
        /// Returns the union of the sequence with specified other sequence.
        /// </summary>
        /// <param name="otherSequence">The other sequence.</param>
        public OrderedSequence<T> Union(OrderedSequence<T> otherSequence) {
            return new OrderedSequence<T>(_ft.Merge(otherSequence._ft));
        }

        /// <summary>
        /// Intersects the sequence with <paramref name="otherSequence"/>.
        /// </summary>
        /// <param name="otherSequence">The other sequence.</param>
        public OrderedSequence<T> Intersect(OrderedSequence<T> otherSequence) {
            return new OrderedSequence<T>(_ft.Intersect(otherSequence._ft));
        }

        /// <summary>
        /// Splits the sequence into two parts. The first one contains all elements less
        /// than <paramref cref="key"/>, the second one all greater elements. Elements
        /// equal to <paramref cref="key"/> go into one of them according to the 
        /// <paramref name="equalGoLeft"/> parameter.
        /// </summary>
        /// <param name="key">The element on which the sequence is split.</param>
        /// <param name="equalGoLeft">if set to <c>true</c>, elements with the measure
        /// equal to <see cref="key"/> will be at the left side of the split; otherwise,
        /// they will be on the right side.</param>
        public Tuple<OrderedSequence<T>, OrderedSequence<T>> Split(T key, bool equalGoLeft) {
            var pair = _ft.Split(key, equalGoLeft);
            return Pair.New(
                new OrderedSequence<T>(pair.Item1),
                new OrderedSequence<T>(pair.Item2));
        }

        /// <summary>
        /// Reduces the finger tree from the right.
        /// </summary>
        /// <typeparam name="A">The type of the accumulator.</typeparam>
        /// <param name="binOp">The binary operation.</param>
        /// <param name="initial">The initial accumulator value.</param>
        /// <returns>
        /// The final accumulator value.
        /// </returns>
        public A FoldRight<A>(Func<T, A, A> binOp, A initial) {
            return _ft.FoldRight((el, a) => binOp(el.Value, a), initial);
        }

        /// <summary>
        /// Reduces the finger tree from the left.
        /// </summary>
        /// <typeparam name="A">The type of the accumulator.</typeparam>
        /// <param name="binOp">The binary operation.</param>
        /// <param name="initial">The initial accumulator value.</param>
        /// <returns>
        /// The final accumulator value.
        /// </returns>
        public A FoldLeft<A>(Func<A, T, A> binOp, A initial) {
            return _ft.FoldLeft((a, el) => binOp(a, el.Value), initial);
        }
    }

    /// <summary>
    /// Utility methods for creating <see cref="OrderedSequence{T}"/>.
    /// </summary>
    /// <seealso cref="OrderedSequence{T}"/>
    public static class OrderedSequence {
        /// <summary>
        /// Returns the empty <see cref="OrderedSequence{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of the elements of the sequence.</typeparam>
        public static OrderedSequence<T> Empty<T>() where T : IComparable<T> {
            return OrderedSequence<T>.EmptyInstance;
        }

        /// <summary>
        /// Creates a <see cref="OrderedSequence{T}"/> with the elements from 
        /// <paramref name="sequence"/>.
        /// </summary>
        /// <typeparam name="T">The type of the elements of the sequence.</typeparam>
        /// <param name="sequence">The sequence of pairs (key, element) placed into the
        /// sequence initially.
        /// </param>
        /// <remarks>If <paramref name="sequence"/> is sorted (either descending or
        /// ascending), the running time is O(n); otherwise it is O(n * log n)</remarks>
        public static OrderedSequence<T> FromEnumerable<T>(IEnumerable<T> sequence) where T : IComparable<T> {
            return Empty<T>().InsertRange(sequence);
        }
    }
}