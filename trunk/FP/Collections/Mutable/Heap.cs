/* (C) Alexey Romanov 2008 */

using System;
using System.Collections;
using System.Collections.Generic;
using FP.Collections.Immutable;

namespace FP.Collections.Mutable {
    /// <summary>
    /// A generic ternary heap.
    /// </summary>
    /// <typeparam name="K">Type of the key.</typeparam>
    /// <typeparam name="D">Type of the data.</typeparam>
    public class Heap<K, D> : ICollection<D> where K : IComparable<K> {

        /// <summary>
        /// Gets a value indicating whether this instance is a min heap.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is a min heap; <c>false</c> if this instance is a max heap.
        /// </value>
        public bool IsMinHeap {
            get { return _isMinHeap; }
        }
        
        private readonly bool _isMinHeap;
        private readonly List<Pair<K, D>> _list;

        public Heap(bool isMinHeap) : this(isMinHeap, 10) {}

        public Heap(bool isMinHeap, int capacity) {
            _isMinHeap = isMinHeap;
            _list = new List<Pair<K, D>>(capacity);
        }

        public static Heap<K, D> MinHeap() {
            return MinHeap(10);
        }

        public static Heap<K, D> MinHeap(int capacity) {
            return new Heap<K, D>(true, capacity);
        }

        public static Heap<K, D> MaxHeap() {
            return MaxHeap(10);
        }

        public static Heap<K, D> MaxHeap(int capacity) {
            return new Heap<K, D>(false, capacity);
        }

        ///<summary>
        ///Adds an item to the <see cref="Heap{K,D}"/>.
        ///</summary>
        ///<param name="item">The object to add to the <see cref="Heap{K,D}"/>.</param>
        public void Add(D item) {
            throw new NotImplementedException();
        }

        ///<summary>
        ///Removes all items from the <see cref="Heap{K,D}"/>.
        ///</summary>
        public void Clear() {
            throw new NotImplementedException();
        }

        ///<summary>
        ///Determines whether the <see cref="Heap{K,D}"/> contains a specific value.
        ///</summary>
        ///<returns>
        ///true if <paramref name="item"/> is found in the <see cref="Heap{K,D}"/>; otherwise, false.
        ///</returns>
        ///<param name="item">The object to locate in the <see cref="Heap{K,D}"/>.</param>
        public bool Contains(D item) {
            throw new NotImplementedException();
        }

        ///<summary>
        ///Copies the elements of the <see cref="Heap{K,D}"/> to an <see cref="T:System.Array"/>, starting at a particular <see cref="T:System.Array"/> index.
        ///</summary>
        ///<param name="array">The one-dimensional <see cref="T:System.Array"/> that is the destination of the elements copied from <see cref="Heap{K,D}"/>. The <see cref="T:System.Array"/> must have zero-based indexing.</param>
        ///<param name="arrayIndex">The zero-based index in <paramref name="array"/> at which copying begins.</param>
        ///<exception cref="T:System.ArgumentNullException"><paramref name="array"/> is null.</exception>
        ///<exception cref="T:System.ArgumentOutOfRangeException"><paramref name="arrayIndex"/> is less than 0.</exception>
        ///<exception cref="T:System.ArgumentException"><paramref name="array"/> is multidimensional.-or-<paramref name="arrayIndex"/> is equal to or greater than the length of <paramref name="array"/>.-or-The number of elements in the source <see cref="Heap{K,D}"/> is greater than the available space from <paramref name="arrayIndex"/> to the end of the destination <paramref name="array"/>.-or-Type <paramref name="T"/> cannot be cast automatically to the type of the destination <paramref name="array"/>.</exception>
        public void CopyTo(D[] array, int arrayIndex) {
            throw new NotImplementedException();
        }

        ///<summary>
        ///Removes the first occurrence of a specific object from the <see cref="Heap{K,D}"/>.
        ///</summary>
        ///<returns>
        ///true if <paramref name="item"/> was successfully removed from the <see cref="Heap{K,D}"/>; otherwise, false. This method also returns false if <paramref name="item"/> is not found in the original <see cref="Heap{K,D}"/>.
        ///</returns>
        ///<param name="item">The object to remove from the <see cref="Heap{K,D}"/>.</param>
        public bool Remove(D item) {
            throw new NotImplementedException();
        }

        ///<summary>
        ///Gets the number of elements contained in the <see cref="Heap{K,D}"/>.
        ///</summary>
        ///<returns>
        ///The number of elements contained in the <see cref="Heap{K,D}"/>.
        ///</returns>
        public int Count {
            get { throw new NotImplementedException(); }
        }

        ///<summary>
        ///Gets a value indicating whether the <see cref="Heap{K,D}"/> is read-only.
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
            return ((IEnumerable<D>) this).GetEnumerator();
        }

        ///<summary>
        ///Returns an enumerator that iterates through the collection.
        ///</summary>
        ///<returns>
        ///A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
        ///</returns>
        ///<filterpriority>1</filterpriority>
        public IEnumerator<D> GetEnumerator() {
            throw new NotImplementedException();
        }
    }
}