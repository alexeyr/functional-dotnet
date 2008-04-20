/* (C) Alexey Romanov 2008 */

using System;
using FP.Collections.Immutable;

namespace FP.Linq {
    /// <summary>
    /// Implements query pattern on <see cref="Result{T}"/>. Makes <see cref="Result{T}"/> a monad.
    /// </summary>
    public static class ResultMonad {
        public static Result<T> Where<T>(this Result<T> result, Func<T, bool> function) {
            bool passes = result.Match(function, x => true);
            return passes ? result : Result.Failure<T>("Unsuitable result");
        }

        public static Result<T2> Select<T1, T2>(this Result<T1> result, Func<T1, T2> function) {
            return result.SelectMany(function.Compose<T1, T2, Result<T2>>(Result.Success));
        }

        public static Result<T3> SelectMany<T1, T2, T3>(this Result<T1> result, Func<T1, Result<T2>> function, Func<T1, T2, T3> combiner) {
            return result.SelectMany(x => function(x).Select(y => combiner(x, y)));
        }

        public static Result<T2> SelectMany<T1, T2>(this Result<T1> result, Func<T1, Result<T2>> function) {
            return result.Match(function, Result.Failure<T2>);
        }
    }
}
