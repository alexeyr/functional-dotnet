using System;
using System.Linq;
using FP;
using FP.Collections.Immutable;
using Xunit;
using XunitExtensions;

namespace FPTests {
    public class SequenceTests {
        private readonly RandomAccessSequence<int> _empty = RandomAccessSequence.Empty<int>();
        private readonly int[] _testData;
        private readonly RandomAccessSequence<int> _seq;
        private const int N = 10000;
        private readonly Random _rnd = new Random();

        public SequenceTests() {
            _testData = new int[N];
            for (int i = 0; i < N; i++)
                _testData[i] = _rnd.Next();
            _seq = RandomAccessSequence.FromEnumerable(_testData);
        }

        [Fact]
        public void IsEmptyWorks() {
            Assert.True(_empty.IsEmpty);
            Assert.False(_seq.IsEmpty);
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
            Assert2.SequenceEqual(_testData, seq);
        }
        [Fact]
        public void PrependWorks() {
            var seq = _empty;
            foreach (var i in _testData)
                seq = seq.Prepend(i);
            Assert2.SequenceEqual(_testData.Reverse(), seq);
        }

        [Fact]
        public void SplitAtWorks() {
            for (int i = 1; i < 50; i++) {
                var split = _seq.SplitAt(i);
                Assert2.SequenceEqual(_seq.Take(i), split.First);
                Assert2.SequenceEqual(_seq.Skip(i), split.Second);
            }
        }

        [Fact]
        public void IndexingWorks() {
            for (int i = 0; i < N; i++) {
                Assert.Equal(_testData[i], _seq[i]);
            }
        }

        [Fact]
        public void SetAt1() {
            Assert.Equal(15, _seq.SetAt(5, 15)[5]);
        }

        [Fact]
        public void SetAtWorks() {
            var seq = _seq;
            for (int i = 0; i < 100; i++) {
                seq = seq.SetAt(i, i + 10);
                Assert.Equal(i + 10, seq[i]);
            }
            for (int i = 0; i < 100; i++)
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
            for (int i = 1; i <= N; i++) {
                Assert.Equal(_testData[N - i], seq.Last);
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

        [Fact]
        public void InsertAtAndRemoveAtWork() {
            int i1 = _rnd.Next(N);
            int i2 = _rnd.Next(i1);
            int i3 = _rnd.Next(i2);
            var seq = _seq.InsertAt(i3, 0).InsertAt(i2, 0).InsertAt(i1, 0);
            Assert.Equal(0, seq[i3]);
            Assert.Equal(0, seq[i2]);
            Assert.Equal(0, seq[i1]);
            Assert2.SequenceEqual(_testData, seq.RemoveAt(i3).RemoveAt(i2 - 1).RemoveAt(i1 - 2));
        }
    }
}