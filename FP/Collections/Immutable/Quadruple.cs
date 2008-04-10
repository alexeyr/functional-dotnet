using System;

namespace FP.Collections.Immutable {
    /// <summary>
    /// A tuple with four elements.
    /// </summary>
    /// <typeparam name="T1">The type of the first element.</typeparam>
    /// <typeparam name="T2">The type of the second element.</typeparam>
    /// <typeparam name="T3">The type of the third element.</typeparam>
    /// <typeparam name="T4">The type of the fourth element.</typeparam>
    [Serializable]
    public struct Quadruple<T1, T2, T3, T4> {
        private readonly T1 _first;
        private readonly T4 _fourth;
        private readonly T2 _second;
        private readonly T3 _third;

        /// <summary>
        /// Initializes a new instance of the <see cref="Quadruple{T1, T2, T3, T4}"/> struct.
        /// </summary>
        /// <param name="first">The first element of the tuple.</param>
        /// <param name="second">The second element of the tuple.</param>
        /// <param name="third">The third element of the tuple.</param>
        /// <param name="fourth">The fourth element of the tuple.</param>
        public Quadruple(T1 first, T2 second, T3 third, T4 fourth) {
            _first = first;
            _second = second;
            _third = third;
            _fourth = fourth;
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

        /// <summary>
        /// Gets the fourth element of the tuple.
        /// </summary>
        /// <value>The fourth element of the tuple.</value>
        public T4 Fourth {
            get { return _fourth; }
        }
    }

    public static class Quadruple {
        /// <summary>
        /// Creates a <see cref="Quadruple{T1,T2,T3,T4}"/> with the specified elements.
        /// Provided to make use of generic parameter inference.
        /// </summary>
        /// <typeparam name="T1">The type of the 1.</typeparam>
        /// <typeparam name="T2">The type of the 2.</typeparam>
        /// <typeparam name="T3">The type of the 3.</typeparam>
        /// <typeparam name="T4">The type of the 4.</typeparam>
        /// <param name="first">The first element of the tuple.</param>
        /// <param name="second">The second element of the tuple.</param>
        /// <param name="third">The third element of the tuple.</param>
        /// <param name="fourth">The fourth element of the tuple.</param>
        /// <returns></returns>
        public static Quadruple<T1,T2,T3,T4> New<T1, T2, T3, T4>(
            T1 first, T2 second, T3 third, T4 fourth) {
            return new Quadruple<T1, T2, T3, T4>(first, second, third, fourth);
        }
    }
}