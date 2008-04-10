using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using FP;

namespace FPTests {
    public class StringTests {
        
        [Fact]
        public void Words_EmptyStringContainsNoWords() {
            Assert.Empty("".Words());
        }

        [Fact]
        public void Words_ShouldIncludePunctuationAndIgnoreExtraWhitespace() {
            Assert.True("This, \r\n  is  a \t test.".Words().SequenceEqual(
                            new[] { "This,", "is", "a", "test." }));
        }

        [Fact]
        public void UnWordsAndUnlines_ShouldReturnEmptyStringsOnEmptySequences() {
            Assert.Equal((new List<string>()).UnWordsAsString(), "");
            Assert.Equal((new List<string>()).UnLinesAsString(), "");
        }

        [Fact] 
        public void Lines_ShouldWorkWithAllLineEndings() {
            Assert.True("This,   is \r\n a \t\n test.".Lines().
                            SequenceEqual(new[] {"This,   is ", " a \t", " test."}));
        }

        [Fact]
        public void UnWords_ShouldWork() {
            Assert.Equal(new[] {"This,", "is", "a", "test."}.UnWordsAsString(),
                         "This, is a test.");
        }

        [Fact]
        public void UnLines_ShouldWork() {
            Assert.Equal(new[] { "This,", "is", "a", "test." }.UnLinesAsString(),
@"This,
is
a
test.");
        }
    }
}
