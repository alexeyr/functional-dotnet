using FP.Collections.Immutable;
using FP.Linq;
using Xunit;

namespace FPTests {
    public class MaybeTests {
        [Fact]
        public void Maybe_NullShouldConvertToNothing() {
            Maybe<string> m = (string) null;
            Assert.Equal(m, Maybe.Nothing<string>());
            Assert.False(m.HasValue);
        }

        [Fact]
        public void Maybe_ValueShouldConvertToSomething() {
            Maybe<string> m = "test";
            Assert.Equal(m.Value, "test");
        }

        [Fact]
        public void Maybe_QueriesShouldWork() {
            var query = from x in Maybe.Just(4)
                        from y in Maybe.Just(5)
                        select x + y;
            Assert.Equal(query.Value, 9);
        }

        [Fact]
        public void Maybe_NullsShouldPropagateInQueries() {
            var query = from x in Maybe.Just(4)
                        from y in Maybe.Nothing<int>()
                        select x + y;
            Assert.False(query.HasValue);
        }

        [Fact]
        public void Maybe_DefaultOperatorShouldWork() {
            Assert.Equal(Maybe<int>.Nothing || Maybe.Just(3) || Maybe.Just(5), Maybe.Just(3));
        }

        [Fact]
        public void Maybe_DefaultOperatorShouldWorkWithValues1() {
            Assert.Equal(Maybe<int>.Nothing || Maybe.Just(3) || 5, Maybe.Just(3));
        }

        [Fact]
        public void Maybe_DefaultOperatorShouldWorkWithValues2() {
            Assert.Equal(Maybe<int>.Nothing || Maybe<int>.Nothing || 5, 5);
        }

    }
}
