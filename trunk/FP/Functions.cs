/* (C) Alexey Romanov 2008 */

using System;

namespace FP {
    /// <summary>
    /// Provides a set of static (Shared in Visual Basic) methods for manipulating functions.
    /// </summary>
    public static class Functions {
        /// <summary>
        /// Composes two fuctions left to right. This is the same as <c>flip (.)</c> in Haskell.
        /// </summary>
        /// <param name="func1">First function.</param>
        /// <param name="func2">Second function.</param>
        /// <returns>The composition of functions.</returns>
        public static Func<T1, T3> Compose<T1, T2, T3>(
            this Func<T1, T2> func1, Func<T2, T3> func2) {
            return x => func2(func1(x));
        }

        /// <summary>
        /// Flips the order of arguments of a function.
        /// </summary>
        /// <param name="func">The function.</param>
        /// <returns>The fuction with flipped arguments.</returns>
        public static Func<T2, T1, TR> Flip<T1, T2, TR>(
            this Func<T1, T2, TR> func) {
            return (y, x) => func(x, y);
        }

        /// <summary>
        /// Does nothing.
        /// </summary>
        public static readonly Action DoNothing = () => { };
        
        /// <summary>
        /// The identity function.
        /// </summary>
        /// <typeparam name="T">The type of argument and result.</typeparam>
        public static Func<T, T> Id<T>() {
            return x => x;
        }
    }
}