#region License
/*
* Quadruple.cs is part of functional-dotnet project
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

namespace FP.Core {
    /// <summary>
    /// A tuple with four elements.
    /// </summary>
    /// <typeparam name="T1">The type of the first element.</typeparam>
    /// <typeparam name="T2">The type of the second element.</typeparam>
    /// <typeparam name="T3">The type of the third element.</typeparam>
    /// <typeparam name="T4">The type of the fourth element.</typeparam>
    /// <seealso cref="Quadruple"/>
    [Serializable]
    public struct Quadruple<T1, T2, T3, T4> : IEquatable<Quadruple<T1, T2, T3, T4>> {
        private readonly T1 _first;
        private readonly T2 _second;
        private readonly T3 _third;
        private readonly T4 _fourth;

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="quadruple1">The quadruple1.</param>
        /// <param name="quadruple2">The quadruple2.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(Quadruple<T1, T2, T3, T4> quadruple1, Quadruple<T1, T2, T3, T4> quadruple2) {
            return !quadruple1.Equals(quadruple2);
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="quadruple1">The quadruple1.</param>
        /// <param name="quadruple2">The quadruple2.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(Quadruple<T1, T2, T3, T4> quadruple1, Quadruple<T1, T2, T3, T4> quadruple2) {
            return quadruple1.Equals(quadruple2);
        }

        ///<summary>
        ///Indicates whether the current object is equal to another object of the same type.
        ///</summary>
        ///
        ///<returns>
        ///true if the current object is equal to the <paramref name="other"/> parameter; otherwise, false.
        ///</returns>
        ///
        ///<param name="other">An object to compare with this object.</param>
        public bool Equals(Quadruple<T1, T2, T3, T4> other) {
            return Equals(_first, other._first) && Equals(_second, other._second) &&
                   Equals(_third, other._third) && Equals(_fourth, other._fourth);
        }

        ///<summary>
        ///Indicates whether this instance and a specified object are equal.
        ///</summary>
        ///
        ///<returns>
        ///true if <paramref name="obj" /> and this instance are the same type and represent the same value; otherwise, false.
        ///</returns>
        ///
        ///<param name="obj">Another object to compare to. </param><filterpriority>2</filterpriority>
        public override bool Equals(object obj) {
            if (!(obj is Quadruple<T1, T2, T3, T4>)) return false;
            return Equals((Quadruple<T1, T2, T3, T4>) obj);
        }

        ///<summary>
        ///Returns the hash code for this instance.
        ///</summary>
        ///
        ///<returns>
        ///A 32-bit signed integer that is the hash code for this instance.
        ///</returns>
        ///<filterpriority>2</filterpriority>
        public override int GetHashCode() {
            int result = _first != null ? _first.GetHashCode() : 0;
            result = 29*result + (_second != null ? _second.GetHashCode() : 0);
            result = 29*result + (_third != null ? _third.GetHashCode() : 0);
            result = 29*result + (_fourth != null ? _fourth.GetHashCode() : 0);
            return result;
        }

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

    /// <summary>
    /// A static class which contains a method to create <see cref="Quadruple{T1,T2,T3,T4}"/>.
    /// </summary>
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