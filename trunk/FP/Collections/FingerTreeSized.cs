/*
* FingerTreeSized.cs is part of functional-dotnet project
* 
* Copyright (c) 2009 Alexey Romanov
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
using FP.HaskellNames;
using FP.Util;
using FP.Validation;

namespace FP.Collections {
    /// <summary>
    /// A specialization of <see cref="FingerTree{T,V}"/> using number of elements as measure
    /// and <see cref="Monoids.Size"/> as the monoid.
    /// </summary>
    /// <typeparam name="T">The type of the leaves.</typeparam>
    [Serializable]
    internal abstract class FingerTreeSized<T> : IEquatable<FingerTreeSized<T>>,
                                                  IDeque<T, FingerTreeSized<T>>,
                                                  IMeasured<int>, IFoldable<T>,
                                                  ICatenable<FingerTreeSized<T>> where T : IMeasured<int> {
        /// <summary>
        /// Gets the measure of the tree.
        /// </summary>
        /// <value>The measure.</value>
        public abstract int Measure { get; } // Measure

        private static readonly Empty _emptyInstance = new Empty();

        internal static Empty EmptyInstance {
            get {
                return _emptyInstance;
            }
        } // EmptyInstance

        internal static FingerTreeSized<FTNode<T, int>>.Empty EmptyInstanceNested {
            get {
                return FingerTreeSized<FTNode<T, int>>.EmptyInstance;
            }
        } // EmptyInstanceNested

        private Single MakeSingle(T value) {
            return new Single(value);
        } // MakeSingle

        private Deep MakeDeep(T[] left, Func<FingerTreeSized<FTNode<T, int>>> func, T[] right, int measure) {
            return MakeDeep(left, new LazyValue<FingerTreeSized<FTNode<T, int>>>(func), right, measure);
        } // MakeDeep

        private Deep MakeDeep(T[] left, LazyValue<FingerTreeSized<FTNode<T, int>>> middle, T[] right, int measure) {
            return new Deep(left, middle, right, measure);
        } // MakeDeep

        private Deep MakeDeepForceMiddle(T[] left, LazyValue<FingerTreeSized<FTNode<T, int>>> middle, T[] right) {
            return new Deep(left, middle, right);
        } // MakeDeepForceMiddle

        private FingerTreeSized<T> DeepL(T[] left, FingerTreeSized<FTNode<T, int>> middle, T[] right) {
            Debug.Assert(left.Length <= 4);
            Debug.Assert(right.Length != 0 && right.Length <= 4);

            if (left.Length != 0)
                return MakeDeepForceMiddle(left, middle, right);

            return RotateL(middle, right);
        } // DeepL

        private FingerTreeSized<T> RotateL(FingerTreeSized<FTNode<T, int>> middle, T[] right) {
            if (middle.IsEmpty)
                return FingerTree.SizedFromArray(right);
            int measure = middle.Measure + right.SumMeasures();
            return MakeDeep(middle.Head.AsArray, () => middle.Tail, right, measure);
        } // RotateL

        private FingerTreeSized<T> DeepR(T[] left, FingerTreeSized<FTNode<T, int>> middle, T[] right) {
            Debug.Assert(left.Length != 0 && left.Length <= 4);
            Debug.Assert(right.Length <= 4);

            if (right.Length != 0)
                return MakeDeepForceMiddle(left, middle, right);

            return RotateR(left, middle);
        } // DeepR

        private FingerTreeSized<T> RotateR(T[] left, FingerTreeSized<FTNode<T, int>> middle) {
            if (middle.IsEmpty)
                return FingerTree.SizedFromArray(left);
            int measure = left.SumMeasures() + middle.Measure;
            return MakeDeep(left, () => middle.Init, middle.Last.AsArray, measure);
        }

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="IEnumerator{T}"/> object that can be used to iterate through the collection.
        /// </returns>
        public abstract IEnumerator<T> GetEnumerator();

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        } // IEnumerable.GetEnumerator

        public abstract IEnumerable<T> ReverseIterator();

        /// <summary>
        /// Gets the head of the list, provided it is not empty.
        /// </summary>
        /// <value>The head.</value>
        /// <exception cref="EmptyEnumerableException">if the list is empty.</exception>
        public abstract T Head { get; } // Head

        /// <summary>
        /// Gets the tail of the list, provided it is not empty.
        /// </summary>
        /// <value>The tail.</value>
        /// <exception cref="EmptyEnumerableException">if the list is empty.</exception>
        public abstract FingerTreeSized<T> Tail { get; } // Tail

        /// <summary>
        /// Gets the initial sublist (all elements but the last) of the list, provided it is not empty.
        /// </summary>
        /// <value>The last element.</value>
        /// <exception cref="EmptyEnumerableException">if the list is empty.</exception>
        public abstract FingerTreeSized<T> Init { get; } // Init

        /// <summary>
        /// Gets the last element of the list, provided it is not empty.
        /// </summary>
        /// <value>The last element of the list.</value>
        /// <exception cref="EmptyEnumerableException">if the list is empty.</exception>
        public abstract T Last { get; } // Last

        /// <summary>
        /// Gets a value indicating whether this list is empty.
        /// </summary>
        /// <value><c>true</c> if this list is empty; otherwise, <c>false</c>.</value>
        public abstract bool IsEmpty { get; }

        protected abstract bool IsSingle { get; }

        /// <summary>
        /// Appends the sequence of elements to the end of the tree.
        /// </summary>
        /// <param name="ts">The sequence.</param>
        public FingerTreeSized<T> AppendRange(IEnumerable<T> ts) {
            return ts.FoldLeft((tree, a) => tree | a, this);
        } // AppendRange

        /// <summary>
        /// Prepends the sequence of elements to the beginning of the tree.
        /// </summary>
        /// <param name="ts">The sequence.</param>
        public FingerTreeSized<T> PrependRange(IEnumerable<T> ts) {
            return ts.FoldRight((a, tree) => a | tree, this);
        } // PrependRange

        /// <summary>
        /// Prepends the specified element to the beginning of the list.
        /// </summary>
        /// <param name="newHead">The new head.</param>
        /// <returns>The resulting list.</returns>
        public abstract FingerTreeSized<T> Prepend(T newHead);

        /// <summary>
        /// Appends the specified element to the end of the list.
        /// </summary>
        /// <param name="newLast">The new last element.</param>
        /// <returns>The resulting list.</returns>
        public abstract FingerTreeSized<T> Append(T newLast);

        /// <summary>
        /// Concatenates the tree with another tree.
        /// </summary>
        /// <param name="otherTree">Another tree.</param>
        /// <returns>The result of concatenation.</returns>
        public abstract FingerTreeSized<T> Concat(FingerTreeSized<T> otherTree);

        protected abstract FingerTreeSized<T> App3(IEnumerable<T> middleList, FingerTreeSized<T> rightTree);

        internal abstract Split<T, FingerTreeSized<T>> SplitTreeAt(int index, int initial);

        /// <summary>
        /// Splits the list according to the specified predicate.
        /// </summary>
        /// <param name="index">The predicate.</param>
        /// <remarks>The result has the following properties.
        /// <code>
        /// var left = tree.Split(predicate).Item1;
        /// var right = tree.Split(predicate).Item2;
        /// ---------
        /// tree.SequenceEquals(left + right);
        /// left.IsEmpty() || !predicate(left.Measure);
        /// right.IsEmpty() || predicate(left.Measure + right.Head.Measure);
        /// </code>
        /// If there are several possible splits for which these properties hold,
        /// any of them may be returned.
        /// </remarks>
        public virtual Tuple<FingerTreeSized<T>, FingerTreeSized<T>> SplitAt(int index) {
            if (index >= Measure) return Pair.New(this, (FingerTreeSized<T>)EmptyInstance);
            var split = SplitTreeAt(index, 0);
            return Pair.New(split.Left, split.Middle | split.Right);
        } // Split

        public FingerTreeSized<T> Take(int index) {
            return SplitAt(index).Item1;
        }

        public FingerTreeSized<T> Skip(int index) {
            return SplitAt(index).Item2;
        }

        /// <summary>
        /// Reverses this tree.
        /// </summary>
        /// <param name="f">The function to be applied to all elements of the tree.</param>
        /// <returns>The reverse tree.</returns>
        internal abstract FingerTreeSized<T> ReverseTree(Func<T, T> f);

        /// <summary>
        /// Prepends <paramref name="item"/> to <paramref name="tree"/>.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="tree">The tree.</param>
        public static FingerTreeSized<T> operator |(T item, FingerTreeSized<T> tree) {
            return tree.Prepend(item);
        } // op_BitwiseOr

        /// <summary>
        /// Appends <paramref name="item"/> to <paramref name="tree"/>.
        /// </summary>
        /// <param name="tree">The tree.</param>
        /// <param name="item">The item.</param>
        public static FingerTreeSized<T> operator |(FingerTreeSized<T> tree, T item) {
            return tree.Append(item);
        } // op_BitwiseOr

        /// <summary>
        /// Concatenates <paramref name="tree1"/> and <paramref name="tree2"/>.
        /// </summary>
        /// <param name="tree1">The tree1.</param>
        /// <param name="tree2">The tree2.</param>
        public static FingerTreeSized<T> operator +(FingerTreeSized<T> tree1, FingerTreeSized<T> tree2) {
            return tree1.Concat(tree2);
        } // op_Addition

        /// <summary>
        /// Reduces the finger tree from the right.
        /// </summary>
        /// <typeparam name="A">The type of the accumulator.</typeparam>
        /// <param name="binOp">The binary operation.</param>
        /// <param name="initial">The initial accumulator value.</param>
        /// <returns>
        /// The final accumulator value.
        /// </returns>
        public abstract A FoldRight<A>(Func<T, A, A> binOp, A initial);

        /// <summary>
        /// Reduces the finger tree from the left.
        /// </summary>
        /// <typeparam name="A">The type of the accumulator.</typeparam>
        /// <param name="binOp">The binary operation.</param>
        /// <param name="initial">The initial accumulator value.</param>
        /// <returns>
        /// The final accumulator value.
        /// </returns>
        public abstract A FoldLeft<A>(Func<A, T, A> binOp, A initial);

        public abstract bool Equals(FingerTreeSized<T> other);

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to this
        /// instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object"/> to compare with this
        /// instance.</param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="System.Object"/> is equal to this
        /// instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            var ft = obj as FingerTreeSized<T>;
            if (ft == null) return false;
            return Equals(ft);
        } // Equals

        /// <summary>
        /// Serves as a hash function for a particular type. 
        /// </summary>
        /// <returns>
        /// A hash code for the current <see cref="T:System.Object" />.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public abstract override int GetHashCode();

        /// <summary>
        /// Determines whether two instances of <see cref="FingerTree&lt;T, V&gt;"/> are equal.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// <c>true</c> if the arguments are equal; <c>false</c> otherwise.
        /// </returns>
        public static bool operator ==(FingerTreeSized<T> left, FingerTreeSized<T> right) {
            return Equals(left, right);
        } // op_Equality

        /// <summary>
        /// Determines whether two instances of <see cref="FingerTree&lt;T, V&gt;"/> are unequal.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>
        /// <c>true</c> if the arguments are not equal; <c>false</c> otherwise.
        /// </returns>
        public static bool operator !=(FingerTreeSized<T> left, FingerTreeSized<T> right) {
            return !Equals(left, right);
        } // op_Inequality

        /// <summary>
        /// An empty <see cref="FingerTree{T,V}"/>.
        /// </summary>
        [DebuggerDisplay("Empty")]
        [Serializable]
        public sealed class Empty : FingerTreeSized<T> {
            /// <summary>
            /// Gets the measure of the tree.
            /// </summary>
            /// <value>The measure.</value>
            public override int Measure {
                get { return 0; }
            } // Measure

            /// <summary>
            /// Returns an enumerator that iterates through a collection.
            /// </summary>
            /// <returns>
            /// An <see cref="IEnumerator{T}"/> object that can be used to iterate through the collection.
            /// </returns>
            public override IEnumerator<T> GetEnumerator() {
                yield break;
            }

            public override IEnumerable<T> ReverseIterator() {
                yield break;
            }

            /// <summary>
            /// Gets the head of the list.
            /// </summary>
            /// <value>Throws <see cref="EmptyEnumerableException"/>.</value>
            /// <exception cref="EmptyEnumerableException"></exception>
            public override T Head {
                get { throw new EmptyEnumerableException(); }
            } // Head

            /// <summary>
            /// Gets the tail of the list.
            /// </summary>
            /// <value>Throws <see cref="EmptyEnumerableException"/>.</value>
            /// <exception cref="EmptyEnumerableException"></exception>
            public override FingerTreeSized<T> Tail {
                get { throw new EmptyEnumerableException(); }
            } // Tail

            /// <summary>
            /// Gets the initial sublist (all elements but the last) of the list.
            /// </summary>
            /// <value>Throws <see cref="EmptyEnumerableException"/>.</value>
            /// <exception cref="EmptyEnumerableException"></exception>
            public override FingerTreeSized<T> Init {
                get { throw new EmptyEnumerableException(); }
            } // Init

            /// <summary>
            /// Gets the last element of the list.
            /// </summary>
            /// <value>Throws <see cref="EmptyEnumerableException"/>.</value>
            /// <exception cref="EmptyEnumerableException"></exception>
            public override T Last {
                get { throw new EmptyEnumerableException(); }
            } // Last

            /// <summary>
            /// Gets a value indicating whether this list is empty.
            /// </summary>
            /// <value><c>true</c>.</value>
            public override bool IsEmpty {
                get { return true; }
            }

            protected override bool IsSingle {
                get { return false; }
            }

            /// <summary>
            /// Prepends the specified element to the beginning of the list.
            /// </summary>
            /// <param name="newHead">The new head.</param>
            /// <returns>The resulting list.</returns>
            public override FingerTreeSized<T> Prepend(T newHead) {
                return MakeSingle(newHead);
            } // Prepend

            /// <summary>
            /// Appends the specified element to the end of the list.
            /// </summary>
            /// <param name="newLast">The new last element..</param>
            /// <returns>The resulting list.</returns>
            public override FingerTreeSized<T> Append(T newLast) {
                return MakeSingle(newLast);
            }

            /// <summary>
            /// Concatenates the tree with another.
            /// </summary>
            /// <param name="otherTree">Another tree.</param>
            /// <returns>The result of concatenation.</returns>
            public override FingerTreeSized<T> Concat(FingerTreeSized<T> otherTree) {
                return otherTree;
            }

            protected override FingerTreeSized<T> App3(IEnumerable<T> middleList, FingerTreeSized<T> rightTree) {
                if (rightTree.IsEmpty) return this.AppendRange(middleList);
                return rightTree.PrependRange(middleList);
            }

            /// <summary>
            /// Splits the tree.
            /// </summary>
            /// <param name="index">The predicate.</param>
            /// <param name="initial">The initial.</param>
            /// <exception cref="EmptyEnumerableException">Empty tree can't be split.</exception>
            internal override Split<T, FingerTreeSized<T>> SplitTreeAt(int index, int initial) {
                throw new EmptyEnumerableException("Empty tree can't be split");
            } // SplitTree

            /// <summary>
            /// Splits the list according to the specified predicate. The result has the following properties.
            /// <code>
            /// var left = tree.Split(predicate).Item1;
            /// var right = tree.Split(predicate).Item2;
            /// ---------
            /// tree.SequenceEquals(left + right);
            /// left.IsEmpty() || !predicate(left.TotalMeasure);
            /// right.IsEmpty() || !predicate(left.TotalMeasure + right.Head.Measure);
            /// </code>
            /// If there are several splits, the split returned is not guaranteed to be the first one!
            /// </summary>
            /// <param name="index">The predicate.</param>
            public override Tuple<FingerTreeSized<T>, FingerTreeSized<T>> SplitAt(int index) {
                var empty = (FingerTreeSized<T>)this;
                return Pair.New(empty, empty);
            } // Split

            internal override FingerTreeSized<T> ReverseTree(Func<T, T> f) {
                return this;
            } // ReverseTree

            /// <summary>
            /// Reduces the finger tree from the right.
            /// </summary>
            /// <typeparam name="A">The type of the accumulator.</typeparam>
            /// <param name="binOp">The binary operation.</param>
            /// <param name="initial">The initial accumulator value.</param>
            /// <returns>
            /// The final accumulator value.
            /// </returns>
            public override A FoldRight<A>(Func<T, A, A> binOp, A initial) {
                return initial;
            } // FoldRight

            /// <summary>
            /// Reduces the finger tree from the left.
            /// </summary>
            /// <typeparam name="A">The type of the accumulator.</typeparam>
            /// <param name="binOp">The binary operation.</param>
            /// <param name="initial">The initial accumulator value.</param>
            /// <returns>
            /// The final accumulator value.
            /// </returns>
            public override A FoldLeft<A>(Func<A, T, A> binOp, A initial) {
                return initial;
            }

            // Invariant

            /// <summary>
            /// Indicates whether the current object is equal to another object of the same type.
            /// </summary>
            /// <returns>
            /// true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
            /// </returns>
            /// <param name="other">An object to compare with this object.</param>
            public override bool Equals(FingerTreeSized<T> other) {
                return !ReferenceEquals(null, other) && other.IsEmpty;
            } // Equals

            /// <summary>
            /// Serves as a hash function for a particular type. 
            /// </summary>
            /// <returns>
            /// A hash code for the current <see cref="T:System.Object" />.
            /// </returns>
            /// <filterpriority>2</filterpriority>
            public override int GetHashCode() {
                return 0;
            } // GetHashCode
        } // class Empty

        /// <summary>
        /// A <see cref="FingerTree{T,V}"/> with the single element <see cref="Value"/>.
        /// </summary>
        [DebuggerDisplay("Single(Value = {Value}")]
        [Serializable]
        public sealed class Single : FingerTreeSized<T> {
            /// <summary>
            /// The value of the element.
            /// </summary>
            public readonly T Value;

            internal Single(T value) {
                Value = value;
            } // Single

            // FoldLeft

            /// <summary>
            /// Gets the measure of the tree.
            /// </summary>
            /// <value>The measure.</value>
            public override int Measure {
                get { return Value.Measure; }
            } // Measure

            public override IEnumerator<T> GetEnumerator() {
                yield return Value;
            } // GetEnumerator

            public override IEnumerable<T> ReverseIterator() {
                yield return Value;
            }

            /// <summary>
            /// Gets the head of the list.
            /// </summary>
            /// <value><see cref="Value"/>.</value>
            public override T Head {
                get { return Value; }
            } // Head

            /// <summary>
            /// Gets the tail of the list.
            /// </summary>
            /// <value><see cref="FingerTreeSized{T}.EmptyInstance"/>.</value>
            public override FingerTreeSized<T> Tail {
                get { return EmptyInstance; }
            } // Tail

            /// <summary>
            /// Gets the initial sublist (all elements but the last) of the list.
            /// </summary>
            /// <value><see cref="FingerTreeSized{T}.EmptyInstance"/>.</value>
            public override FingerTreeSized<T> Init {
                get { return EmptyInstance; }
            } // Init

            /// <summary>
            /// Gets the last element of the list.
            /// </summary>
            /// <value><see cref="Value"/>.</value>
            public override T Last {
                get { return Value; }
            } // Last

            /// <summary>
            /// Gets a value indicating whether this list is empty.
            /// </summary>
            /// <value><c>false</c>.</value>
            public override bool IsEmpty {
                get { return false; }
            }

            protected override bool IsSingle {
                get { return true; }
            }

            /// <summary>
            /// Prepends the specified element to the beginning of the list.
            /// </summary>
            /// <param name="newHead">The new head.</param>
            /// <returns>The resulting list.</returns>
            public override FingerTreeSized<T> Prepend(T newHead) {
                return MakeDeep(
                    new[] { newHead }, EmptyInstanceNested, new[] { Value },
                    newHead.Measure + Value.Measure);
            } // Prepend

            /// <summary>
            /// Appends the specified element to the end of the list.
            /// </summary>
            /// <param name="newLast">The new last element.</param>
            /// <returns>The resulting list.</returns>
            public override FingerTreeSized<T> Append(T newLast) {
                return MakeDeep(
                    new[] { Value }, EmptyInstanceNested, new[] { newLast },
                    Value.Measure + newLast.Measure);
            }

            /// <summary>
            /// Concatenates the tree with another.
            /// </summary>
            /// <param name="otherTree">Another tree.</param>
            /// <returns>The result of concatenation.</returns>
            public override FingerTreeSized<T> Concat(FingerTreeSized<T> otherTree) {
                return Value | otherTree;
            }

            protected override FingerTreeSized<T> App3(IEnumerable<T> middleList, FingerTreeSized<T> rightTree) {
                if (rightTree.IsEmpty) return this.AppendRange(middleList);
                if (rightTree.IsSingle)
                    return this.AppendRange(middleList) | rightTree.Head;
                return Value | rightTree.PrependRange(middleList);
            }

            internal override Split<T, FingerTreeSized<T>> SplitTreeAt(int index, int initial) {
                return new Split<T, FingerTreeSized<T>>(EmptyInstance, Value, EmptyInstance);
            } // SplitTree

            internal override FingerTreeSized<T> ReverseTree(Func<T, T> f) {
                var newValue = f(Value);
                return newValue.Equals(Value) ? this : MakeSingle(newValue);
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
            public override A FoldRight<A>(Func<T, A, A> binOp, A initial) {
                return binOp(Value, initial);
            } // FoldRight

            /// <summary>
            /// Reduces the finger tree from the left.
            /// </summary>
            /// <typeparam name="A">The type of the accumulator.</typeparam>
            /// <param name="binOp">The binary operation.</param>
            /// <param name="initial">The initial accumulator value.</param>
            /// <returns>
            /// The final accumulator value.
            /// </returns>
            public override A FoldLeft<A>(Func<A, T, A> binOp, A initial) {
                return binOp(initial, Value);
            }

            /// <summary>
            /// Indicates whether the current object is equal to another object of the same type.
            /// </summary>
            /// <returns>
            /// true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
            /// </returns>
            /// <param name="other">An object to compare with this object.</param>
            public override bool Equals(FingerTreeSized<T> other) {
                if (ReferenceEquals(null, other)) return false;
                if (ReferenceEquals(this, other)) return true;
                if (!other.IsSingle)
                    return false;
                return Equals(other.Head, Value);
            } // Equals

            /// <summary>
            /// Serves as a hash function for a particular type. 
            /// </summary>
            /// <returns>
            /// A hash code for the current <see cref="T:System.Object" />.
            /// </returns>
            /// <filterpriority>2</filterpriority>
            public override int GetHashCode() {
                return Value.GetHashCode();
            } // GetHashCode
        } // class Single

        /// <summary>
        /// A <see cref="FingerTree{T,V}"/> with more than one element.
        /// </summary>
        [Serializable]
        public sealed class Deep : FingerTreeSized<T> {
            private readonly int _measure;

            /// <summary>
            /// Gets the measure of the tree.
            /// </summary>
            /// <value>The measure.</value>
            public override int Measure {
                get {
                    return _measure;
                }
            }

            private readonly T[] _left;
            private readonly T[] _right;
            private readonly LazyValue<FingerTreeSized<FTNode<T, int>>> _middleLazy;

            private FingerTreeSized<FTNode<T, int>> Middle {
                get {
                    return _middleLazy.Value;
                }
            }

            internal Deep(T[] left, LazyValue<FingerTreeSized<FTNode<T, int>>> middleLazy, T[] right) {
                _left = left;
                _right = right;
                _middleLazy = middleLazy;
                int measure = _left.SumMeasures() + Middle.Measure;
                measure = measure + _right.SumMeasures();
                _measure = measure;
                Debug.Assert(_left != null && _left.Length > 0 && _left.Length <= 4);
                Debug.Assert(_right != null && _right.Length > 0 && _right.Length <= 4);
            } // Deep

            internal Deep(T[] left, LazyValue<FingerTreeSized<FTNode<T, int>>> middleLazy, T[] right,
                          int measure) {
                _left = left;
                _right = right;
                _middleLazy = middleLazy;
                _measure = measure;
                Debug.Assert(_left != null && _left.Length > 0 && _left.Length <= 4);
                Debug.Assert(_right != null && _right.Length > 0 && _right.Length <= 4);
            } // Deep

            /// <summary>
            /// Returns an enumerator that iterates through a collection.
            /// </summary>
            /// <returns>
            /// An <see cref="IEnumerator{T}"/> object that can be used to iterate through the collection.
            /// </returns>
            public override IEnumerator<T> GetEnumerator() {
                foreach (var t in _left)
                    yield return t;
                // CompilerBug: doesn't compile without AsEnumerable!
                foreach (var node in Middle.AsEnumerable()) {
                    foreach (var t in node)
                        yield return t;
                }
                foreach (var t in _right)
                    yield return t;
            } // GetEnumerator

            public override IEnumerable<T> ReverseIterator() {
                foreach (var t in _right.ReverseIterator())
                    yield return t;
                foreach (var node in Middle.ReverseIterator()) {
                    foreach (var t in node.AsArray.ReverseIterator())
                        yield return t;
                }
                foreach (var t in _left)
                    yield return t;
            }

            /// <summary>
            /// Gets the head of the list, provided it is not empty.
            /// </summary>
            /// <value>The head.</value>
            /// <exception cref="EmptyEnumerableException">if the list is empty.</exception>
            public override T Head {
                get { return _left[0]; }
            } // Head

            /// <summary>
            /// Gets the tail of the list, provided it is not empty.
            /// </summary>
            /// <value>The tail.</value>
            /// <exception cref="EmptyEnumerableException">if the list is empty.</exception>
            public override FingerTreeSized<T> Tail {
                get {
                    int newMeasure = Measure - _left[0].Measure;
                    if (_left.Length == 1)
                        return RotateL(Middle, _right);
                    return MakeDeepForceMiddle(_left.CopyNoChecks(1), _middleLazy, _right);
                }
            } // Tail

            /// <summary>
            /// Gets the initial sublist (all elements but the last) of the list, provided it is not empty.
            /// </summary>
            /// <value>The last element.</value>
            /// <exception cref="EmptyEnumerableException">if the list is empty.</exception>
            public override FingerTreeSized<T> Init {
                get {
                    var rightLast = _right.Length - 1;
                    int newMeasure = Measure - _right[rightLast].Measure;
                    if (_right.Length == 1)
                        return RotateR(_left, Middle);
                    return MakeDeepForceMiddle(_left, _middleLazy, _right.CopyNoChecks(0, rightLast));
                }
            } // Init

            /// <summary>
            /// Gets the last element of the list, provided it is not empty.
            /// </summary>
            /// <value>The last element of the list.</value>
            /// <exception cref="EmptyEnumerableException">if the list is empty.</exception>
            public override T Last {
                get { return _right[_right.Length - 1]; }
            } // Last

            /// <summary>
            /// Gets a value indicating whether this list is empty.
            /// </summary>
            /// <value><c>true</c>.</value>
            public override bool IsEmpty {
                get { return false; }
            }

            protected override bool IsSingle {
                get { return false; }
            }

            /// <summary>
            /// Prepends the specified element to the beginning of the list.
            /// </summary>
            /// <param name="newHead">The new head.</param>
            /// <returns>The resulting list.</returns>
            public override FingerTreeSized<T> Prepend(T newHead) {
                int newMeasure = newHead.Measure + Measure;

                var leftLength = _left.Length;
                if (leftLength != 4) {
                    var newLeft = new T[leftLength + 1];
                    newLeft[0] = newHead;
                    Array.Copy(_left, 0, newLeft, 1, leftLength);
                    return MakeDeep(newLeft, _middleLazy, _right, newMeasure);
                }
                return MakeDeep(
                    new[] { newHead, _left[0] },
                    new FTNode<T, int>(_left[1].Measure + _left[2].Measure + _left[3].Measure, _left[1], _left[2], _left[3]) | Middle,
                    _right,
                    newMeasure);
            } // Prepend

            /// <summary>
            /// Appends the specified element to the end of the list.
            /// </summary>
            /// <param name="newLast">The new last element..</param>
            /// <returns>The resulting list.</returns>
            public override FingerTreeSized<T> Append(T newLast) {
                int newMeasure = Measure + newLast.Measure;

                var rightLength = _right.Length;
                if (rightLength != 4) {
                    var newRight = new T[rightLength + 1];
                    Array.Copy(_right, 0, newRight, 0, rightLength);
                    newRight[rightLength] = newLast;
                    return MakeDeep(_left, Middle, newRight, newMeasure);
                }
                return MakeDeep(
                    _left,
                    Middle | new FTNode<T, int>(_right[0].Measure + _right[1].Measure + _right[2].Measure, _right[0], _right[1], _right[2]),
                    new[] { _right[3], newLast },
                    newMeasure);
            }

            /// <summary>
            /// Concatenates the tree with another.
            /// </summary>
            /// <param name="otherTree">Another tree.</param>
            /// <returns>The result of concatenation.</returns>
            public override FingerTreeSized<T> Concat(FingerTreeSized<T> otherTree) {
                if (otherTree.IsEmpty)
                    return this;
                if (otherTree.IsSingle)
                    return this | otherTree.Head;
                return App3(Enumerable.Empty<T>(), otherTree);
            }

            protected override FingerTreeSized<T> App3(IEnumerable<T> middleList, FingerTreeSized<T> rightTree) {
                if (rightTree.IsEmpty) return this.AppendRange(middleList);
                if (rightTree.IsSingle) return this.AppendRange(middleList) | rightTree.Head;
                // ReSharper disable PossibleNullReferenceException
                var rightDeep = rightTree as Deep;
                int newMeasure = Measure + middleList.SumMeasures();
                newMeasure = newMeasure + rightDeep.Measure;
                return MakeDeep(
                    _left,
                    () => Middle.App3(
                              Nodes(_right.Concat(middleList).Concat(rightDeep._left)),
                              rightDeep.Middle),
                    rightDeep._right,
                    newMeasure);
                // ReSharper restore PossibleNullReferenceException
            }

            private IEnumerable<FTNode<T, int>> Nodes(IEnumerable<T> elements) {
                Debug.Assert(elements.Count() >= 2);
                var buffer = new Queue<T>(5);
                foreach (var t in elements) {
                    buffer.Enqueue(t);
                    if (buffer.Count == 5) {
                        var t1 = buffer.Dequeue();
                        var t2 = buffer.Dequeue();
                        var t3 = buffer.Dequeue();
                        yield return new FTNode<T, int>(
                            t1.Measure + t2.Measure + t3.Measure, t1, t2, t3);
                    }
                } // foreach
                switch (buffer.Count) {
                    case 2:
                        var t1 = buffer.Dequeue();
                        var t2 = buffer.Dequeue();
                        yield return new FTNode<T, int>(
                            t1.Measure + t2.Measure, t1, t2);
                        break;
                    case 3:
                        t1 = buffer.Dequeue();
                        t2 = buffer.Dequeue();
                        var t3 = buffer.Dequeue();
                        yield return new FTNode<T, int>(
                            t1.Measure + t2.Measure + t3.Measure, t1, t2, t3);
                        break;
                    case 4:
                        t1 = buffer.Dequeue();
                        t2 = buffer.Dequeue();
                        yield return new FTNode<T, int>(
                            t1.Measure + t2.Measure, t1, t2);
                        t3 = buffer.Dequeue();
                        var t4 = buffer.Dequeue();
                        yield return new FTNode<T, int>(
                            t3.Measure + t4.Measure, t3, t4);
                        break;
                } // switch
            }

            internal override Split<T, FingerTreeSized<T>> SplitTreeAt(int index, int initial) {
                int totalLeft = initial + _left.SumMeasures();
                // is split on the left?
                if (index < totalLeft) {
                    var splitLeft = _left.SplitArrayAt(index, initial);
                    return new Split<T, FingerTreeSized<T>>(
                        FingerTree.SizedFromArray(splitLeft.Left),
                        splitLeft.Middle,
                        DeepL(splitLeft.Right, Middle, _right));
                } // if

                int totalMiddle = totalLeft + Middle.Measure;
                // is split in the middle?
                if (index < totalMiddle) {
                    var splitMiddle = Middle.SplitTreeAt(index, totalLeft);
                    int totalLeftAndMiddleLeft = totalLeft + splitMiddle.Left.Measure;
                    var splitMiddleMiddle = splitMiddle.Middle.AsArray.
                        SplitArrayAt(index, totalLeftAndMiddleLeft);
                    return new Split<T, FingerTreeSized<T>>(
                        DeepR(_left, splitMiddle.Left, splitMiddleMiddle.Left),
                        splitMiddleMiddle.Middle,
                        DeepL(splitMiddleMiddle.Right, splitMiddle.Right, _right));
                } // if

                // it must be on the right
                var splitRight = _right.SplitArrayAt(index, totalMiddle);
                return new Split<T, FingerTreeSized<T>>(
                    DeepR(_left, Middle, splitRight.Left),
                    splitRight.Middle,
                    FingerTree.SizedFromArray(splitRight.Right));
            } // SplitTree

            internal override FingerTreeSized<T> ReverseTree(Func<T, T> f) {
                var newLeft = _right.MapReverse(f);
                var newRight = _left.MapReverse(f);
                return MakeDeep(
                    newLeft,
                    () => Middle.ReverseTree(node => node.Reverse(f)),
                    newRight,
                    Measure);
            } // ReverseTree

            /// <summary>
            /// Reduces the finger tree from the right.
            /// </summary>
            /// <typeparam name="A">The type of the accumulator.</typeparam>
            /// <param name="binOp">The binary operation.</param>
            /// <param name="initial">The initial accumulator value.</param>
            /// <returns>
            /// The final accumulator value.
            /// </returns>
            public override A FoldRight<A>(Func<T, A, A> binOp, A initial) {
                Func<FTNode<T, int>, A, A> binOp1 = (n, a) => n.FoldRight(binOp, a);
                return Enumerables.FoldRight(_left, binOp, Middle.FoldRight(binOp1, Enumerables.FoldRight(_right, binOp, initial)));
            } // FoldRight

            /// <summary>
            /// Reduces the finger tree from the left.
            /// </summary>
            /// <typeparam name="A">The type of the accumulator.</typeparam>
            /// <param name="binOp">The binary operation.</param>
            /// <param name="initial">The initial accumulator value.</param>
            /// <returns>
            /// The final accumulator value.
            /// </returns>
            public override A FoldLeft<A>(Func<A, T, A> binOp, A initial) {
                Func<A, FTNode<T, int>, A> binOp1 = (a, n) => n.FoldLeft(binOp, a);
                return _right.FoldLeft(
                    binOp, Middle.FoldLeft(binOp1, _left.FoldLeft(binOp, initial)));
            }

            /// <summary>
            /// Indicates whether the current object is equal to another object of the same type.
            /// </summary>
            /// <returns>
            /// true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
            /// </returns>
            /// <param name="other">An object to compare with this object.</param>
            public override bool Equals(FingerTreeSized<T> other) {
                if (ReferenceEquals(null, other)) return false;
                if (ReferenceEquals(this, other)) return true;
                if (other.IsEmpty || other.IsSingle) return false;
                // ReSharper disable PossibleNullReferenceException
                // other is Deep
                var otherDeep = other as Deep;
                return Arrays.ContentEquals(otherDeep._left, _left) &&
                       Arrays.ContentEquals(otherDeep._right, _right) &&
                       (Equals(otherDeep._middleLazy, _middleLazy) || otherDeep.Middle.Equals(Middle));
                // ReSharper restore PossibleNullReferenceException
            } // Equals

            /// <summary>
            /// Serves as a hash function for a particular type. 
            /// </summary>
            /// <returns>
            /// A hash code for the current <see cref="T:System.Object" />.
            /// </returns>
            /// <filterpriority>2</filterpriority>
            public override int GetHashCode() {
                unchecked {
                    int result = _measure.GetHashCode();
                    result = (result * 397) ^ _left.GetHashCode();
                    result = (result * 397) ^ _right.GetHashCode();
                    result = (result * 397) ^ _middleLazy.GetHashCode();
                    return result;
                }
            } // GetHashCode
        } // class Deep
    }
}