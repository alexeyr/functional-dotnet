/* (C) Alexey Romanov 2008 */

using System;
using System.Collections.Generic;

namespace FP.Collections.Immutable {
    /// <summary>
    /// A tuple with two elements.
    /// </summary>
    /// <typeparam name="T1">The type of the first element.</typeparam>
    /// <typeparam name="T2">The type of the second element.</typeparam>
    [Serializable]
    public struct Pair<T1, T2> : IEquatable<Pair<T1,T2>> {
        private readonly T1 _first;
        private readonly T2 _second;

        /// <summary>
        /// Initializes a new instance of the <see cref="Pair{T1,T2}"/> struct.
        /// </summary>
        /// <param name="first">The first element of the tuple.</param>
        /// <param name="second">The second element of the tuple.</param>
        public Pair(T1 first, T2 second) {
            _first = first;
            _second = second;
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

        public static implicit operator KeyValuePair<T1, T2>(
            Pair<T1, T2> pair) {
            return new KeyValuePair<T1, T2>(pair._first, pair._second);
        }

        public static implicit operator Pair<T1, T2>(
            KeyValuePair<T1, T2> pair) {
            return new Pair<T1, T2>(pair.Key, pair.Value);
        }

        ///<summary>
        ///Indicates whether the current object is equal to another object of the same type.
        ///</summary>
        ///
        ///<returns>
        ///true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
        ///</returns>
        ///
        ///<param name="other">An object to compare with this object.</param>
        public bool Equals(Pair<T1, T2> other) {
            return _first.Equals(other.First) && _second.Equals(other.Second);
        }

        public static bool operator==(Pair<T1,T2> one, Pair<T1,T2> other) {
            return one.Equals(other);
        }

        public static bool operator !=(Pair<T1, T2> one, Pair<T1, T2> other) {
            return !(one.Equals(other));
        }

        public override bool Equals(object obj) {
            if (!(obj is Pair<T1, T2>)) return false;
            return Equals((Pair<T1, T2>) obj);
        }

        public override int GetHashCode() {
            return (_first != null ? _first.GetHashCode() : 0) + 29*(_second != null ? _second.GetHashCode() : 0);
        }
    }

    public static class Pair {
        /// <summary>
        /// Creates a <see cref="Pair{T1,T2}"/> with the specified elements.
        /// Provided to make use of generic parameter inference.
        /// </summary>
        /// <typeparam name="T1">The type of the 1.</typeparam>
        /// <typeparam name="T2">The type of the 2.</typeparam>
        /// <param name="first">The first element of the tuple.</param>
        /// <param name="second">The second element of the tuple.</param>
        /// <returns></returns>
        public static Pair<T1,T2> New<T1, T2>(T1 first, T2 second) {
            return new Pair<T1, T2>(first, second);
        }
    }
}