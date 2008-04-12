/* (C) Alexey Romanov 2008 */

using System;

namespace FP {
    /// <summary>
    /// Thrown by the methods which require their argument to be non-empty
    /// when called on an empty sequence.
    /// </summary>
    public class EmptySequenceException : InvalidOperationException {
        public EmptySequenceException(string message, Exception innerException)
            : base(message, innerException) {}

        public EmptySequenceException(string message) : base(message) {}

        public EmptySequenceException() {}
    }
}