using System;
using System.Collections.Generic;
using FP.Core;

namespace FP.Collections.Immutable {
    /// <summary>
    /// A read interface for random access sequences.
    /// </summary>
    /// <typeparam name="T">The type of elements.</typeparam>
    /// <typeparam name="TSequence">The type of the sequence.</typeparam>
    public interface IRandomAccessSequenceRead<T, TSequence> : IEnumerable<T> 
        where TSequence : IRandomAccessSequenceRead<T, TSequence> {
        /// <summary>
        /// Gets the number of elements in the sequence.
        /// </summary>
        /// <value>The number of elements in the sequence.</value>
        int Count { get; }

        /// <summary>
        /// Gets a value indicating whether this list is empty.
        /// </summary>
        /// <value><c>true</c>.</value>
        bool IsEmpty { get; }

        /// <summary>
        /// Returns a pair of sequences, the first contains the first <paramref name="count"/> of
        /// the sequence and the second one contains the rest of them.
        /// </summary>
        /// <param name="count">The index at which the sequence will be split.</param>
        /// <remarks>if <code>count &lt;= 0 || count &gt;= Count</code>, the corresponding part 
        /// of the result will be empty.</remarks>
        Tuple<TSequence, TSequence> SplitAt(int count);

        /// <summary>
        /// Gets the <see cref="T"/> at the specified index.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"><c>index</c> is out of range.</exception>
        T this[int index] { get; }
    }
}