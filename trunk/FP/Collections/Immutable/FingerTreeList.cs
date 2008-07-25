using System;
using System.Collections;
using System.Collections.Generic;
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
    /// <typeparam name="M"></typeparam>
    public abstract class FingerTreeList<T, M> : /* IImmutableList<T>,*/ IMeasured<M>, IEnumerable where T : IMeasured<M> {
        /// <summary>
        /// The monoid to be used to combine the measures of values.
        /// </summary>
        public readonly Monoid<M> MeasureMonoid;
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
        public abstract M Measure { get; }

        internal Empty _emptyInstance;
        internal FingerTreeList<Node<T, M>, M>.Empty _emptyInstanceNested;

        internal FingerTreeList(Monoid<M> measureMonoid) {
            MeasureMonoid = measureMonoid;
            _emptyInstance = new Empty(measureMonoid);
            _emptyInstanceNested = new FingerTreeList<Node<T, M>, M>.Empty(measureMonoid);
        }

        /// <summary>
        /// An empty <see cref="FingerTreeList{T,M}"/>.
        /// </summary>
        internal class Empty : FingerTreeList<T, M> {
            internal Empty(Monoid<M> measureMonoid) : base(measureMonoid) {}
            public override Func<A, A> ReduceR<A>(Func<T, A, A> binOp) {
                return Functions.Id<A>();
            }

            public override Func<A, A> ReduceL<A>(Func<A, T, A> binOp) {
                return Functions.Id<A>();
            }

            public override M Measure {
                get { return MeasureMonoid.Zero; }
            }

            public override FingerTreeList<T, M> Prepend(T newHead) {
                return new Single(newHead, MeasureMonoid);
            }

            public override FingerTreeList<T, M> Append(T newLast) {
                throw new System.NotImplementedException();
            }

            public override IEnumerator<T> GetEnumerator() {
                throw new System.NotImplementedException();
            }

            public override T Head {
                get { throw new System.NotImplementedException(); }
            }

            public override FingerTreeList<T, M> Tail {
                get { throw new System.NotImplementedException(); }
            }

            public override bool IsEmpty {
                get { throw new System.NotImplementedException(); }
            }
        }

        /// <summary>
        /// A <see cref="FingerTreeList{T,M}"/> with the single element <see cref="Value"/>.
        /// </summary>
        internal class Single : FingerTreeList<T, M> {
            /// <summary>
            /// The value of the element.
            /// </summary>
            public readonly T Value;
            
            internal Single(T value, Monoid<M> measureMonoid) : base(measureMonoid){
                Value = value;
            }

            public override Func<A, A> ReduceR<A>(Func<T, A, A> binOp) {
                return a => binOp(Value, a);
            }

            public override Func<A, A> ReduceL<A>(Func<A, T, A> binOp) {
                return a => binOp(a, Value);
            }

            public override M Measure {
                get { return Value.Measure;}
            }

            public override FingerTreeList<T, M> Prepend(T newHead) {
                return new Deep(new[] {newHead}, _emptyInstanceNested, new[] {Value}, MeasureMonoid);
            }

            public override FingerTreeList<T, M> Append(T newLast) {
                throw new System.NotImplementedException();
            }

            public override IEnumerator<T> GetEnumerator() {
                throw new System.NotImplementedException();
            }

            public override T Head {
                get { throw new System.NotImplementedException(); }
            }

            public override FingerTreeList<T, M> Tail {
                get { throw new System.NotImplementedException(); }
            }

            public override bool IsEmpty {
                get { throw new System.NotImplementedException(); }
            }
        }

        /// <summary>
        /// A <see cref="FingerTreeList{T,M}"/> with more than one element.
        /// </summary>
        internal class Deep : FingerTreeList<T, M>  {
            private readonly M _measure;
            private readonly T[] _left;
            private readonly T[] _right;
            private readonly FingerTreeList<Node<T, M>, M> _middle;

            internal Deep(T[] left, FingerTreeList<Node<T, M>, M> middle, T[] right, Monoid<M> measureMonoid)
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

            private static Func<A, A> ArrayReduceR<A>(T[] array, Func<T, A, A> binOp) {
                return a => array.FoldRight(a, binOp);
            }

            private static Func<A, A> ArrayReduceL<A>(T[] array, Func<A, T, A> binOp) {
                return a => array.FoldLeft(a, binOp);
            }

            public override Func<A, A> ReduceR<A>(Func<T, A, A> binOp) {
                Func<Node<T, M>, A, A> binOp1 = (n, a) => n.ReduceR(binOp)(ArrayReduceR(_right, binOp)(a));
                return a => ArrayReduceR(_left, binOp)(_middle.ReduceR(binOp1)(a));
            }

            public override Func<A, A> ReduceL<A>(Func<A, T, A> binOp) {
                Func<A, Node<T, M>, A> binOp1 = (a, n) => ArrayReduceL(_right, binOp)(a);
                return a => _middle.ReduceL(binOp1)(ArrayReduceL(_left, binOp)(a));
            }

            public override M Measure {
                get { return _measure; }
            }

            public override FingerTreeList<T, M> Prepend(T newHead) {
                if (_left.Length != 4) {
                    var newLeft = new T[_left.Length + 1];
                    newLeft[0] = newHead;
                    _left.CopyTo(newLeft, 1);
                    return new Deep(newLeft, _middle, _right, MeasureMonoid);
                }
                return new Deep(new[] {newHead, _left[0]},
                                _middle.Prepend(new Node<T, M>.Node3(_left[1], _left[2], _left[3],
                                    MeasureMonoid)),
                                _right, MeasureMonoid);
            }

            public override FingerTreeList<T, M> Append(T newLast) {
                throw new System.NotImplementedException();
            }

            public override IEnumerator<T> GetEnumerator() {
                throw new System.NotImplementedException();
            }

            public override T Head {
                get { throw new System.NotImplementedException(); }
            }

            public override FingerTreeList<T, M> Tail {
                get { throw new System.NotImplementedException(); }
            }

            public override bool IsEmpty {
                get { throw new System.NotImplementedException(); }
            }
        }

        public abstract FingerTreeList<T, M> Prepend(T newHead);

        public abstract FingerTreeList<T, M> Append(T newLast);

        public abstract IEnumerator<T> GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        public abstract T Head { get; }

        public abstract FingerTreeList<T, M> Tail { get; }

        public abstract bool IsEmpty { get; }
    }
}