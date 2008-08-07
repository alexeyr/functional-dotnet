#region License
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
#endregion

using System.Collections.Generic;
using FP.Core;

namespace FP.Collections.Immutable {
    /// <summary>
    /// Represents an immutable collection of key/value pairs.
    /// </summary>
    /// <typeparam name="K">The type of keys.</typeparam>
    /// <typeparam name="V">The type of values.</typeparam>
    /// <seealso cref="IDictionary{TKey,TValue}"/>
    public interface IImmutableDictionary<K, V> {
        /// <summary>
        /// Determines whether this dictionary contains the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>
        /// <c>true</c> if this dictionary contains the specified key; otherwise, <c>false</c>.
        /// </returns>
        bool Contains(K key);
        /// <summary>
        /// Looks up the specified key.
        /// </summary>
        /// <value><see cref="Maybe.Just{T}"/><c>(value)</c> if the dictionary contains the specified key 
        /// and associates <c>value</c> to it and <see cref="Maybe.Nothing{T}"/> otherwise.</value>
        Maybe<V> this[K key] { get; }
        /// <summary>
        /// Adds the specified key with the specified value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns>The resulting dictionary.</returns>
        IImmutableDictionary<K, V> Add(K key, V value);
        /// <summary>
        /// Removes the specified key and the associated value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>The resulting dictionary.</returns>
        IImmutableDictionary<K, V> Remove(K key);
        /// <summary>
        /// Gets the keys. Doesn't guarantee anything about the order in which they are yielded.
        /// </summary>
        /// <value>The keys.</value>
        IEnumerable<K> Keys { get; }
        /// <summary>
        /// Gets the values. Doesn't guarantee anything about the order in which they are yielded.
        /// </summary>
        /// <value>The values.</value>
        IEnumerable<V> Values { get; }
        /// <summary>
        /// Gets the key-value pairs. Doesn't guarantee anything about the order in which they are yielded.
        /// </summary>
        /// <value>The pairs.</value>
        IEnumerable<KeyValuePair<K, V>> Pairs { get; }
    }
}