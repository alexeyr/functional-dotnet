using System.Collections.Generic;
using FP.HaskellNames;

namespace FP.Collections.Immutable {
    /// <summary>
    /// A finger-tree-based priority queue.
    /// </summary>
    /// <typeparam name="P">The type of priority values.</typeparam>
    /// <typeparam name="T">The type of elements.</typeparam>
    public class PriorityQueue<P, T> {
        private readonly Monoid<P> _monoid;
        private readonly FingerTree<Element, P> _ft;

        private struct Element : IMeasured<P> {
            internal readonly T Value;
            internal readonly P Priority;
            public Element(P priority, T value) {
                Value = value;
                Priority = priority;
            }

            public P Measure {
                get { return Priority; }
            }
        }

        private PriorityQueue(Monoid<P> monoid, FingerTree<Element, P> ft) {
            _monoid = monoid;
            _ft = ft;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PriorityQueue&lt;P, T&gt;"/> class.
        /// </summary>
        /// <param name="comparer">The comparer.</param>
        /// <param name="min">The minimal value according to <paramref name="comparer"/>.</param>
        public PriorityQueue(IComparer<P> comparer, P min) :
            this(new Monoid<P>(min, (x, y) => comparer.Min(x, y))) {}

        /// <summary>
        /// Initializes a new instance of the <see cref="PriorityQueue&lt;P, T&gt;"/> class.
        /// </summary>
        internal PriorityQueue(Monoid<P> monoid) : this(monoid, FingerTree.Empty<Element, P>(monoid)) {}

        /// <summary>
        /// Initializes a new instance of the <see cref="PriorityQueue&lt;P, T&gt;"/> class.
        /// </summary>
        internal PriorityQueue(Monoid<P> monoid, IEnumerable<Pair<P, T>> sequence) :
            this(monoid, FingerTree.FromEnumerable(sequence.Map(pair => new Element(pair.First, pair.Second)), monoid)) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PriorityQueue&lt;P, T&gt;"/> class.
        /// </summary>
        /// <param name="min">The minimal value according to the default comparer.</param>
        public PriorityQueue(P min) : this(Comparer<P>.Default, min) {}

        /// <summary>
        /// Initializes a new instance of the <see cref="PriorityQueue&lt;P, T&gt;"/> class.
        /// </summary>
        /// <param name="comparer">The comparer.</param>
        /// <param name="min">The minimal value according to <paramref name="comparer"/>.</param>
        /// <param name="sequence">The sequence of pairs (priority, element) placed into the queue initially.</param>
        public PriorityQueue(IEnumerable<Pair<P, T>> sequence, IComparer<P> comparer, P min) :
            this(new Monoid<P>(min, (x, y) => comparer.Min(x, y)), sequence) {}

        /// <summary>
        /// Initializes a new instance of the <see cref="PriorityQueue&lt;P, T&gt;"/> class.
        /// </summary>
        /// <param name="min">The minimal value according to the default comparer.</param>
        /// <param name="sequence">The sequence of pairs (priority, element) placed into the queue initially.</param>
        public PriorityQueue(IEnumerable<Pair<P, T>> sequence, P min) : this(sequence, Comparer<P>.Default, min) { }

        /// <summary>
        /// Adds an item to to <see cref="PriorityQueue{P,T}"/>.
        /// </summary>
        /// <param name="priority">The priority.</param>
        /// <param name="item">The item.</param>
        /// <returns>A <see cref="PriorityQueue{P,T}"/> containing all items of the original 
        /// <see cref="PriorityQueue{P,T}"/> and <paramref name="item"/>.</returns>
        public PriorityQueue<P, T> Enqueue(P priority, T item) {
            return new PriorityQueue<P, T>(_monoid, _ft.Append(new Element(priority, item)));
        }

        /// <summary>
        /// Dequeues the element with the maximal priority.
        /// </summary>
        /// <returns>A triple where the first element is the priority of the dequeued element,
        /// the second is its value and the third is the original <see cref="PriorityQueue{P,T}"/> 
        /// without this element.</returns>
        /// <remarks>If several elements have the maximal priority, the first of them shall be dequeued.</remarks>
        public Triple<P, T, PriorityQueue<P, T>> Dequeue() {
            var split = _ft.SplitTree(p => Equals(_monoid.Plus(p, MaxPriority), p), _monoid.Zero);
            var item = split.Middle;
            return Triple.New(item.Priority, item.Value, new PriorityQueue<P, T>(_monoid, split.Left.Concat(split.Right)));
        }

        /// <summary>
        /// Returns the element with the maximal priority without dequeuing it.
        /// </summary>
        /// <returns>A triple where the first element is the priority of the dequeued element,
        /// the second is its value and the third is the original <see cref="PriorityQueue{P,T}"/> 
        /// without this element.</returns>
        /// <remarks>If several elements have the maximal priority, the first of them shall be returned.</remarks>
        public Pair<P, T> Peek() {
            var item = _ft.SplitTree(p => Equals(_monoid.Plus(p, MaxPriority), p), _monoid.Zero).Middle;
            return Pair.New(item.Priority, item.Value);
        }

        /// <summary>
        /// Merges the queue with specified other queue.
        /// </summary>
        /// <param name="otherQueue">The other queue.</param>
        /// <returns>The queue containing all elements in both queues.</returns>
        public PriorityQueue<P, T> Merge(PriorityQueue<P, T> otherQueue) {
            return new PriorityQueue<P, T>(_monoid, _ft.Concat(otherQueue._ft));
        }

        /// <summary>
        /// Gets the maximum priority of the elements in the queue.
        /// </summary>
        /// <value>The maximum priority.</value>
        public P MaxPriority {
            get { return _ft.Measure; }
        }
    }

    /// <summary>
    /// Utility methods for creating <see cref="PriorityQueue{P,T}"/>.
    /// </summary>
    /// <seealso cref="PriorityQueue{P,T}"/>
    public static class PriorityQueue {
        /// <summary>
        /// Initializes a new instance of the <see cref="PriorityQueue{P,T}"/> class.
        /// </summary>
        /// <param name="min">The minimal value according to the default comparer..</param>
        public static PriorityQueue<P, T> Empty<P, T>(P min) {
            return Empty<P, T>(Comparer<P>.Default, min);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PriorityQueue{P,T}"/> class.
        /// </summary>
        /// <param name="comparer">The comparer.</param>
        /// <param name="min">The minimal value according to <paramref name="comparer"/>.</param>
        public static PriorityQueue<P, T> Empty<P, T>(IComparer<P> comparer, P min) {
            return new PriorityQueue<P, T>(comparer, min);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PriorityQueue{P,T}"/> class.
        /// </summary>
        /// <param name="min">The minimal value according to the default comparer..</param>
        /// <param name="sequence">The sequence of pairs (priority, element) placed into the queue initially.</param>
        public static PriorityQueue<P, T> FromEnumerable<P, T>(IEnumerable<Pair<P, T>> sequence, P min) {
            return FromEnumerable(sequence, Comparer<P>.Default, min);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PriorityQueue{P,T}"/> class.
        /// </summary>
        /// <param name="comparer">The comparer.</param>
        /// <param name="min">The minimal value according to <paramref name="comparer"/>.</param>
        /// <param name="sequence">The sequence of pairs (priority, element) placed into the queue initially.</param>
        public static PriorityQueue<P, T> FromEnumerable<P, T>(IEnumerable<Pair<P, T>> sequence, IComparer<P> comparer, P min) {
            return new PriorityQueue<P, T>(sequence, comparer, min);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PriorityQueue{P,T}"/> class.
        /// </summary>
        public static PriorityQueue<double, T> Empty<T>() {
            return new PriorityQueue<double, T>(Monoids.Priority);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PriorityQueue{P,T}"/> class.
        /// </summary>
        /// <param name="sequence">The sequence of pairs (priority, element) placed into the queue initially.</param>
        public static PriorityQueue<double, T> FromEnumerable<T>(IEnumerable<Pair<double, T>> sequence) {
            return new PriorityQueue<double, T>(Monoids.Priority, sequence);
        }
    }
}