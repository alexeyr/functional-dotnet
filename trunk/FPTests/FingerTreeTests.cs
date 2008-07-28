using System.Collections.Generic;
using FP;
using FP.Collections.Immutable;
using Xunit;
using XunitExtensions;

namespace FPTests {
    public class SequenceTests {
        private readonly RandomAccessSequence<int> _empty = new RandomAccessSequence<int>();
        private readonly IEnumerable<int> _testData = Ints.Range(0, 10);
        private readonly RandomAccessSequence<int> _seq;
        public SequenceTests() {
            _seq = new RandomAccessSequence<int>(_testData);
        }

        [Fact]
        public void EnumerateWorks() {
            Assert2.SequenceEqual(_testData, _seq);
        }

        [Fact]
        public void AppendWorks() {
            var seq = _empty;
            foreach (var i in _testData)
                seq = seq.Append(i);
            Assert2.SequenceEqual(Ints.Range(0, 10), seq);
        }
        [Fact]
        public void PrependWorks() {
            var seq = _empty;
            foreach (var i in _testData)
                seq = seq.Prepend(i);
            Assert2.SequenceEqual(Ints.Range(10, 0, -1), seq);
        }

        [Fact]
        public void SplitAtWorks() {
            for (int i = 1; i < 10; i++) {
                var split = _seq.SplitAt(i);
                Assert2.SequenceEqual(Ints.Range(0, i - 1), split.First);
                Assert2.SequenceEqual(Ints.Range(i, 10), split.Second);
            }
        }
    }
}