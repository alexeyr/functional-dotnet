using System;
using System.Collections.Generic;
using FP.HaskellNames;

namespace FP.Collections.Immutable {
    /// <summary>
    /// A finger-tree-based priority queue.
    /// </summary>
    /// <typeparam name="P">The type of priority values.</typeparam>
    /// <typeparam name="T">The type of elements.</typeparam>
    /// <remarks>Do not use the default constructor.</remarks>
    public struct PriorityQueue<P, T> : IEquatable<PriorityQueue<P, T>> {
        private Monoid<P> Monoid { get { return _ft.MeasureMonoid; } }
        private readonly FingerTree<Element, P> _ft;

        internal struct Element : IMeasured<P> {
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

        internal PriorityQueue(FingerTree<Element, P> ft) {
            _ft = ft;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PriorityQueue&lt;P, T&gt;"/> class.
        /// </summary>
        /// <param name="comparer">The comparer.</param>
        /// <param name="min">The minimal value according to <paramref name="comparer"/>.</param>
        public PriorityQueue(IComparer<P> comparer, P min) :
            this(Monoids.PriorityM(min, comparer)) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PriorityQueue&lt;P, T&gt;"/> class.
        /// </summary>
        internal PriorityQueue(Monoid<P> monoid) : this(FingerTree.Empty<Element, P>(monoid)) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PriorityQueue&lt;P, T&gt;"/> class.
        /// </summary>
        internal PriorityQueue(Monoid<P> monoid, IEnumerable<Pair<P, T>> sequence) :
            this(FingerTree.FromEnumerable(sequence.Map(pair => new Element(pair.First, pair.Second)), monoid)) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PriorityQueue&lt;P, T&gt;"/> class.
        /// </summary>
        /// <param name="min">The minimal value according to the default comparer.</param>
        public PriorityQueue(P min) : this(Comparer<P>.Default, min) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="PriorityQueue&lt;P, T&gt;"/> class.
        /// </summary>
        /// <param name="comparer">The comparer.</param>
        /// <param name="min">The minimal value according to <paramref name="comparer"/>.</param>
        /// <param name="sequence">The sequence of pairs (priority, element) placed into the queue initially.</param>
        public PriorityQueue(IEnumerable<Pair<P, T>> sequence, IComparer<P> comparer, P min) :
            this(Monoids.PriorityM(min, comparer), sequence) { }

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
            return new PriorityQueue<P, T>(_ft.Append(new Element(priority, item)));
        }

        /// <summary>
        /// Dequeues the element with the maximal priority.
        /// </summary>
        /// <returns>A triple where the first element is the priority of the dequeued element,
        /// the second is its value and the third is the original <see cref="PriorityQueue{P,T}"/> 
        /// without this element.</returns>
        /// <remarks>If several elements have the maximal priority, the first of them shall be dequeued.</remarks>
        public Triple<P, T, PriorityQueue<P, T>> Dequeue() {
            var monoid = Monoid;
            var maxPriority = MaxPriority;
            var split = _ft.SplitTree(p => Equals(monoid.Plus(p, maxPriority), p), Monoid.Zero);
            var item = split.Middle;
            return Triple.New(item.Priority, item.Value, new PriorityQueue<P, T>(split.Left.Concat(split.Right)));
        }

        /// <summary>
        /// Returns the element with the maximal priority without dequeuing it.
        /// </summary>
        /// <returns>A triple where the first element is the priority of the dequeued element,
        /// the second is its value and the third is the original <see cref="PriorityQueue{P,T}"/> 
        /// without this element.</returns>
        /// <remarks>If several elements have the maximal priority, the first of them shall be returned.</remarks>
        public Pair<P, T> Peek() {
            var monoid = Monoid;
            var maxPriority = MaxPriority;
            var item = _ft.SplitTree(p => Equals(monoid.Plus(p, maxPriority), p), Monoid.Zero).Middle;
            return Pair.New(item.Priority, item.Value);
        }

        /// <summary>
        /// Merges the queue with specified other queue.
        /// </summary>
        /// <param name="otherQueue">The other queue.</param>
        /// <returns>The queue containing all elements in both queues.</returns>
        public PriorityQueue<P, T> Union(PriorityQueue<P, T> otherQueue) {
            return new PriorityQueue<P, T>(_ft.Concat(otherQueue._ft));
        }

        /// <summary>
        /// Gets the maximum priority of the elements in the queue.
        /// </summary>
        /// <value>The maximum priority.</value>
        public P MaxPriority {
            get { return _ft.Measure; }
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
        /// </returns>
        /// <param name="other">An object to compare with this object.</param>
        public bool Equals(PriorityQueue<P, T> other) {
            return Equals(other.Monoid, Monoid) && Equals(other._ft, _ft);
        }

        /// <summary>
        /// Indicates whether this instance and a specified object are equal.
        /// </summary>
        /// <returns>
        /// true if <paramref name="obj" /> and this instance are the same type and represent the same value; otherwise, false.
        /// </returns>
        /// <param name="obj">Another object to compare to. </param><filterpriority>2</filterpriority>
        public override bool Equals(object obj) {
            if (obj.GetType() != typeof (PriorityQueue<P, T>)) return false;
            return Equals((PriorityQueue<P, T>) obj);
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>
        /// A 32-bit signed integer that is the hash code for this instance.
        /// </returns>
        /// <filterpriority>2</filterpriority>
        public override int GetHashCode() {
            unchecked {
                return ((Monoid != null ? Monoid.GetHashCode() : 0)*397) ^ (_ft != null ? _ft.GetHashCode() : 0);
            }
        }

        public static bool operator ==(PriorityQueue<P, T> left, PriorityQueue<P, T> right) {
            return left.Equals(right);
        }

        public static bool operator !=(PriorityQueue<P, T> left, PriorityQueue<P, T> right) {
            return !left.Equals(right);
        }

        /// <summary>
        /// Merges <paramref name="tree1"/> and <paramref name="tree2"/>.
        /// </summary>
        public static PriorityQueue<P, T> operator +(PriorityQueue<P, T> tree1, PriorityQueue<P, T> tree2) {
            return tree1.Union(tree2);
        }
    }

    /// <summary>
    /// Utility methods for creating <see cref="PriorityQueue{P,T}"/>.
    /// </summary>
    /// <seealso cref="PriorityQueue{P,T}"/>
    public static class PriorityQueue {
        /// <summary>
        /// Creates an empty <see cref="PriorityQueue{P,T}"/>.
        /// </summary>
        /// <param name="comparer">Used to compare priorities.</param>
        /// <param name="min">The minimal value according to <paramref name="comparer"/>.</param>
        /// <remarks>If an element with priority less than <paramref name="min"/> is ever enqueued,
        /// the behaviour of the resulting queue is undefined.</remarks>
        public static PriorityQueue<P, T> Empty<P, T>(IComparer<P> comparer, P min) {
            return new PriorityQueue<P, T>(comparer, min);
        }

        /// <summary>
        /// Creates an empty <see cref="PriorityQueue{P,T}"/>.
        /// </summary>
        /// <param name="min">The minimal value according to the default comparer.</param>
        /// <remarks>If an element with priority less than <paramref name="min"/> is ever enqueued,
        /// the behaviour of the resulting queue is undefined.</remarks>
        public static PriorityQueue<P, T> Empty<P, T>(P min) where P : IComparable<P> {
            return Empty<P, T>(Comparer<P>.Default, min);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PriorityQueue{P,T}"/> class.
        /// </summary>
        /// <remarks>If an element with priority <see cref="double.NaN"/> is ever enqueued,
        /// the behaviour of the resulting queue is undefined.</remarks>
        public static PriorityQueue<double, T> Empty<T>() {
            return new PriorityQueue<double, T>(Monoids.Priority);
        }

        /// <summary>
        /// Creates a <see cref="PriorityQueue{P,T}"/> with the elements from <paramref name="sequence"/>.
        /// </summary>
        /// <param name="comparer">The comparer.</param>
        /// <param name="min">The minimal value according to <paramref name="comparer"/>.</param>
        /// <param name="sequence">The sequence of pairs (priority, element) placed into the queue initially.</param>
        /// <remarks>If an element with priority less than <paramref name="min"/> is ever enqueued,
        /// the behaviour of the resulting queue is undefined.</remarks>
        public static PriorityQueue<P, T> FromEnumerable<P, T>(IEnumerable<Pair<P, T>> sequence, IComparer<P> comparer, P min) {
            return new PriorityQueue<P, T>(sequence, comparer, min);
        }

        /// <summary>
        /// Creates a <see cref="PriorityQueue{P,T}"/> with the elements from <paramref name="sequence"/>.
        /// </summary>
        /// <param name="min">The minimal value according to the default comparer.</param>
        /// <param name="sequence">The sequence of pairs (priority, element) placed into the queue initially.</param>
        /// <remarks>If an element with priority less than <paramref name="min"/> is ever enqueued,
        /// the behaviour of the resulting queue is undefined.</remarks>
        public static PriorityQueue<P, T> FromEnumerable<P, T>(IEnumerable<Pair<P, T>> sequence, P min) where P : IComparable<P> {
            return FromEnumerable(sequence, Comparer<P>.Default, min);
        }

        /// <summary>
        /// Creates a <see cref="PriorityQueue{P,T}"/> with the elements from <paramref name="sequence"/>.
        /// </summary>
        /// <param name="sequence">The sequence of pairs (priority, element) placed into the queue initially.</param>
        /// <remarks>If an element with priority <see cref="double.NaN"/> is ever enqueued,
        /// the behaviour of the resulting queue is undefined.</remarks>
        public static PriorityQueue<double, T> FromEnumerable<T>(IEnumerable<Pair<double, T>> sequence) {
            return new PriorityQueue<double, T>(Monoids.Priority, sequence);
        }

        /// <summary>
        /// Creates a <see cref="PriorityQueue{P,T}"/> with a single element.
        /// </summary>
        /// <param name="priority">The priority of the element.</param>
        /// <param name="item">The value of the element.</param>
        /// <param name="comparer">The comparer.</param>
        /// <param name="min">The minimal value according to <paramref name="comparer"/>.</param>
        public static PriorityQueue<P, T> Singleton<P, T>(P priority, T item, IComparer<P> comparer, P min) {
            return new PriorityQueue<P, T>(FingerTree.Single(new PriorityQueue<P, T>.Element(priority, item), Monoids.PriorityM(min, comparer)));
        }

        /// <summary>
        /// Creates a <see cref="PriorityQueue{P,T}"/> with a single element.
        /// </summary>
        /// <param name="priority">The priority of the element.</param>
        /// <param name="item">The value of the element.</param>
        /// <param name="min">The minimal value according to the default comparer.</param>
        public static PriorityQueue<P, T> Singleton<P, T>(P priority, T item, P min) where P : IComparable<P> {
            return Singleton(priority, item, Comparer<P>.Default, min);
        }

        /// <summary>
        /// Creates a <see cref="PriorityQueue{P,T}"/> with a single element.
        /// </summary>
        /// <param name="priority">The priority of the element.</param>
        /// <param name="item">The value of the element.</param>
        /// <remarks>If an element with priority <see cref="double.NaN"/> is ever enqueued,
        /// the behaviour of the resulting queue is undefined.</remarks>
        public static PriorityQueue<double, T> Singleton<T>(double priority, T item) {
            return new PriorityQueue<double, T>(
                FingerTree.Single(new PriorityQueue<double, T>.Element(priority, item),
                                  Monoids.Priority));
        }
    }
}