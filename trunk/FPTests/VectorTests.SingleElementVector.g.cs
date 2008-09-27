using System;
using Xunit;
using Microsoft.Pex.Framework.Generated;

namespace FPTests
{
    public partial class VectorTests {

        [Fact]
        public void SingleElementVector04() {
            this.SingleElementVector(33554432, 3);
        }

        [Fact]
        public void SingleElementVector05() {
            this.SingleElementVector(33554432, 3);
        }

        [Fact]
        public void SingleElementVector10() {
            this.SingleElementVector(33554432, 3);
        }

        [Fact]
        public void SingleElementVector08() {
            this.SingleElementVector(33554432, 3);
        }

        [Fact]
        [PexGeneratedBy(typeof(VectorTests))]
        public void SingleElementVector01() {
            this.SingleElementVector<string>(0, "");
        }

        [Fact]
        [PexGeneratedBy(typeof(VectorTests))]
        public void SingleElementVector02() {
            this.SingleElementVector<string>(0, (string)null);
        }

        [Fact]
        [PexGeneratedBy(typeof(VectorTests))]
        public void SingleElementVector03() {
            this.SingleElementVector<string>(2, "");
        }

        [Fact]
        public void SingleElementVector06() {
            this.SingleElementVector<string>(1073741824, "");
        }

        [Fact]
        [PexGeneratedBy(typeof(VectorTests))]
        public void SingleElementVector09() {
            this.SingleElementVector<int>(0, 1);
        }

        [Fact]
        [PexGeneratedBy(typeof(VectorTests))]
        public void SingleElementVector11() {
            this.SingleElementVector<int>(1, 3);
        }

        [Fact]
        public void SingleElementVector12() {
            this.SingleElementVector<int>(1073741824, 3);
        }

        [Fact]
        [PexGeneratedBy(typeof(VectorTests))]
        public void SingleElementVector17() {
            this.SingleElementVector<string>(1073741824, "");
        }

        [Fact]
        [PexGeneratedBy(typeof(VectorTests))]
        public void SingleElementVector20() {
            this.SingleElementVector<int>(1073741824, 3);
        }

    }
}
