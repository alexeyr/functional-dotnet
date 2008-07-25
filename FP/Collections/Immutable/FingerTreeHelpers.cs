using System;
using System.Collections.Generic;
using FP.Collections.Immutable.FingerTrees;
using FP.HaskellNames;

namespace FP.Collections.Immutable {
    /// <summary>
    /// Utility class.
    /// </summary>
    public static class FingerTree {
        /// <summary>
        /// Creates the empty tree-list with the specified measure monoid.
        /// </summary>
        /// <typeparam name="T">The type of the elements of the list.</typeparam>
        /// <typeparam name="V">The type of the measure values.</typeparam>
        /// <param name="measureMonoid">The measure monoid.</param>
        /// <returns></returns>
        public static FingerTreeList<T, V>.Empty Empty<T, V>(Monoid<V> measureMonoid) where T : IMeasured<V> {
            return new FingerTreeList<T, V>.Empty(measureMonoid);
        }

        /// <summary>
        /// Creates the tree-list from the specified sequence.
        /// </summary>
        /// <typeparam name="T">The type of the elements of the list.</typeparam>
        /// <typeparam name="V">The type of the measure values.</typeparam>
        /// <param name="sequence">The sequence.</param>
        /// <param name="measureMonoid">The measure monoid.</param>
        /// <returns></returns>
        public static FingerTreeList<T, V> FromEnumerable<T,V>(IEnumerable<T> sequence, Monoid<V> measureMonoid) where T : IMeasured<V> {
//            Func<T, FingerTreeList<T, V>, FingerTreeList<T, V>> prepend1 = (a, tree) => tree.Prepend(a);
//            return sequence.ReduceR(prepend1)(Empty<T,V>(measureMonoid));
            Func<FingerTreeList<T, V>, T, FingerTreeList<T, V>> append = (tree, a) => tree.Append(a);
            return sequence.ReduceL(append)(Empty<T, V>(measureMonoid));
        }
    }
}

namespace FP.Collections.Immutable.FingerTrees {
    /// <summary>
    /// Utility class.
    /// </summary>
    public static class FingerTree2 {
        /// <summary>
        /// Reduces the specified sequence from the right.
        /// </summary>
        /// <typeparam name="T">The type of the elements of the sequence.</typeparam>
        /// <typeparam name="A"></typeparam>
        /// <param name="sequence">The sequence.</param>
        /// <param name="binOp">The binary operation.</param>
        /// <returns></returns>
        public static Func<A, A> ReduceR<T, A>(this IEnumerable<T> sequence, Func<T, A, A> binOp) {
            return a => sequence.FoldRight(a, binOp);
        }

        /// <summary>
        /// Reduces the specified sequence from the left.
        /// </summary>
        /// <typeparam name="T">The type of the elements of the sequence.</typeparam>
        /// <typeparam name="A"></typeparam>
        /// <param name="sequence">The sequence.</param>
        /// <param name="binOp">The binary operation.</param>
        /// <returns></returns>
        public static Func<A, A> ReduceL<T, A>(this IEnumerable<T> sequence, Func<A, T, A> binOp) {
            return a => sequence.FoldLeft(a, binOp);
        }
    }
}