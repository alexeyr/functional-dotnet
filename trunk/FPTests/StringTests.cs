/*
* StringTests.cs is part of functional-dotnet project
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

using System.Collections.Generic;
using System.Linq;
using FP.Text;
using Xunit;

namespace FPTests {
    public partial class StringTests {
        
        [Fact]
        public void Test_EmptyStringContainsNoWords() {
            Assert.Empty("".Words());
        }

        [Fact]
        public void Test_WordsShouldIncludePunctuationAndIgnoreExtraWhitespace() {
            Assert.True("This, \r\n  is  a \t test.".Words().SequenceEqual(
                            new[] { "This,", "is", "a", "test." }));
        }

        [Fact]
        public void Test_UnWordsAndUnlines_ShouldReturnEmptyStringsOnEmptySequences() {
            Assert.Equal((new List<string>()).UnWordsAsString(), "");
            Assert.Equal((new List<string>()).UnLinesAsString(), "");
        }

        [Fact] 
        public void Test_Lines_ShouldWorkWithAllLineEndings() {
            Assert.True("This,   is \r\n a \t\n test.".Lines().
                            SequenceEqual(new[] {"This,   is ", " a \t", " test."}));
        }

        [Fact]
        public void Test_UnWords() {
            Assert.Equal(new[] {"This,", "is", "a", "test."}.UnWordsAsString(),
                         "This, is a test.");
        }

        [Fact]
        public void Test_UnLines() {
            Assert.Equal(new[] { "This,", "is", "a", "test." }.UnLinesAsString(),
@"This,
is
a
test.");
        }
    }
}
