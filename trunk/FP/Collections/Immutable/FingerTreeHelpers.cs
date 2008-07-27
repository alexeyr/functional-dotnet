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
        internal static Func<A, A> ReduceR<T, A>(this IEnumerable<T> sequence, Func<T, A, A> binOp) {
            return a => sequence.FoldRight(binOp, a);
        }

        /// <summary>
        /// Reduces the specified sequence from the left.
        /// </summary>
        /// <typeparam name="T">The type of the elements of the sequence.</typeparam>
        /// <typeparam name="A"></typeparam>
        /// <param name="sequence">The sequence.</param>
        /// <param name="binOp">The binary operation.</param>
        /// <returns></returns>
        internal static Func<A, A> ReduceL<T, A>(this IEnumerable<T> sequence, Func<A, T, A> binOp) {
            return a => sequence.FoldLeft(binOp, a);
        }

        internal static Split<T, T[]> SplitArray<T, V>(this T[] array, Monoid<V> monoid, Func<V, bool> pred, V init) where T : IMeasured<V> {
            var left = new List<T>(Math.Min(array.Length, 10));
            if (array.Length == 1) {
                var empty = new T[0];
                return new Split<T, T[]>(empty, array[0], empty);
            }

            V total = init;
            for (int offset = 0; offset < array.Length - 1; offset++) {
                T t = array[offset];
                if (pred(total)) {
                    var right = new T[array.Length - offset - 1];
                    Array.Copy(array, offset + 1, right, 0, right.Length);
                    return new Split<T, T[]>(left.ToArray(), t, right);
                }
                left.Add(t);
                total = monoid.Plus(total, t.Measure);
            }
            
            return new Split<T, T[]>(left.ToArray(), array[array.Length - 1], new T[0]);
        }

        internal static V SumMeasures<V, T>(this IEnumerable<T> sequence, Monoid<V> monoid, V init) where T : IMeasured<V> {
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
        internal readonly FT _left;
        internal readonly T _center;
        internal readonly FT _right;

        public Split(FT left, T center, FT right) : this() {
            _left = left;
            _right = right;
            _center = center;
        }
    }
}