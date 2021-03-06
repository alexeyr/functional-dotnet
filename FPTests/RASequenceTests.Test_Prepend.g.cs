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
        public void Test_Prepend01() {
            int[] ints = new int[0];
            this.Test_Prepend<int>(ints, 2, ints);
        }

        [Fact]
        [PexGeneratedBy(typeof(RASequenceTests))]
        public void Test_Prepend02() {
            int[] ints = new int[1];
            this.Test_Prepend<int>(ints, 2, ints);
        }

        [Fact]
        [PexGeneratedBy(typeof(RASequenceTests))]
        public void Test_Prepend03() {
            int[] ints = new int[2];
            this.Test_Prepend<int>(ints, 2, ints);
        }

        [Fact]
        [PexGeneratedBy(typeof(RASequenceTests))]
        public void Test_Prepend04() {
            int[] ints = new int[3];
            this.Test_Prepend<int>(ints, 2, ints);
        }

        [Fact]
        [PexGeneratedBy(typeof(RASequenceTests))]
        public void Test_Prepend05() {
            int[] ints = new int[2];
            int[] ints1 = new int[6];
            this.Test_Prepend<int>(ints, 0, ints1);
        }

        [Fact]
        [PexGeneratedBy(typeof(RASequenceTests))]
        public void Test_Prepend06() {
            int[] ints = new int[6];
            this.Test_Prepend<int>(ints, 2, ints);
        }

        [Fact]
        [PexGeneratedBy(typeof(RASequenceTests))]
        public void Test_Prepend07() {
            int[] ints = new int[14];
            this.Test_Prepend<int>(ints, 2, ints);
        }

    }
}
