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
        public void Test_SingleElementVector01() {
            this.Test_SingleElementVector<int>(0, 1);
        }

        [Fact]
        [PexGeneratedBy(typeof(VectorTests))]
        public void Test_SingleElementVector02() {
            this.Test_SingleElementVector<int>(1, 3);
        }

        [Fact]
        [PexGeneratedBy(typeof(VectorTests))]
        public void Test_SingleElementVector03() {
            this.Test_SingleElementVector<int>(1073741824, 3);
        }

        [Fact]
        [PexGeneratedBy(typeof(VectorTests))]
        public void Test_SingleElementVector04() {
            this.Test_SingleElementVector<object>(0, (object)null);
        }

        [Fact]
        [PexGeneratedBy(typeof(VectorTests))]
        public void Test_SingleElementVector05() {
            object boxi = (object)(default(int));
            this.Test_SingleElementVector<object>(0, boxi);
        }

        [Fact]
        [PexGeneratedBy(typeof(VectorTests))]
        public void Test_SingleElementVector06() {
            object s0 = new object();
            this.Test_SingleElementVector<object>(0, s0);
        }

        [Fact]
        [PexGeneratedBy(typeof(VectorTests))]
        public void Test_SingleElementVector08() {
            this.Test_SingleElementVector<object>(1073741824, (object)null);
        }

[Fact]
[PexGeneratedBy(typeof(VectorTests))]
public void Test_SingleElementVector15()
{
    object boxl = (object)(default(ulong));
    this.Test_SingleElementVector<object>(0, boxl);
}
[Fact]
[PexGeneratedBy(typeof(VectorTests))]
public void Test_SingleElementVector17()
{
    object boxu = (object)(default(uint));
    this.Test_SingleElementVector<object>(0, boxu);
}
[Fact]
[PexGeneratedBy(typeof(VectorTests))]
public void Test_SingleElementVector18()
{
    object boxw = (object)(default(ushort));
    this.Test_SingleElementVector<object>(0, boxw);
}
[Fact]
[PexGeneratedBy(typeof(VectorTests))]
public void Test_SingleElementVector19()
{
    object box = (object)(default(TimeSpan));
    this.Test_SingleElementVector<object>(0, box);
}
[Fact]
[PexGeneratedBy(typeof(VectorTests))]
public void Test_SingleElementVector20()
{
    object boxb = (object)(default(sbyte));
    this.Test_SingleElementVector<object>(0, boxb);
}
[Fact]
[PexGeneratedBy(typeof(VectorTests))]
public void Test_SingleElementVector21()
{
    object boxl = (object)(default(long));
    this.Test_SingleElementVector<object>(0, boxl);
}
[Fact]
[PexGeneratedBy(typeof(VectorTests))]
public void Test_SingleElementVector22()
{
    object boxw = (object)(default(short));
    this.Test_SingleElementVector<object>(0, boxw);
}
[Fact]
[PexGeneratedBy(typeof(VectorTests))]
public void Test_SingleElementVector23()
{
    object box = (object)(default(Guid));
    this.Test_SingleElementVector<object>(0, box);
}
[Fact]
[PexGeneratedBy(typeof(VectorTests))]
public void Test_SingleElementVector24()
{
    object boxd = (object)(default(double));
    this.Test_SingleElementVector<object>(0, boxd);
}
[Fact]
[PexGeneratedBy(typeof(VectorTests))]
public void Test_SingleElementVector25()
{
    object boxc = (object)(default(char));
    this.Test_SingleElementVector<object>(0, boxc);
}
[Fact]
[PexGeneratedBy(typeof(VectorTests))]
public void Test_SingleElementVector26()
{
    object boxb = (object)(default(byte));
    this.Test_SingleElementVector<object>(0, boxb);
}
[Fact]
[PexGeneratedBy(typeof(VectorTests))]
public void Test_SingleElementVector27()
{
    object boxb = (object)(default(bool));
    this.Test_SingleElementVector<object>(0, boxb);
}
[Fact]
[PexGeneratedBy(typeof(VectorTests))]
public void Test_SingleElementVector28()
{
    object box = (object)(default(DateTimeOffset));
    this.Test_SingleElementVector<object>(0, box);
}
[Fact]
[PexGeneratedBy(typeof(VectorTests))]
public void Test_SingleElementVector29()
{
    object box = (object)(default(DateTime));
    this.Test_SingleElementVector<object>(0, box);
}
[Fact]
[PexGeneratedBy(typeof(VectorTests))]
public void Test_SingleElementVector30()
{
    Version version;
    version = new Version(0, 0, 0, 0);
    this.Test_SingleElementVector<object>(0, (object)version);
}
    }
}
