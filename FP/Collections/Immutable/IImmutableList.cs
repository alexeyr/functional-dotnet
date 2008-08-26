#region License
/*
* IImmutableList.cs is part of functional-dotnet project
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
using System.Collections.Generic;
using FP.Core;

namespace FP.Collections.Immutable {
    /// <summary>
    /// Represents an immutable list of values.
    /// </summary>
    /// <typeparam name="T">The type of elements of the list.</typeparam>
    /// <typeparam name="TList">The type of the list.</typeparam>
    public interface IImmutableList<T, TList> : IEnumerable<T> where TList : IImmutableList<T, TList> {
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
        TList Tail { get; }
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
        TList Prepend(T newHead);
    }

    /// <summary>
    /// Extension methods for <see cref="IImmutableList{T,TList}"/>.
    /// </summary>
    public static class ImmutableLists {
        /// <summary>
        /// Case analysis on lists.
        /// </summary>
        /// <typeparam name="T">The type of the elements of the list.</typeparam>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <param name="list">The list.</param>
        /// <param name="ifEmpty">The action to do if the list is empty.</param>
        /// <param name="ifNotEmpty">The action to do if the list is not empty.</param>
        public static void Match<T, TList>(
            this TList list, Action ifEmpty, Action<T, TList> ifNotEmpty) where TList : IImmutableList<T, TList> {
            if (list.IsEmpty)
                ifEmpty();
            else
                ifNotEmpty(list.Head, list.Tail);
        }

        /// <summary>
        /// Case analysis on lists.
        /// </summary>
        /// <typeparam name="T">The type of the elements of the list.</typeparam>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <typeparam name="R">The return type of the functions.</typeparam>
        /// <param name="list">The list.</param>
        /// <param name="ifEmpty">The function to call if the list is empty.</param>
        /// <param name="ifNotEmpty">The function to call if the list is not empty.</param>
        public static R Match<T, TList, R>(
            this TList list, Func<R> ifEmpty, Func<T, TList, R> ifNotEmpty)
            where TList : IImmutableList<T, TList> {
            return list.IsEmpty ? ifEmpty() : ifNotEmpty(list.Head, list.Tail);
        }
    }
}
