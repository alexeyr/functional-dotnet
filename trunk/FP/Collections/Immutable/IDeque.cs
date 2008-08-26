using FP.Core;

namespace FP.Collections.Immutable {
    /// <summary>
    /// An immutable double-ended queue.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TDeque">The type of the deque.</typeparam>
    public interface IDeque<T, TDeque> : IImmutableList<T, TDeque> where TDeque : IDeque<T, TDeque> {
        /// <summary>
        /// Appends the specified element to the end of the list.
        /// </summary>
        /// <param name="newLast">The new last element.</param>
        /// <returns>The resulting list.</returns>
        TDeque Append(T newLast);

        /// <summary>
        /// Gets the initial sublist (all elements but the last) of the list.
        /// </summary>
        /// <value>Throws <see cref="EmptySequenceException"/>.</value>
        /// <exception cref="EmptySequenceException"></exception>
        TDeque Init { get; }

        /// <summary>
        /// Gets the last element of the list.
        /// </summary>
        /// <value>Throws <see cref="EmptySequenceException"/>.</value>
        /// <exception cref="EmptySequenceException"></exception>
        T Last { get; }
    }
}