using System;
using Xunit;
using Microsoft.Pex.Framework.Generated;

namespace FPTests
{
    public partial class VectorTests {

        [Fact]
        [PexGeneratedBy(typeof(VectorTests))]
        public void ReplaceSingleElement01() {
            this.ReplaceSingleElement(0, 1);
        }

        [Fact]
        [PexGeneratedBy(typeof(VectorTests))]
        public void ReplaceSingleElement02() {
            this.ReplaceSingleElement(1, 1);
        }

        [Fact]
        [PexGeneratedBy(typeof(VectorTests))]
        public void ReplaceSingleElement03() {
            this.ReplaceSingleElement(0, 4);
        }

        [Fact]
        public void ReplaceSingleElement04() {
            this.ReplaceSingleElement(1073741824, 0);
        }

        [Fact]
        public void ReplaceSingleElement08() {
            this.ReplaceSingleElement(1073741826, 0);
        }

    }
}
