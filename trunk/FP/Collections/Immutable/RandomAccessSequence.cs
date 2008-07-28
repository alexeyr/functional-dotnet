using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using FP.HaskellNames;

namespace FP.Collections.Immutable {
    /// <summary>
    /// A finger-tree-based random access sequence.
    /// </summary>
    /// <typeparam name="T">Type of the elements of the sequence.</typeparam>
    /// TODO: A lot of sharing is lost due to the lack of newtype! Consider making it a specialisation!
    public class RandomAccessSequence<T> : IEnumerable<T>, IEquatable<RandomAccessSequence<T>> {
        private readonly FingerTree<Element, int> _ft;

        /// <summary>
        /// An element of the sequence.
        /// </summary>
        [DebuggerDisplay("Value = {Value}")]
        private struct Element : IMeasured<int> {
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
        /// Initializes a new instance of the <see cref="RandomAccessSequence{T}"/> class.
        /// </summary>
        public RandomAccessSequence() {
            _ft = FingerTree.Empty<Element, int>(Monoids.Size);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RandomAccessSequence{T}"/> class.
        /// </summary>
        /// <param name="sequence">The sequence.</param>
        public RandomAccessSequence(IEnumerable<T> sequence) {
            _ft = FingerTree.FromEnumerable(sequence.Map(x => new Element(x)), Monoids.Size);
        }

        private RandomAccessSequence(FingerTree<Element, int> ft) {
            _ft = ft;
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<T> GetEnumerator() {
            foreach (var element in _ft) {
                yield return element.Value;
            };
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
        /// </summary>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
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
        public int Count { get { return _ft.Measure; } }

        public RandomAccessSequence<T> Append(T newLast) {
            return new RandomAccessSequence<T>(_ft.Append(new Element(newLast)));
        }

        public RandomAccessSequence<T> Prepend(T newHead) {
            return new RandomAccessSequence<T>(_ft.Prepend(new Element(newHead)));
        }

        public Pair<RandomAccessSequence<T>, RandomAccessSequence<T>> SplitAt(int index) {
            var ftSplit = _ft.Split(i => i > index);
            return Pair.New(new RandomAccessSequence<T>(ftSplit.First), new RandomAccessSequence<T>(ftSplit.Second));
        }

        public T this[int index] { get { return _ft.SplitTree(i => i > index, 0).Middle.Value; } }

        public RandomAccessSequence<T> SetAt(int index, T newValue) {
            var split = _ft.SplitTree(i => i > index, 0);
            return new RandomAccessSequence<T>(split.Left.Append(new Element(newValue)).Concat(split.Right));
        }

        public RandomAccessSequence<T> Concat(RandomAccessSequence<T> otherSequence) {
            return new RandomAccessSequence<T>(_ft.Concat(otherSequence._ft));
        }

        public T Head { get { return _ft.Head.Value; } }
        public RandomAccessSequence<T> Tail { get { return new RandomAccessSequence<T>(_ft.Tail); } }
        public T Last { get { return _ft.Last.Value; } }
        public RandomAccessSequence<T> Init { get { return new RandomAccessSequence<T>(_ft.Init); } }
        
        public RandomAccessSequence<T> InsertAt(int index, T newValue) {
            var ftSplit = _ft.Split(i => i > index);
            return new RandomAccessSequence<T>(ftSplit.First.Append(new Element(newValue)).Concat(ftSplit.Second));
        }

        public RandomAccessSequence<T> RemoveAt(int index) {
            var split = _ft.SplitTree(i => i > index, 0);
            return new RandomAccessSequence<T>(split.Left.Concat(split.Right));
        }
    }
}