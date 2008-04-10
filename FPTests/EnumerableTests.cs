using System.Collections.Generic;
using System.Linq;
using Xunit;
using FP;

namespace FPTests
{
    public class EnumerableTests
    {
        [Fact]
        public void TailShouldThrowOnEmptySeq()
        {
            var emptyList = Enumerable.Empty<int>();
            Assert.Throws(typeof(EmptySequenceException), () => emptyList.Tail().FirstOrDefault());
        }
    }
}
