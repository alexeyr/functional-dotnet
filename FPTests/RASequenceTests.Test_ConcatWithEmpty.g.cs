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
        public void Test_ConcatWithEmpty01() {
            int[] ints = new int[0];
            this.Test_ConcatWithEmpty<int>(ints);
        }

        [Fact]
        [PexGeneratedBy(typeof(RASequenceTests))]
        public void Test_ConcatWithEmpty02() {
            int[] ints = new int[1];
            this.Test_ConcatWithEmpty<int>(ints);
        }

        [Fact]
        [PexGeneratedBy(typeof(RASequenceTests))]
        public void Test_ConcatWithEmpty03() {
            int[] ints = new int[2];
            this.Test_ConcatWithEmpty<int>(ints);
        }

        [Fact]
        [PexGeneratedBy(typeof(RASequenceTests))]
        public void Test_ConcatWithEmpty04() {
            int[] ints = new int[3];
            this.Test_ConcatWithEmpty<int>(ints);
        }

        [Fact]
        [PexGeneratedBy(typeof(RASequenceTests))]
        public void Test_ConcatWithEmpty05() {
            int[] ints = new int[6];
            this.Test_ConcatWithEmpty<int>(ints);
        }

        [Fact]
        [PexGeneratedBy(typeof(RASequenceTests))]
        public void Test_ConcatWithEmpty07() {
            int[] ints = new int[14];
            this.Test_ConcatWithEmpty<int>(ints);
        }

        [Fact]
        [PexGeneratedBy(typeof(RASequenceTests))]
        public void Test_Concat01() {
            int[] ints = new int[0];
            this.Test_Concat<int>(ints, ints);
        }

        [Fact]
        [PexGeneratedBy(typeof(RASequenceTests))]
        public void Test_Concat02() {
            int[] ints = new int[1];
            this.Test_Concat<int>(ints, ints);
        }

        [Fact]
        [PexGeneratedBy(typeof(RASequenceTests))]
        public void Test_Concat03() {
            int[] ints = new int[1];
            int[] ints1 = new int[0];
            this.Test_Concat<int>(ints, ints1);
        }

        [Fact]
        [PexGeneratedBy(typeof(RASequenceTests))]
        public void Test_Concat04() {
            int[] ints = new int[0];
            int[] ints1 = new int[2];
            this.Test_Concat<int>(ints, ints1);
        }

        [Fact]
        [PexGeneratedBy(typeof(RASequenceTests))]
        public void Test_Concat05() {
            int[] ints = new int[0];
            int[] ints1 = new int[3];
            this.Test_Concat<int>(ints, ints1);
        }

        [Fact]
        [PexGeneratedBy(typeof(RASequenceTests))]
        public void Test_Concat09() {
            int[] ints = new int[2];
            this.Test_Concat<int>(ints, ints);
        }

        [Fact]
        [PexGeneratedBy(typeof(RASequenceTests))]
        public void Test_Concat10() {
            int[] ints = new int[3];
            this.Test_Concat<int>(ints, ints);
        }

        [Fact]
        [PexGeneratedBy(typeof(RASequenceTests))]
        public void Test_Concat11() {
            int[] ints = new int[6];
            this.Test_Concat<int>(ints, ints);
        }

        [Fact]
        [PexGeneratedBy(typeof(RASequenceTests))]
        public void Test_Concat12() {
            int[] ints = new int[7];
            this.Test_Concat<int>(ints, ints);
        }

        [Fact]
        [PexGeneratedBy(typeof(RASequenceTests))]
        public void Test_Concat13() {
            int[] ints = new int[14];
            this.Test_Concat<int>(ints, ints);
        }

        [Fact]
        [PexGeneratedBy(typeof(RASequenceTests))]
        public void Test_Concat27() {
            int[] ints = new int[3];
            int[] ints1 = new int[6];
            this.Test_Concat<int>(ints, ints1);
        }

        [Fact]
        [PexGeneratedBy(typeof(RASequenceTests))]
        public void Test_Concat31() {
            int[] ints = new int[2];
            int[] ints1 = new int[0];
            this.Test_Concat<int>(ints, ints1);
        }

        [Fact]
        [PexGeneratedBy(typeof(RASequenceTests))]
        public void Test_Concat16() {
            int[] ints = new int[0];
            int[] ints1 = new int[6];
            this.Test_Concat<int>(ints, ints1);
        }

        [Fact]
        [PexGeneratedBy(typeof(RASequenceTests))]
        public void Test_Concat17() {
            int[] ints = new int[0];
            int[] ints1 = new int[14];
            this.Test_Concat<int>(ints, ints1);
        }

        [Fact]
        [PexGeneratedBy(typeof(RASequenceTests))]
        public void Test_Concat25() {
            int[] ints = new int[15];
            this.Test_Concat<int>(ints, ints);
        }

        [Fact]
        [PexGeneratedBy(typeof(RASequenceTests))]
        public void Test_Concat26() {
            int[] ints = new int[30];
            this.Test_Concat<int>(ints, ints);
        }

        [Fact]
        [PexGeneratedBy(typeof(RASequenceTests))]
        public void Test_Concat28() {
            int[] ints = new int[15];
            int[] ints1 = new int[30];
            this.Test_Concat<int>(ints, ints1);
        }

        [Fact]
        [PexGeneratedBy(typeof(RASequenceTests))]
        public void Test_Concat29() {
            int[] ints = new int[7];
            int[] ints1 = new int[14];
            this.Test_Concat<int>(ints, ints1);
        }

[Fact]
[PexGeneratedBy(typeof(RASequenceTests))]
public void Test_Concat33()
{
    int[] ints = new int[4];
    int[] ints1 = new int[27];
    this.Test_Concat<int>(ints, ints1);
}
[Fact]
[PexGeneratedBy(typeof(RASequenceTests))]
public void Test_Concat34()
{
    int[] ints = new int[8];
    int[] ints1 = new int[27];
    this.Test_Concat<int>(ints, ints1);
}
[Fact]
[PexGeneratedBy(typeof(RASequenceTests))]
public void Test_Concat35()
{
    int[] ints = new int[5];
    int[] ints1 = new int[1];
    this.Test_Concat<int>(ints, ints1);
}
[Fact]
[PexGeneratedBy(typeof(RASequenceTests))]
public void Test_Concat36()
{
    int[] ints = new int[30];
    int[] ints1 = new int[28];
    this.Test_Concat<int>(ints, ints1);
}
[Fact]
[PexGeneratedBy(typeof(RASequenceTests))]
public void Test_Concat37()
{
    int[] ints = new int[14];
    int[] ints1 = new int[7];
    this.Test_Concat<int>(ints, ints1);
}
[Fact]
[PexGeneratedBy(typeof(RASequenceTests))]
public void Test_Concat38()
{
    int[] ints = new int[13];
    int[] ints1 = new int[27];
    this.Test_Concat<int>(ints, ints1);
}
[Fact]
[PexGeneratedBy(typeof(RASequenceTests))]
public void Test_Concat39()
{
    int[] ints = new int[33];
    int[] ints1 = new int[8];
    this.Test_Concat<int>(ints, ints1);
}
    }
}
