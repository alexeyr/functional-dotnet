﻿#region License
/*
* PriorityQueue.cs is part of functional-dotnet project
* 
* Copyright (c) 2008 Alexey Romanov
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
#endregion

using System.Collections.Generic;
using FP.Core;

namespace FP.Collections.Mutable {
    /// <summary>
    /// A generic priority queue.
    /// </summary>
    /// <typeparam name="P">The type of priority.</typeparam>
    /// <typeparam name="T">The type of stored data.</typeparam>
    public class PriorityQueue<P, T> : Heap<Tuple<P, T>> {
        /// <summary>
        /// Initializes a new instance of the <see cref="PriorityQueue{P,T}"/> class.
        /// </summary>
        public PriorityQueue() : this(10, Comparer<P>.Default) { }
        /// <summary>
        /// Initializes a new instance of the <see cref="PriorityQueue{P,T}"/> class.
        /// </summary>
        /// <param name="keyComparer">The key comparer.</param>
        public PriorityQueue(IComparer<P> keyComparer) : this(10, keyComparer) { }
        /// <summary>
        /// Initializes a new instance of the <see cref="PriorityQueue{P,T}"/> class.
        /// </summary>
        /// <param name="capacity">The capacity.</param>
        public PriorityQueue(int capacity) : this(capacity, Comparer<P>.Default) { }
        /// <summary>
        /// Initializes a new instance of the <see cref="PriorityQueue{P,T}"/> class.
        /// </summary>
        /// <param name="capacity">The capacity.</param>
        /// <param name="keyComparer">The key comparer.</param>
        public PriorityQueue(int capacity, IComparer<P> keyComparer) : base(capacity, new ComparerByKey(keyComparer)) { }

        /// <summary>
        /// Enqueues the specified item with the specified priority.
        /// </summary>
        /// <param name="priority">The priority.</param>
        /// <param name="item">The item.</param>
        public void Enqueue(P priority, T item) {
            Add(Pair.New(priority, item));
        }
        /// <summary>
        /// Dequeues the item with the greatest priority.
        /// </summary>
        /// <returns>The dequeued item.</returns>
        public Tuple<P, T> Dequeue() {
            return RemoveAndReturnRoot();
        }

        ///<summary>
        ///Compares two pairs by their first elements.
        ///</summary>
        private class ComparerByKey : IComparer<Tuple<P, T>> {
            private readonly IComparer<P> _baseComparer;
            /// <summary>
            /// Initializes a new instance of the <see cref="PriorityQueue{P,T}.ComparerByKey"/> class.
            /// </summary>
            /// <param name="baseComparer">The base comparer.</param>
            public ComparerByKey(IComparer<P> baseComparer) {
                _baseComparer = baseComparer;
            }

            ///<returns>
            ///Value Condition Less than zero<paramref name="x" /> is less than <paramref name="y" />.Zero<paramref name="x" /> equals <paramref name="y" />.Greater than zero<paramref name="x" /> is greater than <paramref name="y" />.
            ///</returns>
            ///
            ///<param name="x">The first object to compare.</param>
            ///<param name="y">The second object to compare.</param>
            public int Compare(Tuple<P, T> x, Tuple<P, T> y) {
                return _baseComparer.Compare(x.Item1, y.Item1);
            }
        }
    }
}
