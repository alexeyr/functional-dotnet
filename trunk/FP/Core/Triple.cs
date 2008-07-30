/* (C) Alexey Romanov 2008 */

using System;

namespace FP.Core {
    /// <summary>
    /// A static class which contains a method to create <see cref="Tuple{T1,T2,T3}"/>.
    /// </summary>
    public static class Triple {
        /// <summary>
        /// Creates a <see cref="Tuple{T1,T2,T3}"/> with the specified elements.
        /// Provided to make use of generic parameter inference.
        /// </summary>
        /// <typeparam name="T1">The type of the 1.</typeparam>
        /// <typeparam name="T2">The type of the 2.</typeparam>
        /// <typeparam name="T3">The type of the 3.</typeparam>
        /// <param name="first">The first element of the tuple.</param>
        /// <param name="second">The second element of the tuple.</param>
        /// <param name="third">The third element of the tuple.</param>
        /// <returns></returns>
        public static Tuple<T1,T2,T3> New<T1, T2, T3>(T1 first, T2 second, T3 third) {
            return new Tuple<T1, T2, T3>(first, second, third);
        }
    }
}