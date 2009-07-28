/*
* IImmutableDictionary.cs is part of functional-dotnet project
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

using System;
using System.Collections.Generic;
using FP.Core;

namespace FP.Collections.Persistent {
    /// <summary>
    /// Represents an immutable collection of key/value pairs.
    /// </summary>
    /// <typeparam name="TKey">The type of keys.</typeparam>
    /// <typeparam name="TValue">The type of values.</typeparam>
    /// <typeparam name="TDictionary">The type of the dictionary.</typeparam>
    /// <seealso cref="System.Collections.Generic.IDictionary{TKey,TValue}"/>
    public interface IDictionary<TKey, TValue, TDictionary>
        where TDictionary : IDictionary<TKey, TValue, TDictionary> {
        /// <summary>
        /// Determines whether this dictionary contains the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>
        /// <c>true</c> if this dictionary contains the specified key; otherwise, <c>false</c>.
        /// </returns>
        bool Contains(TKey key);

        /// <summary>
        /// Looks up the specified key.
        /// </summary>
        /// <value><see cref="Optional.Some{T}"/><c>(value)</c> if the
        /// dictionary contains the specified key  and associates <c>value</c>
        /// to it and <see cref="Optional.None{T}"/> otherwise.</value>
        Optional<TValue> this[TKey key] { get; }

        /// <summary>
        /// Adds the specified key with the specified value.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="combiner">
        /// The function to be called if the given key is already present. The
        /// arguments are the key, the current value, and the added value. The
        /// result is inserted in place of the current value.
        /// </param>
        /// <returns>
        /// The resulting dictionary.
        /// </returns>
        TDictionary Add(TKey key, TValue value, Func<TKey, TValue, TValue, TValue> combiner);

        /// <summary>
        /// Removes the specified key and the associated value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>The resulting dictionary.</returns>
        TDictionary Remove(TKey key);

        /// <summary>
        /// Gets the keys. Doesn't guarantee anything about the order in which they are yielded.
        /// </summary>
        /// <value>The keys.</value>
        IEnumerable<TKey> Keys { get; }

        /// <summary>
        /// Gets the values. Doesn't guarantee anything about the order in which they are yielded.
        /// </summary>
        /// <value>The values.</value>
        IEnumerable<TValue> Values { get; }

        /// <summary>
        /// Gets the key-value pairs. Doesn't guarantee anything about the order
        /// in which they are yielded.
        /// </summary>
        /// <value>The pairs.</value>
        IEnumerable<Tuple<TKey, TValue>> KeyValuePairs { get; }
        }
}