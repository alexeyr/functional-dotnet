using System.Collections.Generic;

namespace FP.Collections.Immutable {
    /// <summary>
    /// A finger-tree-based priority queue.
    /// </summary>
    /// <typeparam name="P">The type of priority values.</typeparam>
    /// <typeparam name="T">The type of elements.</typeparam>
    public class PriorityQueue<P, T> {
        private readonly Monoid<P> _monoid;
        private readonly FingerTree<Element, P> _ft;

        private class Element : IMeasured<P> {
            internal readonly T Value;
            internal readonly P Priority;
            public Element(T value, P priority) {
                Value = value;
                Priority = priority;
            }

            public P Measure {
                get { return Priority; }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PriorityQueue&lt;P, T&gt;"/> class.
        /// </summary>
        /// <param name="comparer">The comparer.</param>
        /// <param name="min">The minimal value according to <paramref name="comparer"/>.</param>
        public PriorityQueue(IComparer<P> comparer, P min) {
            _monoid = new Monoid<P>(min, (x, y) => comparer.Min(x, y));
            _ft = FingerTree.Empty<Element, P>(_monoid);
        }
    }
}