using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using FP.HaskellNames;

namespace FP.Collections.Immutable {
    /// <summary>
    /// A finger-tree-based random access sequence.
    /// An amortized running time is given for each operation, with <i>n</i> referring to the length of the sequence and i being the integral index used by some operations. 
    /// </summary>
    /// <typeparam name="T">Type of the elements of the sequence.</typeparam>
    public class RandomAccessSequence<T> : IEnumerable<T>, IEquatable<RandomAccessSequence<T>> {
        // TODO: A lot of sharing is lost due to the lack of newtype! Consider making it a specialisation!
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

        /// <summary>
        /// Prepends the specified element to the beginning of the list.
        /// </summary>
        /// <param name="newHead">The new head.</param>
        /// <returns>The resulting list.</returns>
        public RandomAccessSequence<T> Prepend(T newHead) {
            return new RandomAccessSequence<T>(_ft.Prepend(new Element(newHead)));
        }

        /// <summary>
        /// Appends the specified element to the end of the list.
        /// </summary>
        /// <param name="newLast">The new last element.</param>
        /// <returns>The resulting list.</returns>
        public RandomAccessSequence<T> Append(T newLast) {
            return new RandomAccessSequence<T>(_ft.Append(new Element(newLast)));
        }

        /// <summary>
        /// Returns a pair of sequences, the first contains the first <paramref name="count"/> of
        /// the sequence and the second one contains the rest of them.
        /// </summary>
        /// <param name="count">The index at which the sequence will be split.</param>
        /// <returns></returns>
        public Pair<RandomAccessSequence<T>, RandomAccessSequence<T>> SplitAt(int count) {
            var ftSplit = _ft.Split(i => i > count);
            return Pair.New(new RandomAccessSequence<T>(ftSplit.First), new RandomAccessSequence<T>(ftSplit.Second));
        }

        /// <summary>Returns a specified number of contiguous elements from the start of the sequence.</summary>
        /// <returns>A <see cref="RandomAccessSequence{T}" /> that contains the specified number of elements from the start of the input sequence.</returns>
        /// <param name="count">The number of elements to return.</param>
        public RandomAccessSequence<T> Take(int count) {
            return SplitAt(count).First;
        }

        /// <summary>Bypasses a specified number of elements in a sequence and then returns the remaining elements.</summary>
        /// <returns>A <see cref="RandomAccessSequence{T}" /> that contains the elements that occur after the specified index in the input sequence.</returns>
        /// <param name="count">The number of elements to return.</param>
        public RandomAccessSequence<T> Skip(int count) {
            return SplitAt(count).Second;
        }

        /// <summary>
        /// Gets the <see cref="T"/> at the specified index.
        /// </summary>
        public T this[int index] { get { return _ft.SplitTree(i => i > index, 0).Middle.Value; } }

        /// <summary>
        /// Replaces the <see cref="T"/> at the specified index with the specified value.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="newValue">The new value.</param>
        /// <returns>The sequence where the element at <paramref name="index"/> has the value <paramref name="newValue"/>
        /// and all other elements have the same value as in the original sequence.</returns>
        public RandomAccessSequence<T> SetAt(int index, T newValue) {
            var split = _ft.SplitTree(i => i > index, 0);
            return new RandomAccessSequence<T>(split.Left.Append(new Element(newValue)).Concat(split.Right));
        }

        /// <summary>
        /// Concatenates the sequence with another.
        /// </summary>
        /// <param name="otherSequence">Another sequence.</param>
        /// <returns>The result of concatenation.</returns>
        public RandomAccessSequence<T> Concat(RandomAccessSequence<T> otherSequence) {
            return new RandomAccessSequence<T>(_ft.Concat(otherSequence._ft));
        }

        /// <summary>
        /// Gets a value indicating whether this list is empty.
        /// </summary>
        /// <value><c>true</c>.</value>
        public bool IsEmpty { get { return _ft.IsEmpty; } }

        /// <summary>
        /// Gets the head of the list.
        /// </summary>
        /// <value>Throws <see cref="EmptySequenceException"/>.</value>
        /// <exception cref="EmptySequenceException"></exception>
        public T Head { get { return _ft.Head.Value; } }
        /// <summary>
        /// Gets the tail of the list.
        /// </summary>
        /// <value>Throws <see cref="EmptySequenceException"/>.</value>
        /// <exception cref="EmptySequenceException"></exception>
        public RandomAccessSequence<T> Tail { get { return new RandomAccessSequence<T>(_ft.Tail); } }
        /// <summary>
        /// Gets the initial sublist (all elements but the last) of the list.
        /// </summary>
        /// <value>Throws <see cref="EmptySequenceException"/>.</value>
        /// <exception cref="EmptySequenceException"></exception>
        public RandomAccessSequence<T> Init { get { return new RandomAccessSequence<T>(_ft.Init); } }
        /// <summary>
        /// Gets the last element of the list.
        /// </summary>
        /// <value>Throws <see cref="EmptySequenceException"/>.</value>
        /// <exception cref="EmptySequenceException"></exception>
        public T Last { get { return _ft.Last.Value; } }

        /// <summary>
        /// Inserts <paramref name="newValue"/> at <paramref name="index"/>.
        /// </summary>
        /// <param name="index">The index where the new element shall be inserted.</param>
        /// <param name="newValue">The new value.</param>
        /// <returns></returns>
        public RandomAccessSequence<T> InsertAt(int index, T newValue) {
            var ftSplit = _ft.Split(i => i > index);
            return new RandomAccessSequence<T>(ftSplit.First.Append(new Element(newValue)).Concat(ftSplit.Second));
        }

        /// <summary>
        /// Removes the element at index <paramref name="index"/>.
        /// </summary>
        /// <param name="index">The index of the element to remove.</param>
        /// <returns></returns>
        public RandomAccessSequence<T> RemoveAt(int index) {
            var split = _ft.SplitTree(i => i > index, 0);
            return new RandomAccessSequence<T>(split.Left.Concat(split.Right));
        }

        /// <summary>
        /// Reverses the sequence.
        /// </summary>
        /// <returns>The sequence containing the same elements in reverse order.</returns>
        public RandomAccessSequence<T> Reverse() {
            // TODO: temp realisation until specialisation
            //-- | /O(n)/. The reverse of a sequence.
            //reverse :: Seq a -> Seq a
            //reverse (Seq xs) = Seq (reverseTree id xs)
            //
            //reverseTree :: (a -> a) -> FingerTree a -> FingerTree a
            //reverseTree _ Empty = Empty
            //reverseTree f (Single x) = Single (f x)
            //reverseTree f (Deep s pr m sf) =
            //	Deep s (reverseDigit f sf)
            //		(reverseTree (reverseNode f) m)
            //		(reverseDigit f pr)
            //
            //reverseDigit :: (a -> a) -> Digit a -> Digit a
            //reverseDigit f (One a) = One (f a)
            //reverseDigit f (Two a b) = Two (f b) (f a)
            //reverseDigit f (Three a b c) = Three (f c) (f b) (f a)
            //reverseDigit f (Four a b c d) = Four (f d) (f c) (f b) (f a)
            //
            //reverseNode :: (a -> a) -> Node a -> Node a
            //reverseNode f (Node2 s a b) = Node2 s (f b) (f a)
            //reverseNode f (Node3 s a b c) = Node3 s (f c) (f b) (f a)

            return
                new RandomAccessSequence<T>(
                    _ft.ReduceL(FingerTree<Element, int>._append)(_ft.EmptyInstance));

            //TODO: Appendd/PrependRange, InsertRangeAt, RemoveRangeAt (consider specialisation first!)
        }
    }
}