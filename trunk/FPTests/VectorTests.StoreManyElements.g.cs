using System;
using Xunit;
using Microsoft.Pex.Framework.Generated;

namespace FPTests
{
    public partial class VectorTests {

        [Fact]
        public void StoreManyElements06() {
            string[] strings = new string[31];
            this.StoreManyElements<string>(strings);
        }

        [Fact]
        public void StoreManyElements11() {
            int[] ints = new int[30];
            this.StoreManyElements<int>(ints);
        }

        [Fact]
        public void StoreManyElements15() {
            int[] ints = new int[15];
            this.StoreManyElements<int>(ints);
        }

        [Fact]
        public void StoreManyElements13() {
            int[] ints = new int[15];
            this.StoreManyElements<int>(ints);
        }

        [Fact]
        public void StoreManyElements07() {
            string[] strings = new string[31];
            this.StoreManyElements<string>(strings);
        }

        [Fact]
        public void StoreManyElements16() {
            string[] strings = new string[31];
            this.StoreManyElements<string>(strings);
        }

        [Fact]
        public void StoreManyElements21() {
            int[] ints = new int[15];
            this.StoreManyElements<int>(ints);
        }

        [Fact]
        public void StoreManyElements22() {
            string[] strings = new string[31];
            this.StoreManyElements<string>(strings);
        }

        [Fact]
        [PexGeneratedBy(typeof(VectorTests))]
        public void StoreManyElements08() {
            int[] ints = new int[0];
            this.StoreManyElements<int>(ints);
        }

        [Fact]
        [PexGeneratedBy(typeof(VectorTests))]
        public void StoreManyElements09() {
            int[] ints = new int[1];
            this.StoreManyElements<int>(ints);
        }

        [Fact]
        [PexGeneratedBy(typeof(VectorTests))]
        public void StoreManyElements10() {
            int[] ints = new int[2];
            this.StoreManyElements<int>(ints);
        }

        [Fact]
        public void StoreManyElements12() {
            int[] ints = new int[15];
            this.StoreManyElements<int>(ints);
        }

        [Fact]
        public void StoreManyElements04() {
            int[] ints = new int[14];
            this.StoreManyElements<int>(ints);
        }

        [Fact]
        [PexGeneratedBy(typeof(VectorTests))]
        public void StoreManyElements05() {
            string[] ss = new string[0];
            this.StoreManyElements<string>(ss);
        }

        [Fact]
        [PexGeneratedBy(typeof(VectorTests))]
        public void StoreManyElements14() {
            string[] ss = new string[1];
            this.StoreManyElements<string>(ss);
        }

        [Fact]
        [PexGeneratedBy(typeof(VectorTests))]
        public void StoreManyElements17() {
            string[] ss = new string[1];
            ss[0] = "\0\0\0";
            this.StoreManyElements<string>(ss);
        }

        [Fact]
        [PexGeneratedBy(typeof(VectorTests))]
        public void StoreManyElements19() {
            string[] ss = new string[2];
            this.StoreManyElements<string>(ss);
        }

        [Fact]
        [PexGeneratedBy(typeof(VectorTests))]
        public void StoreManyElements20() {
            string[] ss = new string[2];
            ss[0] = "\0";
            ss[1] = "\0";
            this.StoreManyElements<string>(ss);
        }

        [Fact]
        public void StoreManyElements23() {
            string[] ss = new string[14];
            this.StoreManyElements<string>(ss);
        }

        [Fact]
        public void StoreManyElements26() {
            string[] ss = new string[31];
            this.StoreManyElements<string>(ss);
        }

        [Fact]
        public void StoreManyElements30() {
            int[] ints = new int[30];
            this.StoreManyElements<int>(ints);
        }

        [Fact]
        public void StoreManyElements18() {
            int[] ints = new int[62];
            this.StoreManyElements<int>(ints);
        }

        [Fact]
        public void StoreManyElements31() {
            string[] ss = new string[62];
            this.StoreManyElements<string>(ss);
        }

        [Fact]
        [PexGeneratedBy(typeof(VectorTests))]
        public void StoreManyElements24() {
            int[] ints = new int[33];
            this.StoreManyElements<int>(ints);
        }

        [Fact]
        public void StoreManyElements25() {
            int[] ints = new int[62];
            this.StoreManyElements<int>(ints);
        }

        [Fact]
        public void StoreManyElements37() {
            string[] ss = new string[62];
            this.StoreManyElements<string>(ss);
        }

        [Fact]
        [PexGeneratedBy(typeof(VectorTests))]
        public void StoreManyElements38() {
            string[] ss = new string[34];
            this.StoreManyElements<string>(ss);
        }

    }
}
