// This file contains automatically generated unit tests.
// Do NOT modify this file manually.
// 
// When Pex is invoked again,
// it might remove or update any previously generated unit tests.
// 
// If the contents of this file becomes outdated, e.g. if it does not
// compile anymore, you may delete this file and invoke Pex again.
using System;
using Xunit;
using Microsoft.Pex.Framework.Generated;

namespace FPTests
{
    public partial class EnumerableTests {
        [Fact]
        [PexGeneratedBy(typeof(EnumerableTests))]
        public void SortAscendingTest01() {
            int[] ints = new int[0];
            this.SortAscendingTest(ints);
        }

        [Fact]
        [PexGeneratedBy(typeof(EnumerableTests))]
        public void SortAscendingTest02() {
            int[] ints = new int[1];
            this.SortAscendingTest(ints);
        }

        [Fact]
        [PexGeneratedBy(typeof(EnumerableTests))]
        public void SortAscendingTest03() {
            int[] ints = new int[3];
            ints[0] = 4194688;
            ints[1] = 553779264;
            ints[2] = 4194688;
            this.SortAscendingTest(ints);
        }

    }
}
