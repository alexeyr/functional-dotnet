using System.Collections.Generic;
using System.Linq;
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
                Assert2.SequenceEqual(_seq.Take(i), split.First);
                Assert2.SequenceEqual(_seq.Skip(i), split.Second);
            }
        }

        [Fact]
        public void IndexingWorks() {
            for (int i = 0; i < 10; i++) {
                Assert.Equal(i, _seq[i]);
            }
        }

        [Fact]
        public void SetAtWorks() {
            var seq = _seq;
            for (int i = 0; i < 10; i++) {
                seq = seq.SetAt(i, i + 10);
                Assert.Equal(i + 10, seq[i]);
            }
            for (int i = 0; i < 10; i++)
                Assert.Equal(i + 10, seq[i]);
        }

        [Fact]
        public void HeadAndTailWork() {
            Assert.Throws<EmptySequenceException>(() => { var a = _empty.Head; });
            Assert.Throws<EmptySequenceException>(() => { var a = _empty.Tail; });
            Assert.Throws<EmptySequenceException>(() => { var a = _empty.Init; });
            Assert.Throws<EmptySequenceException>(() => { var a = _empty.Last; });
            var seq = _seq;
            foreach (var i in _testData) {
                Assert.Equal(i, seq.Head);
                seq = seq.Tail;
            }
            Assert2.SequenceEqual(_empty, seq);

            seq = _seq;
            foreach (var i in _testData) {
                Assert.Equal(10 - i, seq.Last);
                seq = seq.Init;
            }
            Assert2.SequenceEqual(_empty, seq);
        }

        [Fact]
        public void ConcatWorks() {
            Assert2.SequenceEqual(_testData.Concat(_testData), _seq.Concat(_seq));
            Assert2.SequenceEqual(_testData, _seq.Concat(_empty));
            Assert2.SequenceEqual(_testData, _empty.Concat(_seq));
        }
    }
}