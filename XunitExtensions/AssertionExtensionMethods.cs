using System;
/* (C) Alexey Romanov 2008 */

using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace XunitExtensions.ExtensionMethods {
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
        /// <param name="actual">The first sequence.</param>
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
