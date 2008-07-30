/* (C) Alexey Romanov 2008 */

using System;

namespace FP.Core {
    /// <summary>
    /// Thrown by the methods which require their argument to be non-empty
    /// when called on an empty sequence.
    /// </summary>
    public class EmptySequenceException : ArgumentException {
        /// <summary>
        /// Initializes a new instance of the <see cref="EmptySequenceException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public EmptySequenceException(string message, Exception innerException)
            : base(message, innerException) {}

        /// <summary>
        /// Initializes a new instance of the <see cref="EmptySequenceException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public EmptySequenceException(string message) : base(message) {}

        /// <summary>
        /// Initializes a new instance of the <see cref="EmptySequenceException"/> class.
        /// </summary>
        public EmptySequenceException() {}
    }
}