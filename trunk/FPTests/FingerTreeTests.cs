using System.Collections.Generic;
using FP;
using FP.Collections.Immutable;
using Xunit;
using XunitExtensions;

namespace FPTests {
    public class SequenceTests {
        private readonly Sequence<int> _empty = new Sequence<int>();
        private readonly IEnumerable<int> _testData = Ints.Range(0, 10);

        [Fact]
        public void EnumerateWorks() {
            var seq = new Sequence<int>(_testData);
            Assert2.SequenceEqual(_testData, seq);
        }

        [Fact]
        public void AppendWorks() {
            var seq = _empty;
            foreach (var i in _testData) {
                seq = seq.Append(i);
                Assert2.SequenceEqual(Ints.Range(0, i), seq);
            }
        }
    }
}