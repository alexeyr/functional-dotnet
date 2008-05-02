/* (C) Alexey Romanov 2008 */

using System;
using System.Collections.Generic;

namespace FP.Collections.Immutable {
    /// <summary>
    /// Represents an immutable list of values, which can be
    /// </summary>
    /// <typeparam name="T"></typeparam>
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
        /// <exception cref="EmptySequenceException">if the list <see cref="IsEmpty"/>.</exception>
        IImmutableList<T> Tail { get; }
        /// <summary>
        /// Gets a value indicating whether this list is empty.
        /// </summary>
        /// <value><c>true</c> if this list is empty; otherwise, <c>false</c>.</value>
        bool IsEmpty { get; }
        /// <summary>
        /// Prepends a new head.
        /// </summary>
        /// <param name="newHead">The new head.</param>
        /// <returns>The list with <paramref name="newHead"/> as <see cref="Head"/>
        /// and this list as <see cref="Tail"/>.</returns>
        IImmutableList<T> Prepend(T newHead);
    }

    /// <summary>
    /// Extension methods for <see cref="IImmutableList{T}"/>.
    /// </summary>
    public static class ImmutableLists {
        /// <summary>
        /// Case analysis on lists.
        /// </summary>
        /// <typeparam name="T">The type of the elements of the list.</typeparam>
        /// <param name="list">The list.</param>
        /// <param name="ifEmpty">The action to do if the list is empty.</param>
        /// <param name="ifNotEmpty">The action to do if the list is not empty.</param>
        public static void Match<T>(
            this IImmutableList<T> list, Action ifEmpty, Action<T, IImmutableList<T>> ifNotEmpty) {
            if (list.IsEmpty)
                ifEmpty();
            else
                ifNotEmpty(list.Head, list.Tail);
        }

        /// <summary>
        /// Case analysis on lists.
        /// </summary>
        /// <typeparam name="T">The type of the elements of the list.</typeparam>
        /// <typeparam name="R">The return type of the functions.</typeparam>
        /// <param name="list">The list.</param>
        /// <param name="ifEmpty">The function to call if the list is empty.</param>
        /// <param name="ifNotEmpty">The function to call if the list is not empty.</param>
        public static R Match<T,R>(
            this IImmutableList<T> list, Func<R> ifEmpty, Func<T, IImmutableList<T>, R> ifNotEmpty) {
            return list.IsEmpty ? ifEmpty() : ifNotEmpty(list.Head, list.Tail);
        }
    }
}
