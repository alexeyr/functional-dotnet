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
using Microsoft.Pex.Framework.Exceptions;

namespace FPTests
{
    public partial class RopeTests {
        [Fact]
        [PexGeneratedBy(typeof(RopeTests))]
        public void Test_EndsWithOrdinal01() {
            this.Test_EndsWithOrdinal("", "");
        }

        [Fact]
        [PexGeneratedBy(typeof(RopeTests))]
        public void Test_EndsWithOrdinal02() {
            this.Test_EndsWithOrdinal("\0", "\0");
        }

        [Fact]
        [PexGeneratedBy(typeof(RopeTests))]
        public void Test_EndsWithOrdinal03() {
            this.Test_EndsWithOrdinal("\0\0", "\0\0");
        }

        [Fact]
        [PexGeneratedBy(typeof(RopeTests))]
        public void Test_EndsWithOrdinal04() {
            this.Test_EndsWithOrdinal("\0", "\u0001");
        }

        [Fact]
        [PexGeneratedBy(typeof(RopeTests))]
        public void Test_EndsWithOrdinal05() {
            this.Test_EndsWithOrdinal("", "\0");
        }

        [Fact]
        [PexGeneratedBy(typeof(RopeTests))]
        public void Test_EndsWithOrdinal15() {
            this.Test_EndsWithOrdinal("PP", "pp");
        }

    }
}
