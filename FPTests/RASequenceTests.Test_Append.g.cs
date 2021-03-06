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
    public partial class RASequenceTests {
        [Fact]
        [PexGeneratedBy(typeof(RASequenceTests))]
        public void Test_Append01() {
            int[] ints = new int[0];
            this.Test_Append<int>(ints, 2, ints);
        }

        [Fact]
        [PexGeneratedBy(typeof(RASequenceTests))]
        public void Test_Append02() {
            int[] ints = new int[1];
            this.Test_Append<int>(ints, 2, ints);
        }

        [Fact]
        [PexGeneratedBy(typeof(RASequenceTests))]
        public void Test_Append03() {
            int[] ints = new int[2];
            this.Test_Append<int>(ints, 2, ints);
        }

        [Fact]
        [PexGeneratedBy(typeof(RASequenceTests))]
        public void Test_Append04() {
            int[] ints = new int[3];
            this.Test_Append<int>(ints, 2, ints);
        }

        [Fact]
        [PexGeneratedBy(typeof(RASequenceTests))]
        public void Test_Append05() {
            int[] ints = new int[6];
            this.Test_Append<int>(ints, 2, ints);
        }

        [Fact]
        [PexGeneratedBy(typeof(RASequenceTests))]
        public void Test_Append06() {
            int[] ints = new int[7];
            this.Test_Append<int>(ints, 2, ints);
        }

        [Fact]
        [PexGeneratedBy(typeof(RASequenceTests))]
        public void Test_Append07() {
            int[] ints = new int[0];
            int[] ints1 = new int[1];
            this.Test_Append<int>(ints, 3, ints1);
        }

        [Fact]
        [PexGeneratedBy(typeof(RASequenceTests))]
        public void Test_Append08() {
            int[] ints = new int[5];
            this.Test_Append<int>(ints, 2, ints);
        }

    }
}
