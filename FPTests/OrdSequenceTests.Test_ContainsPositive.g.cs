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
        public void Test_ContainsPositive01() {
            int[] ints = new int[1];
            this.Test_ContainsPositive<int>(ints, 0);
        }

        [Fact]
        [PexGeneratedBy(typeof(OrdSequenceTests))]
        public void Test_ContainsPositive02() {
            int[] ints = new int[2];
            ints[0] = 4097;
            ints[1] = 2048;
            this.Test_ContainsPositive<int>(ints, 4097);
        }

        [Fact]
        [PexGeneratedBy(typeof(OrdSequenceTests))]
        public void Test_ContainsPositive03() {
            int[] ints = new int[3];
            ints[0] = 134218243;
            ints[1] = 134217697;
            ints[2] = 256;
            this.Test_ContainsPositive<int>(ints, 134218243);
        }

        [Fact]
        [PexGeneratedBy(typeof(OrdSequenceTests))]
        public void Test_ContainsPositive04() {
            int[] ints = new int[7];
            ints[0] = 1610612222;
            ints[1] = -1879015264;
            ints[2] = -1610613440;
            ints[3] = -1925239588;
            ints[4] = -1073741313;
            ints[5] = 1073741824;
            ints[6] = 1610612222;
            this.Test_ContainsPositive<int>(ints, 1610612222);
        }

        [Fact]
        [PexGeneratedBy(typeof(OrdSequenceTests))]
        public void Test_ContainsPositive05() {
            int[] ints = new int[7];
            ints[0] = 2080374782;
            ints[1] = -2113929216;
            ints[2] = -1073741824;
            ints[3] = -2135121962;
            ints[4] = -1064304642;
            ints[5] = 1073741824;
            ints[6] = 2080374782;
            this.Test_ContainsPositive<int>(ints, -2113929216);
        }

        [Fact]
        [PexGeneratedBy(typeof(OrdSequenceTests))]
        public void Test_ContainsPositive06() {
            int[] ints = new int[14];
            ints[0] = 2086666241;
            ints[1] = -1483228254;
            ints[2] = 402173952;
            ints[3] = -1781014836;
            ints[4] = 1611005951;
            ints[5] = 2065235968;
            ints[6] = 270436380;
            ints[7] = 3221536;
            ints[8] = 919551;
            ints[9] = 285157408;
            ints[10] = 1073741824;
            ints[11] = 831782911;
            ints[12] = 402653184;
            ints[13] = 324009983;
            this.Test_ContainsPositive<int>(ints, 2086666241);
        }

        [Fact]
        [PexGeneratedBy(typeof(OrdSequenceTests))]
        public void Test_ContainsPositive07() {
            int[] ints = new int[6];
            ints[0] = -1068114943;
            ints[1] = -1242529536;
            ints[2] = 1717567489;
            ints[3] = -1242529536;
            ints[4] = -1068114943;
            ints[5] = -1068114943;
            this.Test_ContainsPositive<int>(ints, -1068114943);
        }

        [Fact]
        [PexGeneratedBy(typeof(OrdSequenceTests))]
        public void Test_ContainsPositive08() {
            int[] ints = new int[4];
            ints[0] = 1073809920;
            ints[1] = 65535;
            ints[2] = 2046;
            ints[3] = 1073741824;
            this.Test_ContainsPositive<int>(ints, 1073809920);
        }

        [Fact]
        [PexGeneratedBy(typeof(OrdSequenceTests))]
        public void Test_ContainsPositive17() {
            int[] ints = new int[6];
            ints[0] = 17237088;
            ints[1] = 12603393;
            ints[2] = -1358951843;
            ints[3] = 17237088;
            ints[4] = -16899108;
            ints[5] = -2143289316;
            this.Test_ContainsPositive<int>(ints, 17237088);
        }

        [Fact]
        [PexGeneratedBy(typeof(OrdSequenceTests))]
        public void Test_ContainsPositive18() {
            int[] ints = new int[7];
            ints[0] = 548408067;
            ints[1] = 31980545;
            ints[2] = -135264515;
            ints[3] = 25165824;
            ints[4] = 508526526;
            ints[5] = -1610620937;
            ints[6] = int.MinValue;
            this.Test_ContainsPositive<int>(ints, 31980545);
        }

        [Fact]
        public void Test_ContainsPositive25() {
            int[] ints = new int[31];
            ints[0] = 555745664;
            ints[1] = -1792900776;
            ints[2] = 1511800329;
            ints[3] = -1792900776;
            ints[4] = 566005415;
            ints[5] = -1006632947;
            ints[6] = -1943044092;
            ints[7] = -1336934214;
            ints[8] = -1336934214;
            ints[9] = -1336934214;
            ints[10] = -1336934214;
            ints[11] = -165536492;
            ints[12] = -165536492;
            ints[13] = -165536492;
            ints[14] = -165536492;
            ints[15] = -165536492;
            ints[16] = -165536492;
            ints[17] = -165536492;
            ints[18] = -165536492;
            ints[19] = -165536492;
            ints[20] = -165536492;
            ints[21] = -165536492;
            ints[22] = -165536492;
            ints[23] = -165536492;
            ints[24] = -165536492;
            ints[25] = -165536492;
            ints[26] = -165536492;
            ints[27] = -165536492;
            ints[28] = -165536492;
            ints[29] = -165536492;
            ints[30] = -165536492;
            this.Test_ContainsPositive<int>(ints, 16);
        }

        [Fact]
        [PexGeneratedBy(typeof(OrdSequenceTests))]
        public void Test_ContainsPositive36() {
            int[] ints = new int[7];
            ints[0] = 200524800;
            ints[1] = -2080915456;
            ints[2] = -1543503550;
            ints[3] = -2146680834;
            ints[4] = -1141088221;
            ints[5] = -2080358864;
            ints[6] = -1610170037;
            this.Test_ContainsPositive<int>(ints, -2080915456);
        }

        [Fact]
        [PexGeneratedBy(typeof(OrdSequenceTests))]
        public void Test_ContainsPositive37() {
            int[] ints = new int[14];
            ints[0] = 1342179329;
            ints[1] = 73689721;
            ints[2] = 1010468416;
            ints[3] = -4196356;
            ints[4] = 1090521248;
            ints[5] = 1157615391;
            ints[6] = 973080574;
            ints[7] = 63434816;
            ints[8] = 53475558;
            ints[9] = 66586624;
            ints[10] = 1073742336;
            ints[11] = 1005584384;
            ints[12] = 858767448;
            ints[13] = 924315164;
            this.Test_ContainsPositive<int>(ints, 1073742336);
        }

        [Fact]
        [PexGeneratedBy(typeof(OrdSequenceTests))]
        public void Test_ContainsPositive38() {
            int[] ints = new int[6];
            ints[0] = -32751;
            ints[1] = -2147483630;
            ints[2] = -2147479531;
            ints[3] = int.MinValue;
            ints[4] = 2147418112;
            ints[5] = -65538;
            this.Test_ContainsPositive<int>(ints, -32751);
        }

        [Fact]
        [PexGeneratedBy(typeof(OrdSequenceTests))]
        public void Test_ContainsPositive40() {
            int[] ints = new int[14];
            ints[0] = 570943111;
            ints[1] = -1639907327;
            ints[2] = -1572346143;
            ints[3] = -2126635057;
            ints[4] = -535291904;
            ints[5] = 33554432;
            ints[6] = -932175807;
            ints[7] = -939528187;
            ints[8] = -942671364;
            ints[9] = -533708798;
            ints[10] = -327621628;
            ints[11] = -395997668;
            ints[12] = -976230687;
            ints[13] = -533708798;
            this.Test_ContainsPositive<int>(ints, -1572346143);
        }

        [Fact]
        [PexGeneratedBy(typeof(OrdSequenceTests))]
        public void Test_ContainsPositive41() {
            int[] ints = new int[9];
            ints[0] = 1334919188;
            ints[1] = -2140092664;
            ints[2] = 1325914113;
            ints[3] = -2144583698;
            ints[4] = 8;
            ints[5] = 1065361784;
            ints[6] = 1065361706;
            ints[7] = -2147483641;
            ints[8] = -2147483642;
            this.Test_ContainsPositive<int>(ints, 1334919188);
        }

        [Fact]
        [PexGeneratedBy(typeof(OrdSequenceTests))]
        public void Test_ContainsPositive42() {
            int[] ints = new int[14];
            ints[0] = 301989888;
            ints[1] = 270545010;
            ints[2] = 276828034;
            ints[3] = 149605950;
            ints[4] = 10095733;
            ints[5] = 10095734;
            ints[6] = 35261955;
            ints[7] = 10095730;
            ints[8] = 10095730;
            ints[9] = 4194304;
            ints[10] = 7010702;
            ints[11] = 42008306;
            ints[12] = 16777216;
            ints[13] = 168034304;
            this.Test_ContainsPositive<int>(ints, 10095730);
        }

        [Fact]
        [PexGeneratedBy(typeof(OrdSequenceTests))]
        public void Test_ContainsPositive43() {
            int[] ints = new int[14];
            ints[0] = 1073743880;
            ints[1] = -2147483355;
            ints[2] = -2147483350;
            ints[3] = -2147483360;
            ints[4] = -2147483640;
            ints[5] = -22;
            ints[6] = 939525642;
            ints[7] = 10095730;
            ints[8] = 10095730;
            ints[9] = 4194304;
            ints[10] = 7010702;
            ints[11] = 42008306;
            ints[12] = 16777216;
            ints[13] = 168034304;
            this.Test_ContainsPositive<int>(ints, 10095730);
        }

        [Fact]
        [PexGeneratedBy(typeof(OrdSequenceTests))]
        public void Test_ContainsPositive44() {
            int[] ints = new int[14];
            ints[0] = 103809097;
            ints[1] = -2121801741;
            ints[2] = -1139785658;
            ints[3] = -2123501856;
            ints[4] = -1813004287;
            ints[5] = -939517279;
            ints[6] = -1139785658;
            ints[7] = 10095730;
            ints[8] = 10095730;
            ints[9] = 4194304;
            ints[10] = 7010702;
            ints[11] = 42008306;
            ints[12] = 16777216;
            ints[13] = 168034304;
            this.Test_ContainsPositive<int>(ints, 10095730);
        }

        [Fact]
        [PexGeneratedBy(typeof(OrdSequenceTests))]
        public void Test_ContainsPositive45() {
            int[] ints = new int[14];
            ints[0] = 1644232969;
            ints[1] = 149918722;
            ints[2] = 655958216;
            ints[3] = -525471986;
            ints[4] = 843149408;
            ints[5] = 1606324097;
            ints[6] = 573700085;
            ints[7] = 67108864;
            ints[8] = 66576384;
            ints[9] = 243007488;
            ints[10] = 814481408;
            ints[11] = 576454656;
            ints[12] = 33554432;
            ints[13] = 807556864;
            this.Test_ContainsPositive<int>(ints, 1644232969);
        }

        [Fact]
        [PexGeneratedBy(typeof(OrdSequenceTests))]
        public void Test_ContainsPositive47() {
            int[] ints = new int[6];
            ints[0] = 1883255041;
            ints[1] = -452992950;
            ints[2] = 1845493792;
            ints[3] = 1110446145;
            ints[4] = 1110446145;
            ints[5] = 281018387;
            this.Test_ContainsPositive<int>(ints, 1883255041);
        }

        [Fact]
        [PexGeneratedBy(typeof(OrdSequenceTests))]
        public void Test_ContainsPositive48() {
            int[] ints = new int[14];
            ints[0] = 1073741831;
            ints[1] = -1739051325;
            ints[2] = -392048121;
            ints[3] = -2015641601;
            ints[4] = -1341624359;
            ints[5] = -200077552;
            ints[6] = 1008992257;
            ints[7] = -1107333065;
            ints[8] = -392048121;
            ints[9] = -1547698048;
            ints[10] = -1736708024;
            ints[11] = 1073126656;
            ints[12] = -553644031;
            ints[13] = -1476263807;
            this.Test_ContainsPositive<int>(ints, -1476263807);
        }

        [Fact]
        public void Test_ContainsPositive49() {
            int[] ints = new int[62];
            ints[0] = 536887327;
            ints[1] = 43265864;
            ints[2] = -21282801;
            ints[3] = 477134848;
            ints[4] = 1371439136;
            ints[5] = 43265864;
            ints[15] = 1102871936;
            ints[17] = 114688;
            ints[60] = -2147483642;
            this.Test_ContainsPositive<int>(ints, 8);
        }

        [Fact]
        [PexGeneratedBy(typeof(OrdSequenceTests))]
        public void Test_ContainsPositive50() {
            int[] ints = new int[14];
            ints[0] = 1342676992;
            ints[1] = -1308622832;
            ints[2] = 201326789;
            ints[3] = -1942913013;
            ints[4] = 1275068425;
            ints[5] = 1282938786;
            ints[6] = 1283461118;
            ints[7] = 528950896;
            ints[8] = 528950896;
            ints[9] = 1326452734;
            ints[10] = 1661193464;
            ints[11] = 1947996417;
            ints[12] = 2013523864;
            ints[13] = 1879049216;
            this.Test_ContainsPositive<int>(ints, 1275068425);
        }

        [Fact]
        public void Test_ContainsPositive51() {
            int[] ints = new int[56];
            ints[0] = -1006615552;
            ints[1] = -1073741824;
            ints[2] = -1073741826;
            ints[3] = -2080374786;
            ints[4] = 1916796928;
            ints[6] = -1103101954;
            ints[7] = -1879080986;
            ints[8] = 2093056;
            ints[15] = 1073741848;
            this.Test_ContainsPositive<int>(ints, 58);
        }

[Fact]
[PexGeneratedBy(typeof(OrdSequenceTests))]
public void Test_ContainsPositive11()
{
    int[] ints = new int[14];
    ints[0] = 1073743880;
    ints[1] = -2147483355;
    ints[2] = -2147483350;
    ints[3] = -2147483360;
    ints[4] = -2147483640;
    ints[5] = 234;
    ints[6] = 939525642;
    ints[7] = 10095730;
    ints[8] = 10095730;
    ints[9] = 4194304;
    ints[10] = 7010702;
    ints[11] = 42008306;
    ints[12] = 16777216;
    ints[13] = 168034304;
    this.Test_ContainsPositive<int>(ints, 10095730);
}
[Fact(Skip = "the test state was: path bounds exceeded")]
[PexGeneratedBy(typeof(OrdSequenceTests))]
public void Test_ContainsPositive16()
{
    int[] ints = new int[62];
    ints[0] = 536887327;
    ints[1] = 43265864;
    ints[2] = -21282801;
    ints[3] = 477134848;
    ints[4] = 1371439136;
    ints[5] = 43265864;
    ints[15] = 1102871936;
    ints[17] = 114688;
    ints[60] = -2147483642;
    this.Test_ContainsPositive<int>(ints, 8);
}
[Fact(Skip = "the test state was: path bounds exceeded")]
[PexGeneratedBy(typeof(OrdSequenceTests))]
public void Test_ContainsPositive20()
{
    int[] ints = new int[56];
    ints[0] = -1006615552;
    ints[1] = -1073741824;
    ints[2] = -1073741826;
    ints[3] = -2080374786;
    ints[4] = 1916796928;
    ints[6] = -1103101954;
    ints[7] = -1879080986;
    ints[8] = 2093056;
    ints[15] = 1073741848;
    this.Test_ContainsPositive<int>(ints, 58);
}
    }
}
