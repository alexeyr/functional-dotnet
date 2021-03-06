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
        public void SortDescendingTest01() {
            int[] ints = new int[0];
            this.SortDescendingTest(ints);
        }

        [Fact]
        [PexGeneratedBy(typeof(EnumerableTests))]
        public void SortDescendingTest02() {
            int[] ints = new int[1];
            this.SortDescendingTest(ints);
        }

        [Fact]
        [PexGeneratedBy(typeof(EnumerableTests))]
        public void SortDescendingTest03() {
            int[] ints = new int[2];
            ints[0] = -1209991166;
            ints[1] = 938475777;
            this.SortDescendingTest(ints);
        }

    }
}
