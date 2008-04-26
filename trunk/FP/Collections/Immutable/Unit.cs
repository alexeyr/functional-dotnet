/* (C) Alexey Romanov 2008 */

using System;

namespace FP.Collections.Immutable {
    /// <summary>
    /// A type with a single value.
    /// </summary>
    public struct Unit : IEquatable<Unit> {
        public bool Equals(Unit unit) {
            return true;
        }

        public override bool Equals(object obj) {
            if (!(obj is Unit)) return false;
            return Equals((Unit) obj);
        }

        public override int GetHashCode() {
            return 0;
        }

        public static bool operator ==(Unit u1, Unit u2) {
            return true;
        }

        public static bool operator !=(Unit u1, Unit u2) {
            return false;
        }

        public static readonly Unit _ = default(Unit);
    }
}
