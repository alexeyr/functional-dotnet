#region License
/*
* MaybeTests.cs is part of functional-dotnet project
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
#endregion

using System;
using FP.Core;
using FP.Linq;
using Xunit;

namespace FPTests {
    public class MaybeTests {
        [Fact]
        public void Maybe_NullShouldConvertToNothing() {
            Maybe<string> m = (string)null;
            Assert.Equal(m, Maybe.Nothing<string>());
            Assert.False(m.HasValue);
        }

        [Fact]
        public void Maybe_ValueShouldConvertToSomething() {
            Maybe<string> m = "test";
            Assert.Equal(m.Value, "test");
        }

        [Fact]
        public void Maybe_QueriesShouldWork() {
            var query = from x in Maybe.Just(4)
                        from y in Maybe.Just(5)
                        select x + y;
            Assert.Equal(query.Value, 9);
        }

        [Fact]
        public void Maybe_NullsShouldPropagateInQueries() {
            var query = from x in Maybe.Just(4)
                        from y in Maybe.Nothing<int>()
                        select x + y;
            Assert.False(query.HasValue);
        }

        [Fact]
        public void Maybe_DefaultOperatorShouldWork() {
            Assert.Equal(Maybe<int>.Nothing || Maybe.Just(3) || Maybe.Just(5), Maybe.Just(3));
        }

        [Fact]
        public void Maybe_DefaultOperatorShouldWorkWithValues1() {
            Assert.Equal(Maybe<int>.Nothing || Maybe.Just(3) || 5, Maybe.Just(3));
        }

        [Fact]
        public void Maybe_DefaultOperatorShouldWorkWithValues2() {
            Assert.Equal(Maybe<int>.Nothing || Maybe<int>.Nothing || 5, 5);
        }

        [Fact]
        public void Maybe_TryCatchesExceptions() {
            int zero = 0;
            Assert.Equal(Maybe<int>.Nothing, Maybe.Try(() => 1 / zero));
            Assert.Equal(Maybe<int>.Nothing, Maybe.Try<int, DivideByZeroException>(() => 1 / zero));
        }
    }
}
