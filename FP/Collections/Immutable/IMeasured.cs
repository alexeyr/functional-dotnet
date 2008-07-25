using System;

namespace FP.Collections.Immutable {
    /// <summary>
    /// The type implementing the interface can be measured by elements of the type <typeparamref name="M"/>.
    /// <typeparamref name="M"/> must have a monoidal structure.
    /// </summary>
    /// <typeparam name="M"></typeparam>
    public interface IMeasured<M> {
        /// <summary>
        /// Gets the measure of the object.
        /// </summary>
        /// <value>The measure.</value>
        M Measure { get; }
    }
}