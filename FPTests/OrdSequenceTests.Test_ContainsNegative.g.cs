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
        public void Test_ContainsNegative01() {
            int[] ints = new int[0];
            this.Test_ContainsNegative<int>(ints, 2);
        }

        [Fact]
        [PexGeneratedBy(typeof(OrdSequenceTests))]
        public void Test_ContainsNegative02() {
            int[] ints = new int[1];
            this.Test_ContainsNegative<int>(ints, 2);
        }

        [Fact]
        [PexGeneratedBy(typeof(OrdSequenceTests))]
        public void Test_ContainsNegative03() {
            int[] ints = new int[1];
            ints[0] = 1;
            this.Test_ContainsNegative<int>(ints, 0);
        }

        [Fact]
        [PexGeneratedBy(typeof(OrdSequenceTests))]
        public void Test_ContainsNegative04() {
            int[] ints = new int[2];
            this.Test_ContainsNegative<int>(ints, 2);
        }

        [Fact]
        [PexGeneratedBy(typeof(OrdSequenceTests))]
        public void Test_ContainsNegative05() {
            int[] ints = new int[2];
            ints[0] = -1679912848;
            ints[1] = 1610616719;
            this.Test_ContainsNegative<int>(ints, 2);
        }

        [Fact]
        [PexGeneratedBy(typeof(OrdSequenceTests))]
        public void Test_ContainsNegative06() {
            int[] ints = new int[2];
            ints[0] = 524289;
            ints[1] = 521208;
            this.Test_ContainsNegative<int>(ints, 2);
        }

        [Fact]
        [PexGeneratedBy(typeof(OrdSequenceTests))]
        public void Test_ContainsNegative07() {
            int[] ints = new int[3];
            this.Test_ContainsNegative<int>(ints, 2);
        }

        [Fact]
        [PexGeneratedBy(typeof(OrdSequenceTests))]
        public void Test_ContainsNegative08() {
            int[] ints = new int[6];
            this.Test_ContainsNegative<int>(ints, 2);
        }

        [Fact]
        [PexGeneratedBy(typeof(OrdSequenceTests))]
        public void Test_ContainsNegative09() {
            int[] ints = new int[14];
            this.Test_ContainsNegative<int>(ints, 2);
        }

        [Fact]
        [PexGeneratedBy(typeof(OrdSequenceTests))]
        public void Test_ContainsNegative10() {
            int[] ints = new int[3];
            ints[0] = 524289;
            ints[1] = 521208;
            this.Test_ContainsNegative<int>(ints, 2);
        }

        [Fact]
        [PexGeneratedBy(typeof(OrdSequenceTests))]
        public void Test_ContainsNegative11() {
            int[] ints = new int[6];
            ints[0] = 524289;
            ints[1] = 521208;
            this.Test_ContainsNegative<int>(ints, 2);
        }

        [Fact]
        [PexGeneratedBy(typeof(OrdSequenceTests))]
        public void Test_ContainsNegative12() {
            int[] ints = new int[3];
            ints[0] = -1679912848;
            ints[1] = 1610616719;
            this.Test_ContainsNegative<int>(ints, 2);
        }

        [Fact]
        [PexGeneratedBy(typeof(OrdSequenceTests))]
        public void Test_ContainsNegative15() {
            int[] ints = new int[7];
            ints[0] = 537028617;
            ints[1] = -1870568446;
            ints[2] = -1996623901;
            ints[3] = -1870568446;
            ints[4] = -1870568446;
            ints[5] = -1870568446;
            ints[6] = -1870568446;
            this.Test_ContainsNegative<int>(ints, 9);
        }

        [Fact]
        [PexGeneratedBy(typeof(OrdSequenceTests))]
        public void Test_ContainsNegative16() {
            int[] ints = new int[15];
            ints[0] = 536870945;
            ints[1] = -1879044084;
            ints[2] = -1879053330;
            this.Test_ContainsNegative<int>(ints, 8);
        }

        [Fact]
        public void Test_ContainsNegative25() {
            int[] ints = new int[31];
            ints[0] = 1069727744;
            ints[1] = -2145157247;
            ints[2] = -2145173632;
            ints[3] = 536870912;
            ints[4] = 1073741824;
            ints[6] = -28544;
            ints[7] = -537198209;
            ints[15] = 1610608638;
            this.Test_ContainsNegative<int>(ints, 1073748354);
        }

        [Fact]
        public void Test_ContainsNegative28() {
            int[] ints = new int[31];
            ints[0] = 1611816960;
            ints[1] = -1597618853;
            ints[2] = -1626890825;
            ints[3] = 1610895616;
            ints[4] = 2146384869;
            ints[5] = 1089683329;
            ints[6] = -1070528436;
            ints[7] = -1070596094;
            ints[8] = -1070528482;
            ints[9] = -165;
            ints[10] = -117;
            ints[15] = 1610608638;
            this.Test_ContainsNegative<int>(ints, 1073748354);
        }

        [Fact]
        [PexGeneratedBy(typeof(OrdSequenceTests))]
        public void Test_ContainsNegative39() {
            int[] ints = new int[31];
            this.Test_ContainsNegative<int>(ints, 2);
        }

        [Fact]
        [PexGeneratedBy(typeof(OrdSequenceTests))]
        public void Test_ContainsNegative52() {
            int[] ints = new int[15];
            ints[0] = 8386560;
            ints[1] = -2147481600;
            ints[2] = -2147481602;
            ints[3] = -2064;
            ints[4] = -2;
            this.Test_ContainsNegative<int>(ints, 8);
        }

        [Fact]
        [PexGeneratedBy(typeof(OrdSequenceTests))]
        public void Test_ContainsNegative55() {
            int[] ints = new int[15];
            ints[0] = 1082130432;
            ints[1] = -18874305;
            ints[2] = -1073743826;
            ints[3] = 335544320;
            ints[4] = 538968006;
            ints[5] = -1090559969;
            this.Test_ContainsNegative<int>(ints, 8);
        }

        [Fact]
        [PexGeneratedBy(typeof(OrdSequenceTests))]
        public void Test_ContainsNegative57() {
            int[] ints = new int[15];
            ints[0] = 603979777;
            ints[1] = -1114111;
            ints[2] = -2081488896;
            ints[3] = 536870912;
            ints[4] = -2081488897;
            ints[5] = 536870912;
            ints[6] = 536870911;
            this.Test_ContainsNegative<int>(ints, 8);
        }

        [Fact]
        [PexGeneratedBy(typeof(OrdSequenceTests))]
        public void Test_ContainsNegative23() {
            int[] ints = new int[8];
            this.Test_ContainsNegative<int>(ints, 2);
        }

        [Fact]
        [PexGeneratedBy(typeof(OrdSequenceTests))]
        public void Test_ContainsNegative30() {
            int[] ints = new int[6];
            ints[0] = -1679912848;
            ints[1] = 1610616719;
            this.Test_ContainsNegative<int>(ints, 2);
        }

        [Fact]
        [PexGeneratedBy(typeof(OrdSequenceTests))]
        public void Test_ContainsNegative31() {
            int[] ints = new int[6];
            ints[0] = int.MinValue;
            ints[1] = int.MaxValue;
            ints[2] = 2130706433;
            ints[3] = 2130444288;
            this.Test_ContainsNegative<int>(ints, 2);
        }

        [Fact]
        [PexGeneratedBy(typeof(OrdSequenceTests))]
        public void Test_ContainsNegative35() {
            int[] ints = new int[15];
            ints[0] = 2145394687;
            ints[1] = -2145394688;
            ints[2] = -2147475458;
            ints[3] = 1073741824;
            ints[4] = -2088962;
            this.Test_ContainsNegative<int>(ints, 8);
        }

        [Fact]
        [PexGeneratedBy(typeof(OrdSequenceTests))]
        public void Test_ContainsNegative36() {
            int[] ints = new int[15];
            ints[0] = 1207959552;
            ints[1] = -2013265922;
            ints[2] = -2013265924;
            ints[3] = 1073741824;
            ints[4] = 1073741822;
            ints[5] = int.MinValue;
            this.Test_ContainsNegative<int>(ints, 8);
        }

        [Fact]
        [PexGeneratedBy(typeof(OrdSequenceTests))]
        public void Test_ContainsNegative37() {
            int[] ints = new int[15];
            ints[0] = 1073744899;
            ints[1] = -1073509375;
            ints[2] = -2147475457;
            ints[3] = 1073741824;
            ints[4] = 536875902;
            ints[5] = int.MinValue;
            ints[6] = -1610841984;
            this.Test_ContainsNegative<int>(ints, 8);
        }

        [Fact]
        [PexGeneratedBy(typeof(OrdSequenceTests))]
        public void Test_ContainsNegative41() {
            int[] ints = new int[15];
            ints[0] = int.MaxValue;
            ints[1] = -1207679934;
            ints[2] = -2013578322;
            ints[3] = 1073741824;
            ints[4] = 1073741822;
            ints[5] = -1421279252;
            ints[6] = -1140850436;
            ints[7] = -1749286882;
            this.Test_ContainsNegative<int>(ints, 8);
        }

        [Fact]
        [PexGeneratedBy(typeof(OrdSequenceTests))]
        public void Test_ContainsNegative44() {
            int[] ints = new int[15];
            ints[0] = 1073741825;
            ints[1] = -1073217536;
            ints[2] = -1091899393;
            ints[3] = -404091136;
            ints[4] = 524272;
            ints[5] = -2147155969;
            ints[6] = -1207959552;
            ints[7] = -331610244;
            ints[8] = -401481089;
            this.Test_ContainsNegative<int>(ints, 8);
        }

        [Fact]
        [PexGeneratedBy(typeof(OrdSequenceTests))]
        public void Test_ContainsNegative45() {
            int[] ints = new int[15];
            ints[0] = 1074003969;
            ints[1] = -860815360;
            ints[2] = -1757675521;
            ints[3] = 599527151;
            ints[4] = -2013576194;
            ints[5] = -1142161416;
            this.Test_ContainsNegative<int>(ints, 8);
        }

        [Fact]
        [PexGeneratedBy(typeof(OrdSequenceTests))]
        public void Test_ContainsNegative46() {
            int[] ints = new int[15];
            ints[0] = 1610874880;
            ints[1] = -1962934014;
            ints[2] = -1962934274;
            ints[3] = 1610645556;
            ints[4] = 1346643457;
            ints[5] = -1870665741;
            ints[6] = -1874598560;
            ints[7] = -1879048194;
            ints[8] = -1874598560;
            ints[9] = -402669929;
            ints[10] = -4161390;
            ints[11] = -4161390;
            ints[12] = -4161390;
            ints[13] = -4161390;
            ints[14] = -558061;
            this.Test_ContainsNegative<int>(ints, 8);
        }

        [Fact]
        [PexGeneratedBy(typeof(OrdSequenceTests))]
        public void Test_ContainsNegative59() {
            int[] ints = new int[15];
            ints[0] = 805306368;
            ints[1] = -4097;
            ints[2] = -1879052290;
            ints[3] = -4096;
            ints[4] = 536870912;
            ints[5] = -4098;
            ints[6] = -1879048194;
            ints[7] = -1073741826;
            this.Test_ContainsNegative<int>(ints, 8);
        }

        [Fact]
        [PexGeneratedBy(typeof(OrdSequenceTests))]
        public void Test_ContainsNegative60() {
            int[] ints = new int[16];
            ints[0] = 1409351747;
            ints[1] = -1106789953;
            ints[2] = -1562468784;
            ints[3] = 1174949888;
            ints[4] = 771692609;
            ints[5] = -234616800;
            ints[6] = 1073741824;
            ints[7] = 8394816;
            ints[8] = -553484408;
            ints[9] = -8329712;
            ints[10] = -402585856;
            ints[11] = -1041204225;
            ints[12] = -906030081;
            ints[13] = -906003457;
            ints[14] = -847264256;
            ints[15] = 1409351747;
            this.Test_ContainsNegative<int>(ints, 17);
        }

        [Fact]
        [PexGeneratedBy(typeof(OrdSequenceTests))]
        public void Test_ContainsNegative61() {
            int[] ints = new int[15];
            ints[0] = 1140162560;
            ints[1] = -2134900736;
            ints[2] = -2143977730;
            ints[3] = 587202560;
            ints[4] = 4194304;
            ints[5] = -1573175368;
            ints[6] = -2130706425;
            ints[7] = -2130706436;
            ints[8] = -2024266781;
            ints[9] = -1879014401;
            ints[10] = -1614807042;
            ints[11] = -1614807042;
            ints[12] = -1883269120;
            ints[13] = -4161390;
            ints[14] = -558061;
            this.Test_ContainsNegative<int>(ints, 8);
        }

[Fact(Skip = "the test state was: path bounds exceeded")]
[PexGeneratedBy(typeof(OrdSequenceTests))]
public void Test_ContainsNegative34()
{
    int[] ints = new int[31];
    ints[0] = 1069727744;
    ints[1] = -2145157247;
    ints[2] = -2145173632;
    ints[3] = 536870912;
    ints[4] = 1073741824;
    ints[6] = -28544;
    ints[7] = -537198209;
    ints[15] = 1610608638;
    this.Test_ContainsNegative<int>(ints, 1073748354);
}
[Fact(Skip = "the test state was: path bounds exceeded")]
[PexGeneratedBy(typeof(OrdSequenceTests))]
public void Test_ContainsNegative38()
{
    int[] ints = new int[31];
    ints[0] = 1611816960;
    ints[1] = -1597618853;
    ints[2] = -1626890825;
    ints[3] = 1610895616;
    ints[4] = 2146384869;
    ints[5] = 1089683329;
    ints[6] = -1070528436;
    ints[7] = -1070596094;
    ints[8] = -1070528482;
    ints[9] = -165;
    ints[10] = 139;
    ints[15] = 1610608638;
    this.Test_ContainsNegative<int>(ints, 1073748354);
}
[Fact]
[PexGeneratedBy(typeof(OrdSequenceTests))]
public void Test_ContainsNegative40()
{
    int[] ints = new int[15];
    ints[0] = 8386560;
    ints[1] = -2147481600;
    ints[2] = -2147481602;
    ints[3] = -2064;
    ints[4] = 254;
    this.Test_ContainsNegative<int>(ints, 8);
}
[Fact]
[PexGeneratedBy(typeof(OrdSequenceTests))]
public void Test_ContainsNegative51()
{
    int[] ints = new int[8];
    ints[0] = 1109917711;
    ints[1] = -2111307769;
    ints[2] = int.MinValue;
    ints[3] = -1037565947;
    ints[4] = 34078724;
    ints[5] = int.MinValue;
    ints[6] = 35651588;
    ints[7] = 524288;
    this.Test_ContainsNegative<int>(ints, 0);
}
[Fact]
[PexGeneratedBy(typeof(OrdSequenceTests))]
public void Test_ContainsNegative53()
{
    int[] ints = new int[10];
    ints[0] = 135381891;
    ints[1] = -1073676287;
    ints[2] = int.MinValue;
    ints[3] = 134283264;
    ints[4] = 135381891;
    ints[5] = 570425344;
    ints[6] = 33554434;
    ints[7] = 33554432;
    ints[8] = 33554432;
    ints[9] = 16384;
    this.Test_ContainsNegative<int>(ints, 0);
}
[Fact]
[PexGeneratedBy(typeof(OrdSequenceTests))]
public void Test_ContainsNegative54()
{
    int[] ints = new int[10];
    ints[0] = -1069531135;
    ints[1] = -2147418096;
    ints[2] = int.MinValue;
    ints[3] = -1069546484;
    ints[4] = 1077952512;
    ints[5] = -1069547511;
    ints[6] = -2143289208;
    ints[7] = -1878522736;
    ints[8] = 33554432;
    ints[9] = 16384;
    this.Test_ContainsNegative<int>(ints, 0);
}
[Fact]
[PexGeneratedBy(typeof(OrdSequenceTests))]
public void Test_ContainsNegative56()
{
    int[] ints = new int[10];
    ints[0] = -1590689725;
    ints[1] = -2147221503;
    ints[2] = int.MinValue;
    ints[3] = -1610350592;
    ints[4] = -1586757102;
    ints[5] = -1586757102;
    ints[6] = 33554434;
    ints[7] = 33554432;
    ints[8] = 33554432;
    ints[9] = 16384;
    this.Test_ContainsNegative<int>(ints, 0);
}
[Fact]
[PexGeneratedBy(typeof(OrdSequenceTests))]
public void Test_ContainsNegative58()
{
    int[] ints = new int[10];
    ints[0] = -2125033796;
    ints[1] = -2147483532;
    ints[2] = int.MinValue;
    ints[3] = -2147448719;
    ints[4] = -2125033796;
    ints[5] = 16777348;
    ints[6] = -2147450880;
    ints[7] = -2080374783;
    ints[8] = -2013700601;
    ints[9] = -2013700594;
    this.Test_ContainsNegative<int>(ints, -2013700602);
}
[Fact]
[PexGeneratedBy(typeof(OrdSequenceTests))]
public void Test_ContainsNegative62()
{
    int[] ints = new int[9];
    ints[0] = 671023079;
    ints[1] = -2139161119;
    ints[2] = int.MinValue;
    ints[3] = -2138800127;
    ints[4] = 7634944;
    ints[5] = -2139161119;
    ints[6] = -1477832957;
    ints[7] = 669650432;
    ints[8] = -404091136;
    this.Test_ContainsNegative<int>(ints, 0);
}
    }
}
