using System;

namespace FP.Collections.Immutable {
    /// <summary>
    /// A node in the middle section of a deep finger tree.
    /// </summary>
    /// <typeparam name="T">Type of the elements in the node.</typeparam>
    /// <typeparam name="V">Type of the weight monoid.</typeparam>
    internal abstract class Node<T, V> : IMeasured<V> where T : IMeasured<V> {
        private Node() {}

        public abstract Func<A, A> ReduceR<A>(Func<T, A, A> binOp);
        public abstract Func<A, A> ReduceL<A>(Func<A, T, A> binOp);
        public V Measure { get; private set; }

        /// <summary>
        /// A node with two subtrees.
        /// </summary>
        internal class Node2 : Node<T, V> {
            public readonly T Item1;
            public readonly T Item2;
            public Node2(T item1, T item2, Monoid<V> monoid) {
                Item1 = item1;
                Item2 = item2;
                Measure = monoid.Plus(Item1.Measure, item2.Measure);
            }

            public override Func<A, A> ReduceR<A>(Func<T, A, A> binOp) {
                return (a => binOp(Item1, binOp(Item2, a)));
            }

            public override Func<A, A> ReduceL<A>(Func<A, T, A> binOp) {
                return (a => binOp(binOp(a, Item1), Item2));
            }
        }

        /// <summary>
        /// A node with three subtrees.
        /// </summary>
        internal class Node3 : Node<T, V> {
            public readonly T Item1;
            public readonly T Item2;
            public readonly T Item3;
            public Node3(T item1, T item2, T item3, Monoid<V> monoid) {
                Item1 = item1;
                Item2 = item2;
                Item3 = item3;
                Measure = monoid.Plus(monoid.Plus(item1.Measure, item2.Measure), item3.Measure);
            }

            public override Func<A, A> ReduceR<A>(Func<T, A, A> binOp) {
                return (a => binOp(Item1, binOp(Item2, binOp(Item3, a))));
            }

            public override Func<A, A> ReduceL<A>(Func<A, T, A> binOp) {
                return (a => binOp(binOp(binOp(a, Item1), Item2), Item3));
            }
        }
    }
}
