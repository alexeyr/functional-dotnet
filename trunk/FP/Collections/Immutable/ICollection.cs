using System.Collections.Generic;

namespace FP.Collections.Immutable {
    /// <summary>
    /// Represents a collection.
    /// </summary>
    /// <typeparam name="T">The type of elements.</typeparam>
    public interface ICollection<T> : IEnumerable<T> {
        /// <summary>
        /// Gets a value indicating whether this collection is empty.
        /// </summary>
        /// <value><c>true</c>.</value>
        bool IsEmpty { get; }
    }
}