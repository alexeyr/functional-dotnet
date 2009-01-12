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
    public partial class RopeTests {

        [Fact]
        [PexGeneratedBy(typeof(RopeTests))]
        public void Test_Creation01() {
            string[] ss = new string[0];
            this.Test_Creation(ss);
        }

        [Fact]
        public void Test_Creation04() {
            string[] ss = new string[2];
            ss[0] = new string('\0', 256);
            ss[1] = new string('\0', 256);
            this.Test_Creation(ss);
        }

        [Fact]
        public void Test_Creation06() {
            string[] ss = new string[2];
            ss[0] = new string('\0', 256);
            ss[1] = new string('\0', 256);
            this.Test_Creation(ss);
        }

        [Fact]
        [PexGeneratedBy(typeof(RopeTests))]
        public void Test_Creation05() {
            string[] ss = new string[1];
            ss[0] = "";
            this.Test_Creation(ss);
        }

        [Fact]
        [PexGeneratedBy(typeof(RopeTests))]
        public void Test_Creation07() {
            string[] ss = new string[2];
            ss[0] = "";
            ss[1] = "";
            this.Test_Creation(ss);
        }

        [Fact]
        [PexGeneratedBy(typeof(RopeTests))]
        public void Test_Creation09() {
            string[] ss = new string[4];
            ss[0] = "";
            ss[1] = "";
            ss[2] = "";
            ss[3] = "";
            this.Test_Creation(ss);
        }

        [Fact]
        [PexGeneratedBy(typeof(RopeTests))]
        public void Test_Creation11() {
            string[] ss = new string[3];
            ss[0] = "";
            ss[1] = "";
            ss[2] = "";
            this.Test_Creation(ss);
        }

        [Fact]
        [PexGeneratedBy(typeof(RopeTests))]
        public void Test_Creation18() {
            string[] ss = new string[1];
            ss[0] = "\u8000";
            this.Test_Creation(ss);
        }

        [Fact]
        public void Test_Creation23() {
            string[] ss = new string[4];
            ss[0] = "\u8000\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0";
            ss[1] = "\u8000\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0";
            ss[2] = "\u8000\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0";
            ss[3] = "\u8000\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0";
            this.Test_Creation(ss);
        }

        [Fact]
        public void Test_Creation25() {
            string[] ss = new string[4];
            ss[0] = "\u8000\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0";
            ss[1] = "\u8000\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0";
            ss[2] = "\u8000\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0";
            ss[3] = "\u8000\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0\0";
            this.Test_Creation(ss);
        }

        [Fact]
        [PexGeneratedBy(typeof(RopeTests))]
        public void Test_Creation14() {
            string[] ss = new string[1];
            ss[0] = "\u8000\u8000";
            this.Test_Creation(ss);
        }

        [Fact]
        [PexGeneratedBy(typeof(RopeTests))]
        public void Test_Creation08() {
            string[] ss = new string[1];
            ss[0] = "\0";
            this.Test_Creation(ss);
        }

        [Fact]
        [PexGeneratedBy(typeof(RopeTests))]
        public void Test_Creation10() {
            string[] ss = new string[1];
            ss[0] = "\0\0";
            this.Test_Creation(ss);
        }

        [Fact(Skip = "PexAssume")]
        public void Test_Creation13() {
            string[] ss = new string[30];
            ss[0] = new string('\0', 30);
            ss[1] = new string('\0', 27);
            ss[2] = new string('\0', 25);
            ss[3] = new string('\0', 30);
            ss[4] = new string('\0', 30);
            ss[5] = new string('\0', 30);
            ss[6] = new string('\0', 30);
            ss[7] = new string('\0', 30);
            ss[8] = new string('\0', 30);
            ss[9] = new string('\0', 30);
            ss[10] = new string('\0', 30);
            ss[11] = new string('\0', 30);
            ss[12] = new string('\0', 30);
            ss[13] = new string('\0', 30);
            ss[14] = new string('\0', 30);
            ss[15] = new string('\0', 30);
            ss[16] = new string('\0', 30);
            ss[17] = new string('\0', 30);
            ss[18] = new string('\0', 30);
            ss[19] = new string('\0', 30);
            ss[20] = new string('\0', 30);
            ss[21] = new string('\0', 30);
            ss[22] = new string('\0', 30);
            ss[23] = new string('\0', 30);
            ss[24] = new string('\0', 30);
            ss[25] = new string('\0', 30);
            ss[26] = new string('\0', 30);
            ss[27] = new string('\0', 30);
            ss[28] = new string('\0', 30);
            ss[29] = new string('\0', 30);
            this.Test_Creation(ss);
        }

        [Fact]
        public void Test_Creation15() {
            string[] ss = new string[16];
            ss[0] = new string('\0', 30);
            ss[1] = new string('\0', 28);
            ss[2] = new string('\0', 25);
            ss[3] = "\0\0\0\0\0\0\0\0";
            ss[4] = new string('\0', 30);
            ss[5] = new string('\0', 30);
            ss[6] = new string('\0', 30);
            ss[7] = new string('\0', 30);
            ss[8] = new string('\0', 30);
            ss[9] = new string('\0', 30);
            ss[10] = new string('\0', 30);
            ss[11] = new string('\0', 30);
            ss[12] = new string('\0', 30);
            ss[13] = new string('\0', 30);
            ss[14] = new string('\0', 30);
            ss[15] = new string('\0', 30);
            this.Test_Creation(ss);
        }

        [Fact]
        [PexGeneratedBy(typeof(RopeTests))]
        public void Test_Creation17() {
            string[] ss = new string[2];
            ss[0] = "\0";
            ss[1] = "\0";
            this.Test_Creation(ss);
        }

        [Fact]
        public void Test_Creation19() {
            string[] ss = new string[6];
            ss[0] = new string('\0', 30);
            ss[1] = new string('\0', 26);
            ss[2] = new string('\0', 30);
            ss[3] = new string('\0', 30);
            ss[4] = new string('\0', 30);
            ss[5] = new string('\0', 30);
            this.Test_Creation(ss);
        }

        [Fact]
        public void Test_Creation20() {
            string[] ss = new string[18];
            ss[0] = new string('\0', 25);
            ss[1] = new string('\0', 25);
            ss[2] = new string('\0', 28);
            ss[3] = "\0";
            ss[4] = new string('\0', 26);
            ss[5] = "";
            ss[6] = new string('\0', 25);
            ss[7] = new string('\0', 25);
            ss[8] = new string('\0', 25);
            ss[9] = new string('\0', 25);
            ss[10] = new string('\0', 25);
            ss[11] = new string('\0', 25);
            ss[12] = new string('\0', 25);
            ss[13] = new string('\0', 25);
            ss[14] = new string('\0', 25);
            ss[15] = new string('\0', 25);
            ss[16] = new string('\0', 25);
            ss[17] = new string('\0', 25);
            this.Test_Creation(ss);
        }

        [Fact]
        public void Test_Creation21() {
            string[] ss = new string[30];
            ss[0] = new string('\0', 28);
            ss[1] = "\0\0";
            ss[2] = new string('\0', 20);
            ss[3] = new string('\0', 28);
            ss[4] = new string('\0', 28);
            ss[5] = new string('\0', 28);
            ss[6] = new string('\0', 28);
            ss[7] = new string('\0', 28);
            ss[8] = new string('\0', 28);
            ss[9] = new string('\0', 28);
            ss[10] = new string('\0', 28);
            ss[11] = new string('\0', 28);
            ss[12] = new string('\0', 28);
            ss[13] = new string('\0', 28);
            ss[14] = new string('\0', 28);
            ss[15] = new string('\0', 28);
            ss[16] = new string('\0', 28);
            ss[17] = new string('\0', 28);
            ss[18] = new string('\0', 28);
            ss[19] = new string('\0', 28);
            ss[20] = new string('\0', 28);
            ss[21] = new string('\0', 28);
            ss[22] = new string('\0', 28);
            ss[23] = new string('\0', 28);
            ss[24] = new string('\0', 28);
            ss[25] = new string('\0', 28);
            ss[26] = new string('\0', 28);
            ss[27] = new string('\0', 28);
            ss[28] = new string('\0', 28);
            ss[29] = new string('\0', 28);
            this.Test_Creation(ss);
        }

        [Fact]
        [PexGeneratedBy(typeof(RopeTests))]
        public void Test_Creation22() {
            string[] ss = new string[30];
            ss[0] = "";
            ss[1] = new string('\0', 25);
            ss[2] = "\0";
            ss[3] = "\0";
            ss[4] = "";
            ss[5] = "";
            ss[6] = "";
            ss[7] = "";
            ss[8] = "";
            ss[9] = "";
            ss[10] = "";
            ss[11] = "";
            ss[12] = "";
            ss[13] = "";
            ss[14] = "";
            ss[15] = "";
            ss[16] = "";
            ss[17] = "";
            ss[18] = "";
            ss[19] = "";
            ss[20] = "";
            ss[21] = "";
            ss[22] = "";
            ss[23] = "";
            ss[24] = "";
            ss[25] = "";
            ss[26] = "";
            ss[27] = "";
            ss[28] = "";
            ss[29] = "";
            this.Test_Creation(ss);
        }

        [Fact]
        [PexGeneratedBy(typeof(RopeTests))]
        public void Test_Creation24() {
            string[] ss = new string[3];
            ss[0] = "\0";
            ss[1] = "\0";
            ss[2] = "\0";
            this.Test_Creation(ss);
        }

        [Fact]
        [PexGeneratedBy(typeof(RopeTests))]
        public void Test_Creation26() {
            string[] ss = new string[3];
            ss[0] = new string('\0', 24);
            ss[1] = new string('\0', 26);
            ss[2] = new string('\0', 24);
            this.Test_Creation(ss);
        }

    }
}
