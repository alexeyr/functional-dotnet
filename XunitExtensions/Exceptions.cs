/* (C) Alexey Romanov 2008 */

using Xunit;

namespace XunitExtensions {
    /// <summary>
    /// An exception thrown if sequences should be equal, but aren't.
    /// </summary>
    public class SequenceEqualException : AssertException {
        /// <summary>
        /// Initializes a new instance of the <see cref="SequenceEqualException"/> class.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="expected">The expected.</param>
        /// <param name="actual">The actual.</param>
        public SequenceEqualException(int index, object expected, object actual) :
            base(string.Format("Sequences are different at index {0}: expected {1} but was {2}", 
            index, expected, actual)) {}
    }

    /// <summary>
    /// An exception thrown if sequences should be equivalent, but aren't.
    /// </summary>
    public class SequenceEquivalentException : AssertException {
        /// <summary>
        /// Initializes a new instance of the <see cref="SequenceEquivalentException"/> class.
        /// </summary>
        public SequenceEquivalentException() :
            base("Sequences do not have same elements") { }
    }

    /// <summary>
    /// An exception thrown if one sequence should be a subset of another, but isn't.
    /// </summary>
    public class SubsetException : AssertException {
        /// <summary>
        /// Initializes a new instance of the <see cref="SubsetException"/> class.
        /// </summary>
        public SubsetException() :
            base("One sequence is not a subset of another") { }
    }

    /// <summary>
    /// An exception thrown if an assertion should fail, but succeeds.
    /// </summary>
    public class NotException : AssertException {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotException"/> class.
        /// </summary>
        /// <param name="userMessage">The user message to be displayed</param>
        public NotException(string userMessage) : base(userMessage) {}
    }
}
