using System;

namespace FP.Collections.Immutable {
    /// <summary>
    /// A read interface for random access sequences.
    /// </summary>
    /// <typeparam name="T">The type of elements.</typeparam>
    /// <typeparam name="TSequence">The type of the sequence.</typeparam>
    public interface IRandomAccessSequenceRead<T, TSequence> : ICollection<T> where TSequence : IRandomAccessSequenceRead<T, TSequence> {
        /// <summary>
        /// Returns a subsequence starting at <paramref name="startIndex"/> and consisting of <paramref name="count"/> elements.
        /// </summary>
        /// <param name="startIndex">The start index.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        TSequence Subsequence(int startIndex, int count);

        /// <summary>
        /// Gets the <see cref="T"/> at the specified index.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"><c>index</c> is out of range.</exception>
        T this[int index] { get; }

    }
}