using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace XunitExtensions.ExtensionMethods {
    public static class AssertionExtensionMethods {
        /// <summary>
        /// Verifies that two sequences contain same elements (by the default equality comparer) in the same order.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="seq1">The first sequence.</param>
        /// <param name="seq2">The second sequence.</param>
        /// <exception cref="SequenceEqualException">If the sequences turn out not to be equal.</exception>
        public static void ShouldBeSequenceEqual<T>(this IEnumerable<T> seq1, IEnumerable<T> seq2) {
            ShouldBeSequenceEqual(seq1, seq2, EqualityComparer<T>.Default);
        }

        /// <summary>
        /// Verifies that two sequences contain same elements (by the given comparer) in the same order.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="seq1">The first sequence.</param>
        /// <param name="seq2">The second sequence.</param>
        /// <param name="comparer">The comparer.</param>
        /// <exception cref="SequenceEqualException">If the sequences turn out not to be equal.</exception>
        public static void ShouldBeSequenceEqual<T>(this IEnumerable<T> seq1, IEnumerable<T> seq2, IComparer<T> comparer) {
            ShouldBeSequenceEqual(seq1, seq2, comparer.ToEqualityComparer());
        }

        /// <summary>
        /// Verifies that two sequences contain same elements (by the given equality comparer) in the same order.
        /// </summary>
        /// <param name="seq1">The first sequence.</param>
        /// <param name="seq2">The second sequence.</param>
        /// <param name="equalityComparer">The equality comparer.</param>
        /// <exception cref="SequenceEqualException">If the sequences turn out not to be equal.</exception>
        public static void ShouldBeSequenceEqual<T>(this IEnumerable<T> seq1, IEnumerable<T> seq2, IEqualityComparer<T> equalityComparer) {
            int i = 0;
            using (var enum1 = seq1.GetEnumerator())
            using (var enum2 = seq2.GetEnumerator()) {
                while (true) {
                    if (enum1.MoveNext() != enum2.MoveNext() || !equalityComparer.Equals(enum1.Current, enum2.Current))
                        throw new SequenceEqualException(i);
                    i++;
                }
            }
        }

        /// <summary>
        /// Verifies that two sequences contain same elements, independent of their order.
        /// </summary>
        /// <param name="seq1">The first sequence.</param>
        /// <param name="seq2">The second sequence.</param>
        /// <exception cref="Xunit.TrueException">Thrown if the sequences do not have same elements.</exception>
        public static void ShouldBeSequenceEquivalent<T>(this IEnumerable<T> seq1, IEnumerable<T> seq2) where T : IComparable<T> {
            Assert.True(seq1.OrderBy(x => x).SequenceEqual(seq2.OrderBy(x => x)), "Sequences do not have the same elements");
        }
    }
}
