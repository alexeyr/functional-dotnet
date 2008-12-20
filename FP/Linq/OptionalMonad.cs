/*
* OptionalMonad.cs is part of functional-dotnet project
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

using System;
using FP.Core;

namespace FP.Linq {
    /// <summary>
    /// Implements query pattern on <see cref="Optional{T}"/>. Makes <see cref="Optional{T}"/> a monad.
    /// </summary>
    public static class OptionalMonad {
        public static Optional<T> Where<T>(this Optional<T> optional, Func<T, bool> function) {
            return optional.MapOrElse(function, false) ? optional : Optional<T>.None;
        }

        public static Optional<T2> Select<T1, T2>(this Optional<T1> optional, Func<T1, T2> function) {
            return optional.Map(function);
        }

        public static Optional<T3> SelectMany<T1, T2, T3>(this Optional<T1> optional, Func<T1, Optional<T2>> function, Func<T1, T2, T3> combiner) {
            return optional.SelectMany(x => function(x).Select(y => combiner(x, y)));
        }

        public static Optional<T2> SelectMany<T1, T2>(this Optional<T1> optional, Func<T1, Optional<T2>> function) {
            return optional.MapOrElse(function, Optional<T2>.None);
        }
    }
}
