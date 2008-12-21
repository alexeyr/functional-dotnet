/*
* RandomAccessSequence.cs is part of functional-dotnet project
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

namespace FP.Collections.Immutable {
    /// <summary>
    /// A finger-tree-based random access sequence.
    /// An amortized running time is given for each operation, with <i>n</i> referring to the length 
    /// of the sequence and <i>i</i> being the integral index used by some operations. 
    /// </summary>
    /// <typeparam name="T">Type of the elements of the sequence.</typeparam>
    /// <remarks>Do not use the default constructor.</remarks>
    public struct RandomAccessSequence<T> :
        IEquatable<RandomAccessSequence<T>>,
        IInsertableRandomAccessSequence<T, RandomAccessSequence<T>>,
        IReversible<RandomAccessSequence<T>>, ICatenable<RandomAccessSequence<T>>,
        ISplittable<RandomAccessSequence<T>> {
        private static readonly RandomAccessSequence<T> _emptyInstance =
            new RandomAccessSequence<T>(FingerTree.Empty<Element, int>(Monoids.Size));

        /// <summary>
        /// The empty <see cref="RandomAccessSequence{T}"/>.
        /// </summary>
        public static RandomAccessSequence<T> Empty {
            get { return _emptyInstance; }
        }

        private readonly FingerTree<Element, int> _ft;

        internal bool Invariant {
            get { return _ft != null /*&& _ft.Invariant*/; }
        }

        /// <summary>
        /// An element of the sequence.
        /// </summary>
        [DebuggerDisplay("Value = {Value}")]
        internal struct Element : IMeasured<int> {
            /// <summary>
            /// Gets the measure of the object.
            /// </summary>
            /// <value>The measure.</value>
            public int Measure {
                get { return 1; }
            }

            internal readonly T Value;

            /// <summary>
            /// Initializes a new instance of the <see cref="RandomAccessSequence{T}.Element"/> struct.
            /// </summary>
            /// <param name="value">The value.</param>
            public Element(T value) {
                Value = value;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RandomAccessSequence{T}"/> class
        /// containing the same elements as <paramref name="sequence"/>.
        /// </summary>
        /// <param name="sequence">The original sequence.</param>
        public RandomAccessSequence(IEnumerable<T> sequence) {
            _ft = FingerTree.FromEnumerable(sequence.Map(x => new Element(x)), Monoids.Size);
        }

        internal RandomAccessSequence(FingerTree<Element, int> ft) {
            _ft = ft;
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

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// <i>O(1)</i>
        /// </summary>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        /// <remarks>It is possible to have two unequal sequences with the same elements.</remarks>
        public bool Equals(RandomAccessSequence<T> other) {
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
            if (obj.GetType() != typeof (RandomAccessSequence<T>)) return false;
            return Equals((RandomAccessSequence<T>) obj);
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
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(RandomAccessSequence<T> left, RandomAccessSequence<T> right) {
            return left.Equals(right);
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(RandomAccessSequence<T> left, RandomAccessSequence<T> right) {
            return !left.Equals(right);
        }

        /// <summary>
        /// Gets the number of elements in the sequence.
        /// </summary>
        /// <value>The number of elements in the sequence.</value>
        public int Count {
            get { return _ft.Measure; }
        }

        /// <summary>
        /// Prepends the specified element to the beginning of the list.
        /// </summary>
        /// <param name="newHead">The new head.</param>
        /// <returns>The resulting list.</returns>
        public RandomAccessSequence<T> Prepend(T newHead) {
            return new RandomAccessSequence<T>(new Element(newHead) | _ft);
        }

        /// <summary>
        /// Appends the specified element to the end of the list.
        /// </summary>
        /// <param name="newLast">The new last element.</param>
        /// <returns>The resulting list.</returns>
        public RandomAccessSequence<T> Append(T newLast) {
            return new RandomAccessSequence<T>(_ft | new Element(newLast));
        }

        /// <summary>
        /// Returns a pair of sequences, the first contains the first <paramref name="count"/> of
        /// the sequence and the second one contains the rest of them.
        /// </summary>
        /// <param name="count">The index at which the sequence will be split.</param>
        /// <remarks>if <code>count &lt;= 0 || count &gt;= Count</code>, the corresponding part 
        /// of the result will be empty.</remarks>
        public Tuple<RandomAccessSequence<T>, RandomAccessSequence<T>> SplitAt(int count) {
            if (count <= 0)
                return Pair.New(Empty, this);
            if (count >= Count)
                return Pair.New(this, Empty);
            var ftSplit = _ft.Split(i => i > count);
            return Pair.New(new RandomAccessSequence<T>(ftSplit.Item1),
                            new RandomAccessSequence<T>(ftSplit.Item2));
        }

        /// <summary>Returns a specified number of contiguous elements from the start of the sequence.</summary>
        /// <returns>A <see cref="RandomAccessSequence{T}" /> that contains the specified number of elements from the start of the input sequence.</returns>
        /// <param name="count">The number of elements to return.</param>
        public RandomAccessSequence<T> Take(int count) {
            return SplitAt(count).Item1;
        }

        /// <summary>Bypasses a specified number of elements in a sequence and then returns the remaining elements.</summary>
        /// <returns>A <see cref="RandomAccessSequence{T}" /> that contains the elements that occur after the specified index in the input sequence.</returns>
        /// <param name="count">The number of elements to return.</param>
        public RandomAccessSequence<T> Skip(int count) {
            return SplitAt(count).Item2;
        }

        /// <summary>
        /// Gets the <see cref="T"/> at the specified index.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"><c>index</c> is out of range.</exception>
        public T this[int index] {
            get {
                if (index < 0 || index >= Count)
                    throw new ArgumentOutOfRangeException("index");
                return _ft.SplitTree(i => i > index, 0).Middle.Value;
            }
        }

        /// <summary>
        /// Updates the element at <paramref name="index"/> using <paramref name="function"/>.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="function">The function to apply to the element currently at <paramref name="index"/></param>
        /// <exception cref="ArgumentOutOfRangeException"><c>index</c> is out of range.</exception>
        /// <remarks>
        /// Equivalent to <code>SetAt(index, function(this[index])), but faster.</code>
        /// </remarks>
        public RandomAccessSequence<T> UpdateAt(int index, Func<T, T> function) {
            if (index < 0 || index >= Count)
                throw new ArgumentOutOfRangeException("index");
            var split = _ft.SplitTree(i => i > index, 0);
            T currentValue = split.Middle.Value;
            return
                new RandomAccessSequence<T>((split.Left | new Element(function(currentValue))) +
                                            split.Right);
        }

        /// <summary>
        /// Concatenates the sequence with another.
        /// </summary>
        /// <param name="otherSequence">Another sequence.</param>
        /// <returns>The result of concatenation.</returns>
        public RandomAccessSequence<T> Concat(RandomAccessSequence<T> otherSequence) {
            return new RandomAccessSequence<T>(_ft + otherSequence._ft);
        }

        /// <summary>
        /// Gets a value indicating whether this list is empty.
        /// </summary>
        /// <value><c>true</c>.</value>
        public bool IsEmpty {
            get { return _ft.IsEmpty; }
        }

        /// <summary>
        /// Gets the head of the list.
        /// </summary>
        /// <value>Throws <see cref="EmptySequenceException"/>.</value>
        /// <exception cref="EmptySequenceException"></exception>
        public T Head {
            get { return _ft.Head.Value; }
        }

        /// <summary>
        /// Gets the tail of the list.
        /// </summary>
        /// <value>Throws <see cref="EmptySequenceException"/>.</value>
        /// <exception cref="EmptySequenceException"></exception>
        public RandomAccessSequence<T> Tail {
            get { return new RandomAccessSequence<T>(_ft.Tail); }
        }

        /// <summary>
        /// Gets the initial sublist (all elements but the last) of the list.
        /// </summary>
        /// <value>Throws <see cref="EmptySequenceException"/>.</value>
        /// <exception cref="EmptySequenceException"></exception>
        public RandomAccessSequence<T> Init {
            get { return new RandomAccessSequence<T>(_ft.Init); }
        }

        /// <summary>
        /// Gets the last element of the list.
        /// </summary>
        /// <value>Throws <see cref="EmptySequenceException"/>.</value>
        /// <exception cref="EmptySequenceException"></exception>
        public T Last {
            get { return _ft.Last.Value; }
        }

        /// <summary>
        /// Inserts <paramref name="newValue"/> at <paramref name="index"/>.
        /// </summary>
        /// <param name="index">The index where the new element shall be inserted.</param>
        /// <param name="newValue">The new value.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"><c>index</c> is out of range.</exception>
        public RandomAccessSequence<T> InsertAt(int index, T newValue) {
            if (index < 0 || index >= Count)
                throw new ArgumentOutOfRangeException("index");
            var ftSplit = _ft.Split(i => i > index);
            return
                new RandomAccessSequence<T>((ftSplit.Item1 | new Element(newValue)) + ftSplit.Item2);
        }

        /// <summary>
        /// Removes the element at index <paramref name="index"/>.
        /// </summary>
        /// <param name="index">The index of the element to remove.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"><c>index</c> is out of range.</exception>
        public RandomAccessSequence<T> RemoveAt(int index) {
            if (index < 0 || index >= Count)
                throw new ArgumentOutOfRangeException("index");
            var split = _ft.SplitTree(i => i > index, 0);
            return new RandomAccessSequence<T>(split.Left + split.Right);
        }

        /// <summary>
        /// Prepends a sequence.
        /// </summary>
        /// <param name="ts">The sequence of elements to prepend.</param>
        /// <returns>The new random access sequence consisting of elements of <paramref name="ts"/>
        /// and this random access sequence.</returns>
        public RandomAccessSequence<T> PrependRange(IEnumerable<T> ts) {
            return new RandomAccessSequence<T>(_ft.PrependRange(ts.Map(t => new Element(t))));
        }

        /// <summary>
        /// Appends a sequence.
        /// </summary>
        /// <param name="ts">The sequence of elements to prepend.</param>
        /// <returns>The new random access sequence consisting of
        /// this random access sequence and elements of <paramref name="ts"/>.</returns>
        public RandomAccessSequence<T> AppendRange(IEnumerable<T> ts) {
            return new RandomAccessSequence<T>(_ft.AppendRange(ts.Map(t => new Element(t))));
        }

        /// <summary>
        /// Inserts all elements in <paramref name="ts"/> at <paramref name="index"/>.
        /// </summary>
        /// <param name="index">The index where the new element shall be inserted.</param>
        /// <param name="ts">The collection of values to insert.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"><c>index</c> is out of range.</exception>
        public RandomAccessSequence<T> InsertRangeAt(int index, IEnumerable<T> ts) {
            if (index < 0 || index >= Count)
                throw new ArgumentOutOfRangeException("index");
            var ftSplit = _ft.Split(i => i > index);
            return
                new RandomAccessSequence<T>(
                    (ftSplit.Item1.AppendRange(ts.Map(t => new Element(t)))) + ftSplit.Item2);
        }

        /// <summary>
        /// Removes <paramref name="length"/> elements, starting at index <paramref name="startIndex"/>.
        /// </summary>
        /// <param name="startIndex">The index of the first element to remove.</param>
        /// <param name="length">The number of elements to remove.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="startIndex"/> or 
        /// <paramref name="length"/> is out of range.</exception>
        public RandomAccessSequence<T> RemoveRangeAt(int startIndex, int length) {
            if (startIndex < 0 || startIndex >= Count)
                throw new ArgumentOutOfRangeException("startIndex");
            if (startIndex + length > Count)
                throw new ArgumentOutOfRangeException("length");
            //is special casing this needed?
//            if (length == 0)
//                return this;
//            if (startIndex == 0)
//                return Skip(length);
//            if (startIndex + length == Count)
//                return Take(startIndex);
            var split1 = _ft.Split(i => i >= startIndex);
            var split2 = split1.Item2.Split(i => i >= length);
            return new RandomAccessSequence<T>(split1.Item1 + split2.Item2);
        }

        /// <summary>
        /// Returns the subsequence of length <paramref name="length"/> starting at index <paramref name="startIndex"/>.
        /// </summary>
        /// <param name="startIndex">The index of the first element in the subsequence.</param>
        /// <param name="length">The number of elements in the subsequence.</param>
        /// <exception cref="ArgumentOutOfRangeException"><c>index</c> is out of range.</exception>
        public RandomAccessSequence<T> Subsequence(int startIndex, int length) {
            if (startIndex < 0 || startIndex >= Count)
                throw new ArgumentOutOfRangeException("startIndex");
            if (startIndex + length > Count)
                throw new ArgumentOutOfRangeException("length");
            //is special casing this needed?
            //            if (length == 0)
            //                return this;
            //            if (startIndex == 0)
            //                return Skip(length);
            //            if (startIndex + length == Count)
            //                return Take(startIndex);
            var split1 = _ft.Split(i => i >= startIndex);
            var split2 = split1.Item2.Split(i => i >= length);
            return new RandomAccessSequence<T>(split2.Item1);
        }

        /// <summary>
        /// Reverses the sequence.
        /// </summary>
        /// <returns>The sequence containing the same elements in reverse order.</returns>
        public RandomAccessSequence<T> Reverse() {
            return new RandomAccessSequence<T>(_ft.ReverseTree(Functions.Id<Element>()));
        }

        /// <summary>
        /// Prepends <paramref name="item"/> to <paramref name="seq"/>.
        /// </summary>
        public static RandomAccessSequence<T> operator |(T item, RandomAccessSequence<T> seq) {
            return seq.Prepend(item);
        }

        /// <summary>
        /// Appends <paramref name="item"/> to <paramref name="seq"/>.
        /// </summary>
        public static RandomAccessSequence<T> operator |(RandomAccessSequence<T> seq, T item) {
            return seq.Append(item);
        }

        /// <summary>
        /// Concatenates <paramref name="seq1"/> and <paramref name="seq2"/>.
        /// </summary>
        public static RandomAccessSequence<T> operator +(
            RandomAccessSequence<T> seq1, RandomAccessSequence<T> seq2) {
            return seq1.Concat(seq2);
        }
        }

    /// <summary>
    /// Utility methods for creating <see cref="RandomAccessSequence{T}"/>.
    /// </summary>
    /// <seealso cref="RandomAccessSequence{T}"/>
    public static class RandomAccessSequence {
        /// <summary>
        /// Creates an empty <see cref="RandomAccessSequence{T}"/>.
        /// </summary>
        public static RandomAccessSequence<T> Empty<T>() {
            return RandomAccessSequence<T>.Empty;
        }

        /// <summary>
        /// Creates a <see cref="RandomAccessSequence{T}"/> with a single element.
        /// </summary>
        /// <param name="item">The only item in the sequence.</param>
        public static RandomAccessSequence<T> Singleton<T>(T item) {
            return
                new RandomAccessSequence<T>(
                    FingerTree.Single(new RandomAccessSequence<T>.Element(item), Monoids.Size));
        }

        /// <summary>
        /// Creates a <see cref="RandomAccessSequence{T}"/> containing the elements in <paramref name="sequence"/>.
        /// </summary>
        public static RandomAccessSequence<T> FromEnumerable<T>(IEnumerable<T> sequence) {
            return new RandomAccessSequence<T>(sequence);
        }
    }
}