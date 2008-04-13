using System;
using System.Collections.Generic;

namespace FP {
    /// <summary>
    /// A static class which provides conversions between <see cref="IComparer{T}"/> and <see cref="Comparison{T}"/>.
    /// </summary>
    public static class Comparers {
        /// <summary>
        /// Reverses the specified comparer.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="comparer">The comparer.</param>
        public static IComparer<T> Reverse<T>(this IComparer<T> comparer) {
            return new ReverseComparer<T>(comparer);
        }

        /// <summary>
        /// Converts a comparer to a comparison.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="comparer">The comparer.</param>
        /// <returns></returns>
        public static Comparison<T> ToComparison<T>(this IComparer<T> comparer) {
            return comparer.Compare;
        }

        /// <summary>
        /// Converts a comparison to a comparer.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="comparison">The comparison.</param>
        /// <returns></returns>
        public static IComparer<T> ToComparer<T>(this Comparison<T> comparison) {
            return new FunctorComparer<T>(comparison);
        }
    }

    /// <summary>
    /// The comparer given by a function.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class FunctorComparer<T> : IComparer<T> {
        private readonly Comparison<T> _comparison;

        public FunctorComparer(Comparison<T> comparison) {
            _comparison = comparison;
        }

        ///<summary>
        ///Compares two objects and returns a value indicating whether one is less than, equal to, or greater than the other.
        ///</summary>
        ///
        ///<returns>
        ///Value Condition Less than zero<paramref name="x" /> is less than <paramref name="y" />.Zero<paramref name="x" /> equals <paramref name="y" />.Greater than zero<paramref name="x" /> is greater than <paramref name="y" />.
        ///</returns>
        ///
        ///<param name="x">The first object to compare.</param>
        ///<param name="y">The second object to compare.</param>
        public int Compare(T x, T y) {
            return _comparison(x, y);
        }
    }

    /// <summary>
    /// A reverse comparer.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ReverseComparer<T> : IComparer<T> {
        private readonly IComparer<T> _baseComparer;

        public ReverseComparer(IComparer<T> baseComparer) {
            _baseComparer = baseComparer;
        }

        ///<summary>
        ///Compares two objects and returns a value indicating whether one is less than, equal to, or greater than the other.
        ///</summary>
        ///
        ///<returns>
        ///Value Condition Less than zero<paramref name="x" /> is less than <paramref name="y" />.Zero<paramref name="x" /> equals <paramref name="y" />.Greater than zero<paramref name="x" /> is greater than <paramref name="y" />.
        ///</returns>
        ///
        ///<param name="x">The first object to compare.</param>
        ///<param name="y">The second object to compare.</param>
        public int Compare(T x, T y) {
            return -_baseComparer.Compare(x, y);
        }
    }
}
