/* (C) Alexey Romanov 2008 */

using System;
using FP;

namespace FP.Linq {
    /// <summary>
    /// Implements query pattern on <see cref="Continuation{T,R}"/>. Makes <see cref="Continuation{T,R}"/> a monad.
    /// </summary>
    public static class ContinuationMonad {
        public static Continuation<T2, R> Select<T1, T2, R>(this Continuation<T1, R> cont, Func<T1, T2> function) {
            return cont.SelectMany(function.Compose(t2 => Continuations.FromArgument<T2, R>(t2)));
        }

        public static Continuation<T3, R> SelectMany<T1, T2, T3, R>(this Continuation<T1, R> cont, Func<T1, Continuation<T2, R>> function, Func<T1, T2, T3> combiner) {
            return cont.SelectMany(x => function(x).Select(y => combiner(x, y)));
        }

        public static Continuation<T2, R> SelectMany<T1, T2, R>(this Continuation<T1, R> cont, Func<T1, Continuation<T2, R>> function) {
            return comp => cont(function.Compose<T1, T2, R>(comp));
        }
    }
}
