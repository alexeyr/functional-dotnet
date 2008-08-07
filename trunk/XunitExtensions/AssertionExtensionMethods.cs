#region License
/*
* AssertionExtensionMethods.cs is part of functional-dotnet project
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
using System.Linq;
using Xunit;

namespace XunitExtensions.ExtensionMethods {
    /// <summary>
    /// Contains some assertions missing in xunit.net as extension methods.
    /// </summary>
    public static class AssertionExtensionMethods {
        /// <summary>
        /// Verifies that two sequences contain same elements (by the default equality comparer) in the same order.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="actual">The first sequence.</param>
        /// <param name="expected">The second sequence.</param>
        /// <exception cref="SequenceEqualException">If the sequences turn out not to be equal.</exception>
        public static void ShouldBeSequenceEqualTo<T>(this IEnumerable<T> actual, IEnumerable<T> expected) {
            ShouldBeSequenceEqualTo(actual, expected, EqualityComparer<T>.Default);
        }

        /// <summary>
        /// Verifies that two sequences contain same elements (by the given comparer) in the same order.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="actual">The actual sequence.</param>
        /// <param name="expected">The expected sequence.</param>
        /// <param name="comparer">The comparer.</param>
        /// <exception cref="SequenceEqualException">If the sequences turn out not to be equal.</exception>
        public static void ShouldBeSequenceEqualTo<T>(this IEnumerable<T> actual, IEnumerable<T> expected, IComparer<T> comparer) {
            ShouldBeSequenceEqualTo(actual, expected, comparer.ToEqualityComparer());
        }

        /// <summary>
        /// Verifies that two sequences contain same elements (by the given equality comparer) in the same order.
        /// </summary>
        /// <param name="actual">The first sequence.</param>
        /// <param name="expected">The second sequence.</param>
        /// <param name="equalityComparer">The equality comparer.</param>
        /// <exception cref="SequenceEqualException">If the sequences turn out not to be equal.</exception>
        public static void ShouldBeSequenceEqualTo<T>(this IEnumerable<T> actual, IEnumerable<T> expected, IEqualityComparer<T> equalityComparer) {
            int i = 0;
            using (var enum1 = actual.GetEnumerator())
            using (var enum2 = expected.GetEnumerator()) {
                while (true) {
                    if (enum1.MoveNext() != enum2.MoveNext() || !equalityComparer.Equals(enum1.Current, enum2.Current))
                        throw new SequenceEqualException(i, enum1.Current, enum2.Current);
                    i++;
                }
            }
        }

        /// <summary>
        /// Verifies that two sequences contain same elements, independent of their order.
        /// </summary>
        /// <param name="actual">The first sequence.</param>
        /// <param name="expected">The second sequence.</param>
        /// <exception cref="Xunit.TrueException">Thrown if the sequences do not have same elements.</exception>
        public static void ShouldBeSequenceEquivalent<T>(this IEnumerable<T> actual, IEnumerable<T> expected) where T : IComparable<T> {
            Assert.True(actual.OrderBy(x => x).SequenceEqual(expected.OrderBy(x => x)), "Sequences do not have the same elements");
        }
    }
}
