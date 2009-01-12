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
        public void Test_Indexing01() {
            string[] ss = new string[1];
            ss[0] = "\u8000";
            this.Test_Indexing(ss, 0);
        }

        [Fact]
        [PexGeneratedBy(typeof(RopeTests))]
        public void Test_Indexing02() {
            string[] ss = new string[2];
            ss[0] = "\u8000\u8000\u8000\u8000";
            ss[1] = new string('\u8000', 26);
            this.Test_Indexing(ss, 0);
        }

        [Fact]
        [PexGeneratedBy(typeof(RopeTests))]
        public void Test_Indexing03() {
            string[] ss = new string[2];
            ss[0] = "\u8000";
            ss[1] = new string('\u8000', 26);
            this.Test_Indexing(ss, 4);
        }

        [Fact]
        [PexGeneratedBy(typeof(RopeTests))]
        public void Test_Indexing04() {
            string[] ss = new string[2];
            ss[0] = new string('\u2000', 26);
            ss[1] = new string('\u8000', 16);
            this.Test_Indexing(ss, 28);
        }

        [Fact]
        [PexGeneratedBy(typeof(RopeTests))]
        public void Test_Indexing05() {
            string[] ss = new string[2];
            ss[0] = "\u8000\u8000";
            ss[1] = "\u8000";
            this.Test_Indexing(ss, 0);
        }

        [Fact]
        [PexGeneratedBy(typeof(RopeTests))]
        public void Test_Indexing06() {
            string[] ss = new string[2];
            ss[0] = "\u8000\u8000\u8000";
            ss[1] = "";
            this.Test_Indexing(ss, 0);
        }

        [Fact]
        [PexRaisedException(typeof(NullReferenceException))]
        [PexGeneratedBy(typeof(RopeTests))]
        public void Test_Indexing07() {
            string[] ss = new string[1];
            this.Test_Indexing(ss, 2);
        }

        [Fact]
        [PexGeneratedBy(typeof(RopeTests))]
        public void Test_Indexing08() {
            string[] ss = new string[1];
            ss[0] = "\0";
            this.Test_Indexing(ss, 0);
        }

        [Fact]
        [PexGeneratedBy(typeof(RopeTests))]
        public void Test_Indexing09() {
            string[] ss = new string[2];
            ss[0] = new string('\0', 20);
            ss[1] = new string('\0', 27);
            this.Test_Indexing(ss, 0);
        }

        [Fact]
        [PexGeneratedBy(typeof(RopeTests))]
        public void Test_Indexing10() {
            string[] ss = new string[2];
            ss[0] = new string('\0', 26);
            ss[1] = "\0\0\0\0";
            this.Test_Indexing(ss, 0);
        }

        [Fact]
        [PexGeneratedBy(typeof(RopeTests))]
        public void Test_Indexing11() {
            string[] ss = new string[2];
            ss[0] = new string('\0', 25);
            ss[1] = "\0\0";
            this.Test_Indexing(ss, 26);
        }

        [Fact]
        [PexGeneratedBy(typeof(RopeTests))]
        public void Test_Indexing12() {
            string[] ss = new string[2];
            ss[0] = "\0";
            ss[1] = "\0";
            this.Test_Indexing(ss, 0);
        }

        [Fact]
        [PexGeneratedBy(typeof(RopeTests))]
        public void Test_Indexing13() {
            string[] ss = new string[2];
            ss[0] = "";
            ss[1] = "\0\0";
            this.Test_Indexing(ss, 0);
        }

        [Fact]
        [PexGeneratedBy(typeof(RopeTests))]
        public void Test_Indexing14() {
            string[] ss = new string[3];
            ss[0] = "";
            ss[1] = "";
            ss[2] = "\0\0";
            this.Test_Indexing(ss, 0);
        }

        [Fact]
        [PexGeneratedBy(typeof(RopeTests))]
        public void Test_Indexing15() {
            string[] ss = new string[3];
            ss[0] = "\0\0";
            ss[1] = new string('\0', 28);
            ss[2] = "\0\0\0\0";
            this.Test_Indexing(ss, 8);
        }

        [Fact]
        [PexGeneratedBy(typeof(RopeTests))]
        public void Test_Indexing16() {
            string[] ss = new string[3];
            ss[0] = "\0";
            ss[1] = new string('\0', 28);
            ss[2] = "\0";
            this.Test_Indexing(ss, 0);
        }

        [Fact]
        [PexGeneratedBy(typeof(RopeTests))]
        public void Test_Indexing17() {
            string[] ss = new string[4];
            ss[0] = "";
            ss[1] = "";
            ss[2] = "";
            ss[3] = "\0\0\0\0";
            this.Test_Indexing(ss, 2);
        }

        [Fact]
        [PexGeneratedBy(typeof(RopeTests))]
        public void Test_Indexing18() {
            string[] ss = new string[3];
            ss[0] = "\0";
            ss[1] = "\0\0";
            ss[2] = "\0";
            this.Test_Indexing(ss, 0);
        }

        [Fact]
        [PexGeneratedBy(typeof(RopeTests))]
        public void Test_Indexing30() {
            string[] ss = new string[3];
            ss[0] = new string('\0', 28);
            ss[1] = new string('\0', 28);
            ss[2] = new string('\0', 16);
            this.Test_Indexing(ss, 50);
        }

        [Fact]
        [PexGeneratedBy(typeof(RopeTests))]
        public void Test_Indexing31() {
            string[] ss = new string[3];
            ss[0] = new string('\0', 28);
            ss[1] = "\0";
            ss[2] = "\0";
            this.Test_Indexing(ss, 0);
        }

        [Fact]
        [PexGeneratedBy(typeof(RopeTests))]
        public void Test_Indexing32() {
            string[] ss = new string[4];
            ss[0] = "\0";
            ss[1] = "\0";
            ss[2] = "\0";
            ss[3] = "";
            this.Test_Indexing(ss, 0);
        }

        [Fact]
        [PexGeneratedBy(typeof(RopeTests))]
        public void Test_Indexing33() {
            string[] ss = new string[4];
            ss[0] = "\0";
            ss[1] = "\0";
            ss[2] = "\0";
            ss[3] = "\0";
            this.Test_Indexing(ss, 0);
        }

        [Fact]
        [PexGeneratedBy(typeof(RopeTests))]
        public void Test_Indexing20() {
            string[] ss = new string[2];
            ss[0] = "\0";
            ss[1] = new string('\0', 26);
            this.Test_Indexing(ss, 7);
        }

        [Fact]
        [PexGeneratedBy(typeof(RopeTests))]
        public void Test_Indexing24() {
            string[] ss = new string[3];
            ss[0] = "\0";
            ss[1] = "\0";
            ss[2] = "";
            this.Test_Indexing(ss, 0);
        }

        [Fact]
        [PexGeneratedBy(typeof(RopeTests))]
        public void Test_Indexing25() {
            string[] ss = new string[3];
            ss[0] = "";
            ss[1] = "";
            ss[2] = "\0";
            this.Test_Indexing(ss, 0);
        }

        [Fact(Skip = "PexAssume")]
        [PexGeneratedBy(typeof(RopeTests))]
        public void Test_Indexing26() {
            string[] ss = new string[4];
            ss[0] = "";
            ss[1] = "";
            ss[2] = "";
            ss[3] = "\0\0\0\0\0\0";
            this.Test_Indexing(ss, 0);
        }

        [Fact]
        [PexGeneratedBy(typeof(RopeTests))]
        public void Test_Indexing27() {
            string[] ss = new string[3];
            ss[0] = "\0";
            ss[1] = "\0";
            ss[2] = "\0";
            this.Test_Indexing(ss, 0);
        }

        [Fact]
        [PexGeneratedBy(typeof(RopeTests))]
        public void Test_Indexing28() {
            string[] ss = new string[4];
            ss[0] = new string('\0', 28);
            ss[1] = new string('\0', 25);
            ss[2] = "";
            ss[3] = "\0\0\0\0\0\0\0\0";
            this.Test_Indexing(ss, 30);
        }

        [Fact]
        [PexGeneratedBy(typeof(RopeTests))]
        public void Test_Indexing29() {
            string[] ss = new string[4];
            ss[0] = new string('\0', 26);
            ss[1] = new string('\0', 24);
            ss[2] = "";
            ss[3] = new string('\0', 26);
            this.Test_Indexing(ss, 62);
        }

        [Fact]
        [PexGeneratedBy(typeof(RopeTests))]
        public void Test_Indexing34() {
            string[] ss = new string[4];
            ss[0] = new string('\0', 28);
            ss[1] = "\0\0";
            ss[2] = "";
            ss[3] = "\0";
            this.Test_Indexing(ss, 0);
        }

        [Fact]
        [PexGeneratedBy(typeof(RopeTests))]
        public void Test_Indexing35() {
            string[] ss = new string[4];
            ss[0] = new string('\0', 25);
            ss[1] = new string('\0', 26);
            ss[2] = "";
            ss[3] = "\0\0\0\0";
            this.Test_Indexing(ss, 23);
        }

        [Fact]
        [PexGeneratedBy(typeof(RopeTests))]
        public void Test_Indexing36() {
            string[] ss = new string[10];
            ss[0] = new string('\0', 25);
            ss[1] = "";
            ss[2] = "";
            ss[3] = "";
            ss[4] = "";
            ss[5] = "";
            ss[6] = new string('\0', 25);
            ss[7] = "\0";
            ss[8] = "\0";
            ss[9] = "";
            this.Test_Indexing(ss, 26);
        }

        [Fact]
        [PexGeneratedBy(typeof(RopeTests))]
        public void Test_Indexing37() {
            string[] ss = new string[10];
            ss[0] = new string('\0', 25);
            ss[1] = "";
            ss[2] = "";
            ss[3] = "";
            ss[4] = "";
            ss[5] = "";
            ss[6] = new string('\0', 26);
            ss[7] = new string('\0', 25);
            ss[8] = new string('\0', 26);
            ss[9] = "";
            this.Test_Indexing(ss, 62);
        }

        [Fact]
        [PexGeneratedBy(typeof(RopeTests))]
        public void Test_Indexing38() {
            string[] ss = new string[10];
            ss[0] = new string('\0', 25);
            ss[1] = "";
            ss[2] = "";
            ss[3] = "";
            ss[4] = "";
            ss[5] = "";
            ss[6] = "\0";
            ss[7] = "\0\0";
            ss[8] = "";
            ss[9] = "\0\0";
            this.Test_Indexing(ss, 0);
        }

        [Fact]
        [PexGeneratedBy(typeof(RopeTests))]
        public void Test_Indexing39() {
            string[] ss = new string[5];
            ss[0] = "\0";
            ss[1] = "\0";
            ss[2] = "";
            ss[3] = "\0";
            ss[4] = "\0\0";
            this.Test_Indexing(ss, 2);
        }

        [Fact]
        [PexGeneratedBy(typeof(RopeTests))]
        public void Test_Indexing21() {
            string[] ss = new string[2];
            ss[0] = "";
            ss[1] = "\u8000\0";
            this.Test_Indexing(ss, 0);
        }

        [Fact]
        [PexGeneratedBy(typeof(RopeTests))]
        public void Test_Indexing22() {
            string[] ss = new string[2];
            ss[0] = new string('\0', 25);
            ss[1] = "\u1000\u1000\u1000\u1000";
            this.Test_Indexing(ss, 0);
        }

        [Fact]
        [PexGeneratedBy(typeof(RopeTests))]
        public void Test_Indexing23() {
            string[] ss = new string[2];
            ss[0] = "\0";
            ss[1] = new string('\u1000', 28);
            this.Test_Indexing(ss, 14);
        }

        [Fact]
        [PexGeneratedBy(typeof(RopeTests))]
        public void Test_Indexing40() {
            string[] ss = new string[2];
            ss[0] = "\u4000\u4000";
            ss[1] = "\0";
            this.Test_Indexing(ss, 0);
        }

        [Fact]
        [PexGeneratedBy(typeof(RopeTests))]
        public void Test_Indexing41() {
            string[] ss = new string[3];
            ss[0] = "";
            ss[1] = "\0\0";
            ss[2] = "\u8000";
            this.Test_Indexing(ss, 0);
        }

        [Fact]
        [PexGeneratedBy(typeof(RopeTests))]
        public void Test_Indexing42() {
            string[] ss = new string[4];
            ss[0] = "";
            ss[1] = "";
            ss[2] = "";
            ss[3] = "\u8000\0\0\0\0\0";
            this.Test_Indexing(ss, 0);
        }

        [Fact]
        [PexGeneratedBy(typeof(RopeTests))]
        public void Test_Indexing43() {
            string[] ss = new string[8];
            ss[0] = "\0\0";
            ss[1] = "\0";
            ss[2] = "\0";
            ss[3] = "\0";
            ss[4] = "\u4000\u4000";
            ss[5] = "\0";
            ss[6] = "\0\0\0\0";
            ss[7] = "\0\0";
            this.Test_Indexing(ss, 0);
        }

        [Fact]
        [PexGeneratedBy(typeof(RopeTests))]
        public void Test_Indexing44() {
            string[] ss = new string[8];
            ss[0] = new string('\0', 16);
            ss[1] = new string('\0', 26);
            ss[2] = new string('\0', 26);
            ss[3] = "\0\0\0\0\0\0\0\0";
            ss[4] = new string('\0', 26);
            ss[5] = new string('\0', 16);
            ss[6] = new string('\0', 28);
            ss[7] = new string('\u8000', 26);
            this.Test_Indexing(ss, 2);
        }

        [Fact]
        [PexGeneratedBy(typeof(RopeTests))]
        public void Test_Indexing45() {
            string[] ss = new string[8];
            ss[0] = new string('\u8000', 28);
            ss[1] = "\0";
            ss[2] = "\0";
            ss[3] = new string('\0', 26);
            ss[4] = "\0\0";
            ss[5] = new string('\0', 28);
            ss[6] = "\0\0";
            ss[7] = new string('\0', 26);
            this.Test_Indexing(ss, 14);
        }

        [Fact]
        [PexGeneratedBy(typeof(RopeTests))]
        public void Test_Indexing46() {
            string[] ss = new string[8];
            ss[0] = new string('\0', 28);
            ss[1] = "\0";
            ss[2] = "\0";
            ss[3] = new string('\0', 26);
            ss[4] = "\u1000";
            ss[5] = new string('\0', 28);
            ss[6] = "\0";
            ss[7] = "\0";
            this.Test_Indexing(ss, 56);
        }

    }
}
