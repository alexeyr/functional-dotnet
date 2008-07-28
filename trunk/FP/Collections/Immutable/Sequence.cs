using System;
using System.Collections;
using System.Collections.Generic;
using FP.HaskellNames;

namespace FP.Collections.Immutable {
    /// <summary>
    /// A finger-tree-based random access sequence.
    /// </summary>
    /// <typeparam name="T">Type of the elements of the sequence.</typeparam>
    /// TODO: A lot of sharing is lost due to the lack of newtype! Consider making it a specialisation!
    public class Sequence<T> : IEnumerable<T>, IEquatable<Sequence<T>> {
        private readonly FingerTree<Element, int> _ft;

        /// <summary>
        /// An element of the sequence.
        /// </summary>
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
            /// Initializes a new instance of the <see cref="Sequence&lt;T&gt;.Element"/> struct.
            /// </summary>
            /// <param name="value">The value.</param>
            public Element(T value) {
                Value = value;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Sequence&lt;T&gt;"/> class.
        /// </summary>
        public Sequence() {
            _ft = FingerTree.Empty<Element, int>(Monoids.Size);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Sequence&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="sequence">The sequence.</param>
        public Sequence(IEnumerable<T> sequence) {
            _ft = FingerTree.FromEnumerable(sequence.Map(x => new Element(x)), Monoids.Size);
        }

        private Sequence(FingerTree<Element, int> ft) {
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
        public bool Equals(Sequence<T> other) {
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
            if (obj.GetType() != typeof (Sequence<T>)) return false;
            return Equals((Sequence<T>) obj);
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
        public static bool operator ==(Sequence<T> left, Sequence<T> right) {
            return left.Equals(right);
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(Sequence<T> left, Sequence<T> right) {
            return !left.Equals(right);
        }

        /// <summary>
        /// Gets the number of elements in the sequence.
        /// </summary>
        /// <value>The number of elements in the sequence.</value>
        public int Count { get { return _ft.Measure; } }

        public Sequence<T> Append(T newLast) {
            return new Sequence<T>(_ft.Append(new Element(newLast)));
        }

        public Sequence<T> Prepend(T newHead) {
            return new Sequence<T>(_ft.Prepend(new Element(newHead)));
        }

        public Pair<Sequence<T>, Sequence<T>> SplitAt(int index) {
            var ftSplit = _ft.Split(i => i > index);
            return Pair.New(new Sequence<T>(ftSplit.First), new Sequence<T>(ftSplit.Second));
        }

        public T this[int index] { get { return _ft.SplitTree(i => i > index, 0).Middle.Value; } }

        public Sequence<T> SetAt(int index) {
            throw new NotImplementedException();
        }

        public Sequence<T> Concat(Sequence<T> otherSequence) {
            return new Sequence<T>(_ft.Concat(otherSequence._ft));
        }

        public T Head { get { return _ft.Head.Value; } }
        public Sequence<T> Tail { get { return new Sequence<T>(_ft.Tail); } }
        public T Last { get { return _ft.Last.Value; } }
        public Sequence<T> Init { get { return new Sequence<T>(_ft.Init); } }
        
        public Sequence<T> InsertAt(int index) {
            throw new NotImplementedException();
        }

        public Sequence<T> RemoveAt(int index) {
            throw new NotImplementedException();
        }
    }
}