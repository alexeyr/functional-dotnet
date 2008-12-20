/*
* FingerTreeHelpers.cs is part of functional-dotnet project
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
using System.Linq;

namespace FP.Collections.Immutable {
    /// <summary>
    /// Utility class.
    /// </summary>
    public static class FingerTree {
        /// <summary>
        /// Creates the empty tree with the specified measure monoid.
        /// </summary>
        /// <typeparam name="T">The type of the elements of the tree.</typeparam>
        /// <typeparam name="V">The type of the measure values.</typeparam>
        /// <param name="measureMonoid">The measure monoid.</param>
        public static FingerTree<T, V>.Empty Empty<T, V>(Monoid<V> measureMonoid)
            where T : IMeasured<V> {
            return new FingerTree<T, V>.Empty(measureMonoid);
        }

        /// <summary>
        /// Creates the tree with the single element <paramref name="item"/> and the specified measure monoid.
        /// </summary>
        /// <typeparam name="T">The type of the elements of the tree.</typeparam>
        /// <typeparam name="V">The type of the measure values.</typeparam>
        /// <param name="item">The item.</param>
        /// <param name="measureMonoid">The measure monoid.</param>
        public static FingerTree<T, V>.Single Single<T, V>(T item, Monoid<V> measureMonoid)
            where T : IMeasured<V> {
            return new FingerTree<T, V>.Single(item, measureMonoid,
                                               new FingerTree<T, V>.Empty(measureMonoid));
        }

        /// <summary>
        /// Creates the tree from the specified sequence.
        /// </summary>
        /// <typeparam name="T">The type of the elements of the tree.</typeparam>
        /// <typeparam name="V">The type of the measure values.</typeparam>
        /// <param name="sequence">The sequence.</param>
        /// <param name="measureMonoid">The measure monoid.</param>
        public static FingerTree<T, V> FromEnumerable<T, V>(IEnumerable<T> sequence,
                                                            Monoid<V> measureMonoid)
            where T : IMeasured<V> {
            return Empty<T, V>(measureMonoid).AppendRange(sequence);
        }

        internal static Split<T, T[]> SplitArray<T, V>(this T[] array, Monoid<V> monoid,
                                                       Func<V, bool> pred, V init)
            where T : IMeasured<V> {
            if (array.Length == 1) {
                var empty = new T[0];
                return new Split<T, T[]>(empty, array[0], empty);
            }

            var left = new List<T>(Math.Min(array.Length, 10));
            V total = init;
            for (int offset = 0; offset < array.Length - 1; offset++) {
                T t = array[offset];
                total = monoid.Plus(total, t.Measure);
                if (pred(total)) {
                    var right = array.Skip(offset + 1).ToArray();
                    return new Split<T, T[]>(left.ToArray(), t, right);
                }
                left.Add(t);
            }

            return new Split<T, T[]>(left.ToArray(), array[array.Length - 1], new T[0]);
        }

        internal static V SumMeasures<V, T>(this IEnumerable<T> sequence, Monoid<V> monoid, V init)
            where T : IMeasured<V> {
            V total = init;
            foreach (var t in sequence)
                total = monoid.Plus(total, t.Measure);
            return total;
        }
    }

    /// <summary>
    /// Represents a splitting of a structure of type <typeparamref name="FT"/> with elements
    /// of type <typeparamref name="T"/>
    /// </summary>
    internal struct Split<T, FT> where FT : IEnumerable<T> {
        internal readonly FT Left;
        internal readonly T Middle;
        internal readonly FT Right;

        internal Split(FT left, T middle, FT right)
            : this() {
            Left = left;
            Right = right;
            Middle = middle;
        }
    }
}