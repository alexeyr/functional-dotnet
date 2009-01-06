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
using System.Diagnostics;
using System.Linq;
using FP.Util;

namespace FP.Collections {
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
            return FingerTree<T, V>.GetEmptyFromCache<T>(measureMonoid);
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
            return new FingerTree<T, V>.Single(item, measureMonoid);
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

        /// <summary>
        /// Creates the tree from the specified sequence.
        /// </summary>
        /// <typeparam name="T">The type of the elements of the tree.</typeparam>
        /// <typeparam name="V">The type of the measure values.</typeparam>
        /// <param name="array">The small array.</param>
        /// <param name="measureMonoid">The measure monoid.</param>
        internal static FingerTree<T, V> FromSmallArray<T, V>(T[] array,
                                                            Monoid<V> measureMonoid)
            where T : IMeasured<V> {
            Debug.Assert(array.Length <= 6);
            switch (array.Length) {
                case 0:
                    return Empty<T, V>(measureMonoid);
                case 1:
                    return Single(array[0], measureMonoid);
                default:
                    var emptyNested =
                        (FingerTree<FTNode<T, V>, V>) Empty<FTNode<T, V>, V>(measureMonoid);
                    switch (array.Length) {
                        case 2:
                            return new FingerTree<T, V>.Deep(
                                new[] { array[0] }, emptyNested, new[] { array[1] },
                                measureMonoid);
                        case 3:
                            return new FingerTree<T, V>.Deep(
                                new[] { array[0], array[1] }, emptyNested, new[] { array[2] },
                                measureMonoid);
                        case 4:
                            return new FingerTree<T, V>.Deep(
                                new[] { array[0], array[1] }, emptyNested, new[] { array[2], array[3] },
                                measureMonoid);
                        case 5:
                            return new FingerTree<T, V>.Deep(
                                new[] { array[0], array[1], array[2] }, emptyNested, new[] { array[3], array[4] },
                                measureMonoid);
                        case 6:
                            return new FingerTree<T, V>.Deep(
                                new[] { array[0], array[1], array[2] }, emptyNested, new[] { array[3], array[4], array[5] },
                                measureMonoid);
                        default:
                            throw new ArgumentException("Can't get here!");
                    }
            }
        }

        internal static Split<T, T[]> SplitArray<T, V>(this T[] array, Func<V, bool> pred, V init, Monoid<V> monoid)
            where T : IMeasured<V> {
            if (array.Length == 1) {
                return new Split<T, T[]>(
                    ArrayUtil.EmptyArray<T>(), array[0], ArrayUtil.EmptyArray<T>());
            }

            V total = init;
            int offset;
            for (offset = 0; offset < array.Length - 1; offset++) {
                total = monoid.Plus(total, array[offset].Measure);
                if (pred(total)) break;
            }
            var left = offset == 0
                           ? ArrayUtil.EmptyArray<T>()
                           : array.CopyNoChecks(0, offset);
            var right = offset == array.Length - 1
                            ? ArrayUtil.EmptyArray<T>()
                            : array.CopyNoChecks(offset + 1);
            return new Split<T, T[]>(left, array[offset], right);
        }

        internal static V SumMeasures<V, T>(this Monoid<V> monoid, V init, IEnumerable<T> sequence)
            where T : IMeasured<V> {
            return monoid.Sum(init, sequence.Select(t => t.Measure));
        }

        internal static V SumMeasures<V, T>(this Monoid<V> monoid, IEnumerable<T> sequence)
        where T : IMeasured<V> {
            return monoid.SumMeasures(monoid.Zero, sequence);
        }

        internal static T[] MapReverse<T>(this T[] array, Func<T, T> f) {
            T[] newArray = array.CopyNoChecks();
            for (int i = 0; i < newArray.Length; i++)
                newArray[i] = f(newArray[i]);
            Array.Reverse(newArray);
            return newArray;
        }
    }
}