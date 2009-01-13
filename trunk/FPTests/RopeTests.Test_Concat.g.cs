// This file contains automatically generated unit tests.
// Do NOT modify this file manually.
// 
// When Pex is invoked again,
// it might remove or update any previously generated unit tests.
// 
// If the contents of this file becomes outdated, e.g. if it does not
// compile anymore, you may delete this file and invoke Pex again.
using System;
using Microsoft.Pex.Framework.Generated;
using Microsoft.Pex.Framework;
using Xunit;

namespace FPTests
{
    public partial class RopeTests {
        [Fact]
        [PexGeneratedBy(typeof(RopeTests))]
        public void Test_Concat01() {
            IPexChoiceRecorder choices = PexChoose.NewTest();
            this.Test_Concat("\u0004", "\u8000");
        }

        [Fact]
        [PexGeneratedBy(typeof(RopeTests))]
        public void Test_Concat02() {
            IPexChoiceRecorder choices = PexChoose.NewTest();
            this.Test_Concat("\u0002", "\u0010\u0010\u0010\u0010\u0010\u0010");
        }

        [Fact]
        [PexGeneratedBy(typeof(RopeTests))]
        public void Test_Concat03() {
            IPexChoiceRecorder choices = PexChoose.NewTest();
            choices.DefaultSession
                .At(2, "goDeeper", (object)true);
            this.Test_Concat("\u0001", "\u4000\u4000\u4000\u4000\u4000\u4000");
        }

        [Fact]
        [PexGeneratedBy(typeof(RopeTests))]
        public void Test_Concat04() {
            IPexChoiceRecorder choices = PexChoose.NewTest();
            choices.DefaultSession
                .At(0, "requireLarge", (object)true)
                .At(2, "requireLarge", (object)true);
            this.Test_Concat(new string('\u0001', 127), new string('\u0001', 127));
        }

        [Fact]
        [PexGeneratedBy(typeof(RopeTests))]
        public void Test_Concat05() {
            IPexChoiceRecorder choices = PexChoose.NewTest();
            choices.DefaultSession
                .At(0, "requireLarge", (object)true)
                .At(1, "goDeeper", (object)true)
                .At(3, "goDeeper", (object)true);
            this.Test_Concat(new string('\u0001', 100), new string('\u0001', 100));
        }

        [Fact]
        [PexGeneratedBy(typeof(RopeTests))]
        public void Test_Concat06() {
            IPexChoiceRecorder choices = PexChoose.NewTest();
            choices.DefaultSession
                .At(2, "goDeeper", (object)true)
                .At(3, "length1", (object)6)
                .At(4, "goDeeper", (object)true);
            this.Test_Concat("\u0080", "\u0001\u0001\u0001\u0001\u0001\u0001");
        }

        [Fact]
        [PexGeneratedBy(typeof(RopeTests))]
        public void Test_Concat07() {
            IPexChoiceRecorder choices = PexChoose.NewTest();
            choices.DefaultSession
                .At(2, "goDeeper", (object)true)
                .At(3, "length1", (object)6)
                .At(4, "goDeeper", (object)true)
                .At(6, "goDeeper", (object)true)
                .At(7, "length1", (object)6)
                .At(8, "goDeeper", (object)true)
                .At(9, "length1", (object)6)
                .At(10, "goDeeper", (object)true)
                .At(12, "goDeeper", (object)true)
                .At(13, "length1", (object)6);
            this.Test_Concat("\u0080", "\u0002\u0002\u0002\u0002\u0002\u0002");
        }

        [Fact]
        [PexGeneratedBy(typeof(RopeTests))]
        public void Test_Concat08() {
            IPexChoiceRecorder choices = PexChoose.NewTest();
            choices.DefaultSession
                .At(0, "requireLarge", (object)true)
                .At(1, "goDeeper", (object)true)
                .At(2, "length1", (object)1)
                .At(4, "requireLarge", (object)true);
            this.Test_Concat(new string('\u0001', 100), new string('\u0002', 100));
        }

        [Fact]
        [PexGeneratedBy(typeof(RopeTests))]
        public void Test_Concat09() {
            IPexChoiceRecorder choices = PexChoose.NewTest();
            choices.DefaultSession
                .At(0, "requireLarge", (object)true)
                .At(2, "requireLarge", (object)true)
                .At(3, "goDeeper", (object)true)
                .At(4, "length1", (object)94);
            this.Test_Concat(new string('@', 100), new string('\u0001', 100));
        }

    }
}
