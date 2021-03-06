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
        public void Test_Intersect01() {
            int[] ints = new int[0];
            this.Test_Intersect<int>(ints, ints);
        }

        [Fact]
        [PexGeneratedBy(typeof(OrdSequenceTests))]
        public void Test_Intersect02() {
            int[] ints = new int[1];
            this.Test_Intersect<int>(ints, ints);
        }

        [Fact]
        [PexGeneratedBy(typeof(OrdSequenceTests))]
        public void Test_Intersect03() {
            int[] ints = new int[1];
            int[] ints1 = new int[1];
            ints1[0] = 67108864;
            this.Test_Intersect<int>(ints, ints1);
        }

        [Fact]
        [PexGeneratedBy(typeof(OrdSequenceTests))]
        public void Test_Intersect04() {
            int[] ints = new int[1];
            int[] ints1 = new int[1];
            ints[0] = 1407196192;
            ints1[0] = 136028355;
            this.Test_Intersect<int>(ints, ints1);
        }

        [Fact]
        [PexGeneratedBy(typeof(OrdSequenceTests))]
        public void Test_Intersect05() {
            int[] ints = new int[1];
            int[] ints1 = new int[2];
            this.Test_Intersect<int>(ints, ints1);
        }

        [Fact]
        [PexGeneratedBy(typeof(OrdSequenceTests))]
        public void Test_Intersect06() {
            int[] ints = new int[1];
            int[] ints1 = new int[2];
            ints1[0] = int.MinValue;
            ints1[1] = int.MaxValue;
            this.Test_Intersect<int>(ints, ints1);
        }

        [Fact]
        [PexGeneratedBy(typeof(OrdSequenceTests))]
        public void Test_Intersect07() {
            int[] ints = new int[1];
            int[] ints1 = new int[2];
            ints1[0] = 67108864;
            ints1[1] = 67108830;
            this.Test_Intersect<int>(ints, ints1);
        }

        [Fact]
        [PexGeneratedBy(typeof(OrdSequenceTests))]
        public void Test_Intersect08() {
            int[] ints = new int[1];
            int[] ints1 = new int[3];
            ints1[0] = 67108864;
            ints1[1] = 67108830;
            this.Test_Intersect<int>(ints, ints1);
        }

        [Fact]
        [PexGeneratedBy(typeof(OrdSequenceTests))]
        public void Test_Intersect09() {
            int[] ints = new int[1];
            int[] ints1 = new int[3];
            this.Test_Intersect<int>(ints, ints1);
        }

        [Fact]
        [PexGeneratedBy(typeof(OrdSequenceTests))]
        public void Test_Intersect10() {
            int[] ints = new int[1];
            int[] ints1 = new int[6];
            ints1[0] = 67108864;
            ints1[1] = 67108830;
            this.Test_Intersect<int>(ints, ints1);
        }

        [Fact]
        [PexGeneratedBy(typeof(OrdSequenceTests))]
        public void Test_Intersect11() {
            int[] ints = new int[2];
            this.Test_Intersect<int>(ints, ints);
        }

        [Fact]
        [PexGeneratedBy(typeof(OrdSequenceTests))]
        public void Test_Intersect12() {
            int[] ints = new int[2];
            ints[0] = int.MinValue;
            ints[1] = int.MaxValue;
            this.Test_Intersect<int>(ints, ints);
        }

        [Fact]
        [PexGeneratedBy(typeof(OrdSequenceTests))]
        public void Test_Intersect13() {
            int[] ints = new int[1];
            int[] ints1 = new int[7];
            ints1[0] = 570442304;
            ints1[1] = -1845489528;
            ints1[2] = -1946142074;
            ints1[3] = -1845489528;
            ints1[4] = -1845489528;
            ints1[5] = -1845489528;
            ints1[6] = -1845489528;
            this.Test_Intersect<int>(ints, ints1);
        }

        [Fact]
        public void Test_Intersect26() {
            int[] ints = new int[1];
            int[] ints1 = new int[30];
            ints1[0] = 1091076109;
            ints1[1] = -1341800698;
            ints1[2] = -1651605648;
            ints1[3] = 419516561;
            ints1[4] = 12599305;
            ints1[5] = -295084032;
            ints1[6] = -1255927417;
            ints1[7] = -1738014692;
            ints1[8] = -674723232;
            ints1[9] = -674723232;
            ints1[10] = -1064844801;
            ints1[11] = -32439968;
            ints1[12] = -32439968;
            ints1[13] = -32439968;
            ints1[14] = -32439968;
            ints1[15] = -32439968;
            ints1[16] = -32439968;
            ints1[17] = -32439968;
            ints1[18] = -32439968;
            ints1[19] = -32439968;
            ints1[20] = -32439968;
            ints1[21] = -32439968;
            ints1[22] = -32439968;
            ints1[23] = -32439968;
            ints1[24] = -32439968;
            ints1[25] = -32439968;
            ints1[26] = -32439968;
            ints1[27] = -32439968;
            ints1[28] = -32439968;
            ints1[29] = -32439968;
            this.Test_Intersect<int>(ints, ints1);
        }

        [Fact]
        [PexGeneratedBy(typeof(OrdSequenceTests))]
        public void Test_Intersect31() {
            int[] ints = new int[3];
            ints[0] = 67108864;
            ints[1] = 67108830;
            this.Test_Intersect<int>(ints, ints);
        }

        [Fact]
        [PexGeneratedBy(typeof(OrdSequenceTests))]
        public void Test_Intersect47() {
            int[] ints = new int[1];
            int[] ints1 = new int[6];
            ints1[0] = 1879048321;
            ints1[1] = -1879050367;
            ints1[2] = -2147483521;
            ints1[3] = -268435456;
            this.Test_Intersect<int>(ints, ints1);
        }

        [Fact]
        [PexGeneratedBy(typeof(OrdSequenceTests))]
        public void Test_Intersect48() {
            int[] ints = new int[1];
            int[] ints1 = new int[7];
            ints1[0] = 539067416;
            ints1[1] = -1628714720;
            ints1[2] = -2145505196;
            ints1[3] = 66846720;
            ints1[4] = 83952140;
            ints1[5] = 83952140;
            ints1[6] = 83952140;
            this.Test_Intersect<int>(ints, ints1);
        }

        [Fact]
        [PexGeneratedBy(typeof(OrdSequenceTests))]
        public void Test_Intersect64() {
            int[] ints = new int[6];
            ints[0] = 67108864;
            ints[1] = 67108830;
            this.Test_Intersect<int>(ints, ints);
        }

        [Fact]
        [PexGeneratedBy(typeof(OrdSequenceTests))]
        public void Test_Intersect34() {
            int[] ints = new int[1];
            int[] ints1 = new int[6];
            ints1[0] = 2147483617;
            ints1[1] = 16415;
            ints1[2] = 30;
            ints1[3] = 1073741824;
            ints1[4] = 1073741825;
            ints1[5] = 2147483616;
            this.Test_Intersect<int>(ints, ints1);
        }

        [Fact]
        [PexGeneratedBy(typeof(OrdSequenceTests))]
        public void Test_Intersect35() {
            int[] ints = new int[1];
            int[] ints1 = new int[7];
            ints1[0] = 2147483617;
            ints1[1] = 16415;
            ints1[2] = 30;
            ints1[3] = 1073741824;
            ints1[4] = 1073741825;
            ints1[5] = 2147483616;
            this.Test_Intersect<int>(ints, ints1);
        }

        [Fact]
        [PexGeneratedBy(typeof(OrdSequenceTests))]
        public void Test_Intersect36() {
            int[] ints = new int[1];
            int[] ints1 = new int[14];
            ints1[0] = 2147483617;
            ints1[1] = 16415;
            ints1[2] = 30;
            ints1[3] = 1073741824;
            ints1[4] = 1073741825;
            ints1[5] = 2147483616;
            this.Test_Intersect<int>(ints, ints1);
        }

        [Fact]
        [PexGeneratedBy(typeof(OrdSequenceTests))]
        public void Test_Intersect37() {
            int[] ints = new int[1];
            int[] ints1 = new int[15];
            ints[0] = 2147385377;
            ints1[0] = 2147385377;
            ints1[2] = -553648356;
            ints1[3] = 130944;
            ints1[4] = 131046;
            ints1[5] = 2147385185;
            ints1[6] = -336068675;
            this.Test_Intersect<int>(ints, ints1);
        }

        [Fact]
        [PexGeneratedBy(typeof(OrdSequenceTests))]
        public void Test_Intersect38() {
            int[] ints = new int[1];
            int[] ints1 = new int[7];
            ints[0] = -536870420;
            ints1[0] = 1611794435;
            ints1[1] = -1877200382;
            ints1[2] = -2147483157;
            ints1[3] = 1067672044;
            ints1[4] = 1595683330;
            ints1[5] = 1611679740;
            ints1[6] = -1879115286;
            this.Test_Intersect<int>(ints, ints1);
        }

        [Fact]
        [PexGeneratedBy(typeof(OrdSequenceTests))]
        public void Test_Intersect39() {
            int[] ints = new int[1];
            int[] ints1 = new int[14];
            ints1[0] = 1644155407;
            ints1[1] = 608169984;
            ints1[2] = -574;
            ints1[3] = 1073741824;
            ints1[4] = 1140839407;
            ints1[5] = 1610599663;
            ints1[6] = -1073741826;
            ints1[7] = -1646268434;
            this.Test_Intersect<int>(ints, ints1);
        }

        [Fact]
        [PexGeneratedBy(typeof(OrdSequenceTests))]
        public void Test_Intersect40() {
            int[] ints = new int[1];
            int[] ints1 = new int[14];
            ints1[0] = 2145386495;
            ints1[1] = -1073610752;
            ints1[2] = -1076232194;
            ints1[3] = 1073741824;
            ints1[4] = 1073741825;
            ints1[5] = 2145386494;
            ints1[6] = -1073741825;
            ints1[7] = -4194305;
            this.Test_Intersect<int>(ints, ints1);
        }

        [Fact]
        [PexGeneratedBy(typeof(OrdSequenceTests))]
        public void Test_Intersect41() {
            int[] ints = new int[1];
            int[] ints1 = new int[14];
            ints1[0] = 939524096;
            ints1[1] = -1677721600;
            ints1[2] = -2080407684;
            ints1[3] = 5306592;
            ints1[4] = 194052174;
            ints1[5] = 275824640;
            ints1[6] = -960233988;
            ints1[7] = -603455487;
            ints1[8] = -602894096;
            ints1[9] = -602894096;
            this.Test_Intersect<int>(ints, ints1);
        }

        [Fact]
        [PexGeneratedBy(typeof(OrdSequenceTests))]
        public void Test_Intersect42() {
            int[] ints = new int[1];
            int[] ints1 = new int[14];
            ints1[0] = 1076693817;
            ints1[1] = -1944384428;
            ints1[2] = -1962413465;
            ints1[3] = 301987841;
            ints1[4] = 537971500;
            ints1[5] = 538931405;
            ints1[6] = -2139295731;
            ints1[7] = -1944384428;
            ints1[8] = 134737148;
            this.Test_Intersect<int>(ints, ints1);
        }

        [Fact]
        [PexGeneratedBy(typeof(OrdSequenceTests))]
        public void Test_Intersect43() {
            int[] ints = new int[1];
            int[] ints1 = new int[14];
            ints1[0] = 1074006045;
            ints1[1] = -167215044;
            ints1[2] = -1162377246;
            ints1[3] = 134484576;
            ints1[4] = 502214235;
            ints1[5] = 671401725;
            ints1[6] = -1074001921;
            ints1[7] = -167215044;
            ints1[8] = -167215044;
            ints1[9] = 1070071808;
            this.Test_Intersect<int>(ints, ints1);
        }

        [Fact]
        [PexGeneratedBy(typeof(OrdSequenceTests))]
        public void Test_Intersect44() {
            int[] ints = new int[1];
            int[] ints1 = new int[14];
            ints1[0] = 1619001345;
            ints1[2] = -1610646056;
            ints1[3] = 48496642;
            ints1[4] = 1073741824;
            ints1[5] = 1610612734;
            ints1[6] = -545267842;
            ints1[10] = 1073741824;
            this.Test_Intersect<int>(ints, ints1);
        }

        [Fact]
        [PexGeneratedBy(typeof(OrdSequenceTests))]
        public void Test_Intersect52() {
            int[] ints = new int[6];
            int[] ints1 = new int[4];
            ints[0] = 1409286433;
            ints[1] = -16375;
            ints[2] = -1719681328;
            ints[3] = 50331167;
            ints[4] = 1278452033;
            ints[5] = 1278452033;
            ints1[0] = 1073741825;
            ints1[1] = -1472200704;
            ints1[2] = 62390271;
            ints1[3] = 1073741825;
            this.Test_Intersect<int>(ints, ints1);
        }

        [Fact]
        [PexGeneratedBy(typeof(OrdSequenceTests))]
        public void Test_Intersect56() {
            int[] ints = new int[1];
            int[] ints1 = new int[15];
            ints[0] = 2147249152;
            ints1[0] = 1879081961;
            ints1[1] = -2113929154;
            ints1[2] = int.MinValue;
            ints1[3] = 838890561;
            ints1[4] = 944965696;
            ints1[5] = 1320073278;
            ints1[6] = 791674880;
            ints1[7] = -2067780688;
            ints1[8] = -6324240;
            ints1[9] = -1073741824;
            ints1[10] = -1076134398;
            ints1[11] = -1459633936;
            ints1[12] = -1326186465;
            ints1[13] = -1048542336;
            ints1[14] = -1040221136;
            this.Test_Intersect<int>(ints, ints1);
        }

        [Fact]
        [PexGeneratedBy(typeof(OrdSequenceTests))]
        public void Test_Intersect60() {
            int[] ints = new int[6];
            ints[0] = 1090519039;
            ints[1] = -2147221503;
            ints[2] = -2147221506;
            ints[3] = 1073741824;
            ints[4] = 1073217536;
            ints[5] = -3932160;
            this.Test_Intersect<int>(ints, ints);
        }

        [Fact]
        [PexGeneratedBy(typeof(OrdSequenceTests))]
        public void Test_Intersect62() {
            int[] ints = new int[1];
            int[] ints1 = new int[14];
            ints1[0] = int.MaxValue;
            ints1[1] = -2080374784;
            ints1[2] = -2080374786;
            ints1[3] = 1073741824;
            ints1[4] = 1074135040;
            ints1[5] = 2147483646;
            ints1[6] = 1040187392;
            ints1[7] = -2081423362;
            ints1[8] = -602894096;
            ints1[9] = -602894096;
            this.Test_Intersect<int>(ints, ints1);
        }

        [Fact]
        [PexGeneratedBy(typeof(OrdSequenceTests))]
        public void Test_Intersect71() {
            int[] ints = new int[1];
            int[] ints1 = new int[14];
            ints1[0] = 1107361823;
            ints1[1] = -1073741823;
            ints1[2] = int.MinValue;
            ints1[3] = 3;
            ints1[4] = 939524096;
            ints1[5] = 1073741824;
            ints1[6] = 2;
            ints1[7] = -1073744896;
            ints1[8] = 1073741696;
            ints1[9] = 813628958;
            ints1[10] = 1191182464;
            ints1[11] = 1191182464;
            ints1[12] = 939524218;
            this.Test_Intersect<int>(ints, ints1);
        }

        [Fact]
        [PexGeneratedBy(typeof(OrdSequenceTests))]
        public void Test_Intersect72() {
            int[] ints = new int[1];
            int[] ints1 = new int[14];
            ints1[0] = 1109393408;
            ints1[1] = -1048576;
            ints1[2] = -1342177282;
            ints1[3] = 1073741824;
            ints1[4] = 1073741825;
            ints1[5] = 1073741826;
            ints1[6] = 1024000;
            ints1[7] = -117571586;
            ints1[8] = 1073741827;
            ints1[9] = -335675392;
            ints1[10] = -536870912;
            ints1[11] = 1191182464;
            ints1[12] = 939524218;
            this.Test_Intersect<int>(ints, ints1);
        }

        [Fact]
        [PexGeneratedBy(typeof(OrdSequenceTests))]
        public void Test_Intersect73() {
            int[] ints = new int[1];
            int[] ints1 = new int[14];
            ints1[0] = 537002004;
            ints1[1] = 1856;
            ints1[2] = -393314;
            ints1[3] = 4097;
            ints1[4] = 132644864;
            ints1[5] = 524287998;
            ints1[6] = 4096;
            ints1[7] = -536869378;
            ints1[8] = 135004160;
            ints1[9] = 135004160;
            ints1[10] = 536935936;
            ints1[11] = 66583310;
            ints1[12] = 939524218;
            this.Test_Intersect<int>(ints, ints1);
        }

[Fact(Skip = "the test state was: path bounds exceeded")]
[PexGeneratedBy(typeof(OrdSequenceTests))]
public void Test_Intersect28()
{
    int[] ints = new int[1];
    int[] ints1 = new int[30];
    ints1[0] = 1091076109;
    ints1[1] = -1341800698;
    ints1[2] = -1651605648;
    ints1[3] = 419516561;
    ints1[4] = 12599305;
    ints1[5] = -295084032;
    ints1[6] = -1255927417;
    ints1[7] = -1738014692;
    ints1[8] = -674723232;
    ints1[9] = -674723232;
    ints1[10] = -1064844801;
    ints1[11] = -32439968;
    ints1[12] = -32439968;
    ints1[13] = -32439968;
    ints1[14] = -32439968;
    ints1[15] = -32439968;
    ints1[16] = -32439968;
    ints1[17] = -32439968;
    ints1[18] = -32439968;
    ints1[19] = -32439968;
    ints1[20] = -32439968;
    ints1[21] = -32439968;
    ints1[22] = -32439968;
    ints1[23] = -32439968;
    ints1[24] = -32439968;
    ints1[25] = -32439968;
    ints1[26] = -32439968;
    ints1[27] = -32439968;
    ints1[28] = -32439968;
    ints1[29] = -32439968;
    this.Test_Intersect<int>(ints, ints1);
}
[Fact]
[PexGeneratedBy(typeof(OrdSequenceTests))]
public void Test_Intersect63()
{
    int[] ints = new int[1];
    int[] ints1 = new int[12];
    ints1[0] = 1;
    ints1[2] = 1073741824;
    ints1[3] = -2;
    ints1[4] = 520093770;
    ints1[5] = 1073741896;
    ints1[6] = int.MinValue;
    ints1[7] = -2;
    ints1[8] = 2146435073;
    ints1[9] = 538931402;
    this.Test_Intersect<int>(ints, ints1);
}
[Fact]
[PexGeneratedBy(typeof(OrdSequenceTests))]
public void Test_Intersect65()
{
    int[] ints = new int[1];
    int[] ints1 = new int[14];
    ints[0] = -8;
    ints1[0] = 687865991;
    ints1[1] = -2147352575;
    ints1[2] = int.MinValue;
    ints1[3] = -2013265792;
    ints1[4] = -1610612735;
    ints1[5] = 536870912;
    ints1[6] = -2013265919;
    ints1[7] = 134217728;
    ints1[8] = -2147483644;
    this.Test_Intersect<int>(ints, ints1);
}
[Fact]
[PexGeneratedBy(typeof(OrdSequenceTests))]
public void Test_Intersect66()
{
    int[] ints = new int[1];
    int[] ints1 = new int[14];
    ints[0] = -8;
    ints1[0] = 2034991638;
    ints1[1] = -1605697015;
    ints1[2] = int.MinValue;
    ints1[3] = -1589641216;
    ints1[4] = -1589641215;
    ints1[5] = 553648128;
    ints1[6] = -1593146878;
    ints1[7] = -653622784;
    ints1[8] = -653622784;
    ints1[9] = int.MinValue;
    this.Test_Intersect<int>(ints, ints1);
}
[Fact]
[PexGeneratedBy(typeof(OrdSequenceTests))]
public void Test_Intersect67()
{
    int[] ints = new int[1];
    int[] ints1 = new int[12];
    ints1[0] = 536870913;
    ints1[1] = 1;
    ints1[2] = 1610612739;
    ints1[4] = -1610612734;
    ints1[5] = 1610612736;
    ints1[7] = 1073741824;
    ints1[8] = 2146435073;
    ints1[9] = 538931402;
    this.Test_Intersect<int>(ints, ints1);
}
    }
}
