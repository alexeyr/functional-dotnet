using System;
using System.Collections;
using System.Collections.Generic;

namespace FP.Text {
    ///<summary>
    ///An adapter of an array to <see cref="ICharSequence{TChar}"/>.
    ///</summary>
    public struct ArrayCharSequence<TChar> : ICharSequence<TChar> {
        private readonly TChar[] _array;

        public ArrayCharSequence(TChar[] array) {
            _array = array;
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<TChar> GetEnumerator() {
            foreach (TChar c in _array)
                yield return c;
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return _array.GetEnumerator();
        }

        /// <summary>
        /// Gets the length of the sequence.
        /// </summary>
        public int Length {
            get { return _array.Length; }
        }

        /// <summary>
        /// Gets the <paramref name="index"/>-th <see cref="TChar"/> in the sequence.
        /// Should be quick constant time.
        /// </summary>
        public TChar this[int index] {
            get { return _array[index]; }
        }

        /// <summary>
        /// Copies the array to <paramref name="destination"/>, starting at <paramref name="destinationIndex"/>.
        /// </summary>
        /// <param name="sourceIndex">The index to start copying from.</param>
        /// <param name="destination">The destination array.</param>
        /// <param name="destinationIndex">The index in the destination array.</param>
        /// <param name="count">The number of elements to copy.</param>
        public void CopyTo(int sourceIndex, TChar[] destination, int destinationIndex, int count) {
            Array.Copy(_array, sourceIndex, destination, destinationIndex, count);
        }
    }
}