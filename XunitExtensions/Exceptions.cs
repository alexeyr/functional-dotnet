/* (C) Alexey Romanov 2008 */

using Xunit;

namespace XunitExtensions {
    public class SequenceEqualException : AssertException {
        public SequenceEqualException(int index) : 
            base(string.Format("Sequences are different at index {0}", index)) {}

        public SequenceEqualException(int index, object expected, object actual) :
            base(string.Format("Sequences are different at index {0}: expected {1} but was {2}", 
            index, expected, actual)) {}
    }

    public class SequenceEquivalentException : AssertException {
        public SequenceEquivalentException() :
            base("Sequences do not have same elements") { }
    }

    public class SubsetException : AssertException {
        public SubsetException() :
            base("One sequence is not a subset of another") { }
    }

    public class NotException : AssertException {
        public NotException(string userMessage) : base(userMessage) {}
    }
}