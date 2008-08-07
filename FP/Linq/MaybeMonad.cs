#region License
/*
* MaybeMonad.cs is part of functional-dotnet project
* 
* Copyright (c) 2008 Alexey Romanov
* All rights reserved.
*
* This source file is available under The New BSD License.
* See license.txt file for more information.
* 
* THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND 
* CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, 
* INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF 
* MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED.
*/
#endregion

using System;
using FP.Core;

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
