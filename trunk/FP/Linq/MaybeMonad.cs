/* (C) Alexey Romanov 2008 */

using System;
using FP.Collections.Immutable;

namespace FP.Linq {
    /// <summary>
    /// Implements query pattern on <see cref="Maybe{T}"/>. Makes <see cref="Maybe{T}"/> a monad.
    /// </summary>
    public static class MaybeMonad {
        public static Maybe<T> Where<T>(this Maybe<T> maybe, Func<T, bool> function) {
            return maybe.MapOrElse(function, false) ? maybe : Maybe<T>.Nothing;
        }

        public static Maybe<T2> Select<T1, T2>(this Maybe<T1> maybe, Func<T1, T2> function) {
            return maybe.Map(function);
        }

        public static Maybe<T3> SelectMany<T1, T2, T3>(this Maybe<T1> maybe, Func<T1, Maybe<T2>> function, Func<T1, T2, T3> combiner) {
            return maybe.SelectMany(x => function(x).Select(y => combiner(x, y)));
        }

        public static Maybe<T2> SelectMany<T1, T2>(this Maybe<T1> maybe, Func<T1, Maybe<T2>> function) {
            return maybe.MapOrElse(function, Maybe<T2>.Nothing);
        }
    }
}
