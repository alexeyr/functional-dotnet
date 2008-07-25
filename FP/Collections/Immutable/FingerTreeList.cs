using System;
using System.Collections;
using System.Collections.Generic;
using FP.Collections.Immutable.FingerTrees;

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
    public abstract class FingerTreeList<T, V> : /* IImmutableList<T>,*/ IMeasured<V>, IEnumerable where T : IMeasured<V> {
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
        /// Gets the measure of the object.
        /// </summary>
        /// <value>The measure.</value>
        public abstract V Measure { get; }

        internal Empty _emptyInstance;
        internal FingerTreeList<FTNode<T, V>, V>.Empty _emptyInstanceNested;

        internal FingerTreeList<T, V> EmptyInstance {
            get {
                return _emptyInstance ?? (_emptyInstance = new Empty(MeasureMonoid));
            }
        }

        internal FingerTreeList<FTNode<T, V>, V>.Empty EmptyInstanceNested {
            get {
                return _emptyInstanceNested ?? (_emptyInstanceNested = new FingerTreeList<FTNode<T, V>, V>.Empty(MeasureMonoid));
            }
        }

        internal FingerTreeList(Monoid<V> measureMonoid) {
            MeasureMonoid = measureMonoid;
            _emptyInstance = null;
            _emptyInstanceNested = null;
        }

        internal FingerTreeList(Monoid<V> measureMonoid, Empty emptyInstance) {
            MeasureMonoid = measureMonoid;
            _emptyInstance = emptyInstance;
            _emptyInstanceNested = null;
        }

        /// <summary>
        /// An empty <see cref="FingerTreeList{T,M}"/>.
        /// </summary>
        public class Empty : FingerTreeList<T, V> {
            internal Empty(Monoid<V> measureMonoid) : base(measureMonoid) {}
            
            public override Func<A, A> ReduceR<A>(Func<T, A, A> binOp) {
                return Functions.Id<A>();
            }

            public override Func<A, A> ReduceL<A>(Func<A, T, A> binOp) {
                return Functions.Id<A>();
            }

            public override V Measure {
                get { return MeasureMonoid.Zero; }
            }

            public override FingerTreeList<T, V> Prepend(T newHead) {
                return new Single(newHead, MeasureMonoid);
            }

            public override FingerTreeList<T, V> Append(T newLast) {
                return new Single(newLast, MeasureMonoid);
            }

            public override IEnumerator<T> GetEnumerator() {
                yield break;
            }

            /// <summary>
            /// Gets the head.
            /// </summary>
            /// <exception cref="EmptySequenceException">Thrown because the sequence is empty.</exception>
            public override T Head {
                get { throw new EmptySequenceException(); }
            }

            /// <summary>
            /// Gets the tail.
            /// </summary>
            /// <exception cref="EmptySequenceException">Thrown because the sequence is empty.</exception>
            public override FingerTreeList<T, V> Tail {
                get { throw new EmptySequenceException(); }
            }

            public override bool IsEmpty {
                get { return true; }
            }
        }

        /// <summary>
        /// A <see cref="FingerTreeList{T,M}"/> with the single element <see cref="Value"/>.
        /// </summary>
        public class Single : FingerTreeList<T, V> {
            /// <summary>
            /// The value of the element.
            /// </summary>
            public readonly T Value;
            
            internal Single(T value, Monoid<V> measureMonoid) : base(measureMonoid){
                Value = value;
            }

            public override Func<A, A> ReduceR<A>(Func<T, A, A> binOp) {
                return a => binOp(Value, a);
            }

            public override Func<A, A> ReduceL<A>(Func<A, T, A> binOp) {
                return a => binOp(a, Value);
            }

            public override V Measure {
                get { return Value.Measure;}
            }

            public override FingerTreeList<T, V> Prepend(T newHead) {
                return new Deep(new[] {newHead}, EmptyInstanceNested, new[] {Value}, MeasureMonoid);
            }

            public override FingerTreeList<T, V> Append(T newLast) {
                return new Deep(new[] { Value }, EmptyInstanceNested, new[] { newLast }, MeasureMonoid);
            }

            public override IEnumerator<T> GetEnumerator() {
                yield return Value;
            }

            public override T Head {
                get { return Value; }
            }

            public override FingerTreeList<T, V> Tail {
                get { return EmptyInstance; }
            }

            public override bool IsEmpty {
                get { return false; }
            }
        }

        /// <summary>
        /// A <see cref="FingerTreeList{T,M}"/> with more than one element.
        /// </summary>
        public class Deep : FingerTreeList<T, V>  {
            private readonly V _measure;
            private readonly T[] _left;
            private readonly T[] _right;
            private readonly FingerTreeList<FTNode<T, V>, V> _middle;

            internal Deep(T[] left, FingerTreeList<FTNode<T, V>, V> middle, T[] right, Monoid<V> measureMonoid)
                : base(measureMonoid) {
                _measure = measureMonoid.Zero;
                foreach (var t in _left)
                    _measure = measureMonoid.Plus(_measure, t.Measure);
                _measure = measureMonoid.Plus(_measure, _middle.Measure);
                foreach (var t in _right)
                    _measure = measureMonoid.Plus(_measure, t.Measure);
                if (_left.Length != 0) {
                    _left = left;
                    _right = right;
                    _middle = middle;
                }
                else {}
            }

            public override Func<A, A> ReduceR<A>(Func<T, A, A> binOp) {
                Func<FTNode<T, V>, A, A> binOp1 = (n, a) => n.ReduceR(binOp)(_right.ReduceR(binOp)(a));
                return a => _left.ReduceR(binOp)(_middle.ReduceR(binOp1)(a));
            }

            public override Func<A, A> ReduceL<A>(Func<A, T, A> binOp) {
                Func<A, FTNode<T, V>, A> binOp1 = (a, n) => _right.ReduceL(binOp)(a);
                return a => _middle.ReduceL(binOp1)(_left.ReduceL(binOp)(a));
            }

            public override V Measure {
                get { return _measure; }
            }

            public override FingerTreeList<T, V> Prepend(T newHead) {
                if (_left.Length != 4) {
                    var newLeft = new T[_left.Length + 1];
                    newLeft[0] = newHead;
                    _left.CopyTo(newLeft, 1);
                    return new Deep(newLeft, _middle, _right, MeasureMonoid);
                }
                return new Deep(new[] {newHead, _left[0]},
                                _middle.Prepend(new FTNode<T, V>.Node3(_left[1], _left[2], _left[3],
                                    MeasureMonoid)),
                                _right, MeasureMonoid);
            }

            public override FingerTreeList<T, V> Append(T newLast) {
                throw new System.NotImplementedException();
            }

            public override IEnumerator<T> GetEnumerator() {
                throw new System.NotImplementedException();
            }

            public override T Head {
                get { throw new System.NotImplementedException(); }
            }

            public override FingerTreeList<T, V> Tail {
                get { throw new System.NotImplementedException(); }
            }

            public override bool IsEmpty {
                get { throw new System.NotImplementedException(); }
            }
        }

        public abstract FingerTreeList<T, V> Prepend(T newHead);

        public abstract FingerTreeList<T, V> Append(T newLast);

        public abstract IEnumerator<T> GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        public abstract T Head { get; }

        public abstract FingerTreeList<T, V> Tail { get; }

        public abstract bool IsEmpty { get; }

    }
}