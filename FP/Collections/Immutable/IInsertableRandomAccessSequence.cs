using System;
using System.Collections.Generic;

namespace FP.Collections.Immutable {
    /// <summary>
    /// Represents a <see cref="IRandomAccessSequence{T,TSequence}"/> where new elements can be inserted or removed.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TSequence">The type of the RA deque.</typeparam>
    public interface IInsertableRandomAccessSequence<T, TSequence> : IRandomAccessSequence<T, TSequence>, IDeque<T, TSequence> where TSequence : IInsertableRandomAccessSequence<T, TSequence> {
        /// <summary>
        /// Inserts <paramref name="newValue"/> at <paramref name="index"/>.
        /// </summary>
        /// <param name="index">The index where the new element shall be inserted.</param>
        /// <param name="newValue">The new value.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"><c>index</c> is out of range.</exception>
        TSequence InsertAt(int index, T newValue);

        /// <summary>
        /// Removes the element at index <paramref name="index"/>.
        /// </summary>
        /// <param name="index">The index of the element to remove.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"><c>index</c> is out of range.</exception>
        TSequence RemoveAt(int index);

        /// <summary>
        /// Inserts all elements in <paramref name="ts"/> at <paramref name="index"/>.
        /// </summary>
        /// <param name="index">The index where the new element shall be inserted.</param>
        /// <param name="ts">The collection of values to insert.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"><c>index</c> is out of range.</exception>
        TSequence InsertRangeAt(int index, IEnumerable<T> ts);

        /// <summary>
        /// Removes <paramref name="length"/> elements, starting at index <paramref name="startIndex"/>.
        /// </summary>
        /// <param name="startIndex">The index of the first element to remove.</param>
        /// <param name="length">The number of elements to remove.</param>
        /// <exception cref="ArgumentOutOfRangeException"><paramref name="startIndex"/> or 
        /// <paramref name="length"/> is out of range.</exception>
        TSequence RemoveRangeAt(int startIndex, int length);
    }
}