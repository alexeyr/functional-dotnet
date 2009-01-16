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
using System.Linq;
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
        public void Test_TrimStart([PexAssumeNotNull] string largeString, char[] trimChars) {
            var largeRope = MakeLargeRope(largeString);
            Assert.Equal(largeString.TrimStart(trimChars), largeRope.TrimStart(trimChars).AsString());
        }

        [PexMethod]
        public void Test_TrimEnd([PexAssumeNotNull] string largeString, char[] trimChars) {
            var largeRope = MakeLargeRope(largeString);
            Assert.Equal(largeString.TrimEnd(trimChars), largeRope.TrimEnd(trimChars).AsString());
        }

        [PexMethod]
        public void Test_Trim([PexAssumeNotNull] string largeString, char[] trimChars) {
            var largeRope = MakeLargeRope(largeString);
            Assert.Equal(largeString.Trim(trimChars), largeRope.Trim(trimChars).AsString());
        }

        [PexMethod]
        public void Test_Reverse([PexAssumeNotNull] string largeString) {
            char[] largeStringCharArray = largeString.ToCharArray();
            var largeRope = MakeLargeRope(largeString);
            Array.Reverse(largeStringCharArray);
            Assert.Equal(new string(largeStringCharArray), largeRope.Reverse().AsString());
        }

        [PexMethod]
        public void Test_Padding([PexAssumeNotNull] string s, int totalWidth, char paddingChar) {
            PexAssume.IsTrue(totalWidth >= 0);
            PexAssume.IsTrue(totalWidth < 1000000); // To avoid out-of-memory errors
            Rope rope = s.ToRope();
            Assert.Equal(s.PadLeft(totalWidth, paddingChar), rope.PadStart(totalWidth, paddingChar).AsString());
            Assert.Equal(s.PadRight(totalWidth, paddingChar), rope.PadEnd(totalWidth, paddingChar).AsString());
        }

        [PexMethod]
        public void Test_StartsWithOrdinal([PexAssumeNotNull] string s1, [PexAssumeNotNull] string s2) {
            var rope1 = s1.ToRope();
            var rope2 = s2.ToRope();
            PexAssert.AreEqual(s1.StartsWith(s2, StringComparison.Ordinal), rope1.StartsWithOrdinal(rope2, false));
            PexAssert.AreEqual(s1.StartsWith(s2, StringComparison.OrdinalIgnoreCase), rope1.StartsWithOrdinal(rope2, true));
        }

        [PexMethod]
        public void Test_EndsWithOrdinal([PexAssumeNotNull] string s1, [PexAssumeNotNull] string s2) {
            var rope1 = s1.ToRope();
            var rope2 = s2.ToRope();
            PexAssert.AreEqual(s1.EndsWith(s2, StringComparison.Ordinal), rope1.EndsWithOrdinal(rope2, false));
            PexAssert.AreEqual(s1.EndsWith(s2, StringComparison.OrdinalIgnoreCase), rope1.EndsWithOrdinal(rope2, true));
        }

        [PexMethod]
        public void Test_IndexOf([PexAssumeNotNull] string s1, char c) {
            var rope = s1.ToRope();
            PexAssert.AreEqual(s1.IndexOf(c), rope.IndexOf(c).ValueOrElse(-1));
            PexAssert.AreEqual(s1.LastIndexOf(c), rope.LastIndexOf(c).ValueOrElse(-1));
        }

        [PexMethod]
        public void Test_IndexOfIgnoringCase([PexAssumeNotNull] string s1, char c) {
            PexAssume.IsTrue(char.IsLetter(c));
            s1 = s1.ToUpperInvariant();
            c = char.ToUpperInvariant(c);
            var rope = s1.ToRope();
            PexAssert.AreEqual(s1.IndexOf(c), rope.IndexOf(c).ValueOrElse(-1));
            PexAssert.AreEqual(s1.LastIndexOf(c), rope.LastIndexOf(c).ValueOrElse(-1));
        }

        [PexMethod]
        public void Test_Join([PexAssumeNotNull] string[] strings, string sep) {
            PexAssume.TrueForAll(strings, s => s != null);
            Rope rope = strings.Select(s => new StringCharSequence(s)).Join<StringCharSequence>(sep);
            string str = string.Join(sep, strings);
            PexAssert.AreEqual(str, rope.AsString());
        }

        [PexMethod]
        public void Test_Concat1([PexAssumeNotNull] string[] strings) {
            PexAssume.TrueForAll(strings, s => s != null);
            Rope rope = strings.Select(s => new StringCharSequence(s)).Concat();
            string str = string.Concat(strings);
            PexAssert.AreEqual(str, rope.AsString());
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