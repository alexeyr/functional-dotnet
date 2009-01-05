using System.Collections.Generic;

namespace FP.Collections {
    /// <summary>
    /// Represents a splitting of a structure of type <typeparamref name="FT"/> with elements
    /// of type <typeparamref name="T"/>
    /// </summary>
    internal struct Split<T, FT> {
        internal readonly FT Left;
        internal readonly T Middle;
        internal readonly FT Right;

        internal Split(FT left, T middle, FT right) {
            Left = left;
            Right = right;
            Middle = middle;
        }
    }
}