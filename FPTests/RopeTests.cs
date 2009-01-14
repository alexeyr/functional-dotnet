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
    [PexClass(typeof(Rope))]
    public partial class RopeTests {
        private Rope MakeLargeRope(string largeString) {
            PexAssume.IsNotNull(largeString);
            var requireLarge = PexChoose.DefaultSession.ChooseValueFrom("requireLarge", false, true);
            if (requireLarge)
                PexAssume.IsTrue(largeString.Length >= 100);
            PexAssume.TrueForAny(largeString, c => c != '\0');
            return MakeLargeRope(largeString, 0, largeString.Length);
        }

        private Rope MakeLargeRope(string largeString, int start, int length) {
            if (length <= 5)
                return largeString.Substring(start, length).ToRope();
            var goDeeper = PexChoose.DefaultSession.ChooseValueFrom("goDeeper", false, true);
            if (goDeeper) {
                int length1 = PexChoose.DefaultSession.ChooseValueFromRange("length1", 0, length);
                return MakeLargeRope(largeString, start, length1).Concat(MakeLargeRope(largeString, start + length1, length - length1));                
            }
            return largeString.Substring(start, length).ToRope();
        }

        [PexMethod(MaxBranches = 40000)]
        public void Test_Creation([PexAssumeNotNull] string largeString) {
            var largeRope = MakeLargeRope(largeString);
            Assert.Equal(largeString, largeRope.AsString());
        }

        [PexMethod(MaxBranches = 40000)]
        public void Test_Enumeration([PexAssumeNotNull] string largeString) {
            var largeRope = MakeLargeRope(largeString);
            Assert2.SequenceEqual(largeString, largeRope);
        }

        [PexMethod(MaxBranches = 40000)]
        public void Test_Indexing([PexAssumeNotNull] string largeString, int i) {
            PexAssume.IsTrue(i >= 0);
            PexAssume.IsTrue(i < largeString.Length);
            var largeRope = MakeLargeRope(largeString);
            Assert.Equal(largeString[i], largeRope[i]);
        }

        [PexMethod(MaxBranches = 40000)]
        public void Test_Concat([PexAssumeNotNull] string largeString1, [PexAssumeNotNull] string largeString2) {
            var rope1 = MakeLargeRope(largeString1);
            var rope2 = MakeLargeRope(largeString2);

            Assert.Equal(largeString1 + largeString2, rope1.Concat(rope2).AsString());
        }

        [PexMethod(MaxBranches = 40000)]
        public void Test_SubString([PexAssumeNotNull] string largeString, int startIndex, int count) {
            PexAssume.IsTrue(startIndex >= 0);
            PexAssume.IsTrue(count >= 0);
            PexAssume.IsTrue(startIndex + count >= 0); // to prevent overflow
            PexAssume.IsTrue(startIndex + count <= largeString.Length);
            var largeRope = MakeLargeRope(largeString);
            Assert.Equal(largeString.Substring(startIndex, count), largeRope.SubString(startIndex, count).AsString());
        }

        [PexMethod]
        public void Test_TrimStart([PexAssumeNotNull] string largeString, [PexAssumeNotNull] char[] trimChars) {
            var largeRope = MakeLargeRope(largeString);
            Assert.Equal(largeString.TrimStart(trimChars), largeRope.TrimStart(trimChars).AsString());
        }

        [PexMethod]
        public void Test_TrimEnd([PexAssumeNotNull] string largeString, [PexAssumeNotNull] char[] trimChars) {
            var largeRope = MakeLargeRope(largeString);
            Assert.Equal(largeString.TrimEnd(trimChars), largeRope.TrimEnd(trimChars).AsString());
        }

        [PexMethod]
        public void Test_Reverse([PexAssumeNotNull] string largeString) {
            char[] largeStringCharArray = largeString.ToCharArray();
            var largeRope = MakeLargeRope(largeString);
            Array.Reverse(largeStringCharArray);
            Assert.Equal(new string(largeStringCharArray), largeRope.Reverse().AsString());
        }

        [Fact]
        public void Test_Rebalance() {
            string digits = "0123456789";
            digits = digits + digits;
            var rope = Rope.EmptyInstance;
            for (int i = 0; i < 32; i++)
                rope = rope.Concat(digits.ToRope());
            for (int i = 0; i < 5; i++)
                digits = digits + digits;
            Assert.Equal(digits, rope.AsString());
        }
    }
}