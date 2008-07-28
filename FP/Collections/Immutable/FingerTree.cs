using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FP.HaskellNames;

namespace FP.Collections.Immutable {
    /// <summary>
    /// A finger-tree-based list (see ??)
    /// Usable for adding and removing elements at both ends (the deque operations), concatenation, 
    /// insertion and deletion at arbitrary points, finding an element satisfying some criterion, 
    /// and splitting the sequence into subsequences based on some property. 
    /// 
    /// I.e., this is a nearly universal functional list implementation.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="V"></typeparam>
    public abstract class FingerTree<T, V> : /* IImmutableList<T>,*/ IMeasured<V>, IEnumerable<T>
        where T : IMeasured<V> {
        /// <summary>
        /// The monoid to be used to combine the measures of values.
        /// </summary>
        public readonly Monoid<V> MeasureMonoid;

        /// <summary>
        /// Reduces the finger tree from the right.
        /// </summary>
        /// <typeparam name="A">The type of the accumulator.</typeparam>
        /// <param name="binOp">The binary operation.</param>
        /// <returns>The function from the initial accumulator value to the final one.</returns>
        public abstract Func<A, A> ReduceR<A>(Func<T, A, A> binOp);

        /// <summary>
        /// Reduces the finger tree from the left.
        /// </summary>
        /// <typeparam name="A">The type of the accumulator.</typeparam>
        /// <param name="binOp">The binary operation.</param>
        /// <returns>The function from the initial accumulator value to the final one.</returns>
        public abstract Func<A, A> ReduceL<A>(Func<A, T, A> binOp);

        /// <summary>
        /// Gets the measure of the tree.
        /// </summary>
        /// <value>The measure.</value>
        public abstract V Measure { get; }

        internal Empty _emptyInstance;
        internal FingerTree<FTNode<T, V>, V>.Empty _emptyInstanceNested;

        internal FingerTree<T, V> EmptyInstance {
            get { return _emptyInstance ?? (_emptyInstance = new Empty(MeasureMonoid)); }
        }

        internal FingerTree<FTNode<T, V>, V>.Empty EmptyInstanceNested {
            get {
                return _emptyInstanceNested ??
                       (_emptyInstanceNested =
                        new FingerTree<FTNode<T, V>, V>.Empty(MeasureMonoid));
            }
        }

        internal static FingerTree<T, V> DeepL(T[] left, FingerTree<FTNode<T, V>, V> middle,
                                                   T[] right, Monoid<V> measureMonoid) {
            if (left.Length != 0)
                return new Deep(left, middle, right, measureMonoid);
            if (middle.IsEmpty)
                return FingerTree.FromEnumerable(right, measureMonoid);
            var middleHead = middle.Head;
            var middleTail = middle.Tail;
            return new Deep(middleHead.ToArray(), middleTail, right,
                            measureMonoid);
        }

        internal static FingerTree<T, V> DeepR(T[] left, FingerTree<FTNode<T, V>, V> middle,
                                                   T[] right, Monoid<V> measureMonoid) {
            if (right.Length != 0)
                return new Deep(left, middle, right, measureMonoid);
            if (middle.IsEmpty)
                return FingerTree.FromEnumerable(right, measureMonoid);
            var middleInit = middle.Init;
            var middleLast = middle.Last;
            return new Deep(left, middleInit, middleLast.ToArray(),
                            measureMonoid);
        }

        internal static FingerTree<T, V> DeepL(T[] left,
                                                   Func<FingerTree<FTNode<T, V>, V>>
                                                       middleSuspended, T[] right,
                                                   Monoid<V> measureMonoid) {
            if (left.Length != 0)
                return new Deep(left, middleSuspended, right, measureMonoid);
            var middle = middleSuspended();
            if (middle.IsEmpty)
                return FingerTree.FromEnumerable(right, measureMonoid);
            var middleHead = middle.Head;
            var middleTail = middle.Tail;
            return new Deep(middleHead.ToArray(), middleTail, right,
                            measureMonoid);
        }

        internal static FingerTree<T, V> DeepR(T[] left,
                                                   Func<FingerTree<FTNode<T, V>, V>>
                                                       middleSuspended, T[] right,
                                                   Monoid<V> measureMonoid) {
            if (right.Length != 0)
                return new Deep(left, middleSuspended, right, measureMonoid);
            var middle = middleSuspended();
            if (middle.IsEmpty)
                return FingerTree.FromEnumerable(right, measureMonoid);
            var middleInit = middle.Init;
            var middleLast = middle.Last;
            return new Deep(left, middleInit, middleLast.ToArray(),
                            measureMonoid);
        }

        internal FingerTree(Monoid<V> measureMonoid) {
            MeasureMonoid = measureMonoid;
            _emptyInstance = null;
            _emptyInstanceNested = null;
        }

        internal FingerTree(Monoid<V> measureMonoid, Empty emptyInstance) {
            MeasureMonoid = measureMonoid;
            _emptyInstance = emptyInstance;
            _emptyInstanceNested = null;
        }

        /// <summary>
        /// An empty <see cref="FingerTree{T,V}"/>.
        /// </summary>
        public class Empty : FingerTree<T, V>, IEquatable<Empty> {
            internal Empty(Monoid<V> measureMonoid) : base(measureMonoid) { }

            /// <summary>
            /// Reduces the finger tree from the right.
            /// </summary>
            /// <typeparam name="A">The type of the accumulator.</typeparam>
            /// <param name="binOp">The binary operation.</param>
            /// <returns>The function from the initial accumulator value to the final one.</returns>
            public override Func<A, A> ReduceR<A>(Func<T, A, A> binOp) {
                return Functions.Id<A>();
            }

            /// <summary>
            /// Reduces the finger tree from the left.
            /// </summary>
            /// <typeparam name="A">The type of the accumulator.</typeparam>
            /// <param name="binOp">The binary operation.</param>
            /// <returns>The function from the initial accumulator value to the final one.</returns>
            public override Func<A, A> ReduceL<A>(Func<A, T, A> binOp) {
                return Functions.Id<A>();
            }

            /// <summary>
            /// Gets the measure of the tree.
            /// </summary>
            /// <value>The measure.</value>
            public override V Measure {
                get { return MeasureMonoid.Zero; }
            }

            protected override bool IsSingle {
                get { return false; }
            }

            protected override FingerTree<T, V> App3(IEnumerable<T> middleList,
                                                         FingerTree<T, V> rightTree) {
                Func<T, FingerTree<T, V>, FingerTree<T, V>> prepend =
                    (a, tree) => tree.Prepend(a);
                return middleList.ReduceR(prepend)(rightTree);
            }

            /// <summary>
            /// Prepends the specified element to the beginning of the list.
            /// </summary>
            /// <param name="newHead">The new head.</param>
            /// <returns>The resulting list.</returns>
            public override FingerTree<T, V> Prepend(T newHead) {
                return new Single(newHead, MeasureMonoid);
            }

            /// <summary>
            /// Appends the specified element to the end of the list.
            /// </summary>
            /// <param name="newLast">The new last element..</param>
            /// <returns>The resulting list.</returns>
            public override FingerTree<T, V> Append(T newLast) {
                return new Single(newLast, MeasureMonoid);
            }

            /// <summary>
            /// Returns an enumerator that iterates through a collection.
            /// <returns>
            /// An <see cref="IEnumerator{T}"/> object that can be used to iterate through the collection.
            /// </returns>
            public override IEnumerator<T> GetEnumerator() {
                yield break;
            }

            /// <summary>
            /// Gets the head of the list.
            /// </summary>
            /// <value>Throws <see cref="EmptySequenceException"/>.</value>
            /// <exception cref="EmptySequenceException"></exception>
            public override T Head {
                get { throw new EmptySequenceException(); }
            }

            /// <summary>
            /// Gets the tail of the list.
            /// </summary>
            /// <value>Throws <see cref="EmptySequenceException"/>.</value>
            /// <exception cref="EmptySequenceException"></exception>
            public override FingerTree<T, V> Tail {
                get { throw new EmptySequenceException(); }
            }

            /// <summary>
            /// Gets the initial sublist (all elements but the last) of the list.
            /// </summary>
            /// <value>Throws <see cref="EmptySequenceException"/>.</value>
            /// <exception cref="EmptySequenceException"></exception>
            public override FingerTree<T, V> Init {
                get { throw new EmptySequenceException(); }
            }

            /// <summary>
            /// Gets the last element of the list.
            /// </summary>
            /// <value>Throws <see cref="EmptySequenceException"/>.</value>
            /// <exception cref="EmptySequenceException"></exception>
            public override T Last {
                get { throw new EmptySequenceException(); }
            }

            /// <summary>
            /// Gets a value indicating whether this list is empty.
            /// </summary>
            /// <value><c>true</c>.</value>
            public override bool IsEmpty {
                get { return true; }
            }

            /// <summary>
            /// Concatenates the list with another.
            /// </summary>
            /// <param name="list">Another tree.</param>
            /// <returns>The result of concatenation.</returns>
            public override FingerTree<T, V> Concat(FingerTree<T, V> list) {
                return list;
            }

            internal override Split<T, FingerTree<T, V>> SplitTree(Func<V, bool> predicate,
                                                                       V initial) {
                throw new EmptySequenceException("Empty tree can't be split");
            }

            /// <summary>
            /// Splits the list according to the specified predicate. The result has the following properties.
            /// <code>
            /// var left = tree.Split(predicate).First;
            /// var right = tree.Split(predicate).Second;
            /// ---------
            /// tree.SequenceEquals(left.Concat(right);
            /// left.IsEmpty() || !predicate(left.TotalMeasure);
            /// right.IsEmpty() || !predicate(left.TotalMeasure + right.Head.Measure);
            /// </code>
            /// If there are several splits, the split returned is not guaranteed to be the first one!
            /// </summary>
            /// <param name="predicate">The predicate.</param>
            /// <returns></returns>
            public override Pair<FingerTree<T, V>, FingerTree<T, V>> Split(
                Func<V, bool> predicate) {
                return Pair.New(EmptyInstance, EmptyInstance);
            }

            /// <summary>
            /// Indicates whether the current object is equal to another object of the same type.
            /// </summary>
            /// <returns>
            /// true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
            /// </returns>
            /// <param name="other">An object to compare with this object.</param>
            public bool Equals(Empty obj) {
                return !ReferenceEquals(null, obj);
            }

            /// <summary>
            /// Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />.
            /// </summary>
            /// <returns>
            /// true if the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />; otherwise, false.
            /// </returns>
            /// <param name="obj">The <see cref="T:System.Object" /> to compare with the current <see cref="T:System.Object" />. </param>
            /// <exception cref="T:System.NullReferenceException">The <paramref name="obj" /> parameter is null.</exception><filterpriority>2</filterpriority>
            public override bool Equals(object obj) {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != typeof(Empty)) return false;
                return Equals((Empty)obj);
            }

            /// <summary>
            /// Serves as a hash function for a particular type. 
            /// </summary>
            /// <returns>
            /// A hash code for the current <see cref="T:System.Object" />.
            /// </returns>
            /// <filterpriority>2</filterpriority>
            public override int GetHashCode() {
                return 0;
            }

            /// <summary>
            /// Implements the operator ==.
            /// </summary>
            /// <param name="left">The left.</param>
            /// <param name="right">The right.</param>
            /// <returns>The result of the operator.</returns>
            public static bool operator ==(Empty left, Empty right) {
                return Equals(left, right);
            }

            /// <summary>
            /// Implements the operator !=.
            /// </summary>
            /// <param name="left">The left.</param>
            /// <param name="right">The right.</param>
            /// <returns>The result of the operator.</returns>
            public static bool operator !=(Empty left, Empty right) {
                return !Equals(left, right);
            }
        }

        /// <summary>
        /// A <see cref="FingerTree{T,V}"/> with the single element <see cref="Value"/>.
        /// </summary>
        public class Single : FingerTree<T, V>, IEquatable<Single> {
            /// <summary>
            /// The value of the element.
            /// </summary>
            public readonly T Value;

            internal Single(T value, Monoid<V> measureMonoid)
                : base(measureMonoid) {
                Value = value;
            }

            /// <summary>
            /// Reduces the finger tree from the right.
            /// </summary>
            /// <typeparam name="A">The type of the accumulator.</typeparam>
            /// <param name="binOp">The binary operation.</param>
            /// <returns>The function from the initial accumulator value to the final one.</returns>
            public override Func<A, A> ReduceR<A>(Func<T, A, A> binOp) {
                return a => binOp(Value, a);
            }

            /// <summary>
            /// Reduces the finger tree from the left.
            /// </summary>
            /// <typeparam name="A">The type of the accumulator.</typeparam>
            /// <param name="binOp">The binary operation.</param>
            /// <returns>The function from the initial accumulator value to the final one.</returns>
            public override Func<A, A> ReduceL<A>(Func<A, T, A> binOp) {
                return a => binOp(a, Value);
            }

            /// <summary>
            /// Gets the measure of the tree.
            /// </summary>
            /// <value>The measure.</value>
            public override V Measure {
                get { return Value.Measure; }
            }

            protected override bool IsSingle {
                get { return true; }
            }

            protected override FingerTree<T, V> App3(IEnumerable<T> middleList,
                                                         FingerTree<T, V> rightTree) {
                Func<FingerTree<T, V>, T, FingerTree<T, V>> append =
                    (tree, a) => tree.Append(a);
                FingerTree<T, V> app3 = middleList.ReduceL(append)(this);
                return rightTree.IsEmpty ? app3 : app3.Prepend(Value);
            }

            /// <summary>
            /// Prepends the specified element to the beginning of the list.
            /// </summary>
            /// <param name="newHead">The new head.</param>
            /// <returns>The resulting list.</returns>
            public override FingerTree<T, V> Prepend(T newHead) {
                return new Deep(new[] { newHead }, EmptyInstanceNested, new[] { Value }, MeasureMonoid);
            }

            /// <summary>
            /// Appends the specified element to the end of the list.
            /// </summary>
            /// <param name="newLast">The new last element.</param>
            /// <returns>The resulting list.</returns>
            public override FingerTree<T, V> Append(T newLast) {
                return new Deep(new[] { Value }, EmptyInstanceNested, new[] { newLast }, MeasureMonoid);
            }

            /// <summary>
            /// Returns an enumerator that iterates through a collection.
            /// <returns>
            /// An <see cref="IEnumerator{T}"/> object that can be used to iterate through the collection.
            /// </returns>
            public override IEnumerator<T> GetEnumerator() {
                yield return Value;
            }

            /// <summary>
            /// Gets the head of the list.
            /// </summary>
            /// <value><see cref="Value"/>.</value>
            public override T Head {
                get { return Value; }
            }

            /// <summary>
            /// Gets the tail of the list.
            /// </summary>
            /// <value><see cref="FingerTree{T,V}.EmptyInstance"/>.</value>
            public override FingerTree<T, V> Tail {
                get { return EmptyInstance; }
            }

            /// <summary>
            /// Gets the initial sublist (all elements but the last) of the list.
            /// </summary>
            /// <value><see cref="FingerTree{T,V}.EmptyInstance"/>.</value>
            public override FingerTree<T, V> Init {
                get { return EmptyInstance; }
            }

            /// <summary>
            /// Gets the last element of the list.
            /// </summary>
            /// <value><see cref="Value"/>.</value>
            public override T Last {
                get { return Value; }
            }

            /// <summary>
            /// Gets a value indicating whether this list is empty.
            /// </summary>
            /// <value><c>false</c>.</value>
            public override bool IsEmpty {
                get { return false; }
            }

            /// <summary>
            /// Concatenates the list with another.
            /// </summary>
            /// <param name="list">Another tree.</param>
            /// <returns>The result of concatenation.</returns>
            public override FingerTree<T, V> Concat(FingerTree<T, V> list) {
                return list.Prepend(Value);
            }

            internal override Split<T, FingerTree<T, V>> SplitTree(Func<V, bool> predicate,
                                                                       V initial) {
                return new Split<T, FingerTree<T, V>>(EmptyInstance, Value, EmptyInstance);
            }

            /// <summary>
            /// Indicates whether the current object is equal to another object of the same type.
            /// </summary>
            /// <returns>
            /// true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
            /// </returns>
            /// <param name="other">An object to compare with this object.</param>
            public bool Equals(Single obj) {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                return Equals(obj.Value, Value);
            }

            /// <summary>
            /// Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />.
            /// </summary>
            /// <returns>
            /// true if the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />; otherwise, false.
            /// </returns>
            /// <param name="obj">The <see cref="T:System.Object" /> to compare with the current <see cref="T:System.Object" />. </param>
            /// <exception cref="T:System.NullReferenceException">The <paramref name="obj" /> parameter is null.</exception><filterpriority>2</filterpriority>
            public override bool Equals(object obj) {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != typeof(Single)) return false;
                return Equals((Single)obj);
            }

            /// <summary>
            /// Serves as a hash function for a particular type. 
            /// </summary>
            /// <returns>
            /// A hash code for the current <see cref="T:System.Object" />.
            /// </returns>
            /// <filterpriority>2</filterpriority>
            public override int GetHashCode() {
                return Value.GetHashCode();
            }

            /// <summary>
            /// Implements the operator ==.
            /// </summary>
            /// <param name="left">The left.</param>
            /// <param name="right">The right.</param>
            /// <returns>The result of the operator.</returns>
            public static bool operator ==(Single left, Single right) {
                return Equals(left, right);
            }

            /// <summary>
            /// Implements the operator !=.
            /// </summary>
            /// <param name="left">The left.</param>
            /// <param name="right">The right.</param>
            /// <returns>The result of the operator.</returns>
            public static bool operator !=(Single left, Single right) {
                return !Equals(left, right);
            }
        }

        /// <summary>
        /// A <see cref="FingerTree{T,V}"/> with more than one element.
        /// </summary>
        public class Deep : FingerTree<T, V>, IEquatable<Deep> {
            private V _measure;
            private readonly T[] _left;
            private readonly T[] _right;
            private FingerTree<FTNode<T, V>, V> _middle;
            private Func<FingerTree<FTNode<T, V>, V>> _middleSuspended;

            private FingerTree<FTNode<T, V>, V> _Middle {
                get {
                    if (_middleSuspended != null) ForceMiddle();
                    return _middle;
                }
            }

            private void ForceMiddle() {
                if (_middleSuspended != null)
                    _middle = _middleSuspended();
                _middleSuspended = null;
                _measure = _left.SumMeasures(MeasureMonoid, MeasureMonoid.Zero);
                _measure = MeasureMonoid.Plus(_measure, _middle.Measure);
                _measure = _right.SumMeasures(MeasureMonoid, _measure);
            }

            internal Deep(T[] left, FingerTree<FTNode<T, V>, V> middle, T[] right,
                          Monoid<V> measureMonoid)
                : base(measureMonoid) {
                _left = left;
                _right = right;
                _middle = middle;
                _measure = _left.SumMeasures(MeasureMonoid, MeasureMonoid.Zero);
                _measure = MeasureMonoid.Plus(_measure, _middle.Measure);
                _measure = _right.SumMeasures(MeasureMonoid, _measure);
            }

            internal Deep(T[] left, Func<FingerTree<FTNode<T, V>, V>> middleSuspended, T[] right,
                          Monoid<V> measureMonoid)
                : base(measureMonoid) {
                _left = left;
                _right = right;
                _middleSuspended = middleSuspended;
            }

            /// <summary>
            /// Reduces the finger tree from the right.
            /// </summary>
            /// <typeparam name="A">The type of the accumulator.</typeparam>
            /// <param name="binOp">The binary operation.</param>
            /// <returns>The function from the initial accumulator value to the final one.</returns>
            public override Func<A, A> ReduceR<A>(Func<T, A, A> binOp) {
                Func<FTNode<T, V>, A, A> binOp1 =
                    (n, a) => n.ReduceR(binOp)(_right.ReduceR(binOp)(a));
                return a => _left.ReduceR(binOp)(_Middle.ReduceR(binOp1)(a));
            }

            /// <summary>
            /// Reduces the finger tree from the left.
            /// </summary>
            /// <typeparam name="A">The type of the accumulator.</typeparam>
            /// <param name="binOp">The binary operation.</param>
            /// <returns>The function from the initial accumulator value to the final one.</returns>
            public override Func<A, A> ReduceL<A>(Func<A, T, A> binOp) {
                Func<A, FTNode<T, V>, A> binOp1 = (a, n) => _right.ReduceL(binOp)(a);
                return a => _Middle.ReduceL(binOp1)(_left.ReduceL(binOp)(a));
            }

            /// <summary>
            /// Gets the measure of the tree.
            /// </summary>
            /// <value>The measure.</value>
            public override V Measure {
                get {
                    if (_middleSuspended != null) ForceMiddle();
                    return _measure;
                }
            }

            protected override bool IsSingle {
                get { return false; }
            }

            protected override FingerTree<T, V> App3(IEnumerable<T> middleList,
                                                         FingerTree<T, V> rightTree) {
                if (rightTree.IsEmpty) {
                    Func<FingerTree<T, V>, T, FingerTree<T, V>> append =
                        (tree, a) => tree.Append(a);
                    return middleList.ReduceL(append)(this);
                }
                if (rightTree.IsSingle) {
                    Func<T, FingerTree<T, V>, FingerTree<T, V>> prepend =
                        (a, tree) => tree.Prepend(a);
                    return middleList.ReduceR(prepend)(this).Append(rightTree.Head);
                }
                var rightDeep = rightTree as Deep;
                return new Deep(this._left,
                                this._Middle.App3(
                                    Nodes(this._right.Concat(middleList).Concat(rightDeep._left)),
                                    rightDeep._Middle), rightDeep._right,
                                MeasureMonoid);
            }

            private IEnumerable<FTNode<T, V>> Nodes(IEnumerable<T> elements) {
                var buffer = new Queue<T>(5);
                foreach (var t in elements) {
                    buffer.Enqueue(t);
                    if (buffer.Count == 5) {
                        yield return
                            new FTNode<T, V>.Node3(buffer.Dequeue(), buffer.Dequeue(),
                                                   buffer.Dequeue(),
                                                   MeasureMonoid);
                    }
                    switch (buffer.Count) {
                        case 2:
                            yield return
                                new FTNode<T, V>.Node2(buffer.Dequeue(), buffer.Dequeue(),
                                                       MeasureMonoid);
                            break;
                        case 3:
                            yield return
                                new FTNode<T, V>.Node3(buffer.Dequeue(), buffer.Dequeue(),
                                                       buffer.Dequeue(), MeasureMonoid);
                            break;
                        case 4:
                            yield return
                                new FTNode<T, V>.Node2(buffer.Dequeue(), buffer.Dequeue(),
                                                       MeasureMonoid);
                            yield return
                                new FTNode<T, V>.Node2(buffer.Dequeue(), buffer.Dequeue(),
                                                       MeasureMonoid);
                            break;
                    }
                }
            }

            /// <summary>
            /// Prepends the specified element to the beginning of the list.
            /// </summary>
            /// <param name="newHead">The new head.</param>
            /// <returns>The resulting list.</returns>
            public override FingerTree<T, V> Prepend(T newHead) {
                if (_left.Length != 4) {
                    T[] newLeft = newHead.Cons(_left).ToArray();
                    //return (_middleSuspended == null) ?
                    //    new Deep(newLeft, _middle, _right, MeasureMonoid) :
                    //    new Deep(newLeft, _middleSuspended, _right, MeasureMonoid);
                    return new Deep(newLeft, _Middle, _right, MeasureMonoid);
                    //see page 7 of the paper
                }
                return new Deep(new[] { newHead, _left[0] },
                                _Middle.Prepend(new FTNode<T, V>.Node3(_left[1], _left[2], _left[3],
                                                                       MeasureMonoid)),
                                _right, MeasureMonoid);
            }

            /// <summary>
            /// Appends the specified element to the end of the list.
            /// </summary>
            /// <param name="newLast">The new last element..</param>
            /// <returns>The resulting list.</returns>
            public override FingerTree<T, V> Append(T newLast) {
                if (_right.Length != 4) {
                    T[] newRight = _right.Append(newLast).ToArray();
                    //return (_middleSuspended == null) ?
                    //    new Deep(newLeft, _middle, _right, MeasureMonoid) :
                    //    new Deep(newLeft, _middleSuspended, _right, MeasureMonoid);
                    return new Deep(_left, _Middle, newRight, MeasureMonoid);
                    //see page 7 of the paper
                }
                return new Deep(_left,
                                _Middle.Append(new FTNode<T, V>.Node3(_right[0], _right[1],
                                                                      _right[2],
                                                                      MeasureMonoid)),
                                new[] { _right[3], newLast }, MeasureMonoid);
            }

            /// <summary>
            /// Returns an enumerator that iterates through a collection.
            /// <returns>
            /// An <see cref="IEnumerator{T}"/> object that can be used to iterate through the collection.
            /// </returns>
            public override IEnumerator<T> GetEnumerator() {
                yield return Head;
                foreach (var t in Tail) yield return t;
            }

            /// <summary>
            /// Gets the head of the list, provided it is not empty.
            /// </summary>
            /// <value>The head.</value>
            /// <exception cref="EmptySequenceException">if the list is empty.</exception>
            public override T Head {
                get { return _left[0]; }
            }

            /// <summary>
            /// Gets the tail of the list, provided it is not empty.
            /// </summary>
            /// <value>The tail.</value>
            /// <exception cref="EmptySequenceException">if the list is empty.</exception>
            public override FingerTree<T, V> Tail {
                get {
                    return (_middleSuspended == null)
                               ? DeepL(_left.Tail().ToArray(), _middle, _right, MeasureMonoid)
                               : DeepL(_left.Tail().ToArray(), _middleSuspended, _right,
                                       MeasureMonoid);
                }
            }

            /// <summary>
            /// Gets the initial sublist (all elements but the last) of the list, provided it is not empty.
            /// </summary>
            /// <value>The last element.</value>
            /// <exception cref="EmptySequenceException">if the list is empty.</exception>
            public override FingerTree<T, V> Init {
                get {
                    return (_middleSuspended == null)
                               ? DeepR(_left, _middle, _right.Init().ToArray(), MeasureMonoid)
                               : DeepR(_left, _middleSuspended, _right.Init().ToArray(),
                                       MeasureMonoid);
                }
            }

            /// <summary>
            /// Gets the last element of the list, provided it is not empty.
            /// </summary>
            /// <value>The last element of the list.</value>
            /// <exception cref="EmptySequenceException">if the list is empty.</exception>
            public override T Last {
                get { return _right[_right.Length - 1]; }
            }

            /// <summary>
            /// Gets a value indicating whether this list is empty.
            /// </summary>
            /// <value><c>true</c>.</value>
            public override bool IsEmpty {
                get { return false; }
            }

            /// <summary>
            /// Concatenates the list with another.
            /// </summary>
            /// <param name="list">Another tree.</param>
            /// <returns>The result of concatenation.</returns>
            public override FingerTree<T, V> Concat(FingerTree<T, V> list) {
                if (list.IsEmpty)
                    return this;
                if (list.IsSingle)
                    return this.Append(list.Head);
                return this.App3(Enumerable.Empty<T>(), list);
            }

            internal override Split<T, FingerTree<T, V>> SplitTree(Func<V, bool> predicate,
                                                                       V initial) {
                V totalLeft = _left.SumMeasures(MeasureMonoid, initial);
                if (predicate(totalLeft)) {
                    var splitLeft = _left.SplitArray(MeasureMonoid, predicate, initial);
                    var newRight = _middleSuspended == null
                                       ? DeepL(splitLeft.Right, _middle, _right, MeasureMonoid)
                                       : DeepL(splitLeft.Right, _middleSuspended, _right,
                                               MeasureMonoid);
                    return
                        new Split<T, FingerTree<T, V>>(
                            FingerTree.FromEnumerable(splitLeft.Left, MeasureMonoid),
                            splitLeft.Middle,
                            newRight);
                }
                V totalMiddle = MeasureMonoid.Plus(totalLeft, _Middle.Measure);
                if (predicate(totalMiddle)) {
                    var splitMiddle = _middle.SplitTree(predicate, totalLeft);
                    V totalLeftAndMiddleLeft = splitMiddle.Left.SumMeasures(MeasureMonoid, totalLeft);
                    var splitMiddleMiddle = splitMiddle.Middle.ToArray().
                        SplitArray(MeasureMonoid, predicate, totalLeftAndMiddleLeft);
                    var newLeft = DeepR(_left, splitMiddle.Left, splitMiddleMiddle.Left,
                                        MeasureMonoid);
                    var newRight = DeepL(splitMiddleMiddle.Right, splitMiddle.Right, _right,
                                         MeasureMonoid);
                    return new Split<T, FingerTree<T, V>>(newLeft, splitMiddleMiddle.Middle,
                                                              newRight);
                }
                {
                    var splitRight =
                        _right.SplitArray
                            (MeasureMonoid,
                             predicate,
                             totalMiddle);
                    var newLeft =
                        _middleSuspended ==
                        null
                            ? DeepR(
                                  _left,
                                  _middle,
                                  splitRight
                                      .
                                      Left,
                                  MeasureMonoid)
                            : DeepR(
                                  _left,
                                  _middleSuspended,
                                  splitRight
                                      .
                                      Left,
                                  MeasureMonoid);
                    return
                        new Split
                            <T,
                                FingerTree
                                    <T, V
                                        >
                                >(
                            newLeft,
                            splitRight.
                                Middle,
                            FingerTree.
                                FromEnumerable
                                (
                                splitRight
                                    .
                                    Right,
                                MeasureMonoid));
                }
            }

            /// <summary>
            /// Indicates whether the current object is equal to another object of the same type.
            /// </summary>
            /// <returns>
            /// true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
            /// </returns>
            /// <param name="other">An object to compare with this object.</param>
            public bool Equals(Deep obj) {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                return Equals(obj._measure, _measure) && Equals(obj._left, _left) &&
                       Equals(obj._right, _right) && Equals(obj._middle, _middle) &&
                       Equals(obj._middleSuspended, _middleSuspended);
            }

            /// <summary>
            /// Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />.
            /// </summary>
            /// <returns>
            /// true if the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />; otherwise, false.
            /// </returns>
            /// <param name="obj">The <see cref="T:System.Object" /> to compare with the current <see cref="T:System.Object" />. </param>
            /// <exception cref="T:System.NullReferenceException">The <paramref name="obj" /> parameter is null.</exception><filterpriority>2</filterpriority>
            public override bool Equals(object obj) {
                if (ReferenceEquals(null, obj)) return false;
                if (ReferenceEquals(this, obj)) return true;
                if (obj.GetType() != typeof(Deep)) return false;
                return Equals((Deep)obj);
            }

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
                    result = (result * 397) ^ (_left.GetHashCode());
                    result = (result * 397) ^ (_right.GetHashCode());
                    result = (result * 397) ^ (_middle != null ? _middle.GetHashCode() : 0);
                    result = (result * 397) ^
                             (_middleSuspended != null ? _middleSuspended.GetHashCode() : 0);
                    return result;
                }
            }

            /// <summary>
            /// Implements the operator ==.
            /// </summary>
            /// <param name="left">The left.</param>
            /// <param name="right">The right.</param>
            /// <returns>The result of the operator.</returns>
            public static bool operator ==(Deep left, Deep right) {
                return Equals(left, right);
            }

            /// <summary>
            /// Implements the operator !=.
            /// </summary>
            /// <param name="left">The left.</param>
            /// <param name="right">The right.</param>
            /// <returns>The result of the operator.</returns>
            public static bool operator !=(Deep left, Deep right) {
                return !Equals(left, right);
            }
        }

        protected abstract bool IsSingle { get; }

        protected abstract FingerTree<T, V> App3(IEnumerable<T> middleList,
                                                     FingerTree<T, V> rightTree);

        /// <summary>
        /// Prepends the specified element to the beginning of the list.
        /// </summary>
        /// <param name="newHead">The new head.</param>
        /// <returns>The resulting list.</returns>
        public abstract FingerTree<T, V> Prepend(T newHead);

        /// <summary>
        /// Appends the specified element to the end of the list.
        /// </summary>
        /// <param name="newLast">The new last element..</param>
        /// <returns>The resulting list.</returns>
        public abstract FingerTree<T, V> Append(T newLast);

        /// <summary>
        /// Returns an enumerator that iterates through a collection.
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
        }

        /// <summary>
        /// Gets the head of the list, provided it is not empty.
        /// </summary>
        /// <value>The head.</value>
        /// <exception cref="EmptySequenceException">if the list is empty.</exception>
        public abstract T Head { get; }

        /// <summary>
        /// Gets the tail of the list, provided it is not empty.
        /// </summary>
        /// <value>The tail.</value>
        /// <exception cref="EmptySequenceException">if the list is empty.</exception>
        public abstract FingerTree<T, V> Tail { get; }

        /// <summary>
        /// Gets the initial sublist (all elements but the last) of the list, provided it is not empty.
        /// </summary>
        /// <value>The last element.</value>
        /// <exception cref="EmptySequenceException">if the list is empty.</exception>
        public abstract FingerTree<T, V> Init { get; }

        /// <summary>
        /// Gets the last element of the list, provided it is not empty.
        /// </summary>
        /// <value>The last element of the list.</value>
        /// <exception cref="EmptySequenceException">if the list is empty.</exception>
        public abstract T Last { get; }

        /// <summary>
        /// Gets a value indicating whether this list is empty.
        /// </summary>
        /// <value><c>true</c> if this list is empty; otherwise, <c>false</c>.</value>
        public abstract bool IsEmpty { get; }

        /// <summary>
        /// Concatenates the list with another.
        /// </summary>
        /// <param name="list">Another tree.</param>
        /// <returns>The result of concatenation.</returns>
        public abstract FingerTree<T, V> Concat(FingerTree<T, V> list);

        internal abstract Split<T, FingerTree<T, V>> SplitTree(Func<V, bool> predicate,
                                                                   V initial);

        /// <summary>
        /// Splits the list according to the specified predicate. The result has the following properties.
        /// <code>
        /// var left = tree.Split(predicate).First;
        /// var right = tree.Split(predicate).Second;
        /// ---------
        /// tree.SequenceEquals(left.Concat(right);
        /// left.IsEmpty() || !predicate(left.Measure);
        /// right.IsEmpty() || predicate(left.Measure + right.Head.Measure);
        /// </code>
        /// If there are several splits, the split returned is not guaranteed to be the first one!
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns></returns>
        public virtual Pair<FingerTree<T, V>, FingerTree<T, V>> Split(
            Func<V, bool> predicate) {
            if (!predicate(Measure)) return Pair.New(this, EmptyInstance);
            var split = SplitTree(predicate, MeasureMonoid.Zero);
            return Pair.New(split.Left, split.Right.Prepend(split.Middle));
        }
    }
}