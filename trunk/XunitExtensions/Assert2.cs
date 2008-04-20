/* (C) Alexey Romanov 2008 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace XunitExtensions {
    /// <summary>
    /// Contains some assertions missing in xunit.net
    /// </summary>
    public static class Assert2 {
        /// <summary>
        /// Verifies that two sequences contain same elements (by the default equality comparer) in the same order.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expected">The first sequence.</param>
        /// <param name="actual">The second sequence.</param>
        /// <exception cref="SequenceEqualException">If the sequences turn out not to be equal.</exception>
        public static void SequenceEqual<T>(IEnumerable<T> expected, IEnumerable<T> actual) {
            SequenceEqual(expected, actual, EqualityComparer<T>.Default);
        }

        /// <summary>
        /// Verifies that two sequences contain same elements (by the given comparer) in the same order.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expected">The first sequence.</param>
        /// <param name="actual">The second sequence.</param>
        /// <param name="comparer">The comparer.</param>
        /// <exception cref="SequenceEqualException">If the sequences turn out not to be equal.</exception>
        public static void SequenceEqual<T>(IEnumerable<T> expected, IEnumerable<T> actual, IComparer<T> comparer) {
            SequenceEqual(expected, actual, comparer.ToEqualityComparer());
        }

        /// <summary>
        /// Verifies that two sequences contain same elements (by the given equality comparer) in the same order.
        /// </summary>
        /// <param name="expected">The first sequence.</param>
        /// <param name="actual">The second sequence.</param>
        /// <param name="equalityComparer">The equality comparer.</param>
        /// <exception cref="SequenceEqualException">If the sequences turn out not to be equal.</exception>
        public static void SequenceEqual<T>(IEnumerable<T> expected, IEnumerable<T> actual, IEqualityComparer<T> equalityComparer) {
            int i = 0;
            using (var enum1 = expected.GetEnumerator())
            using (var enum2 = actual.GetEnumerator()) {
                while (true) {
                    bool hasnext1 = enum1.MoveNext();
                    bool hasnext2 = enum2.MoveNext();
                    if (!(hasnext1 || hasnext2))
                        break;
                    if (hasnext1 != hasnext2 || !equalityComparer.Equals(enum1.Current, enum2.Current))
                        throw new SequenceEqualException(i, enum1.Current, enum2.Current);
                    i++;
                }
            }
        }

        /// <summary>
        /// Verifies that two sequences contain same elements, independent of their order.
        /// </summary>
        /// <param name="seq1">The first sequence.</param>
        /// <param name="actual">The second sequence.</param>
        /// <exception cref="Xunit.TrueException">Thrown if the sequences do not have same elements.</exception>
        public static void SequenceEquivalent<T>(IEnumerable<T> seq1, IEnumerable<T> actual) where T : IComparable<T> {
            Assert.True(seq1.OrderBy(x => x).SequenceEqual(actual.OrderBy(x => x)), "Sequences do not have the same elements");
        }

        /// <summary>
        /// Verifies that the specified assertion fails.
        /// </summary>
        /// <param name="assertion">The assertion.</param>
        /// <param name="userMessage">The user message.</param>
        /// <exception cref="NotException">Thrown if assertion succeeds.</exception>
        public static void Not(Action assertion, string userMessage) {
            bool failed;
            try {
                assertion();
                failed = true;
            }
            catch (AssertException) {
                failed = false;
            }
            if (failed)
                throw new NotException(userMessage);
        }

        /// <summary>
        /// Verifies that the <c>Count</c> property is equal to the number of iterated elements.
        /// </summary>
        /// <param name="collection">The collection.</param>
        /// <exception cref="Xunit.EqualException">Thrown when the collection doesn't have the correct count.</exception>
        public static void IsCountCorrect(ICollection collection) {
            int i = 0;
            foreach (var elt in collection)
                i++;
            Assert.Equal(collection.Count, i);
        }

        /// <summary>
        /// Verifies that <paramref name="subset"/> is a subset of <paramref name="superset"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="subset">The subset.</param>
        /// <param name="superset">The superset.</param>
        /// <exception cref="SubsetException">Thrown if <paramref name="subset"/> is not a subset of <paramref name="superset"/>.</exception>
        public static void IsSubsetOf<T>(IEnumerable<T> subset, IEnumerable<T> superset) where T : IComparable<T> {
            var sortedSubset = subset.OrderBy(x => x);
            var sortedSuperset = superset.OrderBy(x => x);
            using (var enumerator = sortedSuperset.GetEnumerator()) {
                enumerator.MoveNext();
                foreach (var t in sortedSubset) {
                    while (enumerator.Current.CompareTo(t) < 0)
                        if (!enumerator.MoveNext())
                            throw new SubsetException();
                    if (enumerator.Current.CompareTo(t) > 0)
                        throw new SubsetException();
                }
            }
        }
    }
}