#region License
/*
* Heap.cs is part of functional-dotnet project
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

using System;
using System.Collections;
using System.Collections.Generic;
using FP.Core;

namespace FP.Collections.Mutable {
    /// <summary>
    /// A generic ternary max heap.
    /// </summary>
    /// <typeparam name="T">Type of the data. If it is not <see cref="IComparable{K}"/>, 
    /// you need to supply a comparer when creating an instance!</typeparam>
    [Serializable]
    public class Heap<T> : ICollection<T>, ICloneable {
        private readonly IComparer<T> _comparer;
        private List<T> _list;
        /// <summary>
        /// Initializes a new instance of the <see cref="Heap&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="capacity">The capacity.</param>
        /// <param name="comparer">The key comparer.</param>
        protected Heap(int capacity, IComparer<T> comparer) {
            _comparer = comparer;
            _list = new List<T>(capacity);
        }

        /// <summary>
        /// Creates a new min heap and returns it.
        /// </summary>
        /// <returns></returns>
        public static Heap<T> MinHeap() {
            return MinHeap(10);
        }

        /// <summary>
        /// Creates a new min heap and returns it.
        /// </summary>
        /// <param name="capacity">The capacity.</param>
        /// <returns></returns>
        public static Heap<T> MinHeap(int capacity) {
            return new Heap<T>(capacity, new ReverseComparer<T>(Comparer<T>.Default));
        }

        /// <summary>
        /// Creates a new max heap and returns it.
        /// </summary>
        /// <returns></returns>
        public static Heap<T> MaxHeap() {
            return MaxHeap(10);
        }

        /// <summary>
        /// Creates a new max heap and returns it.
        /// </summary>
        /// <param name="capacity">The capacity.</param>
        /// <returns></returns>
        public static Heap<T> MaxHeap(int capacity) {
            return new Heap<T>(capacity, Comparer<T>.Default);
        }

        /// <summary>
        /// Creates a new min heap and returns it.
        /// </summary>
        /// <param name="comparer">The key comparer.</param>
        /// <returns></returns>
        public static Heap<T> MinHeap(IComparer<T> comparer) {
            return MinHeap(10, new ReverseComparer<T>(comparer));
        }

        /// <summary>
        /// Creates a new min heap and returns it.
        /// </summary>
        /// <param name="capacity">The capacity.</param>
        /// <param name="comparer">The key comparer.</param>
        /// <returns></returns>
        public static Heap<T> MinHeap(int capacity, IComparer<T> comparer) {
            return new Heap<T>(capacity, new ReverseComparer<T>(comparer));
        }

        /// <summary>
        /// Creates a new max heap and returns it.
        /// </summary>
        /// <param name="comparer">The key comparer.</param>
        /// <returns></returns>
        public static Heap<T> MaxHeap(IComparer<T> comparer) {
            return MaxHeap(10, comparer);
        }

        /// <summary>
        /// Creates a new max heap and returns it.
        /// </summary>
        /// <param name="capacity">The capacity.</param>
        /// <param name="comparer">The key comparer.</param>
        /// <returns></returns>
        public static Heap<T> MaxHeap(int capacity, IComparer<T> comparer) {
            return new Heap<T>(capacity, comparer);
        }

        ///<summary>
        ///Adds an item to the <see cref="Heap{T}"/>.
        ///</summary>
        ///<param name="item">The object to add to the <see cref="Heap{T}"/>.</param>
        public void Add(T item) {
            _list.Add(item);
            SiftUp(_list.Count - 1);
        }

        ///<summary>
        ///Removes all items from the <see cref="Heap{T}"/>.
        ///</summary>
        public void Clear() {
            _list = new List<T>();
        }

        ///<summary>
        ///Determines whether the <see cref="Heap{T}"/> contains a specific value.
        ///</summary>
        ///<returns>
        ///true if <paramref name="item"/> is found in the <see cref="Heap{T}"/>; otherwise, false.
        ///</returns>
        ///<param name="item">The object to locate in the <see cref="Heap{T}"/>.</param>
        public bool Contains(T item) {
            return _list.Contains(item);
        }

        ///<summary>
        ///Copies the elements of the <see cref="Heap{T}"/> to an <see cref="T:System.Array"/>, starting at a particular <see cref="T:System.Array"/> index.
        ///</summary>
        ///<param name="array">The one-dimensional <see cref="T:System.Array"/> that is the destination of the elements copied from <see cref="Heap{T}"/>. The <see cref="T:System.Array"/> must have zero-based indexing.</param>
        ///<param name="arrayIndex">The zero-based index in <paramref name="array"/> at which copying begins.</param>
        ///<exception cref="T:System.ArgumentNullException"><paramref name="array"/> is null.</exception>
        ///<exception cref="T:System.ArgumentOutOfRangeException"><paramref name="arrayIndex"/> is less than 0.</exception>
        ///<exception cref="T:System.ArgumentException"><paramref name="array"/> is multidimensional.-or-<paramref name="arrayIndex"/> is equal to or greater than the length of <paramref name="array"/>.-or-The number of elements in the source <see cref="Heap{T}"/> is greater than the available space from <paramref name="arrayIndex"/> to the end of the destination <paramref name="array"/>.-or-Type <typeparamref name="T"/> cannot be cast automatically to the type of the destination <paramref name="array"/>.</exception>
        public void CopyTo(T[] array, int arrayIndex) {
            _list.CopyTo(array, arrayIndex);
        }

        ///<summary>
        ///Removes the first occurrence of a specific object from the <see cref="Heap{T}"/>.
        ///</summary>
        ///<returns>
        ///true if <paramref name="item"/> was successfully removed from the <see cref="Heap{T}"/>; otherwise, false. This method also returns false if <paramref name="item"/> is not found in the original <see cref="Heap{T}"/>.
        ///</returns>
        ///<param name="item">The object to remove from the <see cref="Heap{T}"/>.</param>
        /// <exception cref="NotSupportedException">Always.</exception>
        public bool Remove(T item) {
            throw new NotSupportedException();
        }

        ///<summary>
        ///Gets the number of elements contained in the <see cref="Heap{T}"/>.
        ///</summary>
        ///<returns>
        ///The number of elements contained in the <see cref="Heap{T}"/>.
        ///</returns>
        public int Count {
            get { return _list.Count; }
        }

        ///<summary>
        ///Gets a value indicating whether the <see cref="Heap{T}"/> is read-only.
        ///</summary>
        ///<value>
        ///<c>false</c>.
        ///</value>
        public bool IsReadOnly {
            get { return false; }
        }

        ///<summary>
        ///Returns an enumerator that iterates through a collection.
        ///</summary>
        ///<returns>
        ///An <see cref="T:System.Collections.IEnumerator"/> object that can be used to iterate through the collection.
        ///</returns>
        ///<filterpriority>2</filterpriority>
        IEnumerator IEnumerable.GetEnumerator() {
            return ((IEnumerable<T>) this).GetEnumerator();
        }

        ///<summary>
        ///Returns an enumerator that iterates through the collection.
        ///</summary>
        ///<returns>
        ///A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
        ///</returns>
        ///<filterpriority>1</filterpriority>
        public IEnumerator<T> GetEnumerator() {
            return _list.GetEnumerator();
        }

        ///<summary>
        ///Creates a new object that is a copy of the current instance.
        ///</summary>
        ///
        ///<returns>
        ///A new object that is a copy of this instance.
        ///</returns>
        ///<filterpriority>2</filterpriority>
        public object Clone() {
            return new Heap<T>(_list.Capacity, _comparer) { _list = new List<T>(_list) };
        }

        /// <summary>
        /// Find the index of the greatest child of the i-th element of the heap.
        /// </summary>
        /// <param name="i"></param>
        /// <returns>The index of the greatest child of the i-th element or -1 if the i-th element has no children.</returns>
        private int GreatestChild(int i) {
            int k = 3 * i + 1;
            int count = _list.Count;
            if (k >= count) return -1;
            if (k + 1 >= count) return k;
            int j = _comparer.Compare(_list[k], _list[k + 1]) >= 0
                ? k 
                : k + 1;
            if (k + 2 >= count)
                return j;
            return _comparer.Compare(_list[j], _list[k + 2]) >= 0
                       ? j
                       : k + 2;
        }

        /// <summary>
        /// Find the index of the parent of the i-th element of the heap.
        /// </summary>
        /// <param name="i"></param>
        /// <returns>The index of the parent of the i-th element or -1 if the i-th element has no parent.</returns>
        private int Parent(int i) {
            return (i - 1) / 3;
        }

        /// <summary>
        /// Swaps the elements with the specified indexes.
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        private void Swap(int i, int j) {
            T elem = _list[i];
            _list[i] = _list[j];
            _list[j] = elem;
        }

        /// <summary>
        /// Moves the element with the specified index up repeatedly until it reaches the root
        /// or becomes less than (or equal to) its parent.
        /// </summary>
        /// <param name="n">The index of the element to move.</param>
        private void SiftUp(int n) {
            int i = n;
            int j = Parent(i);
            T elem = _list[n];
            while (i > 0 && _comparer.Compare(elem, _list[j]) > 0) {
                Swap(i, j);
                i = j;
                j = Parent(i);
            }
        }

        /// <summary>
        /// Moves the element with the specified index down repeatedly until it has no children
        /// or is greater than (or equal to) all its children.
        /// </summary>
        /// <param name="n">The index of the element to move.</param>
        private void SiftDown(int n) {
            int i = n;
            int j = GreatestChild(i);
            T elem = _list[n];
            while (j != -1 && _comparer.Compare(elem, _list[j]) < 0) {
                Swap(i, j);
                i = j;
                j = GreatestChild(i);
            }
        }

        /// <summary>
        /// Deletes the root of the heap.
        /// </summary>
        /// <returns>The root of the heap.</returns>
        /// <exception cref="InvalidOperationException">if the heap is empty.</exception>
        public T RemoveAndReturnRoot() {
            if (_list.Count == 0)
                throw new InvalidOperationException("The heap is empty");
            T root = _list[0];
            if (_list.Count == 1) {
                _list.Clear();
                return root;
            }
            Swap(0, _list.Count - 1);
            _list.RemoveAt(_list.Count - 1);
            SiftDown(0);
            return root;
        }
    }
}