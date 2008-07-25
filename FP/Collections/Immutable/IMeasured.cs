using System;

namespace FP.Collections.Immutable {
    /// <summary>
    /// The type implementing the interface can be measured by elements of the type <typeparamref name="V"/>.
    /// </summary>
    /// <typeparam name="V"></typeparam>
    public interface IMeasured<V> {
        /// <summary>
        /// Gets the measure of the object.
        /// </summary>
        /// <value>The measure.</value>
        V Measure { get; }
    }
}