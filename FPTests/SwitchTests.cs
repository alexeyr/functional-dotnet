/* (C) Alexey Romanov 2008 */

using System.Collections.Generic;
using FP;
using Xunit;

namespace FPTests {
    public class SwitchTests {

        [Fact]
        public void SwitchExpr_Should_HaveNoResultWithoutACorrectCase() {
            var aSwitch = Switch.ExprOn<int, int>(10)
                .Case(x => x <= 5, x => x)
                .Case(x => x == 8, x => x);
            Assert.False(aSwitch.HasResult);
            Assert.Equal(aSwitch.Result, default(int));

            Assert.False(
                Switch.ExprOn<string, string>("")
                    .Case(x => x.Length > 3, x => x)
                    .Case(x => x.StartsWith("abra"), x => x)
                    .HasResult);
        }

        [Fact]
        public void
            SwitchExpr_Should_ReturnDefaultResultIfOtherCasesDontMatch() {
            Assert.Equal(
                Switch.ExprOn<int, int>(10)
                    .Case(x => x <= 5, x => x)
                    .Case(x => x == 8, x => x)
                    .Default(x => x + 1),
                11);

            Assert.Equal(
                Switch.ExprOn<string, string>("")
                    .Case(x => x.Length > 3, x => x)
                    .Case(x => x.StartsWith("abra"), x => x)
                    .Default(x => x + "ac"),
                "ac");
        }

        [Fact]
        public void SwitchExpr_Should_ReturnFirstResultIfSeveralCasesMatch() {
            Assert.Equal(
                Switch.ExprOn<int, string>(10)
                    .Case(x => x <= 10, x => "x <= 10")
                    .Case(x => x == 10, x => "x == 10")
                    .Default(x => "default"),
                "x <= 10");
        }

        [Fact]
        public void SwitchExpr_Should_TypeSwitchCorrectly() {
            Assert.Equal(
                Switch.ExprOn<IEnumerable<char>, string>("aaa")
                    .Case<string>(x => "x is a string")
                    .Case<char[]>(x => "x is an array")
                    .Default(x => "default"), 
                "x is a string");

            Assert.Equal(
                Switch.ExprOn<IEnumerable<char>, string>(new List<char>("aaa"))
                    .Case<string>(x => "x is a string")
                    .Case<char[]>(x => "x is an array")
                    .Default(x => "default"),
                "default");
        }
    }
}