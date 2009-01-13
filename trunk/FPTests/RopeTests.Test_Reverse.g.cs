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
        public void Test_Reverse01() {
            IPexChoiceRecorder choices = PexChoose.NewTest();
            this.Test_Reverse("@");
        }

        [Fact]
        [PexGeneratedBy(typeof(RopeTests))]
        public void Test_Reverse02() {
            IPexChoiceRecorder choices = PexChoose.NewTest();
            choices.DefaultSession
                .At(0, "requireLarge", (object)true);
            this.Test_Reverse(new string('\u0001', 126));
        }

        [Fact]
        [PexGeneratedBy(typeof(RopeTests))]
        public void Test_Reverse03() {
            IPexChoiceRecorder choices = PexChoose.NewTest();
            choices.DefaultSession
                .At(0, "requireLarge", (object)true)
                .At(1, "goDeeper", (object)true);
            this.Test_Reverse(new string('@', 100));
        }

        [Fact]
        [PexGeneratedBy(typeof(RopeTests))]
        public void Test_Reverse04() {
            IPexChoiceRecorder choices = PexChoose.NewTest();
            choices.DefaultSession
                .At(0, "requireLarge", (object)true)
                .At(1, "goDeeper", (object)true)
                .At(2, "length1", (object)100);
            this.Test_Reverse(new string('\u0001', 100));
        }

        [Fact]
        [PexGeneratedBy(typeof(RopeTests))]
        public void Test_Reverse05() {
            IPexChoiceRecorder choices = PexChoose.NewTest();
            choices.DefaultSession
                .At(0, "requireLarge", (object)true)
                .At(1, "goDeeper", (object)true)
                .At(3, "goDeeper", (object)true);
            this.Test_Reverse(new string('\u0001', 100));
        }

        [Fact]
        [PexGeneratedBy(typeof(RopeTests))]
        public void Test_Reverse06() {
            IPexChoiceRecorder choices = PexChoose.NewTest();
            choices.DefaultSession
                .At(0, "requireLarge", (object)true)
                .At(1, "goDeeper", (object)true)
                .At(2, "length1", (object)1);
            this.Test_Reverse(new string('\u0001', 100));
        }

        [Fact]
        [PexGeneratedBy(typeof(RopeTests))]
        public void Test_Reverse07() {
            IPexChoiceRecorder choices = PexChoose.NewTest();
            choices.DefaultSession
                .At(0, "requireLarge", (object)true)
                .At(1, "goDeeper", (object)true)
                .At(2, "length1", (object)126)
                .At(3, "goDeeper", (object)true)
                .At(4, "length1", (object)126)
                .At(5, "goDeeper", (object)true);
            this.Test_Reverse(new string('\u0001', 126));
        }

        [Fact]
        [PexGeneratedBy(typeof(RopeTests))]
        public void Test_Reverse08() {
            IPexChoiceRecorder choices = PexChoose.NewTest();
            choices.DefaultSession
                .At(0, "requireLarge", (object)true)
                .At(1, "goDeeper", (object)true)
                .At(2, "length1", (object)120)
                .At(3, "goDeeper", (object)true)
                .At(4, "length1", (object)114)
                .At(5, "goDeeper", (object)true)
                .At(7, "goDeeper", (object)true)
                .At(10, "goDeeper", (object)true);
            this.Test_Reverse(new string('\u0002', 126));
        }

        [Fact]
        [PexGeneratedBy(typeof(RopeTests))]
        public void Test_Reverse09() {
            IPexChoiceRecorder choices = PexChoose.NewTest();
            choices.DefaultSession
                .At(0, "requireLarge", (object)true)
                .At(1, "goDeeper", (object)true)
                .At(2, "length1", (object)119)
                .At(3, "goDeeper", (object)true)
                .At(4, "length1", (object)95)
                .At(5, "goDeeper", (object)true)
                .At(7, "goDeeper", (object)true)
                .At(10, "goDeeper", (object)true);
            this.Test_Reverse(new string('\u0004', 126));
        }

        [Fact]
        [PexGeneratedBy(typeof(RopeTests))]
        public void Test_Reverse10() {
            IPexChoiceRecorder choices = PexChoose.NewTest();
            choices.DefaultSession
                .At(0, "requireLarge", (object)true)
                .At(1, "goDeeper", (object)true)
                .At(2, "length1", (object)13)
                .At(3, "goDeeper", (object)true)
                .At(4, "length1", (object)6)
                .At(6, "goDeeper", (object)true)
                .At(7, "length1", (object)6);
            this.Test_Reverse(new string('\u0800', 100));
        }

        [Fact]
        [PexGeneratedBy(typeof(RopeTests))]
        public void Test_Reverse21() {
            IPexChoiceRecorder choices = PexChoose.NewTest();
            choices.DefaultSession
                .At(0, "requireLarge", (object)true)
                .At(1, "goDeeper", (object)true)
                .At(2, "length1", (object)71)
                .At(3, "goDeeper", (object)true)
                .At(4, "length1", (object)40)
                .At(6, "goDeeper", (object)true)
                .At(7, "length1", (object)30);
            this.Test_Reverse(new string('\u0010', 100));
        }

    }
}
