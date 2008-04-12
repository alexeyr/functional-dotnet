/* (C) Alexey Romanov 2008 */

using System;

namespace FP.Collections.Immutable {
    /// <summary>
    /// A tuple with three elements.
    /// </summary>
    /// <typeparam name="T1">The type of the first element.</typeparam>
    /// <typeparam name="T2">The type of the second element.</typeparam>
    /// <typeparam name="T3">The type of the third element.</typeparam>
    [Serializable]
    public struct Triple<T1, T2, T3> {
        private readonly T1 _first;
        private readonly T2 _second;
        private readonly T3 _third;

        /// <summary>
        /// Initializes a new instance of the <see cref="Triple&lt;T1, T2, T3&gt;"/> struct.
        /// </summary>
        /// <param name="first">The first element of the tuple.</param>
        /// <param name="second">The second element of the tuple.</param>
        /// <param name="third">The third element of the tuple.</param>
        public Triple(T1 first, T2 second, T3 third) {
            _first = first;
            _second = second;
            _third = third;
        }

        /// <summary>
        /// Gets the first element of the tuple.
        /// </summary>
        /// <value>The first element of the tuple.</value>
        public T1 First {
            get { return _first; }
        }

        /// <summary>
        /// Gets the second element of the tuple.
        /// </summary>
        /// <value>The second element of the tuple.</value>
        public T2 Second {
            get { return _second; }
        }

        /// <summary>
        /// Gets the third element of the tuple.
        /// </summary>
        /// <value>The third element of the tuple.</value>
        public T3 Third {
            get { return _third; }
        }
    }

    public static class Triple {
        /// <summary>
        /// Creates a <see cref="Triple{T1,T2,T3}"/> with the specified elements.
        /// Provided to make use of generic parameter inference.
        /// </summary>
        /// <typeparam name="T1">The type of the 1.</typeparam>
        /// <typeparam name="T2">The type of the 2.</typeparam>
        /// <typeparam name="T3">The type of the 3.</typeparam>
        /// <param name="first">The first element of the tuple.</param>
        /// <param name="second">The second element of the tuple.</param>
        /// <param name="third">The third element of the tuple.</param>
        /// <returns></returns>
        public static Triple<T1,T2,T3> New<T1, T2, T3>(T1 first, T2 second, T3 third) {
            return new Triple<T1, T2, T3>(first, second, third);
        }
    }
}