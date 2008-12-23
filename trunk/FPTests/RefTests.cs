using FP.Core;
using Xunit;

namespace FPTests {
    public class RefTests {
        private readonly IRef<int> _refInt = Ref.New(0);
        private readonly IRef<string> _refString = Ref.New("");
        private readonly IRef<bool> _refBool = Ref.New(true);
        private readonly IRef<long> _refLong = Ref.New(0L);

        [Fact]
        public void Test_RefNewPicksCorrectType() {
            Assert.IsType<RefInt>(_refInt);
            Assert.IsType<RefObject<string>>(_refString);
            Assert.IsType<RefBool>(_refBool);
            Assert.IsType<RefLong>(_refLong);
        }
    }
}
