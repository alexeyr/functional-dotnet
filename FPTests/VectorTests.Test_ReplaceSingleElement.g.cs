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
    public partial class VectorTests {
        [Fact]
        [PexGeneratedBy(typeof(VectorTests))]
        public void Test_ReplaceSingleElement01() {
            int[] ints = new int[0];
            this.Test_ReplaceSingleElement<int>(ints, 2, 0);
        }

[Fact(Skip = "the test state was: path bounds exceeded")]
[PexGeneratedBy(typeof(VectorTests))]
public void Test_ReplaceSingleElement07()
{
    int[] ints = new int[0];
    this.Test_ReplaceSingleElement<int>(ints, 3, 1073741824);
}
    }
}
