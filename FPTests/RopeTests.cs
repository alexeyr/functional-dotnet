/*
* RopeTests.cs is part of functional-dotnet project
* 
* Copyright (c) 2008 Alexey Romanov
* All rights reserved.
*
* This source file is available under The New BSD License.
* See license.txt file for more information.
* 
* THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND 
* CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, 
* INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF 
* MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED.
*/

using FP.Text;
using Microsoft.Pex.Framework;
using Xunit;
using XunitExtensions;

namespace FPTests {
    [PexClass(typeof (Rope))]
    public partial class RopeTests {
        private IRope<Rope> MakeLargeRope(string[] strings) {
            IRope<Rope> largeRope = string.Empty.ToRope();
            foreach (string s in strings) {
                PexAssume.IsNotNull(s);
                PexAssume.IsTrue(s.Length == 0 || !char.IsControl(s[0]));
                largeRope = largeRope.Concat(s.ToRope());
            }
            return largeRope;
        }

        [PexMethod(MaxBranches = 40000)]
        public void Test_Indexing([PexAssumeNotNull] string[] strings, int i) {
            PexAssume.IsTrue(i >= 0);
            string largeString = string.Concat(strings);
            PexAssume.IsTrue(i < largeString.Length);
            IRope<Rope> largeRope = MakeLargeRope(strings);
            Assert.Equal(largeString[i], largeRope[i]);
        }

        [PexMethod(MaxBranches = 40000)]
        public void Test_Creation([PexAssumeNotNull] string[] strings) {
            string largeString = string.Concat(strings);
            IRope<Rope> largeRope = MakeLargeRope(strings);
            Assert2.SequenceEqual(largeString, largeRope);
            Assert.Equal(largeString, largeRope.AsString());
        }

        [PexMethod(MaxBranches = 40000)]
        public void Test_Concat([PexAssumeNotNull] string[] strings1, [PexAssumeNotNull] string[] strings2) {
            var rope1 = MakeLargeRope(strings1);
            var rope2 = MakeLargeRope(strings2);

            Assert.Equal(string.Concat(strings1) + string.Concat(strings2), rope1.Concat(rope2).AsString());
        }

        [PexMethod(MaxBranches = 40000)]
        public void Test_SubString([PexAssumeNotNull] string[] strings, int startIndex, int count) {
            PexAssume.IsTrue(startIndex >= 0);
            PexAssume.IsTrue(count >= 0);
            PexAssume.IsTrue(startIndex + count >= 0); // to prevent overflow
            string largeString = string.Concat(strings);
            PexAssume.IsTrue(startIndex + count <= largeString.Length);
            IRope<Rope> largeRope = MakeLargeRope(strings);
            Assert.Equal(largeString.Substring(startIndex, count), largeRope.SubString(startIndex, count).AsString());
        }

    }
}