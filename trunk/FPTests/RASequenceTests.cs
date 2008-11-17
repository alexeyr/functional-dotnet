using System.Linq;
using FP.Collections.Immutable;
using FP.Core;
using Microsoft.Pex.Framework;
using Xunit;
using XunitExtensions;

namespace FPTests {
    public partial class RASequenceTests {
        private readonly RandomAccessSequence<int> _empty = RandomAccessSequence.Empty<int>();

        [PexMethod]
        [PexGenericArguments(new[] { typeof(int) })]
        public void Test_Creation<T>([PexAssumeNotNull] T[] arr) {
            var seq = RandomAccessSequence.FromEnumerable(arr);
            PexAssume.IsTrue(seq.Invariant);
        }

        [PexMethod]
        [PexGenericArguments(new[] { typeof(int) })]
        public void Test_IsEmptyWorksCorrectly<T>(T[] arr) {
            Assert.True(_empty.IsEmpty);
            PexAssume.IsNotNullOrEmpty(arr);
            Assert.False(RandomAccessSequence.FromEnumerable(arr).IsEmpty);
        }

        [PexMethod]
        [PexGenericArguments(new[] { typeof(int) })]
        public void Test_Enumerate<T>([PexAssumeNotNull] T[] arr) {
            Assert2.SequenceEqual(arr, RandomAccessSequence.FromEnumerable(arr));
        }

        [Fact]
        [PexGenericArguments(new[] { typeof(int) })]
        public void Test_Append<T>([PexAssumeNotNull] T[] arr) {
            var seq = RandomAccessSequence<T>.Empty;
            foreach (var i in arr)
                seq = seq | i;
            Assert2.SequenceEqual(arr, seq);
        }

        [Fact]
        [PexGenericArguments(new[] { typeof(int) })]
        public void Test_Prepend<T>([PexAssumeNotNull] T[] arr) {
            var seq = RandomAccessSequence<T>.Empty;
            foreach (var i in arr)
                seq = i | seq;
            Assert2.SequenceEqual(arr.Reverse(), seq);
        }

        [PexMethod]
        [PexGenericArguments(new[] {typeof(int)})]
        public void Test_SplitAt<T>([PexAssumeNotNull] T[] arr, int i) {
            var seq = RandomAccessSequence.FromEnumerable(arr);
            PexAssume.IsTrue(i >= 0);
            PexAssume.IsTrue(i < seq.Count);
            var split = seq.SplitAt(i);
            Assert2.SequenceEqual(seq.Take(i), split.Item1);
            Assert2.SequenceEqual(seq.Skip(i), split.Item2);
        }

        [PexMethod]
        [PexGenericArguments(new[] { typeof(int) })]
        public void Test_Indexing<T>([PexAssumeNotNull] T[] arr, int i) {
            var seq = RandomAccessSequence.FromEnumerable(arr);
            PexAssume.IsTrue(i >= 0);
            PexAssume.IsTrue(i < arr.Length);
            Assert.Equal(arr[i], seq[i]);
        }

        [PexMethod]
        [PexGenericArguments(new[] { typeof(int) })]
        public void Test_SetAt<T>([PexAssumeNotNull] T[] arr, int i, T newValue) {
            var seq = RandomAccessSequence.FromEnumerable(arr);
            PexAssume.IsTrue(i >= 0);
            PexAssume.IsTrue(i < seq.Count);
            var newSeq = seq.SetAt(i, newValue);
            foreach (var indexElement in newSeq.WithIndex()) {
                int index = indexElement.Item1;
                T value = indexElement.Item2;
                if (index != i)
                    Assert.Equal(seq[index], value);
                else
                    Assert.Equal(newValue, value);
            }
        }

        [Fact]
        public void Test_HeadAndTailOfEmptySequence() {
            Assert.Throws<EmptySequenceException>(() => { var a = _empty.Head; });
            Assert.Throws<EmptySequenceException>(() => { var a = _empty.Tail; });
            Assert.Throws<EmptySequenceException>(() => { var a = _empty.Init; });
            Assert.Throws<EmptySequenceException>(() => { var a = _empty.Last; });
        }

        [PexMethod(MaxBranches = 20000)]
        [PexGenericArguments(new[] { typeof(int) })]
        public void Test_HeadAndTailOfNonEmptySequence<T>([PexAssumeNotNull] T[] arr) {
            PexAssume.IsNotNullOrEmpty(arr);
            var seq0 = RandomAccessSequence.FromEnumerable(arr);
            var seq = seq0;
            foreach (var t in arr) {
                Assert.Equal(t, seq.Head);
                seq = seq.Tail;
            }
            Assert.Empty(seq);

            seq = seq0;
            foreach (var t in arr.Reverse()) {
                Assert.Equal(t, seq.Last);
                seq = seq.Init;
            }
            Assert.Empty(seq);
        }

        [PexMethod]
        [PexGenericArguments(new[] { typeof(int) })]
        public void Test_ConcatWithEmpty<T>([PexAssumeNotNull] T[] arr) {
            var seq = RandomAccessSequence.FromEnumerable(arr);
            Assert2.SequenceEqual(seq.Concat(Enumerable.Empty<T>()), seq);
            Assert2.SequenceEqual(Enumerable.Empty<T>().Concat(seq), seq);
        }

        [PexMethod]
        [PexGenericArguments(new[] { typeof(int) })]
        public void Test_Concat<T>([PexAssumeNotNull] T[] arr1, [PexAssumeNotNull] T[] arr2) {
            var seq1 = RandomAccessSequence.FromEnumerable(arr1);
            var seq2 = RandomAccessSequence.FromEnumerable(arr2);
            PexAssume.IsTrue(seq1.Invariant);
            PexAssume.IsTrue(seq2.Invariant);
            Assert2.SequenceEqual(seq1.AsEnumerable().Concat(seq2), seq1 + seq2);
        }

        [PexMethod]
        [PexGenericArguments(new[] { typeof(int) })]
        public void Test_InsertAt<T>([PexAssumeNotNull] T[] arr, int i, T newValue) {
            var seq = RandomAccessSequence.FromEnumerable(arr);
            PexAssume.IsTrue(i >= 0);
            PexAssume.IsTrue(i < seq.Count);
            var newSeq = seq.InsertAt(i, newValue);
            var splitOld = seq.SplitAt(i);
            var splitNew = newSeq.SplitAt(i);
            Assert2.SequenceEqual(splitOld.Item1, splitNew.Item1);
            Assert.Equal(newValue, splitNew.Item2.Head);
            Assert2.SequenceEqual(splitOld.Item2, splitNew.Item2.Tail);
        }

        [PexMethod]
        [PexGenericArguments(new[] { typeof(int) })]
        public void Test_Reverse<T>([PexAssumeNotNull] T[] arr) {
            var seq = RandomAccessSequence.FromEnumerable(arr);
            Assert2.SequenceEqual(seq.AsEnumerable().Reverse(), seq.Reverse());
        }
    }
}