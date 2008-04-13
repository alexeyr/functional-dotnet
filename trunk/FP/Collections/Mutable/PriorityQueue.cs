using System;
using System.Collections.Generic;
using FP.Collections.Immutable;

namespace FP.Collections.Mutable {
    /// <summary>
    /// A generic priority queue.
    /// </summary>
    /// <typeparam name="P">The type of priority.</typeparam>
    /// <typeparam name="T">The type of stored data.</typeparam>
    public class PriorityQueue<P, T> : Heap<Pair<P, T>> {
        public PriorityQueue() : this(10, Comparer<P>.Default) { }
        public PriorityQueue(IComparer<P> keyComparer) : this(10, keyComparer) {}
        public PriorityQueue(int capacity) : this(capacity, Comparer<P>.Default) { }
        public PriorityQueue(int capacity, IComparer<P> keyComparer) : base(capacity, new ComparerByKey(keyComparer)) {}

        public void Add(P priority, T item) {
            Add(Pair.New(priority, item));
        }
        public void Enqueue(P priority, T item) {
            Add(priority, item);
        }
        public Pair<P, T> Dequeue() {
            return RemoveAndReturnRoot();
        }

        ///<summary>
        ///Compares two pairs by their first elements.
        ///</summary>
        private class ComparerByKey : IComparer<Pair<P, T>> {
            private readonly IComparer<P> _baseComparer;
            public ComparerByKey(IComparer<P> baseComparer) {
                _baseComparer = baseComparer;
            }
            public ComparerByKey() : this(Comparer<P>.Default) { }
            ///<returns>
            ///Value Condition Less than zero<paramref name="x" /> is less than <paramref name="y" />.Zero<paramref name="x" /> equals <paramref name="y" />.Greater than zero<paramref name="x" /> is greater than <paramref name="y" />.
            ///</returns>
            ///
            ///<param name="x">The first object to compare.</param>
            ///<param name="y">The second object to compare.</param>
            public int Compare(Pair<P, T> x, Pair<P, T> y) {
                return _baseComparer.Compare(x.First, y.First);
            }
        }
    }
}
