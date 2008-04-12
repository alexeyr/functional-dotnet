/* (C) Alexey Romanov 2008 */

using System.Collections.Generic;

namespace FP.Collections.Immutable {
    public interface IImmutableList<T> : IEnumerable<T> {
        /// <summary>
        /// Gets the "head" (first element) of the list.
        /// </summary>
        /// <value>The head of the list.</value>
        /// <exception cref="EmptySequenceException">if the current list <see cref="IsEmpty"/>.</exception>
        T Head { get; }
        /// <summary>
        /// Gets the "tail" (all elements but the first) of the list.
        /// </summary>
        /// <value>The tail of the list.</value>
        /// <exception cref="EmptySequenceException">if the current list <see cref="IsEmpty"/>.</exception>
        IImmutableList<T> Tail { get; }
        /// <summary>
        /// Gets a value indicating whether this instance is empty.
        /// </summary>
        /// <value><c>true</c> if this instance is empty; otherwise, <c>false</c>.</value>
        bool IsEmpty { get; }
        /// <summary>
        /// Prepends a new head.
        /// </summary>
        /// <param name="newHead">The new head.</param>
        /// <returns>The list with <paramref name="newHead"/> as <see cref="Head"/>
        /// and the current list as <see cref="Tail"/>.</returns>
        IImmutableList<T> Prepend(T newHead);
    }
}
