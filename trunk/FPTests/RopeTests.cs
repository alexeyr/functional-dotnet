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

using System;
using FP.Text;
using Microsoft.Pex.Framework;
using Xunit;
using XunitExtensions;

namespace FPTests {
    [PexClass(typeof (Rope))]
    public partial class RopeTests {
        private Rope MakeLargeRope(string[] strings) {
            // Remove all nulls
            for (int i = 0; i < strings.Length; i++)
                PexAssume.IsNotNull(strings[i]);
            PexAssume.TrueForAny(strings, s => s.Length != 0 && !char.IsControl(s[0]));
            Rope largeRope = Rope.EmptyInstance;
            foreach (string s in strings)
                largeRope = largeRope.Concat(s.ToRope());
            return largeRope;
        }

        [PexMethod(MaxBranches = 40000)]
        public void Test_Indexing([PexAssumeNotNull] string[] strings, int i) {
            PexAssume.IsTrue(i >= 0);
            // PexAssume.IsTrue(i < strings.Map(s => s != null ? s.Length : 0).Sum());
            var largeRope = MakeLargeRope(strings);
            PexAssume.IsTrue(i < largeRope.Count);
            string largeString = string.Concat(strings);
            // PexAssume.IsTrue(i < largeString.Length);
            Assert.Equal(largeString[i], largeRope[i]);
        }

        [PexMethod(MaxBranches = 40000)]
        public void Test_Creation([PexAssumeNotNull] string[] strings) {
            var largeRope = MakeLargeRope(strings);
            string largeString = string.Concat(strings);
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
            PexAssume.TrueForAll(strings, s => s != null);
            // PexAssume.IsTrue(startIndex + count < strings.Map(s => s.Length).Sum());
            string largeString = string.Concat(strings);
            PexAssume.IsTrue(startIndex + count <= largeString.Length);
            var largeRope = MakeLargeRope(strings);
            Assert.Equal(largeString.Substring(startIndex, count), largeRope.SubString(startIndex, count).AsString());
        }

        [PexMethod]
        public void Test_TrimStart([PexAssumeNotNull] string[] strings, [PexAssumeNotNull] char[] trimChars) {
            string largeString = string.Concat(strings);
            var largeRope = MakeLargeRope(strings);
            Assert.Equal(largeString.TrimStart(trimChars), largeRope.TrimStart(trimChars).AsString());
        }

        [PexMethod]
        public void Test_TrimEnd([PexAssumeNotNull] string[] strings, [PexAssumeNotNull] char[] trimChars) {
            string largeString = string.Concat(strings);
            var largeRope = MakeLargeRope(strings);
            Assert.Equal(largeString.TrimEnd(trimChars), largeRope.TrimEnd(trimChars).AsString());
        }

        [PexMethod]
        public void Test_Reverse([PexAssumeNotNull] string[] strings) {
            char[] largeStringCharArray = string.Concat(strings).ToCharArray();
            var largeRope = MakeLargeRope(strings);
            Array.Reverse(largeStringCharArray);
            Assert.Equal(new string(largeStringCharArray), largeRope.Reverse().AsString());
        }
    }
}