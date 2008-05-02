/* (C) Alexey Romanov 2008 */

using System;

namespace FP {
    /// <summary>
    /// A delegate describing a continuation which does <paramref name="computation"> given a value of
    /// the type <typeparamref name="T"/> and returns a result of the type <typeparamref name="T"/>.
    /// </summary>
    public delegate R Continuation<T, R>(Func<T, R> computation);

    /// <summary>
    /// Static methods for working with <see cref="Continuation{T,R}"/>.
    /// </summary>
    public static class Continuations {
        /// <summary>
        /// Given any function from <typeparamref name="T"/> to <typeparamref name="R"/>,
        /// calls it on the <see cref="arg"/>.
        /// </summary>
        public static Continuation<T, R> FromArgument<T, R>(T arg) {
            return Wrap<T, R>(arg);
        }

        /// <summary>
        /// Given any function from <typeparamref name="T"/> to <typeparamref name="R"/>,
        /// calls it on the <see cref="arg"/>.
        /// </summary>
        public static Continuation<T, R> Wrap<T, R>(T arg) {
            return comp => comp(arg);
        }

        /// <summary>
        /// Runs the specified continuation.
        /// </summary>
        /// <param name="continuation">The continuation.</param>
        /// <param name="final">The final computation.</param>
        /// <returns></returns>
        public static R Run<T, R>(this Continuation<T, R> continuation, Func<T, R> final) {
            return continuation(final);
        }

        /// <summary>
        /// Maps the specified function over the specified continuation.
        /// </summary>
        /// <param name="continuation">The continuation.</param>
        /// <param name="func">The function.</param>
        /// <returns></returns>
        public static Continuation<T, R> Map<T, R>(this Continuation<T, R> continuation, Func<R, R> func) {
            return comp => func(continuation(comp));
        }
    }
}