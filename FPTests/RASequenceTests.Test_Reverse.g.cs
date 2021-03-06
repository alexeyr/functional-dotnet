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
        public void Test_Reverse01() {
            int[] ints = new int[0];
            this.Test_Reverse<int>(ints);
        }

        [Fact]
        [PexGeneratedBy(typeof(RASequenceTests))]
        public void Test_Reverse02() {
            int[] ints = new int[1];
            this.Test_Reverse<int>(ints);
        }

        [Fact]
        [PexGeneratedBy(typeof(RASequenceTests))]
        public void Test_Reverse03() {
            int[] ints = new int[2];
            this.Test_Reverse<int>(ints);
        }

        [Fact]
        [PexGeneratedBy(typeof(RASequenceTests))]
        public void Test_Reverse04() {
            int[] ints = new int[3];
            this.Test_Reverse<int>(ints);
        }

        [Fact]
        [PexGeneratedBy(typeof(RASequenceTests))]
        public void Test_Reverse05() {
            int[] ints = new int[6];
            this.Test_Reverse<int>(ints);
        }

        [Fact]
        public void Test_Reverse06() {
            int[] ints = new int[6];
            ints[3] = 1;
            this.Test_Reverse<int>(ints);
        }

        [Fact]
        [PexGeneratedBy(typeof(RASequenceTests))]
        public void Test_Reverse07() {
            int[] ints = new int[14];
            this.Test_Reverse<int>(ints);
        }

        [Fact]
        public void Test_Reverse09() {
            int[] ints = new int[25];
            ints[11] = 1;
            this.Test_Reverse<int>(ints);
        }

        [Fact]
        public void Test_Reverse15() {
            int[] ints = new int[6];
            ints[3] = 1;
            this.Test_Reverse<int>(ints);
        }

        [Fact]
        public void Test_Reverse14() {
            int[] ints = new int[6];
            ints[3] = 1;
            this.Test_Reverse<int>(ints);
        }

        [Fact]
        public void Test_Reverse19() {
            int[] ints = new int[126];
            this.Test_Reverse<int>(ints);
        }

        [Fact]
        [PexGeneratedBy(typeof(RASequenceTests))]
        public void Test_Reverse20() {
            int[] ints = new int[21];
            this.Test_Reverse<int>(ints);
        }

[Fact]
[PexGeneratedBy(typeof(RASequenceTests))]
public void Test_Reverse17()
{
    int[] ints = new int[37];
    this.Test_Reverse<int>(ints);
}
[Fact(Skip = "the test state was: duplicate path")]
[PexGeneratedBy(typeof(RASequenceTests))]
public void Test_Reverse18()
{
    int[] ints = new int[37];
    ints[0] = 37;
    ints[1] = 37;
    ints[2] = 37;
    ints[3] = 37;
    ints[4] = 37;
    ints[5] = 37;
    ints[6] = 37;
    ints[7] = 37;
    ints[8] = 37;
    ints[9] = 37;
    ints[10] = 37;
    ints[11] = 37;
    ints[13] = 37;
    ints[14] = 37;
    ints[15] = 37;
    ints[16] = 37;
    ints[17] = 37;
    ints[18] = 37;
    ints[19] = 37;
    ints[20] = 1;
    ints[21] = 37;
    ints[22] = 37;
    ints[23] = 37;
    ints[24] = 37;
    ints[25] = 37;
    ints[26] = 37;
    ints[27] = 37;
    ints[28] = 37;
    ints[29] = 37;
    ints[30] = 37;
    ints[31] = 37;
    ints[32] = 37;
    ints[33] = 37;
    ints[34] = 37;
    ints[35] = 37;
    ints[36] = 37;
    this.Test_Reverse<int>(ints);
}
    }
}
