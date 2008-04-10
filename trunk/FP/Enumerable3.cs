using System;
using System.Collections.Generic;
using System.Linq;

namespace FP.HaskellNames {
    /// <summary>
    /// Provides a set of static (Shared in Visual Basic) methods for querying objects that implement <see cref="IEnumerable{T}"/>. 
    /// This class provides alternate names, for those familiar with functional programming, for the methods already defined in LINQ.
    /// </summary>
    /// <remarks>
    /// The source of each method includes the Haskell version as a comment at the end.
    /// Some methods are not safe to use with mutable collections. These cases are mentioned in the documentation for the method.
    /// The order of arguments has generally been inverted compared to the Haskell versions to facilitate method chaining.
    /// 
    /// See also the Remarks for <see cref="Enumerable"/>.
    /// </remarks>
    public static class Enumerable3 {
        #region Basic Functions

        /// <summary>
        /// Append two sequences. If the first one is infinite, the result is the first list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        /// <remarks>
        /// An alternate name for <see cref="Enumerable.Concat{TSource}"/>.
        /// This is the ++ operator in Haskell, but this is not available in C#.
        /// </remarks>
        public static IEnumerable<T> Append<T>(
            this IEnumerable<T> first, IEnumerable<T> second) {
            return first.Concat(second);

            //(++) :: [a] -> [a] -> [a]
            //[]     ++ ys = ys
            //(x:xs) ++ ys = x : xs ++ ys
        }

        /// <summary>
        /// Extract the first element of a sequence, which must be non-empty.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sequence"></param>
        /// <returns></returns>
        /// <remarks>
        /// An alternate name for <see cref="Enumerable.First(IEnumerable{T})"/>.
        /// </remarks>
        public static T Head<T>(this IEnumerable<T> sequence) {
            try {
                return sequence.First();
            }
            catch (InvalidOperationException e) {
                throw new EmptySequenceException("head", e);
            }

            //head                    :: [a] -> a
            //head (x:_)              =  x
            //head []                 =  badHead
            //
            //badHead = errorEmptyList "head"
        }

        /// <summary>
        /// Returns the length of a finite sequence.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sequence">The sequence.</param>
        /// <returns></returns>
        /// <remarks>
        /// An alternate name for <see cref="Enumerable.Count(IEnumerable{T})"/>.
        /// </remarks>
        public static int Length<T>(this IEnumerable<T> sequence) {
            return sequence.Count();

            //length                  :: [a] -> Int
            //length l                =  len l 0#
            //  where
            //    len :: [a] -> Int# -> Int
            //    len []     a# = I# a#
            //    len (_:xs) a# = len xs (a# +# 1#)
        }

        #endregion

        #region List Transformations

        /// <summary>
        /// Applies the specified function to each element of the specified sequence.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TR">The type of the result.</typeparam>
        /// <param name="sequence">The sequence.</param>
        /// <param name="func">The function.</param>
        /// <returns></returns>
        /// <remarks>
        /// An alternate name for <see cref="Enumerable.Select(IEnumerable{T},Func{T,TResult})"/>.
        /// </remarks>
        public static IEnumerable<TR> Map<T, TR>(
            this IEnumerable<T> sequence, Func<T, TR> func) {
            return sequence.Select(func);

            //map :: (a -> b) -> [a] -> [b]
            //map _ []     = []
            //map f (x:xs) = f x : map f xs
        }

        #endregion

        #region Folds

        /// <summary>
        /// Reduces the sequence from left to right using the specified binary function
        /// starting with the specified accumulator value.
        /// In Haskell notation:
        /// <c>
        /// foldl f z [x1, x2, ..., xn] == (...((z `f` x1) `f` x2) `f`...) `f` xn
        /// </c>
        /// </summary>
        /// <typeparam name="T">The type of the elements in the sequence.</typeparam>
        /// <typeparam name="TAcc">The type of the accumulator.</typeparam>
        /// <param name="sequence">The sequence to fold.</param>
        /// <param name="func">The binary function.</param>
        /// <param name="initialAcc">The initial value of the accumulator.</param>
        /// <returns>The final accumulator value.</returns>
        /// <remarks>
        /// An alternate name for <see cref="Enumerable.Aggregate{TSource,TAccumulate}"/>.
        /// </remarks>
        public static TAcc FoldLeft<T, TAcc>(
            this IEnumerable<T> sequence, TAcc initialAcc,
            Func<TAcc, T, TAcc> func) {
            return sequence.Aggregate(initialAcc, func);
        }

        /// <summary>
        /// Reduces the sequence from left to right using the specified binary function and
        /// starting with the first element of the sequence.
        /// In Haskell notation:
        /// <c>
        /// foldl1 f [x1, x2, ..., xn] == (...(x1 `f` x2) `f`...) `f` xn
        /// </c>
        /// </summary>
        /// <typeparam name="T">The type of the elements in the sequence.</typeparam>
        /// <param name="sequence">The sequence to fold.</param>
        /// <param name="func">The binary function.</param>
        /// <returns>The final accumulator value.</returns>
        /// <remarks>
        /// An alternate name for <see cref="Enumerable.Aggregate{TSource}"/>.
        /// </remarks>
        public static T FoldLeft<T>(this IEnumerable<T> sequence,
                                    Func<T, T, T> func) {
            return sequence.Aggregate(func);
        }

        #endregion

        #region Searching lists

        /// <summary>
        /// Filters a sequence of values based on a predicate.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sequence">The sequence.</param>
        /// <param name="predicate">The predicate.</param>
        /// <returns></returns>
        /// <remarks>
        /// An alternate name for <see cref="Enumerable.Where(IEnumerable{T},Func{T,bool})"/>.
        /// </remarks>
        public static IEnumerable<T> Filter<T>(
            this IEnumerable<T> sequence, Func<T, bool> predicate) {
            return sequence.Where(predicate);
        }

        #endregion
    }
}