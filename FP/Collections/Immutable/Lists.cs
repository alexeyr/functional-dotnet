/* (C) Alexey Romanov 2008 */

using System;
using System.Linq;

namespace FP.Collections.Immutable {
    /// <summary>
    /// More specific versions of some methods from <see cref="Enumerables"/> and
    /// <see cref="Enumerable"/>
    /// </summary>
    public static class Lists {
        /// <summary>
        /// Conses <paramref name="t"/> to <paramref name="list"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t">The head.</param>
        /// <param name="list">The tail.</param>
        /// <returns>A list starting with <paramref name="t"/> and continuing with
        /// <paramref name="list"/>.</returns>
        public static IImmutableList<T> Cons<T>(this T t, IImmutableList<T> list) {
            return list.Prepend(t);
        }

        /// <summary>
        /// Determines whether the specified list is empty.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">The list.</param>
        /// <returns>
        /// 	<c>true</c> if the specified list is empty; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsEmpty<T>(this IImmutableList<T> list) {
            return list.IsEmpty;
        }

        /// <summary>
        /// Gets the "head" (first element) of the list.
        /// </summary>
        /// <returns>The head of the list.</returns>
        /// <exception cref="EmptySequenceException">if the current list is empty.</exception>
        /// <param name="list">The list.</param>
        public static T Head<T>(this IImmutableList<T> list) {
            return list.Head;
        }

        /// <summary>
        /// Gets the "head" (first element) of the list.
        /// </summary>
        /// <returns>The head of the list.</returns>
        /// <exception cref="EmptySequenceException">if the current list is empty.</exception>
        /// <param name="list">The list.</param>
        public static IImmutableList<T> Tail<T>(this IImmutableList<T> list) {
            return list.Tail;
        }

        /// <summary>
        /// Gets the "head" (first element) of the list.
        /// </summary>
        /// <returns>The head of the list.</returns>
        /// <exception cref="EmptySequenceException">if the current list is empty.</exception>
        /// <param name="list">The list.</param>
        public static T First<T>(this IImmutableList<T> list) {
            return list.Head;
        }

        /// <summary>Bypasses a specified number of elements in a list and then returns the remaining elements.</summary>
        /// <returns>An <see cref="IImmutableList{T}" /> that contains the elements that occur after the specified index in the input list.</returns>
        /// <param name="list">An <see cref="IImmutableList{T}" /> to return elements from.</param>
        /// <param name="count">The number of elements to skip before returning the remaining elements.</param>
        public static IImmutableList<T> Skip<T>(this IImmutableList<T> list, int count) {
            while (count > 0 && !list.IsEmpty) {
                list = list.Tail;
                count--;
            }
            return list;
        }

        /// <summary>Bypasses elements in a list as long as a specified condition is true and then returns the remaining elements.</summary>
        /// <returns>An <see cref="IImmutableList{T}" /> that contains the elements from the input sequence starting at the first element in the linear series that does not pass the test specified by <paramref name="predicate" />.</returns>
        /// <param name="list">An <see cref="IImmutableList{T}" /> to return elements from.</param>
        /// <param name="predicate">A function to test each element for a condition.</param>
        public static IImmutableList<T> SkipWhile<T>(this IImmutableList<T> list, Func<T, bool> predicate) {
            while (!list.IsEmpty && predicate(list.Head))
                list = list.Tail;
            return list;
        }

        /// <summary>Bypasses elements in a list as long as a specified condition is true and then returns the remaining elements.</summary>
        /// <returns>An <see cref="IImmutableList{T}" /> that contains the elements from the input sequence starting at the first element in the linear series that does not pass the test specified by <paramref name="predicate" />.</returns>
        /// <param name="list">An <see cref="IImmutableList{T}" /> to return elements from.</param>
        /// <param name="predicate">A function to test each element for a condition; the second parameter of the function represents the index of the source element..</param>
        public static IImmutableList<T> SkipWhile<T>(this IImmutableList<T> list, Func<T, int, bool> predicate) {
            int index = 0;
            while (!list.IsEmpty && predicate(list.Head, index)) {
                list = list.Tail;
                index++;
            }
            return list;
        }
    }
}
