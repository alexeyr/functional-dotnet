using System.Linq;
using Xunit;
using FP;
using XunitExtensions;

namespace FPTests {
    public class EnumerableTests {
        [Fact]
        public void TailShouldThrowOnEmptySeq() {
            var emptyList = Enumerable.Empty<int>();
            Assert.Throws(typeof(EmptySequenceException), () => emptyList.Tail().FirstOrDefault());
        }

        [Fact]
        public void IntersperseTest() {
            Assert.Equal("a,b,c,d,e", "abcde".Intersperse(',').ToStringProper());
        }

        [Fact]
        public void IntercalateTest() {
            Assert.Equal("a,b,c,d,e", new[] {"a","b","c","d","e"}.Intercalate(",").ToStringProper());
        }

        [Fact]
        public void FoldRightTest1() {
            Assert.Equal(-2, new[] {1, 2, 3, 4}.FoldRight((x, y) => x - y)); //1 - (2 - (3 - 4))
        }

        [Fact]
        public void FoldRightTest2() {
            Assert.Equal(-2, new[] { 1, 2, 3, 4 }.FoldRight(0, (x, y) => x - y)); //1 - (2 - (3 - (4 - 0))
        }

        [Fact]
        public void ScanLeftTest() {
            Assert2.SequenceEqual(new[] { 1, 3, 6, 10 }, new[] { 1, 2, 3, 4 }.ScanLeft((x, y) => x + y));
        }

        [Fact]
        public void TopTest_Basic() {
            Assert2.SequenceEqual(new[] {10, 8, 7}, new[] {1, 10, 5, 8, 7, 4}.Top(3));
        }

        [Fact]
        public void TopTest_WithFilter() {
            Assert2.SequenceEqual(new[] { 10, 8, 4 }, new[] { 1, 10, 5, 8, 7, 4 }.Top(3, x => x % 2 == 0));
        }

        [Fact]
        public void TopTest_ShouldReturnEmptySequenceIfThereAreNoSuitableElements() {
            Assert2.SequenceEqual(new int[0] , new[] { 1, 10, 5, 8, 7, 4 }.Top(3, x => x % 12 == 0));
        }

        [Fact]
        public void TopTest_ShouldReturnAllSuitableElementsIfThereAreNotEnough() {
            Assert2.SequenceEqual(new[] {10, 5}, new[] { 1, 10, 5, 8, 7, 4 }.Top(3, x => x % 5 == 0));
        }

        [Fact]
        public void BottomTest() {
            Assert2.SequenceEqual(new[] {1, 4, 5}, new[] {1, 10, 5, 8, 7, 4}.Bottom(3));
        }

        [Fact]
        public void SortTest1() {
            Assert2.SequenceEqual(new[] { 1, 4, 5, 7, 8, 10 }, new[] { 1, 10, 5, 8, 7, 4 }.Sort());
        }

        [Fact]
        public void SortTest2() {
            Assert2.SequenceEqual(new[] { 10, 8, 7, 5, 4, 1 }, new[] { 1, 10, 5, 8, 7, 4 }.SortDescending());
        }
    }
}
