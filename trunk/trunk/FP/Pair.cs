using System.Collections.Generic;

namespace FP
{
    /// <summary>
    /// A tuple with two elements.
    /// </summary>
    /// <typeparam name="T1">The type of the first element.</typeparam>
    /// <typeparam name="T2">The type of the second element.</typeparam>
    public struct Pair<T1, T2>
    {
        private readonly T1 _first;
        private readonly T2 _second;

        /// <summary>
        /// Gets the first element of the tuple.
        /// </summary>
        /// <value>The first element of the tuple.</value>
        public T1 First
        {
            get { return _first; }
        }

        /// <summary>
        /// Gets the second element of the tuple.
        /// </summary>
        /// <value>The second element of the tuple.</value>
        public T2 Second
        {
            get { return _second; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FP.Pair{T1,T2}"/> struct.
        /// </summary>
        /// <param name="first">The first element of the tuple.</param>
        /// <param name="second">The second element of the tuple.</param>
        public Pair(T1 first, T2 second)
        {
            _first = first;
            _second = second;
        }

        public static implicit operator KeyValuePair<T1, T2>(Pair<T1,T2> pair)
        {
            return new KeyValuePair<T1, T2>(pair._first, pair._second);
        }

        public static implicit operator Pair<T1, T2>(KeyValuePair<T1, T2> pair)
        {
            return new Pair<T1, T2>(pair.Key, pair.Value);
        }
    }

    public static class Pair
    {
        /// <summary>
        /// Creates a <see cref="Pair{T1,T2}"/> with the specified elements.
        /// Provided to make use of generic parameter inference.
        /// </summary>
        /// <typeparam name="T1">The type of the 1.</typeparam>
        /// <typeparam name="T2">The type of the 2.</typeparam>
        /// <param name="first">The first element of the tuple.</param>
        /// <param name="second">The second element of the tuple.</param>
        /// <returns></returns>
        public static Pair<T1, T2> New<T1, T2>(T1 first, T2 second)
        {
            return new Pair<T1, T2>(first, second);
        }        
    }
}