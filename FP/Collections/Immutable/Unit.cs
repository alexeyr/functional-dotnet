/* (C) Alexey Romanov 2008 */

using System;

namespace FP.Collections.Immutable {
    /// <summary>
    /// A type with a single value.
    /// </summary>
    public struct Unit : IEquatable<Unit> {
        ///<summary>
        ///Indicates whether the current object is equal to another object of the same type.
        ///</summary>
        ///
        ///<returns>
        ///true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
        ///</returns>
        ///
        ///<param name="other">An object to compare with this object.</param>
        public bool Equals(Unit other) {
            return true;
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
            return obj is Unit;
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>
        /// A 32-bit signed integer that is the hash code for this instance.
        /// </returns>
        public override int GetHashCode() {
            return 0;
        }

        /// <summary>
        /// Implements the operator ==.
        /// </summary>
        /// <param name="u1">The u1.</param>
        /// <param name="u2">The u2.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(Unit u1, Unit u2) {
            return true;
        }

        /// <summary>
        /// Implements the operator !=.
        /// </summary>
        /// <param name="u1">The u1.</param>
        /// <param name="u2">The u2.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(Unit u1, Unit u2) {
            return false;
        }

        /// <summary>
        /// The only value for <see cref="Unit"/>.
        /// </summary>
        public static readonly Unit _ = default(Unit);
    }
}
