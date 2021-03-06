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
    public partial class OrdSequenceTests {
        [Fact]
        [PexGeneratedBy(typeof(OrdSequenceTests))]
        public void Test_Insert01() {
            int[] ints = new int[0];
            this.Test_Insert<int>(ints, ints);
        }

        [Fact]
        [PexGeneratedBy(typeof(OrdSequenceTests))]
        public void Test_Insert02() {
            int[] ints = new int[1];
            this.Test_Insert<int>(ints, ints);
        }

        [Fact]
        [PexGeneratedBy(typeof(OrdSequenceTests))]
        public void Test_Insert03() {
            int[] ints = new int[1];
            int[] ints1 = new int[1];
            ints[0] = -2147479554;
            ints1[0] = 2139062272;
            this.Test_Insert<int>(ints, ints1);
        }

        [Fact]
        [PexGeneratedBy(typeof(OrdSequenceTests))]
        public void Test_Insert04() {
            int[] ints = new int[0];
            int[] ints1 = new int[1];
            this.Test_Insert<int>(ints, ints1);
        }

        [Fact]
        [PexGeneratedBy(typeof(OrdSequenceTests))]
        public void Test_Insert05() {
            int[] ints = new int[1];
            int[] ints1 = new int[2];
            ints[0] = 67108864;
            this.Test_Insert<int>(ints, ints1);
        }

        [Fact]
        [PexGeneratedBy(typeof(OrdSequenceTests))]
        public void Test_Insert06() {
            int[] ints = new int[1];
            int[] ints1 = new int[3];
            ints[0] = 67108864;
            this.Test_Insert<int>(ints, ints1);
        }

        [Fact]
        [PexGeneratedBy(typeof(OrdSequenceTests))]
        public void Test_Insert07() {
            int[] ints = new int[1];
            int[] ints1 = new int[6];
            ints[0] = 67108864;
            this.Test_Insert<int>(ints, ints1);
        }

        [Fact]
        [PexGeneratedBy(typeof(OrdSequenceTests))]
        public void Test_Insert08() {
            int[] ints = new int[2];
            ints[0] = 67108864;
            ints[1] = 67108830;
            this.Test_Insert<int>(ints, ints);
        }

        [Fact]
        [PexGeneratedBy(typeof(OrdSequenceTests))]
        public void Test_Insert09() {
            int[] ints = new int[2];
            ints[0] = int.MinValue;
            ints[1] = int.MaxValue;
            this.Test_Insert<int>(ints, ints);
        }

        [Fact]
        [PexGeneratedBy(typeof(OrdSequenceTests))]
        public void Test_Insert10() {
            int[] ints = new int[3];
            ints[0] = 67108864;
            ints[1] = 67108830;
            this.Test_Insert<int>(ints, ints);
        }

        [Fact]
        [PexGeneratedBy(typeof(OrdSequenceTests))]
        public void Test_Insert11() {
            int[] ints = new int[1];
            int[] ints1 = new int[14];
            ints[0] = 67108864;
            this.Test_Insert<int>(ints, ints1);
        }

        [Fact]
        [PexGeneratedBy(typeof(OrdSequenceTests))]
        public void Test_Insert12() {
            int[] ints = new int[0];
            int[] ints1 = new int[3];
            ints1[0] = int.MinValue;
            ints1[1] = int.MaxValue;
            this.Test_Insert<int>(ints, ints1);
        }

        [Fact]
        [PexGeneratedBy(typeof(OrdSequenceTests))]
        public void Test_Insert13() {
            int[] ints = new int[6];
            ints[0] = 67108864;
            ints[1] = 67108830;
            this.Test_Insert<int>(ints, ints);
        }

        [Fact]
        [PexGeneratedBy(typeof(OrdSequenceTests))]
        public void Test_Insert15() {
            int[] ints = new int[7];
            ints[0] = 17047553;
            ints[1] = -2138832768;
            ints[2] = -2140899012;
            ints[3] = -2138832768;
            ints[4] = -2138832768;
            ints[5] = -2138832768;
            ints[6] = -2138832768;
            this.Test_Insert<int>(ints, ints);
        }

        [Fact]
        public void Test_Insert16() {
            int[] ints = new int[14];
            ints[0] = 1073741824;
            ints[1] = -1107427328;
            ints[2] = -1107427330;
            this.Test_Insert<int>(ints, ints);
        }

        [Fact]
        public void Test_Insert23() {
            int[] ints = new int[14];
            ints[0] = 1687166977;
            ints[1] = 73990144;
            ints[2] = -748544001;
            ints[3] = 751696384;
            ints[4] = 2097119940;
            this.Test_Insert<int>(ints, ints);
        }

        [Fact]
        public void Test_Insert24() {
            int[] ints = new int[14];
            ints[0] = 2140426240;
            ints[1] = 121798687;
            ints[2] = -604110944;
            ints[3] = 1677871070;
            ints[4] = 1864812544;
            ints[5] = 2051012607;
            ints[6] = 1677871070;
            ints[7] = 1677871070;
            ints[8] = 1677871070;
            ints[9] = 1677871070;
            ints[10] = 1677871070;
            ints[11] = 1677871070;
            ints[12] = 1677871070;
            ints[13] = 1677871070;
            this.Test_Insert<int>(ints, ints);
        }

        [Fact]
        public void Test_Insert26() {
            int[] ints = new int[14];
            ints[0] = 1073743872;
            ints[1] = -2147483640;
            ints[2] = -2147483642;
            ints[3] = 1073741823;
            ints[4] = 1073743871;
            ints[5] = 2147448832;
            ints[6] = 1073741822;
            ints[7] = 1073743872;
            ints[8] = 1073743872;
            ints[9] = 1073743872;
            ints[10] = 1073743872;
            ints[11] = 1073743872;
            ints[12] = 1073743872;
            ints[13] = 1073743872;
            this.Test_Insert<int>(ints, ints);
        }

        [Fact]
        [PexGeneratedBy(typeof(OrdSequenceTests))]
        public void Test_Insert35() {
            int[] ints = new int[1];
            int[] ints1 = new int[7];
            ints[0] = 67108864;
            this.Test_Insert<int>(ints, ints1);
        }

        [Fact]
        [PexGeneratedBy(typeof(OrdSequenceTests))]
        public void Test_Insert42() {
            int[] ints = new int[6];
            int[] ints1 = new int[4];
            ints[0] = 131584;
            ints[1] = -932707840;
            ints[2] = -1147143812;
            ints[3] = -932707840;
            ints[4] = -932707840;
            ints[5] = -932707840;
            ints1[0] = 100;
            ints1[1] = 100;
            ints1[2] = 100;
            ints1[3] = 100;
            this.Test_Insert<int>(ints, ints1);
        }

        [Fact]
        [PexGeneratedBy(typeof(OrdSequenceTests))]
        public void Test_Insert44() {
            int[] ints = new int[6];
            int[] ints1 = new int[4];
            ints[0] = 1;
            ints[1] = -805306351;
            ints[2] = -805347316;
            ints[4] = -268435439;
            ints[5] = -932707840;
            ints1[0] = 100;
            ints1[1] = 100;
            ints1[2] = 100;
            ints1[3] = 100;
            this.Test_Insert<int>(ints, ints1);
        }

        [Fact]
        [PexGeneratedBy(typeof(OrdSequenceTests))]
        public void Test_Insert45() {
            int[] ints = new int[7];
            ints[0] = 2147483643;
            ints[1] = -2147483631;
            ints[2] = -2147483633;
            ints[3] = 2147483641;
            ints[4] = 2147483640;
            ints[5] = -2147483643;
            ints[6] = 2147483643;
            this.Test_Insert<int>(ints, ints);
        }

        [Fact]
        [PexGeneratedBy(typeof(OrdSequenceTests))]
        public void Test_Insert46() {
            int[] ints = new int[7];
            ints[0] = 2147483643;
            ints[1] = -2147483631;
            ints[2] = -2147483633;
            ints[3] = 2147483641;
            ints[4] = 2147483640;
            ints[5] = -2147483643;
            ints[6] = int.MinValue;
            this.Test_Insert<int>(ints, ints);
        }

        [Fact]
        public void Test_Insert47() {
            int[] ints = new int[14];
            ints[0] = 2147483643;
            ints[1] = -2147483631;
            ints[2] = -2147483633;
            ints[3] = 2147483641;
            ints[4] = 2147483640;
            ints[5] = -2147483643;
            ints[6] = int.MinValue;
            ints[7] = 2147483643;
            ints[8] = 2147483643;
            ints[9] = 2147483643;
            ints[10] = 2147483643;
            ints[11] = 2147483643;
            ints[12] = 2147483643;
            ints[13] = 2147483643;
            this.Test_Insert<int>(ints, ints);
        }

        [Fact]
        public void Test_Insert48() {
            int[] ints = new int[14];
            ints[0] = 1073741824;
            ints[1] = -1107427328;
            ints[2] = -1107427330;
            this.Test_Insert<int>(ints, ints);
        }

        [Fact]
        public void Test_Insert49() {
            int[] ints = new int[14];
            ints[0] = 1073750016;
            ints[1] = -1073741891;
            ints[2] = -2147475458;
            ints[3] = 1073741824;
            ints[4] = 1073741824;
            ints[5] = int.MinValue;
            this.Test_Insert<int>(ints, ints);
        }

        [Fact]
        [PexGeneratedBy(typeof(OrdSequenceTests))]
        public void Test_Insert50() {
            int[] ints = new int[7];
            int[] ints1 = new int[8];
            ints[0] = 269746438;
            ints[1] = -1902935816;
            ints[2] = -2013675266;
            ints[3] = 2308;
            ints[4] = -573390823;
            ints[5] = -977796386;
            ints1[1] = -1104176895;
            ints1[2] = -1902935816;
            ints1[3] = -536804852;
            ints1[4] = -536865792;
            ints1[5] = -413336399;
            this.Test_Insert<int>(ints, ints1);
        }

        [Fact]
        public void Test_Insert51() {
            int[] ints = new int[15];
            ints[0] = 536873025;
            ints[1] = -1073741567;
            ints[2] = -1346830272;
            ints[3] = 230686720;
            ints[4] = -1071644917;
            ints[5] = -1071645184;
            this.Test_Insert<int>(ints, ints);
        }

        [Fact]
        public void Test_Insert52() {
            int[] ints = new int[14];
            ints[0] = 2147483624;
            ints[1] = -1065353216;
            ints[2] = -1073741850;
            ints[3] = 8388608;
            ints[4] = 8388608;
            ints[5] = -1350567378;
            ints[6] = -1879048204;
            this.Test_Insert<int>(ints, ints);
        }

        [Fact]
        [PexGeneratedBy(typeof(OrdSequenceTests))]
        public void Test_Insert53() {
            int[] ints = new int[7];
            int[] ints1 = new int[8];
            ints[0] = 1610612737;
            ints[1] = -1610612737;
            ints[2] = int.MinValue;
            ints[3] = 1073741824;
            ints[4] = 536870911;
            ints[5] = -1;
            ints[6] = -1073741824;
            ints1[1] = -1104176895;
            ints1[2] = -1902935816;
            ints1[3] = -536804852;
            ints1[4] = -536865792;
            ints1[5] = -413336399;
            this.Test_Insert<int>(ints, ints1);
        }

        [Fact]
        [PexGeneratedBy(typeof(OrdSequenceTests))]
        public void Test_Insert54() {
            int[] ints = new int[7];
            int[] ints1 = new int[6];
            ints[0] = 1073750016;
            ints[1] = -2147307520;
            ints[2] = -2147320322;
            ints[3] = 1073741824;
            ints[4] = 1073655808;
            ints[5] = int.MinValue;
            ints[6] = -2;
            ints1[0] = int.MinValue;
            ints1[1] = int.MinValue;
            ints1[2] = int.MinValue;
            ints1[3] = int.MinValue;
            ints1[4] = int.MinValue;
            ints1[5] = int.MinValue;
            this.Test_Insert<int>(ints, ints1);
        }

        [Fact]
        public void Test_Insert55() {
            int[] ints = new int[14];
            ints[0] = 1610612768;
            ints[1] = -2147352575;
            ints[2] = int.MinValue;
            ints[3] = 1610612766;
            ints[4] = 1610612735;
            ints[5] = -2147352576;
            ints[6] = -2147352834;
            ints[7] = -2147352576;
            ints[9] = 2147483643;
            ints[10] = 2147483643;
            ints[11] = 2147483643;
            ints[12] = 2147483643;
            ints[13] = 2147483643;
            this.Test_Insert<int>(ints, ints);
        }

        [Fact]
        public void Test_Insert56() {
            int[] ints = new int[14];
            ints[0] = 1073742336;
            ints[1] = -1107296257;
            ints[2] = -1111523329;
            ints[3] = 1073741824;
            ints[4] = 1073740800;
            ints[5] = -1075838978;
            ints[6] = -2113929216;
            ints[7] = -2147352576;
            ints[9] = 2147483643;
            ints[10] = 2147483643;
            ints[11] = 2147483643;
            ints[12] = 2147483643;
            ints[13] = 2147483643;
            this.Test_Insert<int>(ints, ints);
        }

        [Fact]
        [PexGeneratedBy(typeof(OrdSequenceTests))]
        public void Test_Insert57() {
            int[] ints = new int[6];
            ints[0] = 63373827;
            ints[1] = 41109;
            ints[2] = -536952968;
            ints[3] = 63373827;
            this.Test_Insert<int>(ints, ints);
        }

        [Fact]
        [PexGeneratedBy(typeof(OrdSequenceTests))]
        public void Test_Insert58() {
            int[] ints = new int[8];
            ints[0] = 1895948808;
            ints[1] = -1942598566;
            ints[2] = -2080382472;
            ints[3] = 1598589541;
            ints[4] = 1342705664;
            ints[5] = -1085848568;
            ints[6] = -1547693824;
            ints[7] = -1085848568;
            this.Test_Insert<int>(ints, ints);
        }

        [Fact]
        public void Test_Insert59() {
            int[] ints = new int[15];
            ints[0] = 1073776640;
            ints[1] = -874023577;
            ints[2] = -1073940481;
            ints[3] = 1073741826;
            ints[4] = 546832512;
            ints[5] = -5242833;
            ints[6] = -5242977;
            ints[7] = -3211201;
            ints[8] = -885374976;
            ints[9] = -885374976;
            ints[10] = -885374976;
            ints[11] = -885374976;
            ints[12] = -885374976;
            ints[13] = -885374976;
            ints[14] = -885374976;
            this.Test_Insert<int>(ints, ints);
        }

        [Fact]
        public void Test_Insert60() {
            int[] ints = new int[14];
            ints[0] = 1140850689;
            ints[1] = -2143289345;
            ints[2] = -2143289349;
            ints[3] = 1073741824;
            ints[4] = 33554432;
            ints[5] = -1073741825;
            ints[6] = -2113931266;
            ints[7] = -2147483393;
            ints[9] = 2147483643;
            ints[10] = 2147483643;
            ints[11] = 2147483643;
            ints[12] = 2147483643;
            ints[13] = 2147483643;
            this.Test_Insert<int>(ints, ints);
        }

[Fact(Skip = "the test state was: path bounds exceeded")]
[PexGeneratedBy(typeof(OrdSequenceTests))]
public void Test_Insert33()
{
    int[] ints = new int[14];
    ints[0] = 1073741824;
    ints[1] = -1107427328;
    ints[2] = -1107427330;
    this.Test_Insert<int>(ints, ints);
}
[Fact(Skip = "the test state was: path bounds exceeded")]
[PexGeneratedBy(typeof(OrdSequenceTests))]
public void Test_Insert34()
{
    int[] ints = new int[14];
    ints[0] = 1687166977;
    ints[1] = 73990144;
    ints[2] = -748544001;
    ints[3] = 751696384;
    ints[4] = 2097119940;
    this.Test_Insert<int>(ints, ints);
}
[Fact(Skip = "the test state was: path bounds exceeded")]
[PexGeneratedBy(typeof(OrdSequenceTests))]
public void Test_Insert36()
{
    int[] ints = new int[14];
    ints[0] = 2140426240;
    ints[1] = 121798687;
    ints[2] = -604110944;
    ints[3] = 1677871070;
    ints[4] = 1864812544;
    ints[5] = 2051012607;
    ints[6] = 1677871070;
    ints[7] = 1677871070;
    ints[8] = 1677871070;
    ints[9] = 1677871070;
    ints[10] = 1677871070;
    ints[11] = 1677871070;
    ints[12] = 1677871070;
    ints[13] = 1677871070;
    this.Test_Insert<int>(ints, ints);
}
[Fact(Skip = "the test state was: path bounds exceeded")]
[PexGeneratedBy(typeof(OrdSequenceTests))]
public void Test_Insert37()
{
    int[] ints = new int[14];
    ints[0] = 1073743872;
    ints[1] = -2147483640;
    ints[2] = -2147483642;
    ints[3] = 1073741823;
    ints[4] = 1073743871;
    ints[5] = 2147448832;
    ints[6] = 1073741822;
    ints[7] = 1073743872;
    ints[8] = 1073743872;
    ints[9] = 1073743872;
    ints[10] = 1073743872;
    ints[11] = 1073743872;
    ints[12] = 1073743872;
    ints[13] = 1073743872;
    this.Test_Insert<int>(ints, ints);
}
[Fact(Skip = "the test state was: path bounds exceeded")]
[PexGeneratedBy(typeof(OrdSequenceTests))]
public void Test_Insert40()
{
    int[] ints = new int[14];
    ints[0] = 2147483643;
    ints[1] = -2147483631;
    ints[2] = -2147483633;
    ints[3] = 2147483641;
    ints[4] = 2147483640;
    ints[5] = -2147483643;
    ints[6] = int.MinValue;
    ints[7] = 2147483643;
    ints[8] = 2147483643;
    ints[9] = 2147483643;
    ints[10] = 2147483643;
    ints[11] = 2147483643;
    ints[12] = 2147483643;
    ints[13] = 2147483643;
    this.Test_Insert<int>(ints, ints);
}
[Fact(Skip = "the test state was: path bounds exceeded")]
[PexGeneratedBy(typeof(OrdSequenceTests))]
public void Test_Insert43()
{
    int[] ints = new int[15];
    ints[0] = 536873025;
    ints[1] = -1073741567;
    ints[2] = -1346830272;
    ints[3] = 230686720;
    ints[4] = -1071644917;
    ints[5] = -1071645184;
    this.Test_Insert<int>(ints, ints);
}
[Fact(Skip = "the test state was: path bounds exceeded")]
[PexGeneratedBy(typeof(OrdSequenceTests))]
public void Test_Insert61()
{
    int[] ints = new int[14];
    ints[0] = 2147483624;
    ints[1] = -1065353216;
    ints[2] = -1073741850;
    ints[3] = 8388608;
    ints[4] = 8388608;
    ints[5] = -1350567378;
    ints[6] = -1879048204;
    this.Test_Insert<int>(ints, ints);
}
[Fact]
[PexGeneratedBy(typeof(OrdSequenceTests))]
public void Test_Insert62()
{
    int[] ints = new int[7];
    int[] ints1 = new int[6];
    ints[0] = 1073750016;
    ints[1] = -2147307520;
    ints[2] = -2147320322;
    ints[3] = 1073741824;
    ints[4] = 1073655808;
    ints[5] = int.MinValue;
    ints[6] = 254;
    ints1[0] = int.MinValue;
    ints1[1] = int.MinValue;
    ints1[2] = int.MinValue;
    ints1[3] = int.MinValue;
    ints1[4] = int.MinValue;
    ints1[5] = int.MinValue;
    this.Test_Insert<int>(ints, ints1);
}
[Fact(Skip = "the test state was: path bounds exceeded")]
[PexGeneratedBy(typeof(OrdSequenceTests))]
public void Test_Insert63()
{
    int[] ints = new int[14];
    ints[0] = 1610612768;
    ints[1] = -2147352575;
    ints[2] = int.MinValue;
    ints[3] = 1610612766;
    ints[4] = 1610612735;
    ints[5] = -2147352576;
    ints[6] = -2147352834;
    ints[7] = -2147352576;
    ints[9] = 2147483643;
    ints[10] = 2147483643;
    ints[11] = 2147483643;
    ints[12] = 2147483643;
    ints[13] = 2147483643;
    this.Test_Insert<int>(ints, ints);
}
[Fact(Skip = "the test state was: path bounds exceeded")]
[PexGeneratedBy(typeof(OrdSequenceTests))]
public void Test_Insert64()
{
    int[] ints = new int[14];
    ints[0] = 1073742336;
    ints[1] = -1107296257;
    ints[2] = -1111523329;
    ints[3] = 1073741824;
    ints[4] = 1073740800;
    ints[5] = -1075838978;
    ints[6] = -2113929216;
    ints[7] = -2147352576;
    ints[9] = 2147483643;
    ints[10] = 2147483643;
    ints[11] = 2147483643;
    ints[12] = 2147483643;
    ints[13] = 2147483643;
    this.Test_Insert<int>(ints, ints);
}
[Fact(Skip = "the test state was: path bounds exceeded")]
[PexGeneratedBy(typeof(OrdSequenceTests))]
public void Test_Insert67()
{
    int[] ints = new int[15];
    ints[0] = 1073776640;
    ints[1] = -874023577;
    ints[2] = -1073940481;
    ints[3] = 1073741826;
    ints[4] = 546832512;
    ints[5] = -5242833;
    ints[6] = -5242977;
    ints[7] = -3211201;
    ints[8] = -885374976;
    ints[9] = -885374976;
    ints[10] = -885374976;
    ints[11] = -885374976;
    ints[12] = -885374976;
    ints[13] = -885374976;
    ints[14] = -885374976;
    this.Test_Insert<int>(ints, ints);
}
[Fact(Skip = "the test state was: path bounds exceeded")]
[PexGeneratedBy(typeof(OrdSequenceTests))]
public void Test_Insert68()
{
    int[] ints = new int[14];
    ints[0] = 1140850689;
    ints[1] = -2143289345;
    ints[2] = -2143289349;
    ints[3] = 1073741824;
    ints[4] = 33554432;
    ints[5] = -1073741825;
    ints[6] = -2113931266;
    ints[7] = -2147483393;
    ints[9] = 2147483643;
    ints[10] = 2147483643;
    ints[11] = 2147483643;
    ints[12] = 2147483643;
    ints[13] = 2147483643;
    this.Test_Insert<int>(ints, ints);
}
[Fact]
[PexGeneratedBy(typeof(OrdSequenceTests))]
public void Test_Insert69()
{
    int[] ints = new int[6];
    int[] ints1 = new int[4];
    ints[0] = 1;
    ints[2] = 1073741824;
    ints[3] = 2113880194;
    ints[4] = 2111520898;
    ints[5] = -1109704573;
    ints1[0] = 2147434625;
    ints1[1] = 2147434625;
    ints1[2] = 2147434625;
    ints1[3] = 2147434625;
    this.Test_Insert<int>(ints, ints1);
}
    }
}
