﻿/*
* SwitchTests.cs is part of functional-dotnet project
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
using FP.Core;
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